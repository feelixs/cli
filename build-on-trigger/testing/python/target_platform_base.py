""" TargetPlatform Class Definition """
import time
import uuid
from dataclasses import dataclass

@dataclass
class TargetPlatformBase:
    target_platform_id: str = None
    name: str = None
    short_description: str = None
    description: str = None
    implemented: str = None
    demo_url: str = None
    has_any_u_i: str = None
    is_humany_playable: str = None
    has_any_a_i: str = None
    detects_win: str = None
    detects_loss: str = None
    detects_tie: str = None
    has_rule_following_a_i: str = None
    has_u_i_for_human: str = None
    full_simple_rules: str = None
    full_m_l_a_i: str = None
    implements_rotate: str = None
    implements_flip: str = None
    implements_custom_translations: str = None
    sort_order: int = None
