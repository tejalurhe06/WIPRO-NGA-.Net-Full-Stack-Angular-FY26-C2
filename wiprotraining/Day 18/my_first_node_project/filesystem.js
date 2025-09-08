const fs = require('fs');
fs.readFile('example.txt','utf8',(err,data)=>{
    if(err)
    {
        console.error('Error reading file :' ,err);
        return ;
    }
    console.log('File contents:' , data);
});



fs.writeFile('output.txt', 'Hello,Node.js!',(err)=>{
    if(err){
        console.error('Error writing : ',err);
        return;
    }
    console.log('File Successfully written!');
});


