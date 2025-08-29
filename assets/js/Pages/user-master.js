
$(document).ready(function () {
    GetQuantity();

    /*custom search Js*/
    $(document.body).on("keyup", "#SearchInput", function (e) {
        if ($("#SearchInput").val() != "") {
            var s = $("#SearchInput").val();
            if (e.keyCode == 13) {
                window.location.href = "/Search.aspx?s=" + s;
            }

            var url = '/WebService.asmx/AutoComplete';
            $.ajax({
                type: 'POST',
                url: url,
                data: "{para:'" + $("#SearchInput").val() + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                async: false,
                success: function (data) {
                    var products = data.d;
                    var listings = "";
                    for (var i = 0; i < products.length; i++) {
                        listings += "<li>";
                        listings += "<a href='/" + products[i].Url + "'>";
                        listings += "<div class='d-flex align-items-center p-5'>";
                        listings += "<div class='imgBox me-5'>";
                        listings += "<img src='/" + products[i].imgurl + "' class='img-fluid' width='60'>";
                        listings += "</div>";
                        listings += "<div class='ProductName'>";
                        listings += "<p class='mb-0'>" + products[i].label + "</p>";
                        listings += "</div>";
                        listings += "</div>";
                        listings += "</a>";
                        listings += "</li>";
                    }
                    if (products.length > 0) {
                        $("#OnPressSearchResult").empty();
                        $("#OnPressSearchResult").append(listings);
                        $("#OnPressSearchResult").css("display", "block");
                    } else {
                        $("#OnPressSearchResult").empty();
                        var notFound = "<li class='NotFoundMsg'><a href='/Contact-Us'><h4>No results found. click here to request the product</h></a></li>"
                        $("#OnPressSearchResult").append(notFound);
                        $("#OnPressSearchResult").css("display", "block");
                    }
                }
            });
            $("#SearchBackground").css("display", "block");

        } else {
            $("#OnPressSearchResult").css("display", "none");
            $("#SearchBackground").css("display", "none");
            $("#SearchInput").val("");
        }
    });
    $(document.body).on("click", "#SearchBackground", function () {
        $("#OnPressSearchResult").css("display", "none");
        $("#SearchBackground").css("display", "none");
        $("#SearchInput").val("");


    });
    $(document.body).on("click", "#SearchBackground_Md", function () {
        $("#OnPressSearchResult_Md").css("display", "none");
        $("#SearchBackground_Md").css("display", "none");
        $(".search-full.show").removeClass("open");
        $("#SearchInput_Md").val("");


    });

});
function GetQuantity() {
    $.ajax({
        type: 'POST',
        url: '/WebService.asmx/GetCartQuantity',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        async: false,
        success: function (res) {
            var ret = res.d.toString(); //== "" ? "" : "<span class=''>" + res.d.toString() + "</span> ";
            $(".Qty").empty();
            $(".Qty").append(ret);
        },
        error: function (err) {
            $(".Qty").empty();
        }
    });
};