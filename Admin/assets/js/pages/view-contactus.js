$(document).ready(function () {

    $(".textcount1").on('keyup', function (event) {
        var elem = $(this);
        var tps = elem.attr("data-id");
        var len = elem.val().length;
        elem.siblings('span').text("Character count : " + len);
        if (tps === "Title") {
            if (len > 60) {
                elem.siblings('span').css("color", "red");
            }
            else {
                elem.siblings('span').css("color", "green");
            }
        }
        else if (tps === "MetaDesc") {
            if (len > 160) {
                elem.siblings('span').css("color", "red");
            }
            else {
                elem.siblings('span').css("color", "green");
            }
        }
    });

    $(document.body).on('click', '.btnmsg', function () {
        $("#Contactinfosingle").empty();
        var elem = $(this);
        var id = elem.attr('data-id');
        var name = elem.attr('data-name');
        $('.modal-header .modal-title').html('Message Information - ' + name);
        $.ajax({
            type: 'POST',
            url: "view-contactus.aspx/GetMessage",
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
                    url: "view-contactus.aspx/Delete",
                    data: "{id: '" + id + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: false,
                    success: function (data2) {
                        if (data2.d.toString() == "Success") {
                            Swal.fire({ title: "Deleted!", text: "Contact has been deleted.", icon: "success", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: false })
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

});