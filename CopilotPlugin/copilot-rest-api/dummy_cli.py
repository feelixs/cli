import subprocess
import requests
import logging
import time
import json
import sys
import re


SERVER_URL = 'http://134.209.173.56:8080/'
POLL_INTERVAL = 2  # seconds

logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s',
    datefmt='%Y-%m-%d %H:%M:%S'
)
logger = logging.getLogger(__name__)


class DummyCli:
    def __init__(self):
        self.BASE_IDS = ['CLI', 'OST']

    def run(self):
        while True:
            # Check for read requests (copilot wants to read SSoT data)
            self.check_read_requests()
            # Check for mark requests (copilot wants to modify SSoT data)
            self.check_mark_requests()
            # Wait before next poll
            time.sleep(POLL_INTERVAL)

    def provide_ssot_data(self, base_id):
        """Provide dummy SSoT data for the requested base"""
        try:
            if base_id not in self.BASE_IDS:
                raise Exception('Invalid base id')

            baseFile = f"{base_id}_DataSchema.json"
            with open(baseFile, 'r') as json_file:
                ssot_data = json.load(json_file)

            # Post the data to the server
            response = requests.post(
                f"{SERVER_URL}/put-read",
                params={'baseId': base_id},
                json={'content': ssot_data, 'filename': baseFile}
            )

            if response.status_code == 200:
                logger.info(f"Successfully provided data for base: {base_id}")
            else:
                logger.info(f"Error providing data for {base_id}: {response.status_code} - {response.text}")

        except requests.exceptions.RequestException as e:
            logger.info(f"Error providing data for {base_id}: {e}")
        except Exception as e:
            logger.info(f"Unexpected error providing data for {base_id}: {e}")

    def check_read_requests(self):
        """Check if there are any pending read requests from the copilot plugin"""
        try:
            for base_id in self.BASE_IDS:
                response = requests.get(f"{SERVER_URL}/check-read-req", params={'baseId': base_id})

                if response.status_code == 200:
                    data = response.json()
                    if data.get('changed'):
                        logger.info(f"Read request detected for base: {base_id}")
                        self.provide_ssot_data(base_id)
                else:
                    logger.info(f"Error checking read requests for {base_id}: {response.status_code}")

        except requests.exceptions.RequestException as e:
            logger.info(f"Error connecting to server: {e}")

    def check_mark_requests(self):
        """Check for any marked changes that need to be processed (write operations)"""
        try:
            for base_id in self.BASE_IDS:
                response = requests.get(f"{SERVER_URL}/check-base", params={'baseId': base_id})
                if response.status_code == 200:
                    data = response.json()
                    if data.get('changed'):
                        logger.info(f"{base_id} - {response.json()}")
                        if data.get('theCmd'):  # a command has been requested instead of replacing all content
                            logger.info(f"Marked request detected for base: {base_id}")

                            # check the command matches allowed commands
                            # Updated regex:
                            # - matches: sed -i '' 's/.../.../g' filename
                            # - allows both ' and " quotes, and escaped slashes
                            allowed_pattern = re.compile(
                                r"^sed\s+-i\s+['\"]?['\"]{0,1}\s+['\"]s/[^/]+/[^/]*/[gI]*['\"]\s+\S+$"
                            )
                            cmd = data['theCmd']
                            # validate the command
                            if allowed_pattern.match(cmd):
                                try:
                                    logger.info(f"Executing command: {cmd}")
                                    result = subprocess.run(cmd, shell=True, check=True, capture_output=True, text=True)
                                    logger.info(f"Command executed successfully: {result.stdout}")
                                except subprocess.CalledProcessError as e:
                                    logger.error(f"Command failed: {e.stderr}")
                            else:
                                logger.warning(f"Rejected command: {cmd}")
                            return
                        elif data.get('content'):
                            content = data.get('content')
                            logger.info(f"Change request detected for base: {base_id}")
                            logger.info(f"Change content: {content}")
                            logger.info(f"Applying changes to SSoT system for base: {base_id}")

                            try:
                                with open(f"{base_id}_DataSchema.json", 'w') as json_file:
                                    parsed_content = json.loads(content)
                                    json.dump(parsed_content, json_file, indent=2)
                            except Exception as e:
                                logger.error(f"Error writing data for {base_id}: {e}")

                            logger.info(f"Change succeeded - file was overwritten: \"{base_id}_DataSchema.json\"")
                else:
                    logger.info(f"Error checking mark requests for {base_id}: {response.status_code}")

        except requests.exceptions.RequestException as e:
            logger.info(f"Error connecting to server for mark requests: {e}")


def main():
    """Main polling loop"""
    logger.info("Starting dummy CLI simulation")
    logger.info(f"Server URL: {SERVER_URL}")
    logger.info(f"Poll interval: {POLL_INTERVAL} seconds")

    dumy = DummyCli()
    try:
        dumy.run()

    except KeyboardInterrupt:
        logger.info("Shutting down dummy CLI")
        sys.exit(0)
    except Exception as e:
        logger.info(f"Unexpected error in main loop: {e}")
        sys.exit(1)


if __name__ == '__main__':
    main()
