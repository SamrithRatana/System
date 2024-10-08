document.addEventListener("DOMContentLoaded", function () {
    var hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/chathub")
        .build();

    hubConnection.start().catch(function (err) {
        return console.error(err.toString());
    });

    hubConnection.on("ReceiveNotification", function (message, profilePictureBase64, username) {
        var notificationElement = document.createElement("div");
        notificationElement.classList.add("notification-item");

        // Create an image element for the profile picture
        var profilePicture = document.createElement("img");
        profilePicture.src = "data:image/png;base64," + profilePictureBase64;
        profilePicture.classList.add("profile-picture-evelope");

        // Create a container for the content (username and message)
        var contentElement = document.createElement("div");
        contentElement.classList.add("notification-content");

        // Create an element for the username
        var usernameElement = document.createElement("span");
        usernameElement.classList.add("notification-username");
        usernameElement.textContent = username;

        // Create an element for the message
        var messageElement = document.createElement("span");
        messageElement.classList.add("notification-message");
        messageElement.textContent = message;

        // Append username and message to content container
        contentElement.appendChild(usernameElement);
        contentElement.appendChild(messageElement);

        // Append profile picture and content to the notification element
        notificationElement.appendChild(profilePicture);
        notificationElement.appendChild(contentElement);

        // Add the new notification to the notification area
        var notificationArea = document.getElementById("notificationArea");
        notificationArea.prepend(notificationElement);

        // Update notification badge count
        var badge = document.getElementById("notificationBadge");
        badge.textContent = parseInt(badge.textContent) + 1;
    });

    window.toggleDropdown = function () {
        var dropdown = document.getElementById("notificationDropdown");
        var badge = document.getElementById("notificationBadge");

        dropdown.classList.toggle("show");

        // Clear the badge count when the dropdown is opened
        if (dropdown.classList.contains("show")) {
            badge.textContent = "0";
        }
    };

    window.clearNotifications = function () {
        var notificationArea = document.getElementById("notificationArea");
        notificationArea.innerHTML = ""; // Clear all notifications

        // Optionally reset badge count
        var badge = document.getElementById("notificationBadge");
        badge.textContent = "0";
    };
});

