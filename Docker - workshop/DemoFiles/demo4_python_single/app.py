from flask import Flask

app = Flask(__name__)

@app.route('/')
def hello_world():
    return 'Jej! you are great! Now improve this image size :)'