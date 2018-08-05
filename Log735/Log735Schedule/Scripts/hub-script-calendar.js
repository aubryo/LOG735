$(document).ready(function () {
    var serveur = $("#hub1");
    var serveurUrl = "";
    if (serveur) {
        serveurUrl = "http://localhost:8089/signalr";
    }
    else {
        serveur = $("#hub2");
        if (serveur)
            serveurUrl = "http://localhost:8088/signalr";
    }

    $(function () {
        //Set the hubs URL for the connection
        $.connection.hub.url = serveurUrl;
        $.connection.hub.qs = {
            'roomId': modelJava.RoomId,
            'roomName': modelJava.RoomName,
            'userName': modelJava.UserName
        };
        // Declare a proxy to reference the hub.
        var privateRoomProxy = $.connection.privateRoomHub;

        var startTime = $.fullCalendar.moment('2018-08-02T08:00:00');
        var endTime = $.fullCalendar.moment('2018-08-02T12:00:00');
        var event = {

            title: "Hell22o",
            description: "Patate",
            start: startTime,
            end: endTime,
            backgroundColor: "#9501fc",
            borderColor: "#fc0101"

        };
        $("#calendar").fullCalendar('renderEvent', event, true);
        //create a function that the hub can call to broadcast messages.
        privateRoomProxy.client.newEvent = function (eventModel) {
            var events = {

                title: "Hello",
                description: "Patate",
                start: moment("2018-08-03T08:30:00"),
                end: moment("2018-08-03T12:30:00"),
                backgroundColor: "#9501fc",
                borderColor: "#fc0101"

            };
            $("#calendar").fullCalendar('renderEvent', events, true);
            addEventLog("Add Event");
        };

        privateRoomProxy.client.removeEvent = function (id) {
            $("#calendar").fullCalendar('removeEvent', id);
            addEventLog("Remove Event");
        };

        privateRoomProxy.client.NotifyConnectionToRoom = function (userName) {
            addEventLog("Connexion de l'utilisateur" + userName);
            
        };
        privateRoomProxy.client.heartbeat = function () {
            // Html encode display name and message.
            var encodedName = $('<div />').text("heartbeat").html();

            // Add the message to the page.
            $('#discussion').append('<li><strong>Recieved ' + encodedName + '</strong></li>');
        };
        $.connection.hub.disconnected(function () {
            if ($.connection.hub.lastError) {
                alert("Disconnected. Reason: " + $.connection.hub.lastError.message);
            }
        });
           // Start the connection.
        $.connection.hub.start().done(function () {
            $('#buttonrefresh').click(function () {
                // Call the Send method on the hub.
                privateRoomProxy.server.newEvent("3");

            });

            $('#heartbeat').click(function () {
                // Call the Send method on the hub.
                //  privateRoomProxy.server.heartbeat();
                // Clear text box and reset focus for next comment.
                $('#message').val('').focus();
            });

            $('#sendHelloObject').click(function () {
                // Call the Send method on the hub.
                // privateRoomProxy.server.sendHelloObject({ Age: 2, Molly: $('#message').val() });
                // Clear text box and reset focus for next comment.
                $('#message').val('').focus();
            });
        });
    });



});

function ConnectToHub(serveurUrl) {
    //Set the hubs URL for the connection
    $.connection.hub.url = serveurUrl;

    // Declare a proxy to reference the hub.
    var privateRoomProxy = $.connection.privateRoomHub;

    privateRoomProxy.client.newEvent = function (eventCourse,eventLab) {
        var events = {

            title: "Hello",
            description: "Patate",
            start: moment("2018-08-03T08:30:00"),
            end: moment("2018-08-03T12:30:00"),
            backgroundColor: "#9501fc",
            borderColor: "#fc0101"

        };
        $("#calendar").fullCalendar('renderEvent', events, true);
        addEventLog("Add Event");
    };

    privateRoomProxy.client.removeEvent = function (id) {
        $("#calendar").fullCalendar('removeEvent', id);
        addEventLog("Remove Event");
    };

    privateRoomProxy.client.heartbeat = function () {
        // Html encode display name and message.
        var encodedName = $('<div />').text("heartbeat").html();

        // Add the message to the page.
        $('#discussion').append('<li><strong>Recieved ' + encodedName + '</strong></li>');
    };
    $.connection.hub.disconnected(function () {
        if ($.connection.hub.lastError) {
            alert("Disconnected. Reason: " + $.connection.hub.lastError.message);
        }
    });
    // Start the connection.
    $.connection.hub.start().done(function () {
        $('#buttonrefresh').click(function () {
            // Call the Send method on the hub.
            privateRoomProxy.server.newEvent("3");

        });

        $('#heartbeat').click(function () {
            // Call the Send method on the hub.
            //  privateRoomProxy.server.heartbeat();
            // Clear text box and reset focus for next comment.
            $('#message').val('').focus();
        });

        $('#sendHelloObject').click(function () {
            // Call the Send method on the hub.
            // privateRoomProxy.server.sendHelloObject({ Age: 2, Molly: $('#message').val() });
            // Clear text box and reset focus for next comment.
            $('#message').val('').focus();
        });
    });
}
function addEventLog(message) {
    var eventLog = $("#eventLog");
    var listMessage = eventLog.find("li");
    if (listMessage.length !== 0) {
        if (listMessage.length <= 20) {
            eventLog.prepend('<li>' + message + '</li>');
        }
        else {
            listMessage.last().remove();
        }

    }
    else {
        console.log(message);
    }

}