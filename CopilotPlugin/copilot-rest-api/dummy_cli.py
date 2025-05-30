#!/usr/bin/env python3
"""
Dummy CLI script that simulates the real CLI functionality.
This script polls the bridge server for read requests and responds with dummy SSoT data.
"""

import requests
import time
import json
import sys
import os

SERVER_URL = os.getenv('SERVER_URL', 'http://134.209.173.56:8080/')
POLL_INTERVAL = 2  # seconds

# Dummy SSoT data for different bases
DUMMY_SSOT_DATA = {
    'default': {
        'entities': [
            {
                'name': 'Customer',
                'fields': [
                    {'name': 'id', 'type': 'integer', 'required': True, 'primaryKey': True},
                    {'name': 'name', 'type': 'string', 'required': True},
                    {'name': 'phone', 'type': 'string', 'required': False},
                    {'name': 'created_at', 'type': 'datetime', 'required': True}
                ]
            },
            {
                'name': 'Order',
                'fields': [
                    {'name': 'id', 'type': 'integer', 'required': True, 'primaryKey': True},
                    {'name': 'customer_id', 'type': 'integer', 'required': True, 'foreignKey': 'Customer.id'},
                    {'name': 'total', 'type': 'decimal', 'required': True},
                    {'name': 'status', 'type': 'string', 'required': True}
                ]
            }
        ],
        'metadata': {
            'version': '1.0.0',
            'last_updated': '2025-05-30T10:30:00Z'
        }
    }
}

def log(message):
    """Simple logging with timestamp"""
    timestamp = time.strftime('%Y-%m-%d %H:%M:%S')
    print(f"[{timestamp}] DUMMY CLI: {message}")

def check_read_requests():
    """Check if there are any pending read requests from the copilot plugin"""
    try:
        # For now, just check a default baseId - in real implementation this would discover all bases
        base_ids = ['default', 'customer_base', 'order_base']
        
        for base_id in base_ids:
            response = requests.get(f"{SERVER_URL}/check-read-req", params={'baseId': base_id})
            
            if response.status_code == 200:
                data = response.json()
                if data.get('changed', False):
                    log(f"Read request detected for base: {base_id}")
                    provide_ssot_data(base_id)
            else:
                log(f"Error checking read requests for {base_id}: {response.status_code}")
                
    except requests.exceptions.RequestException as e:
        log(f"Error connecting to server: {e}")

def provide_ssot_data(base_id):
    """Provide dummy SSoT data for the requested base"""
    try:
        # Get dummy data for this base (or default)
        ssot_data = DUMMY_SSOT_DATA.get(base_id, DUMMY_SSOT_DATA['default'])
        
        # Add some base-specific variations
        if base_id != 'default':
            ssot_data = ssot_data.copy()
            ssot_data['metadata']['base_id'] = base_id
            ssot_data['metadata']['source'] = f"dummy_data_for_{base_id}"
        
        log(f"Providing SSoT data for base: {base_id}")
        log(f"Data contains {len(ssot_data['entities'])} entities")
        
        # Post the data to the server
        response = requests.post(
            f"{SERVER_URL}/put-read",
            params={'baseId': base_id},
            json={'content': ssot_data}
        )
        
        if response.status_code == 200:
            log(f"Successfully provided data for base: {base_id}")
        else:
            log(f"Error providing data for {base_id}: {response.status_code} - {response.text}")
            
    except requests.exceptions.RequestException as e:
        log(f"Error providing data for {base_id}: {e}")
    except Exception as e:
        log(f"Unexpected error providing data for {base_id}: {e}")

def check_mark_requests():
    """Check for any marked changes that need to be processed (write operations)"""
    try:
        # For now, just check a default baseId - in real implementation this would discover all bases
        base_ids = ['default', 'customer_base', 'order_base']
        
        for base_id in base_ids:
            response = requests.get(f"{SERVER_URL}/check-base", params={'baseId': base_id})
            
            if response.status_code == 200:
                data = response.json()
                if data.get('changed', False):
                    content = data.get('content')
                    log(f"Change request detected for base: {base_id}")
                    log(f"Change content: {content}")
                    # In real CLI, this would apply changes to the actual SSoT system
                    # For now, just log that we would process it
                    log(f"[SIMULATION] Would apply changes to SSoT system for base: {base_id}")
            else:
                log(f"Error checking mark requests for {base_id}: {response.status_code}")
                
    except requests.exceptions.RequestException as e:
        log(f"Error connecting to server for mark requests: {e}")

def main():
    """Main polling loop"""
    log("Starting dummy CLI simulation")
    log(f"Server URL: {SERVER_URL}")
    log(f"Poll interval: {POLL_INTERVAL} seconds")
    
    try:
        while True:
            # Check for read requests (copilot wants to read SSoT data)
            check_read_requests()
            
            # Check for mark requests (copilot wants to modify SSoT data)
            check_mark_requests()
            
            # Wait before next poll
            time.sleep(POLL_INTERVAL)
            
    except KeyboardInterrupt:
        log("Shutting down dummy CLI")
        sys.exit(0)
    except Exception as e:
        log(f"Unexpected error in main loop: {e}")
        sys.exit(1)

if __name__ == '__main__':
    main()
