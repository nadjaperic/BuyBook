var Admin = {
    IsUpdate: false,
    CurrentBookId: '',
    CurrentCategoryId: '',
    CurrentPublisherId: '',
    CurrentAuthorId: '',
    CurrentAdminId: ''
}

Admin.openUpdateBook = function(id) {

    Admin.IsUpdate = true;
    Admin.CurrentBookId = id;
    Admin.getBookForModal(id);
}

Admin.openDeleteBook = function(id) {
    Admin.CurrentBookId = id;
}

Admin.openDeleteAdmin = function(id) {
    Admin.CurrentAdminId = id;
}

Admin.openDeleteCategory = function(id) {
    Admin.CurrentCategoryId = id;
}

Admin.openDeletePublisher = function(id) {
    Admin.CurrentPublisherId = id;
}

Admin.openDeleteAuthor = function(id) {
    Admin.CurrentAuthorId = id;
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
        url: '/Admin/GetBook?id=' + id,
        type: 'GET',
        success: function(result) {
            $("#newBookTitle").val(result.title);
            $("#newBookPrice").val(result.price);
            $("#newBookShortDescription").val(result.shortDescription);
            $("#newBookDescription").val(result.description);
            $("#newBookUrl").val(result.bookUrl);
            $("#newBookFeatured").val(result.isFeatured == true ? 1 : 0);

            var authorIds = result.authors.map(a => a.id);
            for (var i = 0; i < authorIds.length; i++) {
                $("#newBookAuthors").find("option[value=" + authorIds[i] + "]").prop("selected", "selected");
            }
           
            var categoriesIds = result.categories.map(a => a.id);
            for (var i = 0; i < categoriesIds.length; i++) {
                $("#newBookCategory").find("option[value=" + categoriesIds[i] + "]").prop("selected", "selected");
            }

            var publishersIds = result.publishers.map(a => a.id);
            for (var i = 0; i < publishersIds.length; i++) {
                $("#newBookPublishers").find("option[value=" + publishersIds[i] + "]").prop("selected", "selected");
            }
        },
        error: function(err) {
            console.log(err);
        }
    });
}

Admin.deleteBook = function() {

    $.ajax({
        url: '/Admin/DeleteBook?id=' + Admin.CurrentBookId,
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
    var publishers = $("#newBookPublishers").val();
    var shortDescription = $("#newBookShortDescription").val();
    var description = $("#newBookDescription").val();
    var url = $("#newBookUrl").val();
    var categories = $("#newBookCategory").val();
    var isFeatured = $("#newBookFeatured").val() == '1';

    if (Admin.IsUpdate) {

        var id = Admin.CurrentBookId;
        var book = {
            Id: id,
            Title: title,
            Price: price,
            AuthorsIds: authors,
            PublishersIds: publishers,
            CategoriesIds: categories,
            ShortDescription: shortDescription,
            Description: description,
            BookUrl: url,
            IsFeatured: isFeatured
        };

        $.ajax({
            url: '/Admin/UpdateBook',
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
            AuthorsIds: authors,
            PublishersIds: publishers,
            CategoriesIds: categories,
            ShortDescription: shortDescription,
            Description: description,
            BookUrl: url,
            IsFeatured: isFeatured
        };

        $.ajax({
            url: '/Admin/AddBook',
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

Admin.addNewCategory = function() {

    var name = $("#newCategoryName").val();

    var model = {
        Name: name
    };

    $.ajax({
        url: '/Admin/CreateCategory',
        data: model,
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

Admin.addNewAdmin = function() {

    var email = $("#newAdminEmail").val();
    var firstName = $("#newAdminFirstName").val();
    var lastName = $("#newAdminLastName").val();

    var model = {
        Email: email,
        FirstName: firstName,
        LastName: lastName
    };

    $.ajax({
        url: '/Admin/AddAdmin',
        data: model,
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

Admin.deleteCategory = function() {

    $.ajax({
        url: '/Admin/DeleteCategory?id=' + Admin.CurrentCategoryId,
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

Admin.deleteAdmin = function() {

    $.ajax({
        url: '/Admin/DeleteAdmin?id=' + Admin.CurrentAdminId,
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

Admin.deletePublisher = function() {

    $.ajax({
        url: '/Admin/DeletePublisher?id=' + Admin.CurrentPublisherId,
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

Admin.deleteAuthor = function() {

    $.ajax({
        url: '/Admin/DeleteAuthor?id=' + Admin.CurrentAuthorId,
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

Admin.addNewPublisher = function() {

    var name = $("#newPublisherName").val();
    var date = $("#newPublisherDate").val();
    var address = $("#newPublisherAddress").val();
    var city = $("#newPublisherCity").val();
    var country = $("#newPublisherCountry").val();

    var model = {
        Name: name,
        DateEstablished: date,
        Address: address,
        City: city,
        Country: country
    };

    $.ajax({
        url: '/Admin/AddPublisher',
        data: model,
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

Admin.addNewAuthor = function() {

    var name = $("#newAuthorName").val();
    var birthDate = $("#newAuthorBirthDate").val();
    var biography = $("#newAuthorBiography").val();

    var model = {
        Name: name,
        BirthDate: birthDate,
        Biography: biography
    };

    $.ajax({
        url: '/Admin/AddAuthor',
        data: model,
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