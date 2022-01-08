module.exports = function (playerAction) {
    console.log(playerAction);

    if (!playerAction) {
        console.log('playerAction is null');
        return -1;
    }

    const computerActions = ['rock', 'scissor', 'paper'];
    if (computerActions.indexOf(playerAction) == -1) {
        console.log('你的输入:' + playerAction + '。请输入rock或paper或scissor。');
    } else {
        console.log('你出了: ' + playerAction);
    }

    var random = Math.floor(Math.random() * 3);
    computerAction = computerActions[random];
    console.log('电脑出了: ' + computerAction)

    if (playerAction == computerAction) {
        console.log('平局');
        return 0;
    }
    else if (
        (computerAction == 'rock' && playerAction == 'scissor') ||
        (computerAction == 'scissor' && playerAction == 'paper') ||
        (computerAction == 'paper' && playerAction == 'rock')
    ) {
        console.log('你输了')
        return -1;
    }
    else {
        console.log('你赢了');
        return 1;
    }
}
