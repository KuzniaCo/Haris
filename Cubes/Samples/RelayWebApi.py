from flask import Flask
from flask import Response
import json
import datetime

app = Flask(__name__)

@app.route("/cubes/relay/aPd12", methods=['GET'])
def get():
    response = {
    "State": "On",
    "MessageSendAt": datetime.datetime.now().isoformat(),
    "AvailableAction": ["GET TurnOn", "GET TurnOff", "GET Switch"],
    "WebHooks": [
        {"id":1933, "url": "http://sebcza.pl/skrypt.php"}
    ]
    }
    return Response(json.dumps(response),  mimetype='application/json')

@app.route("/cubes/relay/aPd12", methods=['POST'])
def action():
    return "Nie wiem"

if __name__ == "__main__":
    app.run()
