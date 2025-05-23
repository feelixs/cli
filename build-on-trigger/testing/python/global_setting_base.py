""" GlobalSetting Class Definition """
import time
import uuid
from dataclasses import dataclass

@dataclass
class GlobalSettingBase:
    global_setting_id: str = None
    name: str = None
    value: str = None
    description: str = None
    default_value: str = None
    associated_link: str = None
    prefix: str = None
    google_key: str = None
    g_i_d: int = None
    is_active_download: bool = None
