""" Cell Class Definition """
import time
import uuid
from dataclasses import dataclass

@dataclass
class CellBase:
    cell_id: str = None
    name: str = None
    description: str = None
    alexes_number: str = None
    is_in_row1: int = None
    is_in_row2: int = None
    is_in_row3: int = None
    is_in_column1: int = None
    is_in_column2: int = None
    is_in_column3: int = None
    is_in_left_right_diagonal: int = None
    is_in_right_left_diagonal: int = None
    is_daily_double: bool = None
    rotate_from: str = None
    clockwise: str = None
    named_set_patterns: str = None
    cell_pattern_cells: str = None
    counter_clockwise: str = None
    rotate_description: str = None
    is_clockwise: bool = None
    flip: str = None
    flip_description: str = None
    cell_index: int = None
    cell_key: int = None
    sample_value: str = None
    default_cell_state: str = None
    current_cell_state: str = None
    x: int = None
    y: int = None
    target_cell_for_cell_patterns: str = None
    sort_order: int = None
    rotate_index: int = None
    clockwise_rotate_from: str = None
    clockwise_rotate_from_index: int = None
    counter_clockwise_rotate_from: str = None
    counter_clockwise_rotate_from_index: int = None
    flip_index: int = None
    flip_from_name: str = None
    default_state: str = None
    current_state: str = None
    rotate_translation: str = None
