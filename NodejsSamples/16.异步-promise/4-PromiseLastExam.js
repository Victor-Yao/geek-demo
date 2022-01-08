/**
 * Promise的最后一个例子，把前面的例子都整合进来了
 * 
 * 进行三轮面试，都通过之后征求所有家庭成员的同意
 * 只要有一处不通过则面试失败
 */

interview(1)
    .then(() => {
        return interview(2);
    })
    .then(() => {
        return interview(3)
    })
    .then(() => {
        return Promise.all([
            family('father').catch(() => {/*ignore father's disgree*/ }),
            family('mother'),
            family('wife'),
        ]).catch((err) => {
            err.round = 'family';
            throw err;
        })
    })
    .then(() => {
        console.log('success');
    })
    .catch((err) => {
        console.log('cry at ' + err.round)
    })


function interview(round) {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            if (Math.random() < 0.1) {
                const error = new Error('failed');
                error.round = round;
                reject(error);
            } else {
                resolve('success');
            }
        }, 500)
    })
}


function family(name) {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            if (Math.random() < 0.99) {
                const error = new Error('disagree');
                error.name = name;
                reject(error);
            } else {
                resolve('agree');
            }
        }, Math.random() * 400)
    })
}