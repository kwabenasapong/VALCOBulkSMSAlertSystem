function openTab(event, tabId) {
    var tabContent = document.getElementsByClassName("tab-content");
    for (var i = 0; i < tabContent.length; i++) {
        tabContent[i].style.display = "none";
    }

    var tabButtons = document.getElementsByClassName("tab-button");
    for (var i = 0; i < tabButtons.length; i++) {
        tabButtons[i].className = tabButtons[i].className.replace(" active", "");
    }

    document.getElementById(tabId).style.display = "block";
    event.currentTarget.className += " active";
}

function openInnerTab(event, tabId) {
    var tab3Content = document.getElementsByClassName("tab3-content");
    for (var i = 0; i < tab3Content.length; i++) {
        tab3Content[i].style.display = "none";
    }

    var tab3Buttons = document.getElementsByClassName("tab3-button");
    for (var i = 0; i < tab3Buttons.length; i++) {
        tab3Buttons[i].className = tab3Buttons[i].className.replace(" active", "");
    }
    document.getElementById("tab3-2-1").style.display = "none";

    document.getElementById(tabId).style.display = "block";
    event.currentTarget.className += " active";
}
// Use 'click' event on tab button instead of 'onview' event
document.getElementsByClassName("tab-button")[0].click();

// Use 'click' event on tab button instead of 'onview' event
document.getElementsByClassName("tab3-button")[0].click();

function insertPlaceholder(placeholder) {
    var textarea = document.getElementById("Content");
    var start = textarea.selectionStart;
    var end = textarea.selectionEnd;
    var text = textarea.value;
    var newText = text.substring(0, start) + placeholder + text.substring(end, text.length);
    textarea.value = newText;
}
