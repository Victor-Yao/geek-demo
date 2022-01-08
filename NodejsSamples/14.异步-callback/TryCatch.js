
try {
    interview('good');
    interview();
} catch (error) {
    console.log('name: ' + error.name);
    console.log('message: ' + error.message);
    console.log('stack: ' + error.stack);
}
finally{
    console.log('finally')
}

function interview(result) {
    if (result == null)
        throw new Error("result is null");

    console.log(result);
}