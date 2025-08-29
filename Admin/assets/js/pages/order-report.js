$(document).ready(function () {
    BindReports();
    $(document.body).on('click', ".pVClick", function () {
        var ele = $(this);
        $(".vPagination a").removeClass("active");
        ele.addClass("active");
        BindReports();
    });
    $(document.body).on('click', ".prVClick", function () {
        var ele = $(this);
        $(".vPagination a").removeClass("active");
        ele.addClass("active");
        BindReports();
    });
    $(document.body).on('click', ".nxVClick", function () {
        var ele = $(this);
        $(".vPagination a").removeClass("active");
        ele.addClass("active");
        BindReports();
    });
    $(document.body).on('click', '.btnSearch', function () {
        $(".vPagination a").removeClass("active");
        BindReports();
    });
    $(document.body).on('change', '#ddlPageSize', function () {
        $(".vPagination a").removeClass("active");
        BindReports();
    });
    $(document.body).on('click', '.dispatchItem', function () {
        var elem = $(this);
        var OrderGuid = elem.attr('data-id');
        var currentStatus = $('#o_r_' + OrderGuid).text();
        $("#btnDispatch").attr("data-id", OrderGuid);

        if (currentStatus != "") {
            if (currentStatus == "Delivered") {
                Snackbar.show({ pos: 'top-right', text: 'Order already delivered!', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                return false;
            }
            if (currentStatus == "Dispatched") {
                Snackbar.show({ pos: 'top-right', text: 'Order has been already dispatched!', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                return false;
            } if (currentStatus == "Cancelled") {

                Snackbar.show({ pos: 'top-right', text: 'Order is already cancelled!', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                return false;
            }
        }

        $("#txtCourierName").val("");
        $("#txtTrackingCode").val("");
        $("#txtTrackingLink").val("");
        $("#txtDate").val("");

        $('#DispatchModal').modal('show');
    });

    $(document.body).on('click', '.UpdatePayment', function () {
        var elem = $(this);
        var OrderGuid = elem.attr('data-id');
        var currentStatus = $('#o_r_' + OrderGuid).text();
        $("#btnPayStatus").attr("data-id", OrderGuid);
        $("#txtPayId").val("");
        $('#PaymentModel').modal('show');
    });

    $(document.body).on("click", "#btnDispatch", function () {
        DispatchOrder($(this));
    });
    $(document.body).on('click', '.deliverItem', function () {
        DeliveryOrder($(this));
    });
    $(document.body).on('click', '.cancelItem', function () {
        var elem = $(this);
        var OrderGuid = elem.attr('data-id');
        var currentStatus = $('#o_r_' + OrderGuid).text();
        $("#btnCancel").attr("data-id", OrderGuid);

        if (currentStatus != "") {
            if (currentStatus == "Delivered") {
                Snackbar.show({ pos: 'top-right', text: 'Order already delivered!', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                return false;
            }
            if (currentStatus == "Cancelled") {
                Snackbar.show({ pos: 'top-right', text: 'Order is already cancelled!', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                return false;
            }
            else {
                $('#CancelModal').modal('show');
            }
        }
    });

    


    $(document.body).on("click", "#btnCancel", function () {
        CancleOrder($(this));
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
                    url: "order-report.aspx/Delete",
                    data: "{id: '" + id + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: false,
                    success: function (data2) {
                        if (data2.d.toString() == "Success") {
                            Swal.fire({ title: "Deleted!", text: "Order has been deleted.", icon: "success", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: false })
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

    //view shipping info
    $(document.body).on('click', '.ViewDeliverAddress', function () {
        var elem = $(this);
        var id = elem.attr('data-id');
        var data = { uGuid: id }
        $.ajax({
            type: 'POST',
            url: "order-report.aspx/BindAddress",
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            async: false,
            success: function (data2) {
                var address = data2.d;
                if (address.length > 0) {

                    $('#txtContactPersonName').text((address[0].FirstName || "N/A") + " " + (address[0].LastName || "N/A"));
                    $('#txtEmail').text(address[0].EmailId || "N/A");
                    $('#txtMobile').text(address[0].Mobile || "N/A");
                    $('#txtAddressLine1').text(address[0].Address1 || "N/A");
                    $('#txtAddressLine2').text(address[0].Address2 || "N/A");
                    $('#txtCity').text(address[0].City || "N/A");
                    $('#txtState').text(address[0].State || "N/A");
                    $('#txtZip').text(address[0].Zip || "N/A");

                    $('#shippingModal').modal('show');
                }
                else if (data2.d == "Permission") {
                    Snackbar.show({ pos: 'top-right', text: 'Access denied. Contact to your administrator', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                }
                else {
                    Snackbar.show({ pos: 'top-right', text: 'Oops! There is some problem now. Pleae try after some time.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                }
            }
        });
    });
    $(document.body).on('click', '.ViewShippingDetails', function () {
        var elem = $(this);
        var id = elem.attr('data-id');
        var name = elem.attr('data-Name') || "N/A";
        var code = elem.attr('data-Code') || "N/A";
        var link = elem.attr('data-Link') || "#";
        var date = elem.attr('data-date') || "#";

        $("#CourierName").text(name);
        $("#TrackingCode").text(code);
        $("#deliveryDate").text(date);

        if (link === "N/A" || link === "#") {
            $("#TrackingLink").html("N/A");
        } else {
            $("#TrackingLink").html("<a href='" + link + "' target='_blank'>" + link + "</a>");
        }
        $('#ShippingDetails').modal('show');
    });

    $(document.body).on("click", "#btnPayStatus", function () {
        PaymentStatusUpdate($(this));
    });

});
function CancleOrder($this) {

    var elem = $this;
    var OrderGuid = $this.attr("data-id");
    var remarks = $("#txtRemarks").val();
    var remarksVisible = $("#ShowRemarks").is(":checked") ? "Yes" : "No";
    var oStatus = "Cancelled";

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: !0,
        confirmButtonClass: "btn btn-primary w-xs me-2 mt-2",
        cancelButtonClass: "btn btn-danger w-xs mt-2",
        confirmButtonText: "Yes, cancel it!",
        buttonsStyling: !1,
        showCloseButton: !0,
    }).then(function (result) {
        if (result.value) {
            $.ajax({
                type: 'POST',
                url: "order-report.aspx/CancelOrder",
                data: "{OrderGuid: '" + OrderGuid + "',remarks:'" + remarks + "', remarksVisible:'" + remarksVisible + "', oStatus:'" + oStatus + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                async: false,
                success: function (data2) {
                    if (data2.d.toString() == "Success") {
                        var orderStatusBadge = "<a href='javascript:void(0);' data-id='" + OrderGuid + "' data-sts='" + oStatus + "' class='badge badge-outline-danger'>" + oStatus + "</a>";

                        Swal.fire({ title: "Cancelled!", text: "Order has been cancelled.", icon: "success", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: false })

                        $("#o_r_" + OrderGuid).html(orderStatusBadge);

                        $("#txtRemarks").val("");
                        $('#CancelModal').modal('hide');
                    }
                    else if (data2.d.toString() == "Permission") {
                        Swal.fire({ title: "Oops...", text: "Permission denied! Please contact to your administrator.", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                    } else if (data2.d.toString() == "Pending") {

                        Swal.fire({ title: "Oops...", text: "Oops! To Remove Sponser the Payout should be in Pending Stage.", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                    }
                    else {
                        Swal.fire({ title: "Oops...", text: "Something went wrong!", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                    }
                }
            });
        }
    })
}
function DeliveryOrder($this) {
    var elem = $this;
    var OrderGuid = elem.attr('data-id');
    var currentStatus = $('#o_r_' + OrderGuid).text();

    if (currentStatus != "") {
        if (currentStatus == "Delivered") {
            Snackbar.show({ pos: 'top-right', text: 'Order already delivered!', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
            return false;
        }
        else if(currentStatus == "Initiated") {
            Snackbar.show({ pos: 'top-right', text: 'Dispatched Order to proceed!', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
            return false;
        } else if (currentStatus == "Cancelled") {
            Snackbar.show({ pos: 'top-right', text: 'Order is already cancelled!', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
            return false;
        }
        else if (currentStatus == "In-Process") {
            Snackbar.show({ pos: 'top-right', text: 'Dispatched Order to proceed!', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
            return false;
        }
    }

    var oStatus = "Delivered";

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: !0,
        confirmButtonClass: "btn btn-primary w-xs me-2 mt-2",
        cancelButtonClass: "btn btn-danger w-xs mt-2",
        confirmButtonText: "Yes, update it!",
        buttonsStyling: !1,
        showCloseButton: !0,
    }).then(function (result) {
        if (result.value) {
            $.ajax({
                type: 'POST',
                url: "order-report.aspx/UpdateOrderStatusDelivered",
                data: "{OrderGuid: '" + OrderGuid + "', oStatus: '" + oStatus + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                async: false,
                success: function (data2) {
                    if (data2.d.toString() == "Success") {
                        var orderStatusBadge = "<a href='javascript:void(0);' data-id='" + OrderGuid + "' data-sts='" + oStatus + "' class='badge badge-outline-success'>" + oStatus + "</a>";

                        Swal.fire({ title: "Delivered!", text: "Order has been delivered.", icon: "success", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: false })

                        $("#o_r_" + OrderGuid).html(orderStatusBadge);
                    }
                    else if (data2.d.toString() == "Permission") {
                        Swal.fire({ title: "Oops...", text: "Permission denied! Please contact to your administrator.", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                    } else if (data2.d.toString() == "Pending") {

                        Swal.fire({ title: "Oops...", text: "Oops! To Remove Sponser the Payout should be in Pending Stage.", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                    }
                    else {
                        Swal.fire({ title: "Oops...", text: "Something went wrong!", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                    }
                }
            });

        }
    })
}

function PaymentStatusUpdate($this) {
    var OrderGuid = $this.attr("data-id");
    var payId = $("#txtPayId").val();
    var isValid = true;

    $("#txtPayId").parent().find(".error").html("");
    var payId = $("#txtPayId").val();
    if (payId === "") {
        isValid = false;
        $("#txtPayId").parent().find(".error").html("Field can't be empty");
    }

    if (isValid) {
        var _data = { OrderGuid: OrderGuid, payId: payId };
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn btn-primary w-xs me-2 mt-2",
            cancelButtonClass: "btn btn-danger w-xs mt-2",
            confirmButtonText: "Yes, Update it!",
            buttonsStyling: false,
            showCloseButton: true,
        }).then(function (result) {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: "order-report.aspx/UpdatePaymentStatus",
                    data: JSON.stringify(_data),
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        if (response.d === "Success") {
                            Swal.fire({
                                title: "Updated!",
                                text: "Payment Status has been Updated.",
                                icon: "success",
                                confirmButtonClass: "btn btn-primary w-xs mt-2",
                                buttonsStyling: false
                            });
                            $("#txtPayId").val("");
                            $('#PaymentModel').modal('hide');
                            BindReports();
                        }
                        else if (response.d.toString() == "Permission") {
                            Swal.fire({ title: "Oops...", text: "Permission denied! Please contact to your administrator.", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                        }
                        else {
                            Swal.fire({ title: "Oops...", text: "Something went wrong!", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                        }
                    }
                });
            } else {
            }
        });
    }
}

function DispatchOrder($this) {
    var currentData = $this.parent().html();
    var $thisParent = $this.parent();

    $thisParent.html('<div class="middle-dot"></div> <div class="middle-dot dot-2"></div> <button class="ht-btn ht-btn-md btnsubmit" type="submit">Sending...</button>');

    var OrderGuid = $this.attr("data-id");
    var courierName = $("#txtCourierName").val();
    var trackingCode = $("#txtTrackingCode").val();
    var trackingLink = $("#txtTrackingLink").val();
    var DelDate = $("#txtDate").val();
    var oStatus = "Dispatched";

    var isValid = true;

    $("#txtCourierName").parent().find(".error").html("");
    $("#txtTrackingCode").parent().find(".error").html("");
    $("#txtTrackingLink").parent().find(".error").html("");
    $("#txtDate").parent().find(".error").html("");

    if (courierName === "") {
        isValid = false;
        $("#txtCourierName").parent().find(".error").html("Field can't be empty");
    }
    if (trackingCode === "") {
        isValid = false;
        $("#txtTrackingCode").parent().find(".error").html("Field can't be empty");
    }
    if (trackingLink === "") {
        isValid = false;
        $("#txtTrackingLink").parent().find(".error").html("Field can't be empty");
    }
    if (DelDate === "") {
        isValid = false;
        $("#txtDate").parent().find(".error").html("Field can't be empty");
    }

    if (isValid) {
        var _data = { OrderGuid: OrderGuid, courierName: courierName, trackingCode: trackingCode, trackingLink: trackingLink, oStatus: oStatus, DelDate: DelDate };

        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn btn-primary w-xs me-2 mt-2",
            cancelButtonClass: "btn btn-danger w-xs mt-2",
            confirmButtonText: "Yes, Update it!",
            buttonsStyling: false,
            showCloseButton: true,
        }).then(function (result) {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: "order-report.aspx/DispatchOrder",
                    data: JSON.stringify(_data),
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        if (response.d === "Success") {
                            var orderStatusBadge = "<a href='javascript:void(0);' data-id='" + OrderGuid + "' data-sts='" + oStatus + "' class='badge badge-outline-primary'>" + oStatus + "</a>";
                            Swal.fire({
                                title: "Updated!",
                                text: "Order has been Updated.",
                                icon: "success",
                                confirmButtonClass: "btn btn-primary w-xs mt-2",
                                buttonsStyling: false
                            });

                            $thisParent.html(currentData);
                            $("#o_r_" + OrderGuid).html(orderStatusBadge);

                            $("#txtCourierName").val("");
                            $("#txtTrackingCode").val("");
                            $("#txtTrackingLink").val("");
                            $("#txtDate").val("");

                            $('#DispatchModal').modal('hide');
                        }
                        else if (response.d === "Permission") {
                            $thisParent.html(currentData);
                            Swal.fire({ title: "Oops...", text: "Permission denied! Please contact to your administrator.", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                        }
                        else {
                            $thisParent.html(currentData);
                            Swal.fire({ title: "Oops...", text: "Something went wrong!", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                        }
                    }
                });
            } else {
                $thisParent.html(currentData);
            }
        });
    }
}
function BindLPage(pageS, cPage, pCount) {
    var noOfPagesCreated = ~~(parseFloat(pCount) / parseInt(pageS));
    var isExtra = (parseFloat(pCount) % parseInt(pageS)) === 0 ? 0 : 1;

    noOfPagesCreated = noOfPagesCreated + isExtra;

    $(".vPagination").empty();

    var pagesss = "";

    var np = parseInt(cPage) % 5 === 0 ? (parseInt(cPage) / parseInt(5) - 1) : parseInt(cPage) / parseInt(5);
    var nearestNextP = (~~np + 1) * 5;
    var pLength = noOfPagesCreated < parseInt(nearestNextP) ? noOfPagesCreated : parseInt(nearestNextP);
    var startPage = (parseInt(nearestNextP) - 4);

    if (parseInt(cPage) > 5) {
        pagesss += "<li class='page-item'><a class='page-link pVClick' href='javascript:void(0);' id='pno_1'>1</a></li>";
        pagesss += "<li class='page-item'><a class='page-link pVClick' href='javascript:void(0);' id='pno_1'>...</a></li>";
    }

    for (var i = startPage; i <= pLength; i++) {
        var act = i === parseInt(cPage) ? "active" : "";
        pagesss += "<li class='page-item'><a class='page-link pVClick " + act + "' href='javascript:void(0);' id='pno_" + (i) + "'>" + (i) + "</a></li>";
    }
    if (noOfPagesCreated > pLength) {
        pagesss += "<li class='page-item'><a class='page-link pVClick' href='javascript:void(0);' id='pno_" + (pLength + 1) + "'>...</a></li>";
        pagesss += "<li class='page-item'><a class='page-link pVClick' href='javascript:void(0);' id='pno_" + (noOfPagesCreated) + "'>" + (noOfPagesCreated) + "</a></li>";
    }



    var prvPg = startPage === 1 ? 1 : startPage - 1;
    var nxtPg = noOfPagesCreated > pLength ? (pLength + 1) : pLength;
    var pgnPrev = "";
    if (parseInt(cPage) > 1) {
        pgnPrev = "<li class='page-item'><a id='pnon_" + prvPg + "' class='page-link prVClick' href='javascript:void(0);' aria-label='Previous'><i class='mdi mdi-chevron-left'></i> Previous</a></li>";
    }
    else {
        pgnPrev = "<li class='page-item'><a id='pnon_" + prvPg + "' class='page-link disabled' href='javascript:void(0);' aria-label='Previous'><i class='mdi mdi-chevron-left'></i> Previous</a></li>";
    }

    var pgnNext = "";

    if (nxtPg != parseInt(cPage)) {
        pgnNext = "<li class='page-item'><a class='page-link nxVClick' href='javascript:void(0);' id='pnon_" + nxtPg + "' aria-label='Next'>Next <i class='mdi mdi-chevron-right'></i></a></li>";
    }
    else {
        pgnNext = "<li class='page-item'><a class='page-link disabled' href='javascript:void(0);' id='pnon_" + nxtPg + "' aria-label='Next'>Next <i class='mdi mdi-chevron-right'></i></a></li>";
    }

    $(".vPagination").append(pgnPrev + pagesss + pgnNext);


}
function BindReports() {
    $("#tblBody").empty();
    $("#showDetails").html("");
    AddKeyFrames();

    var ddlDay = $("[id*=ddlDay]").val() == undefined ? "" : $("[id*=ddlDay]").val();
    var fromDate = $("[id*=txtFrom]").val();
    var toDate = $("[id*=txtTo]").val();
    var oStatus = $("[id*=ddlStatus]").val();
    var pStatus = $("[id*=ddlPayStatus]").val();
    var oParam = $("[id*=txtSearch]").val();

    var pSize = $("#ddlPageSize").val() == "" ? "" : $("#ddlPageSize").val();

    var pno = "1";
    if ($(".vPagination a").hasClass("active")) {
        pno = $(".vPagination .active").attr('id').split('_')[1];
    }
    $(".vPagination").empty();
    var dataS = { pNo: pno, pSize: pSize, fromDate: fromDate, toDate: toDate, ddlDay: ddlDay, oStatus: oStatus, pStatus: pStatus, oParam: oParam }
    $.ajax({
        type: 'POST',
        url: "order-report.aspx/BindReports",
        data: JSON.stringify(dataS),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        async: true,
        success: function (data2) {
            var dataVal = data2.d;
            if (dataVal != null) {
                if (dataVal.Status == "Success") {
                    RemoveKeyFrames();
                    $("#showDetails").html("Showing " + (((parseInt(pno) - 1) * parseInt(pSize)) + 1) + " to " + (parseInt(pno) * parseInt(pSize)) + " of " + dataVal.TotalCount + " entries");
                    $("#tblBody").html(dataVal.LineItems);
                    $(".bs-tooltip").tooltip();
                    BindLPage(pSize, pno, dataVal.TotalCount)
                }
                else {
                    RemoveKeyFrames();
                    Snackbar.show({ pos: 'top-right', text: '' + dataVal.StatusMessage + '', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                }
            }
            else {
                RemoveKeyFrames();
            }
        }
    });
}
function AddKeyFrames() {
    $("#tblBodyLoadingFrame").empty();
    var listings_ = "";
    for (var i = 0; i < 1; i++) {
        listings_ += "<tr><td colspan='13' class='text-center'><div class='spinner-grow text-primary' role='status'><span class='sr-only'>Loading...</span></div><div class='spinner-grow text-secondary' role='status'><span class='sr-only'>Loading...</span></div><div class='spinner-grow text-success' role='status'><span class='sr-only'>Loading...</span></div><div class='spinner-grow text-info' role='status'><span class='sr-only'>Loading...</span></div><div class='spinner-grow text-warning' role='status'><span class='sr-only'>Loading...</span></div><div class='spinner-grow text-danger' role='status'><span class='sr-only'>Loading...</span></div><div class='spinner-grow text-dark' role='status'><span class='sr-only'>Loading...</span></div><div class='spinner-grow text-light' role='status'><span class='sr-only'>Loading...</span></div></td></tr>";
    }
    $("#tblBodyLoadingFrame").append(listings_);

};
function RemoveKeyFrames() {
    $("#tblBodyLoadingFrame").empty();
};





//$(document.body).on('click', '.DeliveryDate', function () {
//    var elem = $(this);
//    var OrderGuid = elem.attr('data-id');
//    var DelDate = elem.attr('data-date');
//    var currentStatus = elem.attr('data-status');
//    $("#btnDelDate").attr("data-id", OrderGuid);
//    if (currentStatus != "") {
//        if (currentStatus == "Delivered") {
//            Snackbar.show({ pos: 'top-right', text: 'Order already delivered!', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
//            return false;
//        }
//       if (currentStatus == "Cancelled") {
//            Snackbar.show({ pos: 'top-right', text: 'Order is already cancelled!', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
//            return false;
//        }
//        if (DelDate != "") {
//            var texts = "Delivery Date is " + DelDate;
//            Snackbar.show({ pos: 'top-right', text: texts, actionTextColor: '#fff', backgroundColor: '#008a3d' });
//            return false;
//        }else {
//            $("#txtDate").val("");
//            $('#DeliveryModal').modal('show');
//        }
//    }

//});
//$(document.body).on("click", "#btnDelDate", function () {
//    DeliverDateUpdate($(this));
//});