#!/usr/bin/env python
# -*- coding: utf-8 -*-
from socket import *
s = socket(AF_INET, SOCK_STREAM) #utworzenie gniazda
s.connect(("192.168.0.4", 11000)) # nawiazanie polaczenia193.70.84.40
while True:
    nb = raw_input('Type message ')
    s.send(nb)
