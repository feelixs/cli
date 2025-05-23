""" AIStrategy Class Definition """
import time
import uuid
from dataclasses import dataclass

@dataclass
class AIStrategyBase:
    a_i_strategy_id: str = None
    name: str = None
    description: str = None
    player_level: str = None
    is_defensive: str = None
    strategy_rank: int = None
    sort_order: int = None
    a_i_levels: str = None
    a_i_level_names: str = None
