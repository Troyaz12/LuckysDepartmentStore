// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {

    var dropdowns = document.querySelectorAll('.nav-item.dropdown');
    
    function activateButton(WomensListItems, womensClothing) {
        
        WomensListItems.classList.add('show');
        womensClothing.classList.add('show')
    }

    function deactivateButton(WomensListItems, womensClothing) {

        WomensListItems.classList.remove('show');
        womensClothing.classList.remove('show');
    }

    dropdowns.forEach(dropdown => {

        var WomensListItems = dropdown.firstElementChild;


        var womensClothing = WomensListItems.nextElementSibling;

        dropdown.addEventListener('mouseenter', () => {
            activateButton(WomensListItems, womensClothing);
        });
        dropdown.addEventListener('mouseleave', () => {
            deactivateButton(WomensListItems, womensClothing);
        });
    });
});