import os
import sqlite3
import subprocess
from flask import Flask, request

app = Flask(__name__)

# 🚨 SQL Injection vulnerability
def get_user_data(username):
    conn = sqlite3.connect("users.db")
    cursor = conn.cursor()
    query = f"SELECT * FROM users WHERE username = '{username}'"  # 🚨 Vulnerable to SQL Injection
    cursor.execute(query)
    result = cursor.fetchall()
    conn.close()
    return result

# 🚨 Command Injection vulnerability
@app.route("/ping", methods=["GET"])
def ping():
    ip = request.args.get("ip")
    output = subprocess.check_output(f"ping -c 3 {ip}", shell=True)  # 🚨 User input directly in shell command
    return output

# 🚨 Hardcoded Credentials
DB_PASSWORD = "supersecret123"  # 🚨 Sensitive information hardcoded

# 🚨 Insecure File Handling
@app.route("/readfile", methods=["GET"])
def read_file():
    filename = request.args.get("file")
    with open(filename, "r") as f:  # 🚨 No validation of file path (Path Traversal)
        return f.read()

if __name__ == "__main__":
    debug_mode = os.getenv("FLASK_DEBUG", "False").lower() in ["true", "1", "t"]
    app.run(debug=debug_mode)
