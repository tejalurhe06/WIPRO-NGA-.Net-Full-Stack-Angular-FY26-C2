var __values = (this && this.__values) || function(o) {
    var s = typeof Symbol === "function" && Symbol.iterator, m = s && o[s], i = 0;
    if (m) return m.call(o);
    if (o && typeof o.length === "number") return {
        next: function () {
            if (o && i >= o.length) o = void 0;
            return { value: o && o[i++], done: !o };
        }
    };
    throw new TypeError(s ? "Object is not iterable." : "Symbol.iterator is not defined.");
};
var e_1, _a, e_2, _b;
var fruits = ['apple', 'banana', 'orange'];
try {
    for (var fruits_1 = __values(fruits), fruits_1_1 = fruits_1.next(); !fruits_1_1.done; fruits_1_1 = fruits_1.next()) {
        var fruit = fruits_1_1.value;
        console.log(fruit);
    }
}
catch (e_1_1) { e_1 = { error: e_1_1 }; }
finally {
    try {
        if (fruits_1_1 && !fruits_1_1.done && (_a = fruits_1.return)) _a.call(fruits_1);
    }
    finally { if (e_1) throw e_1.error; }
}
var uniqueNumbers1 = new Set([1, 2, 3, 4, 5]);
try {
    for (var uniqueNumbers1_1 = __values(uniqueNumbers1), uniqueNumbers1_1_1 = uniqueNumbers1_1.next(); !uniqueNumbers1_1_1.done; uniqueNumbers1_1_1 = uniqueNumbers1_1.next()) {
        var number = uniqueNumbers1_1_1.value;
        console.log(number);
    }
}
catch (e_2_1) { e_2 = { error: e_2_1 }; }
finally {
    try {
        if (uniqueNumbers1_1_1 && !uniqueNumbers1_1_1.done && (_b = uniqueNumbers1_1.return)) _b.call(uniqueNumbers1_1);
    }
    finally { if (e_2) throw e_2.error; }
}
