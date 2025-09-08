let scores1:Map<string,number>= new Map();

//Add values
scores1.set("Tejal",84);
scores1.set("Pranjal",90);
scores1.set("Kanishka",96);

//Get a Value
scores1.get("Pranjal");

//check is a key exists
console.log(scores1.has("Tejal"));

//Iterate over entries ----for of loop
for(let [name,score] of scores1)
{
    console.log(`${name} scored ${score}`);
}

//Remove an entry
scores1.delete("Pranjal");

//Map size
console.log(scores1.size);

//clear all entries
scores1.clear();
console.log(scores1.size);
