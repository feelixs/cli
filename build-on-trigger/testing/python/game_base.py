""" Game Class Definition """
import time
import uuid
from dataclasses import dataclass

@dataclass
class GameBase:
    game_id: str = None
    name: str = None
    player: str = None
    player_name: str = None
    opponent: str = None
    is_a_i: int = None
    opponent_type: str = None
    opponent_type_name: str = None
    a_i_strategies: str = None
    createdtime: time = None
    notes: str = None
    attachments: str = None
    status: str = None
    a_i_level: str = None
