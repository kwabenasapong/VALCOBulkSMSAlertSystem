// JavaScript for Create Template sub-tab in Messages tab

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
