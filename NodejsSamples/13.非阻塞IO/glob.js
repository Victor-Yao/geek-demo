const { Console } = require('console');
const glob = require('glob')

console.time('sync');
var result = glob.sync(__dirname + '/**/*');
console.timeEnd('sync');
// console.log(__dirname)
// console.log(result)
console.log(result.length);

console.time('async')
result = glob(__dirname + '/**/*', function (err, result) {
    console.log(result.length);
})
console.timeEnd('async');

// IO完成之前还可以做别的事
console.log('hello geekbang');