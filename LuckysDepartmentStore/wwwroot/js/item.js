$(window).on('load', function () {
    
        $('.options-radio').change(function (event) {
            updateSizes(event);
        });

        updateSizes();
        document.getElementById("ColorSelection").value = JSON.parse($('#ColorProduct').attr('data-color-id'))[0];

        function updateSizes(event) {
            console.log("Running updateSizes...");
            var selectedRadio = event ? $(event.target) : $('.options-radio:checked');
            var selectedRadioId = selectedRadio.attr('id');
            var colorSelected = $('label[for="' + selectedRadioId + '"]').text();
            var objectArray = [];

            console.log("Selected Color: " + colorSelected);

            var colorProducts = $('#ColorProduct').data('products');
            console.log("Color Products: ", colorProducts);

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

            console.log("Object Array: ", objectArray);

            $.ajax({
                url: $('#ColorProduct').attr('data-url'),
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(objectArray),
                success: function (response) {
                    $('#sizesContainer').html(response);
                    console.log("Sizes updated successfully.");
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

        document.querySelectorAll('input[name="options"]').forEach((elem) => {
            elem.addEventListener("change", function (event) {
                document.getElementById("ColorSelection").value = event.target.value;
            });
        });
    
});
