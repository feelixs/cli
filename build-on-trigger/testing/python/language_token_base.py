""" LanguageToken Class Definition """
import time
import uuid
from dataclasses import dataclass

@dataclass
class LanguageTokenBase:
    language_token_id: str = None
    name: str = None
    meaning_of_life: int = None
    display_name: str = None
    sort_order: int = None
    token: str = None
