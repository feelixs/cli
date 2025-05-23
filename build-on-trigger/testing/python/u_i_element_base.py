""" UIElement Class Definition """
import time
import uuid
from dataclasses import dataclass

@dataclass
class UIElementBase:
    u_i_element_id: str = None
    form: str = None
    button_text: str = None
    button_icon: str = None
    etc: str = None
    color: str = None
