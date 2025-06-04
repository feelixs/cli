""" AILevel Class Definition """
import time
import uuid
from dataclasses import dataclass

@dataclass
class AILevelBase:
    a_i_level_id: str = None
    name: str = None
    player_type: str = None
    a_i_strategies: str = None
    a_i_strategy_names: str = None
    min_a_i_level_index: int = None
    a_i_level_index: int = None
    description: str = None
    sort_order: int = None
    users: str = None
