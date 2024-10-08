let mediaRecorder;
let audioChunks = [];
let isRecording = false;

function startRecording() {
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        navigator.mediaDevices.getUserMedia({ audio: true })
            .then(function (stream) {
                mediaRecorder = new MediaRecorder(stream);
                mediaRecorder.ondataavailable = function (event) {
                    audioChunks.push(event.data);
                };
                mediaRecorder.onstop = function () {
                    let audioBlob = new Blob(audioChunks, { type: 'audio/webm' });
                    let audioUrl = URL.createObjectURL(audioBlob);
                    audioChunks = [];
                    return audioUrl;
                };
                mediaRecorder.start();
                isRecording = true;
            })
            .catch(function (error) {
                console.error('Error accessing microphone:', error);
                alert('Error accessing microphone: ' + error.message);
            });
    } else {
        alert('MediaDevices API not supported');
    }
}

function stopRecording() {
    return new Promise((resolve, reject) => {
        if (mediaRecorder && isRecording) {
            mediaRecorder.stop();
            mediaRecorder.onstop = () => {
                let audioBlob = new Blob(audioChunks, { type: 'audio/webm' });
                let audioUrl = URL.createObjectURL(audioBlob);
                audioChunks = [];
                resolve(audioUrl);
            };
        } else {
            reject('No recording in progress');
        }
    });
}
