console.log(sum(20,90));      //it will work before declaration also
//function declaration syntax
function sum(x,y)
{
    var result = x+y;
    return result;
}

var output = sum(10,20);
console.log(output)

console.log(sum2(1,2));     //give error before declaration
//function expression syntax(more like variable declaration and assignment)

var sum2 = function(x , y)
{
    var result = x+y;
    return result;
}

console.log(sum2(13,12));
