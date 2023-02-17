var Site = {

};

Site.addToCart = function(id) {

    var quantity = $("#quantity").val();

    var book = {
        Id: id,
        Quantity: parseInt(quantity)
    };

    $.ajax({
        url: '/Book/AddToCart',
        data: book,
        type: 'POST',
        success: function(result) {
            window.location.href = "/Checkout/Index";
        },
        error: function(err) {
            console.log(err);
        }
    });
}

Site.finishCheckout = function() {

    $.ajax({
        url: '/Checkout/FinishCheckout',
        type: 'POST',
        success: function(result) {
            window.location.href = "/Home/Index";
        },
        error: function(err) {
            console.log(err);
        }
    });
}