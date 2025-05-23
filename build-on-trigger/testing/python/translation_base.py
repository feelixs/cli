""" Translation Class Definition """
import time
import uuid
from dataclasses import dataclass

@dataclass
class TranslationBase:
    translation_id: str = None
    name: str = None
    id: str = None
    is_clockwise: bool = None
    description: str = None
    cells: str = None
    cell_pattern_translations: str = None
    custom_description: str = None
