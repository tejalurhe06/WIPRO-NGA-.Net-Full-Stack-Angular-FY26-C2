function Logger(constructor : Function)
{
    console.log(`Creating instance of : ${constructor.name}`);
}

@Logger
class User
{
    constructor(public name : string)
    {

    }
}

console.log(logger);