// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(connectToSignalR);

function displayNotification(user, json) {
    console.log(json);var $target = $('div#signalr-notifications');
    var data = JSON.parse(json);
    var message =
        `NEW OWNER! ${ data.name} ${data.surname} <br/>
         (email: ${data.email} <br/>
          number: +7${data.number} <br/>
         income: ${data.income} $ <br/>
         age: ${data.age})`;
    var $div = $(`<div>${message}</div>`);
    $target.prepend($div);
    window.setTimeout(function () { $div.fadeOut(2000, function () { $div.remove(); }); }, 8000);
}

function connectToSignalR() {
    console.log("Connecting to SignalR...");
    window.notificationDivs= new Array();
    var conn = new signalR.HubConnectionBuilder().withUrl("/hub").build();
    conn.on("DisplayNotification", displayNotification);
    conn.start().then(function () {
        console.log("SignalR has started.");
    }).catch(function (err) {
        console.log(err);
    });
}