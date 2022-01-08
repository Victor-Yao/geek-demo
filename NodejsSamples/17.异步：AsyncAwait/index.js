/*
 这段代码和视频里不完全一致，但思路是一样的
*/
(async function () {
    await findJob();
    console.log('trip');
})()

async function findJob() {
    try {
        // 进行三轮面试
        await interview(1);
        await interview(2);
        await interview(3);
        try {
            await Promise.all([
                family('father').catch(() => { /*ignore*/ }),
                family('mother'),
                family('wife')
            ])
        } catch (err) {
            err.round = 'family';
            throw err;
        }
        console.log('smile');
    } catch (err) {
        console.log('cry at ' + err.round)
    }
}


/**
 * 进行第round轮面试
 */
function interview(round) {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            if (Math.random() < 0.2) {
                const error = new Error('failed');
                error.round = round;
                reject(error);

            } else {
                resolve('success');
            }
        }, 500)
    })
}
/**
 * 寻求家人的意见确定要不要接受offer
 */
function family(name) {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            if (Math.random() < 0.8) {
                const error = new Error('disagree');
                error.name = name;
                reject(error);

            } else {
                resolve('agree');
            }
        }, Math.random() * 400)
    })
}