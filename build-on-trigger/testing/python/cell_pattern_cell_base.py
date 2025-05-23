""" CellPatternCell Class Definition """
import time
import uuid
from dataclasses import dataclass

@dataclass
class CellPatternCellBase:
    cell_pattern_cell_id: str = None
    name: str = None
    cell_pattern: str = None
    cell: str = None
    notes: str = None
    attachments: str = None
    status: str = None
    cell_state: str = None
    cell_index: int = None
    cell_state_id: str = None
    cell_cell_state: str = None
    cell_name: str = None
    cell_state_name: str = None
    cell_pattern_type: str = None
