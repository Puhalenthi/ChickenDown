import socket
import threading
import tkinter as tk


class ClientGUI:
    def __init__(self):
        self.root = tk.Tk()
        self.root.title('Chatroom Client')
        self.username_label = tk.Label(self.root, text='Username:')
        self.username_label.pack()
        self.username_entry = tk.Entry(self.root)
        self.username_entry.pack()
        self.port_label = tk.Label(self.root, text='Server Port:')
        self.port_label.pack()
        self.port_entry = tk.Entry(self.root)
        self.port_entry.pack()
        self.connect_button = tk.Button(self.root, text='Connect', command=self.connect)
        self.connect_button.pack()
        self.message_label = tk.Label(self.root, text='Message:')
        self.message_label.pack()
        self.message_entry = tk.Entry(self.root)
        self.message_entry.pack()
        self.send_button = tk.Button(self.root, text='Send', command=self.send_message)
        self.send_button.pack()
        self.chatlog_label = tk.Label(self.root, text='Chat Log:')
        self.chatlog_label.pack()
        self.chatlog_text = tk.Text(self.root)
        self.chatlog_text.pack()
        self.client = None

    def connect(self):
        username = self.username_entry.get()
        port = int(self.port_entry.get())
        self.client = ClientGUI('localhost', port, username, self.receive_message)
        self.client.start()

    def send_message(self):
        username = self.username_entry.get()
        message = self.message_entry.get()
        if self.client:
            self.client.send_message(message)
        self.message_entry.delete(0, tk.END)
        self.display_message(f'{username}: {message}')

    def receive_message(self, message):
        self.display_message(message)

    def display_message(self, message):
        self.chatlog_text.config(state=tk.NORMAL)
        self.chatlog_text.insert(tk.END, message + '\n')
        self.chatlog_text.config(state=tk.DISABLED)
        self.chatlog_text.see(tk.END)


if __name__ == '__main__':
    client_gui = ClientGUI()
    client_gui.root.mainloop()
