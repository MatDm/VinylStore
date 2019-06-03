"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
//document.getElementById("sendButton").disabled = true;

connection.on("DisplayNewChatMessage", function (senderName, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = senderName + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    if (document.getElementById("senderName").innerText === senderName) {
        li.classList.add("senderMessage");
    }
    else {
        li.classList.add("receiverMessage");
    }
    
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var senderName = document.getElementById("senderName").innerText;
   // var userId = document.getElementById("userId").innerText;
    var message = document.getElementById("messageInput").value;
    var receiverName = document.getElementById("receiverName").value;
    connection.invoke("SendMessage", receiverName, senderName, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

function openForm() {
    document.getElementById("myForm").style.display = "block";
    connection.invoke("RetrieveHistory", "receiverName", senderName).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("openButton").style.display = "none";
}

function closeForm() {
    document.getElementById("myForm").style.display = "none";
    document.getElementById("openButton").style.display = "block";
}



