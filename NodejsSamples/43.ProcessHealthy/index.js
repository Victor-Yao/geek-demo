/*process guard*/

const cluster = require('cluster');
const os = require('os');

if (cluster.isMaster) {
    // os.cpus.length / 2
    for (let i = 0; i < 1; i++) {
        createWorker();
    }

    cluster.on('exit', function () {
        setTimeout(() => {
            createWorker()
        }, 5000)
    })

    function createWorker() {
        //new process for beat health check
        var worker = cluster.fork();
        let missedPing = 0;

        //beat here
        let timer = setInterval(() => {
            
            //kill worker if it missed 3 times beat
            if (missedPing >= 3) {
                clearInterval(timer);
                console.log(worker.process.pid + ' became a zombie!');
                process.kill(worker.process.pid);
                // worker.exit(1);
                return;
            }
            //start to beat
            missedPing++;
            console.log('ping#' + worker.process.pid);
            worker.send('ping#' + worker.process.pid);
        }, 1000);

        worker.on('message', (msg) => {
            //received beat
            console.log(msg)
            if (msg == "pong#" + worker.process.pid) {
                console.log('memory#' + process.memoryUsage().rss);
                missedPing--;
            }
        })
    }

} else {
    require('./app');

    //handle error when process is crashed
    process.on('uncaughtException', function (err) {
        console.log(err);
        process.exit(1);
    });

    //respond healthy beat
    process.on('message', function (msg) {
        if (msg == 'ping#' + process.pid) {
            process.send('pong#' + process.pid);
        }
    })

    //kill process when memory usage > 700MB
    if (process.memoryUsage().rss > 734003200) {
        process.exit(1);
    }
}