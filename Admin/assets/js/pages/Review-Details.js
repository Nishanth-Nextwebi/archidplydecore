$(document).ready(function () {

    $(document.body).on('click', '.btnmsg', function () {
        $("#Contactinfosingle").empty();
        var elem = $(this);
        var id = elem.attr('data-id');
        var name = elem.attr('data-name');
        $('.modal-header .modal-title').html('Message Information - ' + name);
        $.ajax({
            type: 'POST',
            url: "Review-Details.aspx/GetMessage",
            data: "{id: '" + id + "'}",
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            async: false,
            success: function (res) {
                var Message = res.d;
                if (Message) {
                    var tableinfo = "<table class='table'><tbody>";
                    tableinfo += "<tr><td>" + Message + "</td></tr>";
                    tableinfo += "</tbody></table>";
                    $("#Contactinfosingle").append(tableinfo);
                } else {
                    $("#Contactinfosingle").html("No message available.");
                }
            }
        });
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
                    url: "Review-Details.aspx/UpdateReview",
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
    $('.btn-approve').on('click', function () {
        var id = $(this).attr('data-id');
        Update(id, "Approve");
    });
    $('.btn-reject').on('click', function () {
        var id = $(this).attr('data-id');
        Update(id, "Reject");
    });
    $('.ReviewFeatured').on('change', function () {
        var id = $(this).attr('data-id');
        var ftr = $(this).prop("checked") ? "Yes" : "No";
        $.ajax({
            type: 'POST',
            url: "Review-Details.aspx/ReviewFeatured",
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
                    if (ftr == "Yes") {
                        Snackbar.show({
                            pos: 'top-right',
                            text: 'Review featured successfully',
                            actionTextColor: '#fff',
                            backgroundColor: '#008a3d'
                        });
                    } else if (ftr == "No") {
                        Snackbar.show({
                            pos: 'top-right',
                            text: 'Review excluded successfully',
                            actionTextColor: '#fff',
                            backgroundColor: '#008a3d'
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
    function Update(id, sts) {
        const toast = swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 3000,
            padding: '2em'
        });
        $.ajax({
            type: 'POST',
            url: "Review-Details.aspx/UpdateReview",
            data: "{id: '" + id + "',sts:'" + sts + "'}",
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            async: false,
            success: function (data2) {
                if (data2.d.toString() == "S") {
                    if (sts === "Approve") {
                        Snackbar.show({
                            pos: 'top-right',
                            text: 'Review Approved',
                            actionTextColor: '#fff',
                            backgroundColor: '#008a3d'
                        });
                        $("#sts_" + id).attr("class", "");
                        $("#sts_" + id).attr("class", "badge badge-outline-success shadow fs-13");
                        $("#sts_" + id).text("Approved");
                    }
                    else if (sts === "Reject") {
                        Snackbar.show({
                            pos: 'top-right',
                            text: 'Review Rejected',
                            actionTextColor: '#fff',
                            backgroundColor: '#008a3d'
                        });
                        $("#sts_" + id).attr("class", "");
                        $("#sts_" + id).attr("class", "badge badge-outline-danger shadow fs-13");
                        $("#sts_" + id).text("Rejected");
                    }
                }
                else if (data2.d.toString() == "P") {
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
            error: function (res) {
                swal.fire(
                    'Error !',
                    'There is some problem now.',
                    'error'
                );
            }
        });
    }
});