const { error } = require("console");

fetch('https://jsonplaceholder.typicode.com/todos/1')
.then((response)=>response.json())
.then(value=>console.log(value))
.catch((error)=>console.log(error.message));

//post data
const newTodo={
    "userId":1,
    "title":" can we be strangers again",
    "completed":false
}
fetch(
    'https://jsonplaceholder.typicode.com/todos/1',
    {
        method:'POST',
        body:JSON.stringify(newTodo)
    },
    {
        header:{
            'Content-Type': 'application/json'
        }
    }
)
    .then(response=>response.json())
    .then(value=>console.log(value))