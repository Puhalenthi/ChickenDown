import socket
import threading
import tkinter as tk

class ChatClient:
    def __init__(self, host, port, username):
        self.host = host
        self.port = port
        self.username = username
        self.socket = None

        self.window = tk.Tk()
        self.window.title(f"Chatroom ({self.username})")

        self.message_frame = tk.Frame(self.window)
        self.scrollbar = tk.Scrollbar(self.message_frame)
        self.message_list = tk.Listbox(self.message_frame, height=15, width=50, yscrollcommand=self.scrollbar.set)
        self.scrollbar.pack(side=tk.RIGHT, fill=tk.Y)
        self.message_list.pack(side=tk.LEFT, fill=tk.BOTH)
        self.message_frame.pack()

        self.message_input = tk.Entry(self.window)
        self.message_input.pack(side=tk.LEFT, fill=tk.BOTH, expand=True)
        self.message_input.bind("<Return>", self.send_message)

        self.send_button = tk.Button(self.window, text="Send", command=self.send_message)
        self.send_button.pack(side=tk.RIGHT)

    def connect(self):
        self.socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.socket.connect((self.host, self.port))
        self.socket.send(self.username.encode())

        threading.Thread(target=self.receive_messages).start()

    def receive_messages(self):
        while True:
            message = self.socket.recv(1024).decode().strip()
            if not message:
                break

            self.message_list.insert(tk.END, message)

    def send_message(self, event=None):
        message = self.message_input.get()
        if message:
            self.socket.send(message.encode())
            self.message_input.delete(0, tk.END)

    def run(self):
        self.window.mainloop()


if __name__ == "__main__":
    host = input("Enter server IP address: ")
    port = int(input("Enter server port: "))
    username = input("Enter username: ")

    client = ChatClient(host, port, username)
    client.connect()
    client.run()