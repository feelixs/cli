""" ScoringMetric Class Definition """
import time
import uuid
from dataclasses import dataclass

@dataclass
class ScoringMetricBase:
    scoring_metric_id: str = None
    strategy: str = None
    points: int = None
    j_s_fiddle: str = None
    j_s_fiddle2: int = None
    windows: str = None
    windows2: int = None
    g_docs: str = None
    g_docs2: int = None
    html: str = None
    html2: int = None
    javascript: str = None
    javascript2: int = None
    c_sharp: str = None
    c_sharp2: int = None
