from CubeServer.ApiModule import run
import thread

class CubeServer:
    def __init__(self):
        thread.start_new_thread(run())
        
