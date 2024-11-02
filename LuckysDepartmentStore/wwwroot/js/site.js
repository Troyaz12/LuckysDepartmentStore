// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {

    var dropdowns = document.querySelectorAll('.nav-item.dropdown');
    
    function activateButton(ListItems, Clothing) {
        
        ListItems.classList.add('show');
        Clothing.classList.add('show')
    }

    function deactivateButton(ListItems, Clothing) {

        ListItems.classList.remove('show');
        Clothing.classList.remove('show');
    }

    dropdowns.forEach(dropdown => {

        var ListItems = dropdown.firstElementChild;


        var Clothing = ListItems.nextElementSibling;

        dropdown.addEventListener('mouseenter', () => {
            activateButton(ListItems, Clothing);
        });
        dropdown.addEventListener('mouseleave', () => {
            deactivateButton(ListItems, Clothing);
        });
    });
});