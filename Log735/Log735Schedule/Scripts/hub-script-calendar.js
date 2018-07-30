//$(document).ready(function () {

//    $.ajax({
//        url: '/PrivateRoom/GetHubConnection',
//        type: "GET",
//        dataType: "JSON",

//        success: function (result) {
//            $(function () {
//                //Set the hubs URL for the connection
//                $.connection.hub.url = result;

//                // Declare a proxy to reference the hub.
//                var chat = $.connection.myHub;

//                // Create a function that the hub can call to broadcast messages.
//                chat.client.addMessage = function (name, message) {
//                    // Html encode display name and message.
//                    var encodedName = $('<div />').text(name).html();
//                    var encodedMsg = $('<div />').text(message).html();
//                    // Add the message to the page.
//                    $('#discussion').append('<li><strong>Recieved addMessage' + encodedName
//                        + '</strong>&nbsp;&nbsp;' + encodedMsg + '</li>');
//                };

//                chat.client.sendHelloObject = function (hello) {
//                    // Html encode display name and message.
//                    var encodedName = $('<div />').text(hello.Molly).html();
//                    var encodedMsg = $('<div />').text(hello.Age).html();
//                    // Add the message to the page.
//                    $('#discussion').append('<li><strong>Recieved sendHelloObject:' + encodedName + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
//                };

//                chat.client.heartbeat = function () {
//                    // Html encode display name and message.
//                    var encodedName = $('<div />').text("heartbeat").html();

//                    // Add the message to the page.
//                    $('#discussion').append('<li><strong>Recieved ' + encodedName + '</strong></li>');
//                };

//                // Get the user name and store it to prepend to messages.
//                $('#displayname').val(prompt('Enter your name:', ''));
//                // Set initial focus to message input box.
//                $('#message').focus();
//                // Start the connection.
//                $.connection.hub.start().done(function () {
//                    $('#sendmessage').click(function () {
//                        // Call the Send method on the hub.
//                        chat.server.addMessage($('#displayname').val(), $('#message').val());
//                        // Clear text box and reset focus for next comment.
//                        $('#message').val('').focus();
//                    });

//                    $('#heartbeat').click(function () {
//                        // Call the Send method on the hub.
//                        chat.server.heartbeat();
//                        // Clear text box and reset focus for next comment.
//                        $('#message').val('').focus();
//                    });

//                    $('#sendHelloObject').click(function () {
//                        // Call the Send method on the hub.
//                        chat.server.sendHelloObject({ Age: 2, Molly: $('#message').val() });
//                        // Clear text box and reset focus for next comment.
//                        $('#message').val('').focus();
//                    });
//                });
//            });
//        }
//    });


//});
           
   