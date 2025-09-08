function foo(x,y,z)
{
    console.log(x,y,z)
    console.log(`foo was called`)

    console.log(arguments);  //object that gives all the values passed while calling in key value pair
    console.log(arguments[0],arguments[2])
    console.log(arguments.length);  //4
}

foo('hello','world','Good', 'Morning');