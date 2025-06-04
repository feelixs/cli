""" LanguageToken Class Definition """
import time
import uuid
from dataclasses import dataclass

@dataclass
class LanguageTokenBase:
    language_token_id: str = None
    name: str = None
    display_name: str = None
    meaning_of_life: int = None
    sort_order: int = None
    refresh_continuous_integration: bool = None
    token: str = None
