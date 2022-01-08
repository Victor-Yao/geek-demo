/**
 * 通过Promise.all完成异步并行任务
 */

Promise.all([
    // family('father').catch(() => { }),
    family('father'),
    family('mother'),
    family('wife')
]).then(() => {
    console.log('family all agree');
}).catch((err) => {
    console.log(err.name + ' not agree');
})


function family(name) {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            if (Math.random() < 0.5) {
                const error = new Error('disagree');
                error.name = name;
                reject(error);
            } else {
                resolve('agree');
            }
        }, Math.random() * 400)
    })
}