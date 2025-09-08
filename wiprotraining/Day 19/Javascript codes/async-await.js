const fetchAndAddTodos = async function(){
    try{
    const response = await fetch('https://jsonplaceholder.typicode.com/todos/1');
    const todo = await response.json();
    console.log(todo);
    }
    catch(error){
        console.log(error.message);
    }
};

fetchAndAddTodos();