const http = require('http');
const fs = require('fs');

console.log(__dirname + '/index.html');

http.createServer(function (req, res) {
    console.log('hello');
    // console.log(req);
    if (req.url == '/favicon.ico') {
        res.writeHead(200);
        res.end();
        return;
    }

    res.writeHead(200);
    fs.createReadStream(__dirname + '/index.html')
        .pipe(res);
})
.listen(3000);
console.log('listen on 3000')