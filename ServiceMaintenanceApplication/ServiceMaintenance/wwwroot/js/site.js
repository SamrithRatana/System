function saveAsFile(fileName, base64String) {
    var link = document.createElement('a');
    link.href = 'data:application/pdf;base64,' + base64String;
    link.download = fileName;
    link.click();


}

// wwwroot/js/site.js

function applyDarkMode() {
    document.body.style.backgroundColor = 'black';
    document.body.style.color = 'white';
}

function applyLightMode() {
    document.body.style.backgroundColor = 'white';
    document.body.style.color = 'black';
}

