//arithmetic operators
var x = 12,y=30;
console.log(10.5+20.24);
console.log(x-y);
console.log(x*y);
console.log(x/y);
console.log(y % x);

//relational operators
var isBefore = "hello" < "hi";
console.log(isBefore);     //true

isBefore = "hello" > "hi"
console.log(isBefore);

var isGreater = 10 > 9;
console.log(isGreater);

console.log(x >=y);
console.log(x != y);

console.log(1 !== '1' );
console.log(1 == '1');  // true -----type unsafe equality operator
console.log(1 ==='1');    //false      --------type safe equality operator

//logical operators
var isRaining = true,goingByWalk = true, goingByCar = false;
var takeUmbrella = isRaining && goingByWalk;
console.log(takeUmbrella);
takeUmbrella = isRaining || goingByWalk;
console.log(takeUmbrella);

console.log(!true);

//miscellaneous operators

var name="tejal",msg="how are you!";
console.log(name +" " +msg);



//relational op > logical op ------precedence