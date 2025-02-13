import os  
import subprocess
import hashlib 

filename = input("Enter a filename to read: ")
with open(filename, 'r') as file:
    data = file.read()

password = 'secretpassword'

command = input("Enter a shell command to execute: ")
subprocess.run(command, shell=True)

hashed_password = hashlib.md5(password.encode()).hexdigest()

os.system('echo "This is a vulnerable file" > vulnerable_file.txt')

username = input("Enter your username: ")
if username == 'admin':
    print("Welcome, admin!") 
