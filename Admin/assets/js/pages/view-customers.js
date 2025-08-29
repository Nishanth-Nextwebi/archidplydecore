$(document).ready(function () {
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
                    url: "view-customers.aspx/Delete",
                    data: "{id: '" + id + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: false,
                    success: function (data2) {
                        if (data2.d.toString() == "Success") {
                            Swal.fire({ title: "Deleted!", text: "Customer has been deleted.", icon: "success", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: false })
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
    $(document.body).on("click", ".blockItem", function () {
        var id = $(this).attr('data-id');
        var guid = $(this).attr('data-guid');
        var ftr = $(this).prop("checked") ? "Yes" : "No";
        $.ajax({
            type: 'POST',
            url: "view-customers.aspx/VerifyCustomers",
            data: "{id: '" + id + "',guid:'" + guid + "',ftr: '" + ftr + "'}",
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            async: false,
            success: function (data2) {
                if (data2.d.toString() == "Success") {
                    if (ftr === "Yes") {
                        $("#sts_" + id).removeAttr("class");
                        $("#sts_" + id).attr("class", "badge badge-outline-danger shadow fs-13");
                        $("#sts_" + id).text("Blocked");
                        Snackbar.show({ pos: 'top-right', text: 'Member Blocked successfully.', actionTextColor: '#fff', backgroundColor: '#008a3d' });
                    }
                    else {
                        $("#sts_" + id).text("Verified");
                        $("#sts_" + id).removeAttr("class");
                        $("#sts_" + id).attr("class", "badge badge-outline-success fs-13 shadow");
                        Snackbar.show({ pos: 'top-right', text: 'Member Unblocked successfully.', actionTextColor: '#fff', backgroundColor: '#008a3d' });
                    }
                }
                else {
                    Snackbar.show({ pos: 'top-right', text: 'Oops!!! There is some error right now, please try again after some time.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });

                }
            },
            error: function (err) {
                Snackbar.show({ pos: 'top-right', text: 'Oops!!! There is some error right now, please try again after some time.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
            }
        });
    });

});