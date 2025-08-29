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
                    url: "view-blogs.aspx/Delete",
                    data: "{id: '" + id + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: false,
                    success: function (data2) {
                        if (data2.d.toString() == "Success") {
                            Swal.fire({ title: "Deleted!", text: "Blog has been deleted.", icon: "success", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: false })
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
    $('.PublishBlog').on('change', function () {
        var id = $(this).attr('data-id');
        var ftr = $(this).prop("checked") ? "Yes" : "No";
        $.ajax({
            type: 'POST',
            url: "view-blogs.aspx/PublishBlog",
            data: "{id: '" + id + "',ftr: '" + ftr + "'}",
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            async: false,
            success: function (data2) {
                if (data2.d.toString() == "Success") {
                    const toast = swal.mixin({
                        toast: true,
                        position: 'top-end',
                        showConfirmButton: false,
                        timer: 3000,
                        padding: '2em'
                    });

                    if (ftr === "Yes") {
                        $("#sts_" + id).removeAttr("class");
                        $("#sts_" + id).attr("class", "badge badge-outline-success shadow fs-13");
                        $("#sts_" + id).text("Published");
                        Snackbar.show({
                            pos: 'top-right',
                            text: 'Blog published successfully',
                            actionTextColor: '#fff',
                            backgroundColor: '#008a3d'
                        });

                    }
                    else {
                        $("#sts_" + id).text("Draft");
                        $("#sts_" + id).removeAttr("class");
                        $("#sts_" + id).attr("class", "badge badge-outline-warning shadow fs-13");
                        Snackbar.show({
                            pos: 'top-right',
                            text: 'Blog Drafted successfully',
                            actionTextColor: '#fff',
                            backgroundColor: '#ffbe0b'
                        });
                    }
                }
                else if (data2.d.toString() == "Permission") {
                    swal.fire(
                        'Error !',
                        'Permission denied. Please contact to your administrator',
                        'error'
                    );
                }
                else {
                    swal.fire(
                        'Error !',
                        'There is some problem now.',
                        'error'
                    );
                }
            },
            error: function (err) {
                swal.fire(
                    'Error !',
                    'There is some problem now. Please try after sometime.',
                    'error'
                );
            }
        });
    });
});