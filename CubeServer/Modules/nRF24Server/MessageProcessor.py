import threading

class MessageProcessor(threading.Thread):
    def __init__(self, threadId, name, message):
        threading.Thread.__init__(self)
        self.threadId = threadId
        self.name = name
        self.message = message
    def run(self):
        self.process()
    def process():
        print("Pobierz z bazy informację o cube. Stworz odpowiedni odpowiedni obiekt");
