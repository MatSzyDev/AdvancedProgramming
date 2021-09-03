// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function() {
    
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chathub")
        .withAutomaticReconnect()
        .configureLogging(signalR.LogLevel.Information)
        .build();

    
    
    async function start() {
        try {
            await connection.start();
            console.log("SignalR Connected.");
        } catch (err) {
            alert(err);
            setTimeout(start, 5000);
        }
    };

    var $form = $('#form');
    var sendingInProgress = false;
    
    $form.on('submit', async function(e) {
        e.preventDefault();
        var message = $("#message").val();

        try {
            if(connection.state !== signalR.HubConnectionState.Connected) {
                alert("Klient SignalR jeszcze się nie połączył");
                return;
            }

            if(!sendingInProgress) {
                sendingInProgress = true;
                await connection.invoke("SendMessage", message);
                $("#message").val("");
                sendingInProgress = false;
            }
        } catch (err) {
            alert(err);
        }
    });


    connection.onclose(async () => {
        await start();
    });

    connection.on("ReceiveMessage", (email, message, date) => {
        $('#chat').append(`
            <div class="chatMessage">
                <b>${email}</b> - ${date} napisał: <br /> ${message}
            </div>
        `)
    });

    // Start the connection.
    if(isAuthentiacted) {
        start();
    }
    
});