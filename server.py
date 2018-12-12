import os
from flask import Flask, send_from_directory, request
from flask_api import FlaskAPI

app = FlaskAPI(__name__, static_url_path='')
dirname = "/home/paulacamargo25/Pictures/SharedFile"

UPLOAD_FOLDER = '/home/paulacamargo25/Pictures/SharedFile'
ALLOWED_EXTENSIONS = set([''jpg'])

app.config['UPLOAD_FOLDER'] = UPLOAD_FOLDER


@app.route('/')
def files():
    return {'data': [
        "http://"+ request.host + "/static"+os.path.join("static", dirname, x)
        for x in os.listdir("/" + dirname)]}

@app.route('/static/<path:path>')
def send_js(path):
    print("finding file")
    requested_file = path.split("/")[-1]
    requested_dir = "/".join(path.split("/")[:-1])
    print(requested_dir, requested_file)
    return send_from_directory("/" + requested_dir, requested_file)

@app.route('/uploader', methods = ['POST'])
def upload_file():
    if request.method == 'POST':
        # check if the post request has the file part
        if 'file' not in request.files:
            flash('No file part')
            return redirect(request.url)
        file = request.files['file']
        # if user does not select file, browser also
        # submit a empty part without filename
        if file.filename == '':
            flash('No selected file')
            return redirect(request.url)
        if file and allowed_file(file.filename):
            filename = secure_filename(file.filename)
            file.save(os.path.join(app.config['UPLOAD_FOLDER'], filename))

