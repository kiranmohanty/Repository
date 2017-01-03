var http=require("http");   // Importing required module
http.createServer(function(request,response){ // creating node server     
	 response.writeHead(200,{'contentType': 'text/plain'}).
	 response.end('Hello World !');
}).listen(8081); //port no to run the app
console.log("node server is up");
//command for running this file  node main.js . Note:- cmd should point to main.js folder directive
