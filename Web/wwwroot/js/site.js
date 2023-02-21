var Site = {

};

Site.addToCart = function(id) {

    var quantity = $("#quantity").val();

    var book = {
        Id: id,
        Quantity: parseInt(quantity)
    };

    $.ajax({
        url: '/Books/AddToCart',
        data: book,
        type: 'POST',
        success: function(result) {
            window.location.href = "/Checkout/Index";
        },
        error: function(err) {
            if (err.status == 401 || err.status == 403) {
                window.location.href = "/Identity/Account/AccessDenied";
            }
        }
    });
}

Site.finishCheckout = function() {

    $.ajax({
        url: '/Checkout/FinishCheckout',
        type: 'POST',
        success: function(result) {
            $("#successMessage").css("display", "block");
        },
        error: function(err) {
            console.log(err);
        }
    });
}

Site.convertCurrency = function() {

    var selectedValueString = $('#currencyConverter').find(":selected").val();

    var selectedValue = parseFloat(selectedValueString);

    var defaultPriceString = $("#bookPriceUSD").text();

    var defualtPrice = parseFloat(defaultPriceString);

    var newPrice = defualtPrice * selectedValue;

    var formattedPrice = Math.round(newPrice).toLocaleString();

    $("#bookPrice").text(formattedPrice);
}

Site.findBook = function() {

    var searchTerm = $("#searchTerm").val();

    window.location.href = "/Books/SearchResult?searchTerm=" + searchTerm;
}