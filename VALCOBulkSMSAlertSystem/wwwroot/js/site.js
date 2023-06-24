// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification

// Coverflow for the image gallery in Home/landing page
var covers = document.querySelectorAll('#coverFlow .cover');
    var currentIndex = 0;

    function showSlide(index) {
        covers.forEach(function (cover, i) {
            cover.style.display = i === index ? 'block' : 'none';
        });
    }

    function nextSlide() {
        currentIndex++;
        if (currentIndex >= covers.length) {
            currentIndex = 0;
        }
        showSlide(currentIndex);
    }

    setInterval(nextSlide, 3000); // Change the interval time (in milliseconds) as needed

