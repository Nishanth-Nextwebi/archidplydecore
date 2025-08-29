$(document).ready(function () {
    PopulateCategory();
    var product = "";
    $(document.body).on("change", "[id*=ddlCategory]", function () {
        PopulateCategory();
    })
    $("#Update").click(function (e) {
        e.preventDefault();
        $.each($('#left-defaults').find('li'), function () {
            if (product == "") {
                product = product + $(this).attr("data-id");
            }
            else {
                product = product + "," + $(this).attr("data-id");
            }
        });

        ArrengeCategory(product);
    });
});
function ArrengeCategory(product) {

    var parameter = { "product": product };
    $.ajax({
        type: 'POST',
        url: "product-order.aspx/ProductOrderUpdate",
        data: JSON.stringify(parameter),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (data2) {
            if (data2.d.toString() == "Success") {
                Snackbar.show({ pos: 'top-right', text: 'Product arranged successfully!', actionTextColor: '#fff', backgroundColor: '#008a3d' });
                PopulateCategory();

            }
            else if (data2.d.toString() == "Permission") {
                Snackbar.show({ pos: 'top-right', text: 'Oops! Access denied. Contact to your administrator', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
            }
            else {
                Snackbar.show({ pos: 'top-right', text: 'Oops! Something went wrong. Please try after some time.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
            }
        },
        error: function (error) {
            Snackbar.show({ pos: 'top-right', text: 'Oops! Something went wrong. Please try after some time.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
        }

    });
}
function PopulateCategory() {
    var category = $("[id*=ddlCategory]").val();
    var parameter = { "category": category };
    $.ajax({
        type: 'POST',
        url: "product-order.aspx/ProductOrder",
        data: JSON.stringify(parameter),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        async: true, // Setting to asynchronous (default behavior)
        success: function (data) {
            var product = data.d;
            var str = "";
            if (product.length > 0) {
                for (var i = 0; i < product.length; i++) {
                    str = str + "<li data-id='" + data.d[i].Id + "' class='ui-state-default media d-md-flex d-block text-sm-left text-center'>" +
                        "<span class='DisplayOrderNumber'>" + (i + 1) + "</span>" +
                        "<span class='ProductName'>" + product[i].ProductName + "</span></li>";
                }
            } else {
                str = "<span>No products available</span>";
            }
            $("#left-defaults").html(str);
        },
        error: function (error) {
            console.error("Error details:", error);
            Snackbar.show({
                pos: 'top-right',
                text: 'Oops! Something went wrong. Please try after some time.',
                actionTextColor: '#fff',
                backgroundColor: '#ea1c1c'
            });
        }
    });
}



































/*$(document).ready(function () {
    PopulateCategory();
    var product = "";
    $("#Update").click(function (e) {
        var elem = $(this);
        elem.val("Please wait...");
        e.preventDefault();
        product = "";
        $.each($('#left-defaults').find('li'), function () {
            if (product == "") {
                product = product + $(this).attr("data-id");
            }
            else {
                product = product + "," + $(this).attr("data-id");
            }
        });
        ArrengeCategory(product);
    });

    function ArrengeCategory(product) {
        var parameter = { "product": product };
        $.ajax({
            type: 'POST',
            url: "product-order.aspx/ProductOrderUpdate",
            data: JSON.stringify(parameter),
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            success: function (data) {
                if (data.d == 0) {
                    swal.fire(
                        'Success',
                        'Product Order Arranged Successfully',
                        'success'
                    );
                    PopulateCategory();
                }
                else if (data.d == 403) {
                    swal.fire(
                        'Error !',
                        'Permission denied. Please contact to your administrator',
                        'error'
                    );
                }
                else {
                    swal.fire(
                        'Error !',
                        'There is some problem now. Please try after sometime.',
                        'error'
                    );
                }
                $("#Update").val("Update Order");
            },
            error: function (error) {
                swal.fire(
                    'Error !',
                    'error; ' + eval(error),
                    'error'
                );
                $("#Update").val("Update Order");
            }
        });
    }
    function PopulateCategory() {
        $.ajax({
            type: 'POST',
            url: "product-order.aspx/ProductOrder",
            data: "{}",
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d != null) {
                    var str = "";
                    for (var i = 0; i < data.d.length; i++) {
                        str = str + "<li data-id='" + data.d[i].Id + "' class='ui-state-default media  d-md-flex d-block text-sm-left text-center col-md-2'><div class='maindiv'><img src='/" + data.d[i].ProductImage + "' alt='" + data.d[i].ProductName + "' class='img-responsive' style='max-width:100%;' /><div><span>" + data.d[i].ProductName + "</span></div></div></li>";
                    }
                    $("#left-defaults").html(str);
                }
            },
            error: function (error) {
                swal.fire(
                    'Error !',
                    'error; ' + eval(error),
                    'error'
                );
            }
        });
    }
});*/