#!/usr/bin/python
import time
from datetime import datetime
import requests
t = ""
while True:
    tmpR = requests.get('http://serwer.sebcza.pl:88/api/cube/temperature/aDs6g')
    if(t != tmpR.content[1:-1]):
        t = tmpR.content[1:-1]
        r = requests.post("http://serwer.sebcza.pl:88/api/cube/display/t6G9P", data={'Row': 1, 'Content': t+" C"})
        print(r.status_code, r.reason)
