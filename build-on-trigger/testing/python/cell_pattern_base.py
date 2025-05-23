""" CellPattern Class Definition """
import time
import uuid
from dataclasses import dataclass

@dataclass
class CellPatternBase:
    cell_pattern_id: str = None
    name: str = None
    cell_pattern_type: str = None
    sort_order: int = None
    description: str = None
    is_win_pattern: str = None
    cell_pattern_cells: str = None
    cell_pattern_cell_cells: str = None
    cell_pattern_cell_cell_names: str = None
    cell_pattern_cel_i_cell_indexes: int = None
    cell_pattern_translations: str = None
    translation_names: str = None
    translations: str = None
    target_cell: str = None
    target_cell_name: str = None
    target_cell_state: str = None
    target_cell_state_name: str = None
    target_description: str = None
    cell_pattern_translation_codes: str = None
    cells: str = None
    target: str = None
    target_cell_index: int = None
    target_cell_state_id: str = None
    cell_pattern_cell_states: str = None
