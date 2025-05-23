""" CellPatternTranslation Class Definition """
import time
import uuid
from dataclasses import dataclass

@dataclass
class CellPatternTranslationBase:
    cell_pattern_translation_id: str = None
    name: str = None
    display_name: str = None
    cell_pattern: str = None
    cell_pattern_name: str = None
    count: int = None
    translation: str = None
    translation_name: str = None
    translation_id: str = None
    c_p_t_code: str = None
    sort_order: int = None
    notes: str = None
    attachments: str = None
    status: str = None
