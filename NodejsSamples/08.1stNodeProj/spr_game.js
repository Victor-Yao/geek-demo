const playerAction = process.argv[process.argv.length - 1];
// console.log(playerAction);
const computerActions = ['rock', 'scissor', 'paper'];

if (!playerAction) {
    console.log('playerAction is null');
    return;
}

if (computerActions.indexOf(playerAction) == -1) {
    console.log('你的输入:' + playerAction + '。请输入rock或paper或scissor。');
} else {
    console.log('你出了: ' + playerAction);
}

var random = Math.floor(Math.random() * 3);
computerAction = computerActions[random];
console.log('电脑出了: ' + computerAction)

if (playerAction == computerAction)
    console.log('平局')
else if (
    (computerAction == 'rock' && playerAction == 'scissor') ||
    (computerAction == 'scissor' && playerAction == 'paper') ||
    (computerAction == 'paper' && playerAction == 'rock')
) {
    console.log('你输了')
}
else {
    console.log('你赢了')
}