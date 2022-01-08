const fs = require('fs');
const game = require('./spr_game');
const express = require('express');

// 玩家胜利次数，如果超过3，则后续往该服务器的请求都返回500
var playerWinCount = 0;
// 玩家的上一次游戏动作
var lastPlayerAction = 0;
// 玩家连续出同一个动作的次数
var sameCount = 0;

const app = express();

// 通过app.get设定 /favicon.ico 路径的路由
// .get 代表请求 method 是 get，所以这里可以用 post、delete 等。这个能力很适合用于创建 rest 服务
app.get('/favicon.ico', function (req, res) {
    // 一句 status(200) 代替 writeHead(200); end();
    res.status(200);
    return;
})

// 设定 /game 路径的路由
app.get('/game',
    function (req, res, next) {
        if (playerWinCount >= 3 || sameCount == 9) {
            res.status(200);
            res.send('不会再玩了！！');
            return;
        }

        // 通过next执行后续中间件
        next();

        if (res.playerWon) {
            playerWinCount++;
        }
    },

    function (req, res, next) {
        // express自动帮我们把query处理好挂在request上
        const query = req.query;
        const playerAction = query.action;

        if (!playerAction) {
            res.status(400);
            res.send('client error');
            return;
        }

        if (lastPlayerAction == playerAction) {
            sameCount++;
            if (sameCount > 3) {
                res.status(400);
                res.send('你作弊！我再也不玩了');
                sameCount = 9;
                return;
            }
        } else {
            sameCount = 0;
        }
        lastPlayerAction = playerAction;

        // 把用户操作挂在response上传递给下一个中间件
        res.playerAction = playerAction;
        next();
    },

    function (req, res) {
        const playerAction = res.playerAction;
        const result = game(playerAction);

        // 如果这里执行setTimeout，会导致前面的洋葱模型失效
        // 因为playerWon不是在中间件执行流程所属的那个事件循环里赋值的
        // setTimeout(()=> {
        res.status(200);
        if (result == 0) {
            res.send('平局');
        } else if (result == -1) {
            res.send('你输了');
        } else {
            res.send('你赢了');
            res.playerWon = true;
        }
    }
)


app.get('/', (req, res) => {
    // send接口会判断你传入的值的类型，文本的话则会处理为text/html
    // Buffer的话则会处理为下载
    res.send(
        fs.readFileSync(__dirname + '/index.html', 'utf-8')
    );
})

app.listen(3000);