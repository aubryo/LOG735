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

        privateRoomProxy.client.errorEvent = function(message)
        {
            addEventLog(message);
        };

        $("#calendar").fullCalendar('renderEvent', event, true);
        //create a function that the hub can call to broadcast messages.
        privateRoomProxy.client.initiateFullCalendar = function (listCalendarEvent) {
            var events = [];
            listCalendarEvent.forEach(function (element) {
                events.push(
                    {
                        _id: element.EventId,
                        title: element.Title,
                        description: element.Description,
                        start: moment(element.Start),
                        end: moment(element.End),
                        backgroundColor: element.BackgroundColor,
                        borderColor: element.BorderColor
                    });

            });
            $("#calendar").fullCalendar('addEventSource', events, true);
        };
        privateRoomProxy.client.newEvent = function (eventCourse, eventLab) {
            var eventCourseCal = {

                _id: eventCourse.EventId,
                title: eventCourse.Title,
                description: eventCourse.Description,
                start: moment(eventCourse.Start),
                end: moment(eventCourse.End),
                backgroundColor: eventCourse.BackgroundColor,
                borderColor: eventCourse.BorderColor
                

            };
            var eventLabCal = {

                _id: eventLab.EventId,
                title: eventLab.Title,
                description: eventLab.Description,
                start: moment(eventLab.Start),
                end: moment(eventLab.End),
                backgroundColor: eventLab.BackgroundColor,
                borderColor: eventLab.BorderColor

            };
            $("#calendar").fullCalendar('renderEvent', eventCourseCal, true);
            $("#calendar").fullCalendar('renderEvent', eventLabCal, true);
            addEventLog("Cours " + eventCourse.Title+ " ajouté");
        };

        privateRoomProxy.client.removeEvent = function (idCourse, courseName) {
       
            $("#calendar").fullCalendar('removeEvents', idCourse);
            addEventLog("Le cours "+courseName+" a été enlevé");
        };

        privateRoomProxy.client.NotifyConnectionToRoom = function (userName) {
            addEventLog("Connexion de l'utilisateur " + userName);
            
        };
        privateRoomProxy.client.notifyLeavingRoom = function (username) {
           
            addEventLog("Déconnexion de l'utilisateur " + userName);
            
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
                privateRoomProxy.server.addEventCourse("3");

            });
            $('#removeEvent').click(function () {

                privateRoomProxy.server.removeEventCourse("3");

            });
            $('#Enlever').click(function () {

                privateRoomProxy.server.removeEventCourse($('#Groupes').val());

            });
            $('#Ajouter').click(function () {
               
                privateRoomProxy.server.addEventCourse($('#Groupes').val());

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


    $("#Courses").change(function () {
        $("#Groupes").empty();
        $.ajax({
            type: 'POST',
            url: '/PrivateRoom/GetCourseGroup', // we are calling json method  

            dataType: 'json',

            data: { id: $("#Courses").val() },
            // here we are get value of selected country and passing same value  
            //as inputto json method GetStates.

                success: function(group) {
                    // states contains the JSON formatted list  
                    // of states passed from the controller  
                    $("#Groupes").append('<option value="">- Choisir un group -</option>');
                    $.each(group, function (i, group) {
          
                 
                        $("#Groupes").append('<option value="' + group.Value + '">' +
                            group.Text + '</option>');
                        // here we are adding option for States  

                    });
                }
           
        });
        return false;
    })  
    $("#Groupes").change(function () {

        $.ajax({
            type: 'POST',
            url: '/PrivateRoom/GetCourseInfo', // we are calling json method  

            dataType: 'json',

            data: { id: $("#Groupes").val() },
            // here we are get value of selected country and passing same value  
            //as inputto json method GetStates.

            success: function (group) {
                // states contains the JSON formatted list  
                // of states passed from the controller  
                $('#CourseInfo').empty();
                $.each(group, function (i, group) {
                    var container = $('<div class="fc-event-container"></div>');
                    var a = $('<a class="fc-time-grid-event fc-event fc-start fc-end" style="background-color:' + group.BackgroundColor + '; border-color:' + group.BorderColor + '; top: 21px; bottom: -109px; z-index: 1; left: 0 %; right: 0 %; ">');
                    var content = $('<div class="fc-content"></div>');
                    var divStart = $('<div class="fc-time""><span>' + group.Start + ' - ' + group.End + '</span></div><div class="fc-title">' + group.Title + '</div>');
                    divStart.appendTo(content);
                    content.appendTo(a);
                    a.appendTo(container);
                    container.appendTo('#CourseInfo');

                });
            },
            error: function (ex) {
                $('#CourseInfo').empty();
            }
        });
    })
  

});

function ConnectToHub(serveurUrl) {
    //Set the hubs URL for the connection

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