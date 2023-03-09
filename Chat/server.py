import socket
import threading

class ChatServer:
    def __init__(self, host, port):
        self.host = host
        self.port = port
        self.server_socket = None
        self.clients = {}

    def start(self):
        self.server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.server_socket.bind((self.host, self.port))
        self.server_socket.listen(5)
        print(f"Server started on port {self.port}.")

        while True:
            client_socket, address = self.server_socket.accept()
            threading.Thread(target=self.handle_client, args=(client_socket, address)).start()

    def handle_client(self, client_socket, address):
        username = client_socket.recv(1024).decode().strip()
        self.clients[username] = client_socket

        print(f"Client {username} connected from {address}.")

        while True:
            message = client_socket.recv(1024).decode().strip()
            if not message:
                break

            print(f"{username}: {message}")
            for client in self.clients.values():
                client.send(f"{username}: {message}".encode())

        del self.clients[username]
        client_socket.close()


if __name__ == "__main__":
    server = ChatServer("", 1234)
    server.start()