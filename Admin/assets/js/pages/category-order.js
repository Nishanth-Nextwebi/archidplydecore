$(document).ready(function () {
    PopulateCategory();
    var category = "";
    $("#Update").click(function (e) {
        var elem = $(this);
        elem.val("Please wait...");
        e.preventDefault();
        category = "";
        $.each($('#left-defaults').find('li'), function () {
            if (category == "") {
                category = category + $(this).attr("data-id");
            }
            else {
                category = category + "," + $(this).attr("data-id");
            }
        });
        ArrengeCategory(category);
    });
    function ArrengeCategory(category) {
        var parameter = { "category": category };
        $.ajax({
            type: 'POST',
            url: "category-order.aspx/CategoryOrderUpdate",
            data: JSON.stringify(parameter),
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            success: function (data) {
                if (data.d == 0) {
                    Snackbar.show({ pos: 'top-right', text: 'Category arranged successfully!', actionTextColor: '#fff', backgroundColor: '#008a3d' });

                    PopulateCategory();
                }
                else if (data.d == 403) {
                    Snackbar.show({ pos: 'top-right', text: 'Permission denied. Please contact to your administrator.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                }
                else {
                    Snackbar.show({ pos: 'top-right', text: 'Oops! Something went wrong. Please try after some time.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
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
        //var category = $(".ddlCat").val();
        //var parameter = { "category": category };
        $.ajax({
            type: 'POST',
            url: "category-order.aspx/CategoryOrder",
            data: "{}",
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d != null) {
                    var str = "";
                    for (var i = 0; i < data.d.length; i++) {
                        str = str + "<li data-id='" + data.d[i].Id + "' class='ui-state-default media  d-md-flex d-block text-sm-left text-center col-md-2'><div class='maindiv'><div class='d-flex'><span class='DisplayOrderNumber'>" + (i + 1) + "</span><span>" + data.d[i].CategoryName + "</span></div></div></li>";
                    }
                    $("#left-defaults").html(str);
                }
            },
            error: function (error) {

                Snackbar.show({ pos: 'top-right', text: 'Oops! Something went wrong. Please try after some time.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });

            }
        });
    }
});