$(document).ready(function () {

        $('.options-radio').change(function (event) {
            updateSizes(event);
        });

        updateSizes();
        document.getElementById("ColorSelection").value = JSON.parse($('#ColorProduct').attr('data-color-id'))[0];

        function updateSizes(event) {
            var selectedRadio = event ? $(event.target) : $('.options-radio:checked');
            var selectedRadioId = selectedRadio.attr('id');
            var colorSelected = $('label[for="' + selectedRadioId + '"]').text();
            var objectArray = [];         

            var colorProducts = $('#ColorProduct').data('products');

            colorProducts.forEach(function (colorProduct) {
                objectArray.push({
                    Name: colorProduct.Name,
                    SizeID: colorProduct.SizeID,
                    SizeName: colorProduct.SizeName,
                    Quantity: colorProduct.Quantity,
                    ColorProductID: colorProduct.ColorProductID,
                    ProductID: colorProduct.ProductID,
                    ColorID: colorProduct.ColorID,
                    SelectedColor: colorSelected
                });
            });

            $.ajax({
                url: $('#ColorProduct').attr('data-url'),
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(objectArray),
                success: function (response) {
                    $('#sizesContainer').html(response);
                },
                error: function (xhr, status, error) {
                    var errorMessage = xhr.status + ': ' + xhr.statusText;
                    alert('Error sending array of objects: ' + errorMessage);
                }
            });
        }

        $("#sizesContainer").on("click", "#sizes", function (e) {
            e.preventDefault();
            var index = $(this).data("index");
            var sizeID = $(this).data("sizeid");
            $("#SizeSelection").val(sizeID);
            $("#sizesContainer .size-button").removeClass("selected");
            $(this).addClass("selected");
        });
        document.getElementById('addToCartButton').addEventListener('click', function (event) {
            var size = document.getElementById('SizeSelection');
            var validationSize = document.getElementById('validationSize');
            var validationQuantity = document.getElementById('validationQuantity');

            if (size.value === '0') {
               // alert("Please select a size before adding to cart.");
                validationSize.style.display = 'block';
                event.preventDefault();  // Prevent form submission
            }
            else {
                validationSize.style.display = 'none';
            }

            var quantity = document.getElementById('Quantity');
            var quantityValue = parseInt(quantity.value);

            if (isNaN(quantityValue) || quantityValue <= '0') {
                // alert("Please select a size before adding to cart.");
                validationQuantity.style.display = 'block';
                event.preventDefault();  // Prevent form submission
            }
            else {
                validationQuantity.style.display = 'none';
            }


        });





});
