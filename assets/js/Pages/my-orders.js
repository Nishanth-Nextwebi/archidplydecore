
$(document).ready(function () {
    BindUserReports();
    $(document.body).on('click', ".pVClick", function () {
        var ele = $(this);
        $(".vPagination a").removeClass("active");
        ele.addClass("active");
        BindUserReports();
    });
    $(document.body).on('click', ".prVClick", function () {
        var ele = $(this);
        $(".vPagination a").removeClass("active");
        ele.addClass("active");
        BindUserReports();
    });
    $(document.body).on('click', ".nxVClick", function () {
        var ele = $(this);
        $(".vPagination a").removeClass("active");
        ele.addClass("active");
        BindUserReports();
    });
    $(document.body).on('change', '#ddlPageSize', function () {
        $(".vPagination a").removeClass("active");
        BindUserReports();
    });
    $(document.body).on('change', '#ddlStatus', function () {
        BindUserReports();
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
                    url: "order-repddasdort.aspx/Delete",
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

 
});

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
function BindUserReports() {
    $("#tblBody").empty();
    $("#showDetails").html("");
    AddKeyFrames();

    var oStatus = $("#ddlStatus").val() == "" ? "" : $("#ddlStatus").val();
    var pSize = $("#ddlPageSize").val() == "" ? "" : $("#ddlPageSize").val();

    var pno = "1";
    if ($(".vPagination a").hasClass("active")) {
        pno = $(".vPagination .active").attr('id').split('_')[1];
    }
    $(".vPagination").empty();
    var dataS = { pNo: pno, pSize: pSize, oStatus: oStatus }
    $.ajax({
        type: 'POST',
        url: "/myorders.aspx/BindUserReports",
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
                    var noDataRow = "<td colspan='100%' style='text-align: center; height: 50px; vertical-align: middle;'>No data to show</td>";
                    $("#tblBody").html(noDataRow);
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