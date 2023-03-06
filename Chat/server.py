import socket
import threading
import tkinter as tk


class Server:
    def __init__(self, port):
        self.port = port
        self.sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.sock.bind(('localhost', self.port))
        self.clients = []
        self.running = False
        self.thread = threading.Thread(target=self.listen)

    def start(self):
        self.running = True
        self.thread.start()

    def stop(self):
        self.running = False
        self.thread.join()

    def listen(self):
        self.sock.listen()
        while self.running:
            client_sock, _ = self.sock.accept()
            self.clients.append(client_sock)
            print(f'New client connected from {client_sock.getpeername()}')

    def send_message(self, message):
        for client in self.clients:
            client.sendall(message.encode())


class ServerGUI:
    def __init__(self):
        self.root = tk.Tk()
        self.root.title('Chatroom Server')
        self.port_label = tk.Label(self.root, text='Port:')
        self.port_label.pack()
        self.port_entry = tk.Entry(self.root)
        self.port_entry.pack()
        self.start_button = tk.Button(self.root, text='Start Server', command=self.start_server)
        self.start_button.pack()
        self.server = None

    def start_server(self):
        port = int(self.port_entry.get())
        self.server = Server(port)
        self.server.start()


if __name__ == '__main__':
    server_gui = ServerGUI()
    server_gui.root.mainloop()
