console.log('===str====');
var str = '12345678';
var buffer = Buffer.alloc(4);
buffer.writeInt32BE(str);
console.log(buffer);
console.log('buffer.readInt32BE(): ' + buffer.readInt32BE());
console.log('buffer.readInt16BE(): ' + buffer.readUInt16BE());


console.log('===num====');
var num = 255;
var buffer1 = Buffer.alloc(4);
buffer1.writeInt32BE(num);
console.log(buffer1);


console.log('===chr====');
var str1 = 'abcdeffgh';
var chr = str1[0];
console.log('chr: ' + chr);
var buffer2 = Buffer.alloc(8);
buffer2.writeInt32BE(str1);
console.log(buffer2);
buffer2.writeInt32BE(chr);
console.log(buffer2);


