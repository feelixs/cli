""" CellState Class Definition """
import time
import uuid
from dataclasses import dataclass

@dataclass
class CellStateBase:
    cell_state_id: str = None
    name: str = None
    id: str = None
    alexes_index: int = None
    default_mark: str = None
    color: str = None
    font_color: str = None
    description: str = None
    cursor: str = None
    sort_order: str = None
    cell_pattern_cells: str = None
    cell_patterns: str = None
    current_state_cells: str = None
    default_state_cells: str = None
    code: str = None
