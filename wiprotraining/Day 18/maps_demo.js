var scores1 = new Map();
//Add values
scores1.set("Tejal", 84);
scores1.set("Pranjal", 90);
scores1.set("Kanishka", 96);
//Get a Value
scores1.get("Pranjal");
//check is a key exists
console.log(scores1.has("Tejal"));
//Iterate over entries ----for of loop
for (var _i = 0, scores1_1 = scores1; _i < scores1_1.length; _i++) {
    var _a = scores1_1[_i], name_1 = _a[0], score = _a[1];
    console.log("".concat(name_1, " scored ").concat(score));
}
//Remove an entry
scores1.delete("Pranjal");
//Map size
console.log(scores1.size);
//clear all entries
scores1.clear();
console.log(scores1.size);
