interface Person{
    name:string;
}

interface Employee extends Person{
    employeeId : number;
}

const a1 : Employee = {
    name:'Tejal',
    employeeId : 46291

};

console.log(`Employee Name : ${a1.name} , Employee Id : ${a1.employeeId}`);