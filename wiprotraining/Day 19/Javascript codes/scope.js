var global_x = 1;
console.log(`outside of function global_x`,global_x);

function foo(){
    var local_foo_x = 2
    console.log(`inside foo,global_x=`,global_x);
    console.log(`inside foo,local_foo_x=`,local_foo_x);
}

foo();

console.log(`outside foo,local_foo_x =`,local_foo_x);      //not accessible