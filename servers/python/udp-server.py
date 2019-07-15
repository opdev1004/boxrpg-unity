import socket
from _thread import *
import threading
import sys

server = "192.168.1.14"
port = 5555

s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

try:
    s.bind((server, port))
except socket.error as e:
    str(e)
