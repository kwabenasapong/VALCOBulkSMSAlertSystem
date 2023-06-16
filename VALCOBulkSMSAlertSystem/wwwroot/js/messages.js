// JavaScript for Messages tab

// Function to handle tab switching for messages sub-tabs
function openInnerTab(evt, tabName) {
    // Hide all tab content
    var tabContents = document.getElementsByClassName("tab3-content");
    for (var i = 0; i < tabContents.length; i++) {
        tabContents[i].style.display = "none";
    }

    // Remove active class from all tab buttons
    var tabButtons = document.getElementsByClassName("tab3-button");
    for (var i = 0; i < tabButtons.length; i++) {
        tabButtons[i].classList.remove("active");
    }
    document.getElementById("tab3-2-1").style.display = "none";

    // Show the selected tab content
    document.getElementById(tabName).style.display = "block";

    // Add active class to the selected tab button
    evt.currentTarget.classList.add("active");
}
// Use 'click' event on tab button instead of 'onview' event
document.getElementsByClassName("tab3-button")[0].click();

// Function to insert placeholder in the message content textarea
function insertPlaceholder(placeholder) {
    var messageContent = document.getElementById("messageContent");
    var startPos = messageContent.selectionStart;
    var endPos = messageContent.selectionEnd;

    // Insert the placeholder at the current cursor position
    messageContent.value = messageContent.value.substring(0, startPos) + placeholder + messageContent.value.substring(endPos);

    // Move the cursor after the inserted placeholder
    messageContent.selectionStart = startPos + placeholder.length;
    messageContent.selectionEnd = startPos + placeholder.length;
    messageContent.focus();
}

// Function to handle switching to the Create Template sub-tab
function openCreateTemplateTab(evt) {
    // Hide all tab content
    var tabContents = document.getElementsByClassName("tab3-2-content");
    for (var i = 0; i < tabContents.length; i++) {
        tabContents[i].style.display = "none";
    }

    // Remove active class from all tab buttons
    var tabButtons = document.getElementsByClassName("tab3-2-button");
    for (var i = 0; i < tabButtons.length; i++) {
        tabButtons[i].classList.remove("active");
    }

    // Show the Create Template sub-tab content
    document.getElementById("tab3-2-1").style.display = "block";

    // Add active class to the Create Template button
    evt.currentTarget.classList.add("active");
}
