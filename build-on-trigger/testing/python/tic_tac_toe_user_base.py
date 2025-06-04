""" TicTacToeUser Class Definition """
import time
import uuid
from dataclasses import dataclass

@dataclass
class TicTacToeUserBase:
    tic_tac_toe_user_id: str = None
    name: str = None
    tic_tac_toe_email_address: str = None
    tic_tac_toe_roles: str = None
    is_a_i: int = None
    a_i_level: str = None
    a_i_strategies: str = None
    notes: str = None
    attachments: str = None
    status: str = None
    games_as_player: str = None
    games_as_opponent: str = None
    level_number: int = None
    a_i_level_description: str = None
