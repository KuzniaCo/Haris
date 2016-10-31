class RelayCube(Cube):
    def __init__(self, relayState):
        self.relayState = relayState

    def onChanged():
        print("Zmienił się status")

    def turnOff():
        print("Wyslij wiadomosc wylaczenia")

    def turnOn():
        print("Wyślij wiadomość włączenia")

    def switchState():
        print("Zmień stan na przeciwny")
