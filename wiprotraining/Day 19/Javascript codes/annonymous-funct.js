// const sum = function(x,y)
// {
//     return x+y;
// };

// console.log(sum(50,3264));

const sum = function(x,y,cb)
{
    var result = x+y;
    cb(result);
};

function logResult(result)
{
    console.log(result)
}


//1 . sum(6,10,logResult);

//  2 .sum(6,10,function logResult(result)
// {
//     console.log(result)
// });

sum(6, 10, function(result)
{
    console.log(result)
});