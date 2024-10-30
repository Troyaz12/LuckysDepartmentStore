// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    // Select all list items
    var button = document.getElementById('women');
    var item = document.getElementById('womenList');
    var firstItem = document.getElementById('women');
    var WomensListItems = document.getElementById('womens');

    var womensClothing = document.getElementById('womensClothing');
    const items = womensClothing.querySelectorAll('li');

    //// This simulates the button being "activated" by hover
    //button.addEventListener('mouseenter', function () {
    //    const firstChild = item.firstElementChild;
    //    const secondChild = firstChild.nextElementSibling;
    //    firstChild.classList.add('show');

    //    secondChild.classList.add('show')
    //    secondChild.style.display = 'block';




    //});
    womensClothing.addEventListener('keydown', function () {
        if (event.key === 'Tab') {
            console.log("hi");
            event.preventDefault();

            const focusedItem = document.activeElement;
            const index = Array.from(items).indexOf(focusedItem);
            const lastIndex = items.length - 1;

            if (event.shiftKey) {
                // Shift+Tab, move backwards
                if (index === 0) {
                    items[lastIndex].focus();
                } else {
                    items[index - 1].focus();
                }
            } else {
                // Regular Tab, move forwards
                if (index === lastIndex) {
                    items[0].focus();
                } else {
                    items[index + 1].focus();
                }
            }
        }
    });





    // Reset when mouse leaves  it was item
    WomensListItems.addEventListener('mouseenter', activateButton);
    WomensListItems.addEventListener('mouseleave', deactivateButton);
    item.addEventListener('keydown', function (event) {
        // Check if the pressed key is the Escape key
        if (event.key === 'Escape' || event.keyCode === 27) {
            const firstChild = item.firstElementChild;
            const secondChild = firstChild.nextElementSibling;
            firstChild.classList.remove('show');
            secondChild.classList.remove('show')
            secondChild.style.display = 'none';
        }
      
    });
    //WomensListItems.addEventListener('click', activateButton);

    function activateButton() {
        const firstChild = item.firstElementChild;
        const secondChild = firstChild.nextElementSibling;
        firstChild.classList.add('show');

        secondChild.classList.add('show')
        secondChild.style.display = 'block';
        secondChild.firstElementChild.focus();
    }

    function deactivateButton() {
        const firstChild = item.firstElementChild;
        const secondChild = firstChild.nextElementSibling;
        firstChild.classList.remove('show');
        secondChild.classList.remove('show')
        secondChild.style.display = 'none';
    }

   




});