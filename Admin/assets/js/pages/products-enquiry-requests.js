$(document.body).on('click', '.btnmsg', function () {
    $("#Contactinfosingle").empty();
    var elem = $(this);
    var id = elem.attr('data-id');
    var name = elem.attr('data-name');
    var message = elem.attr('data-msg');
    $('.modal-header .modal-title').html('Message Information - ' + name);
    var tableinfo = "<table class='table'><tbody>";
    tableinfo += "<tr><td>" + message + "</td></tr>";
    tableinfo += "</tbody></table>";
    $("#Contactinfosingle").append(tableinfo);
});

$(document.body).on('click', '.deleteItem', function () {
    var elem = $(this);
    var id = elem.attr('data-id');
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: !0,
        confirmButtonClass: "btn btn-primary w-xs me-2 mt-2",
        cancelButtonClass: "btn btn-danger w-xs mt-2",
        confirmButtonText: "Yes, delete it!",
        buttonsStyling: !1,
        showCloseButton: !0,
    }).then(function (result) {
        if (result.value) {
            $.ajax({
                type: 'POST',
                url: "products-enquiries.aspx/Delete",
                data: "{id: '" + id + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                async: false,
                success: function (data2) {
                    if (data2.d.toString() == "Success") {
                        Swal.fire({ title: "Deleted!", text: "Item has been deleted.", icon: "success", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: false })
                        elem.parent().parent().remove();
                    }
                    else if (data2.d.toString() == "Permission") {

                        Swal.fire({ title: "Oops...", text: "Permission denied! Please contact to your administrator.", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                    }
                    else {
                        Swal.fire({ title: "Oops...", text: "Something went wrong!", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                    }
                }
            });

        }
    })
});
