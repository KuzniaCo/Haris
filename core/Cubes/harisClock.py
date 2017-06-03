#!/usr/bin/python
import time
from datetime import datetime
import requests
t = ""
while True:
    temp = datetime.now().strftime("%H:%M")
    if(t != temp):
        t = temp
        try:
            r = requests.post("http://serwer.sebcza.pl:88/api/cube/display/t6G9P", data={'Row': 0, 'Content': t})
            print(r.status_code, r.reason)
        except Exception as e:
            print("Problem z requestem") 
