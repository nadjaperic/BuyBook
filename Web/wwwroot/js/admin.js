var Admin = {
    IsUpdate: false,
    CurrentBookId: ''
}

Admin.openUpdateBook = function(id) {

    Admin.IsUpdate = true;
    Admin.CurrentBookId = id;
    Admin.getBookForModal(id);
}

Admin.openDeleteBook = function(id) {

    Admin.CurrentBookId = id;
}

Admin.openAddBook = function() {


    Admin.IsUpdate = false;
    $("#newBookTitle").val('');
    $("#newBookPrice").val('');
    $("#newBookAuthors").val('');
    $("#newBookShortDescription").val('');
    $("#newBookDescription").val('');
    $("#newBookUrl").val('');
    $("#newBookCategory").val(0);
    $("#newBookFeatured").attr('checked', false);
}

Admin.getBookForModal = function(id) {

    $.ajax({
        url: '/Book/GetBook?id=' + id,
        type: 'GET',
        success: function(result) {
            $("#newBookTitle").val(result.title);
           $("#newBookPrice").val(result.price);
            $("#newBookAuthors").val(result.authors);
            $("#newBookShortDescription").val(result.shortDescription);
            $("#newBookDescription").val(result.description);
            $("#newBookUrl").val(result.bookUrl);
            $("#newBookCategory").val(result.categoryInt);
            $("#newBookFeatured").attr('checked', result.isFeatured);
        },
        error: function(err) {
            console.log(err);
        }
    });
}

Admin.deleteBook = function() {

    $.ajax({
        url: '/Book/DeleteBook?id=' + Admin.CurrentBookId,
        type: 'DELETE',
        success: function(result) {
            window.location.reload();
        },
        error: function(err) {
            console.log(err);
            window.location.reload();
        }
    });
}

Admin.addNewBook = function() {

    var title = $("#newBookTitle").val();
    var price = $("#newBookPrice").val();
    var authors = $("#newBookAuthors").val();
    var shortDescription = $("#newBookShortDescription").val();
    var description = $("#newBookDescription").val();
    var url = $("#newBookUrl").val();
    var category = $("#newBookCategory").val();
    var isFeatured = $("#newBookFeatured")[0].checked;

    if (Admin.IsUpdate) {

        var id = Admin.CurrentBookId;
        var book = {
            Id: id,
            Title: title,
            Price: price,
            Authors: authors,
            ShortDescription: shortDescription,
            Description: description,
            BookUrl: url,
            Category: category,
            IsFeatured: isFeatured
        };

        $.ajax({
            url: '/Book/UpdateBook',
            data: book,
            type: 'POST',
            success: function(result) {
                window.location.reload();
            },
            error: function(err) {
                console.log(err);
                window.location.reload();
            }
        });
    }
    else {

        var book = {
            Title: title,
            Price: price,
            Authors: authors,
            ShortDescription: shortDescription,
            Description: description,
            BookUrl: url,
            Category: category,
            IsFeatured: isFeatured
        };

        $.ajax({
            url: '/Book/CreateBook',
            data: book,
            type: 'POST',
            success: function(result) {
                window.location.reload();
            },
            error: function(err) {
                console.log(err);
            }
        });
    }
}