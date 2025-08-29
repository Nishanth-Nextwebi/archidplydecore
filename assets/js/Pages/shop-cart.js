$(document).ready(function () {
    GetCartDetails();
    $(document.body).on("click", ".qty-left-minus", function () {
        $(this).parent().parent().find(".error").remove();
        var $qty = $(this).siblings(".qty-input");
        var pid = $(this).attr("data-pid");
        var prid = $(this).attr("data-prid");
        var currentVal = parseInt($qty.val());
        if (!isNaN(currentVal) && currentVal > 1) {
            $qty.val(currentVal - 1);
        } else {
            $(this).parent().parent().append("<span class='error text-danger'>Quantity can't be less than 1 and greater than 20</span>");
            return;
        }
        var qty = $(this).siblings(".qty-input").val();
        UpdateCart(pid, prid, qty);
    });
    $(document.body).on('click', '.qty-right-plus', function () {
        $(this).parent().parent().find(".error").remove();

        var pid = $(this).attr("data-pid");
        var prid = $(this).attr("data-prid");
        if ($(this).prev().val() < 20) {
            $(this).prev().val(+$(this).prev().val() + 1);
        } else {
            $(this).parent().parent().append("<span class='error text-danger'>Quantity can't be less than 1 and greater than 20</span>");
            return;
        }
        var qty = $(this).siblings(".qty-input").val();
        UpdateCart(pid, prid, qty);

    });
    $(document.body).on('click', '#deleteSelected', function (e) {
        e.preventDefault();
        if ($("#allCartItem .check-product:checked").length === 0) {
            Swal.fire({
                title: 'No Items Selected',
                text: 'Please select at least one item to remove from the cart.',
                icon: 'warning',
                confirmButtonText: 'OK',
                padding: '2em'
            });
            return;
        }
        let selectedItems = [];
        $("#allCartItem .check-product:checked").each(function () {
            const pid = $(this).attr("data-pid");
            const prid = $(this).attr("data-prid");
            if (pid && prid) {
                selectedItems.push({ pid, prid });
            }
        });
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this action!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete them!',
            cancelButtonText: 'No, cancel!',
            padding: '2em'
        }).then(function (result) {
            if (result.value) {
                selectedItems.forEach(item => {
                    RemoveCart(item.pid, item.prid);
                });
                Swal.fire({
                    title: 'Deleted!',
                    text: 'The selected items have been removed from your cart.',
                    icon: 'success',
                    confirmButtonText: 'OK',
                    padding: '2em'
                });
            }
        });
    });
    $(document.body).on('click', '.RemoveFromCartBtn', function () {
        var pid = $(this).attr("data-pid");
        var prid = $(this).attr("data-prid");

        if (pid != "" && prid != "") {
            Swal.fire({
                title: 'Are you sure?',
                text: 'You won\'t be able to revert this!',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes, delete it!',
                cancelButtonText: 'No, cancel!',
                padding: '2em'
            }).then(function (result) {
                 if (result.value) {
                     RemoveCart(pid, prid);
                    Swal.fire({
                        title: 'Deleted!',
                        text: 'The item has been removed from your cart.',
                        icon: 'success',
                        confirmButtonText: 'OK',
                        padding: '2em'
                    });
                } else if (result.dismiss === Swal.DismissReason.cancel) {
                    Swal.fire({
                        title: 'Cancelled',
                        text: 'Your item is safe!',
                        icon: 'info',
                        confirmButtonText: 'OK',
                        padding: '2em'
                    });
                }
            });
        }
    });

    function RemoveCart(pid, prid) {
        $.ajax({
            type: "POST",
            url: "/WebService.asmx/RemoveCart",
            data: "{ pid:'" + pid + "',prid:'" + prid + "'}",
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (res) {
                CartDetailBind(res.d);
                GetCartDetails();
                GetQuantity();
            }
        });
    };

    function UpdateCart(pdid, prid, qty) {
        if (parseInt(qty) > 0) {
            $.ajax({
                type: "POST",
                url: "/WebService.asmx/UpdateCart",
                data: "{pdid:'" + pdid + "',prid:'" + prid + "',qty:'" + qty + "'}",
                contentType: 'application/json; charset=utf-8',
                async: false,
                success: function (res) {
                    CartDetailBind(res.d);
                    GetCartDetails();
                    GetQuantity();
                }
            });
        }
        else {
            alert("Minimum quantity should be 1");
        }
    };
});
function GetCartDetails() {
    $.ajax({
        type: "POST",
        url: "/WebService.asmx/GetUserCart",
        data: "{}",
        contentType: 'application/json; charset=utf-8',
        async: "false",
        success: function (res) {
            CartDetailBind(res.d);
            GetQuantity();
        }
    });
};
function CartDetailBind(products) {
    var listings = "";
    var ProPrice = "";
    var grandTotalPrice = 0, ttlGrandTotal = 0, ttlGrandDiff = 0, ttlDiscount = 0, ttlActual = 0, subtotal=0, ttlDiff = 0, shipPrice = 0;;
    for (var i = 0; i < products.length; i++) {
        var url = "/shop-products/" + products[i].ProductUrl;
        var img = "/" + products[i].ProductImage;
        var pname = products[i].ProductName;
        var qty = parseFloat(products[i].Qty);
        var act = parseFloat(products[i].ActualPrice == "" ? products[i].DiscountPrice : products[i].ActualPrice);
        var disc = parseFloat(products[i].DiscountPrice);

       
        var diff = "";
        ttlDiscount = (disc * qty);
        ttlActual = (act * qty);
        grandTotalPrice = parseFloat(grandTotalPrice) + parseFloat(ttlDiscount);
        ttlGrandTotal = parseFloat(ttlGrandTotal) + parseFloat(ttlActual);
        ttlGrandDiff = parseFloat(ttlGrandTotal) - parseFloat(grandTotalPrice);
        subtotal = grandTotalPrice
        if (products[i].ActualPrice !== products[i].DiscountPrice) {
            ttlDiff = (act * qty) - (disc * qty);
            diff = parseFloat(act) - parseFloat(disc);
            var per = Math.floor(parseFloat(100) - (parseFloat(disc) / (parseFloat(act) / parseFloat(100))));

            ttlDiscount = ttlDiscount.toFixed(2)
            ttlDiscount = parseFloat(ttlDiscount).toLocaleString('en-IN', {
                maximumFractionDigits: 2,
                style: 'currency',
                currency: 'INR'
            });
            ttlActual = ttlActual.toFixed(2);
            ttlActual = parseFloat(ttlActual).toLocaleString('en-IN', {
                maximumFractionDigits: 2,
                style: 'currency',
                currency: 'INR'
            });
            ttlDiff = ttlDiff.toFixed(2);
            ttlDiff = parseFloat(ttlDiff).toLocaleString('en-IN', {
                maximumFractionDigits: 2,
                style: 'currency',
                currency: 'INR'
            });
            diff = "<p class='amountSaved'>You saved " + ttlDiff + "</p>";

        } else {
            ttlDiscount = ttlDiscount.toFixed(2)
            ttlDiscount = parseFloat(ttlDiscount).toLocaleString('en-IN', {
                maximumFractionDigits: 2,
                style: 'currency',
                currency: 'INR'
            });
        }

        ttlGrandDiff = ttlGrandDiff.toFixed(2);
        ttlGrandDiff = parseFloat(ttlGrandDiff).toLocaleString('en-IN', {
            maximumFractionDigits: 2,
            style: 'currency',
            currency: 'INR'
        });
        listings += "<tr class='position-relative'>";
        listings += "<th scope='row' class='pe-5 ps-8 py-7 shop-product'>";
        listings += "<div class='d-flex align-items-center'>";
        listings += "<div class='form-check d-none'>";
        listings += "<input class='form-check-input check-product' data-pid='" + products[i].ProductId + "' data-prid='" + products[i].PriceId +"' check-product' type='checkbox' name='check-product' value='checkbox'>";
        listings += "</div>";
        listings += "<div class='ms-6 me-7'>";
        listings += "<a href='"+url+"'><img src='" + img +"' class='lazy-image' width='75'></a>";
        listings += "</div>";
        listings += "<div>";
        listings += "<a href='" + url + "'><p class='fw-500 mb-1 text-body-emphasis'>" + pname +"</p></a>";
        listings += "<p class='card-text'>";
        listings += "<span class='fs-13px fw-500 text-decoration-line-through pe-3'>₹" + products[i].ActualPrice +"</span>";
        listings += "<span class='fs-15px fw-bold text-body-emphasis'>₹" + products[i].DiscountPrice +"</span>";
        listings += "</p>";
        listings += "</div>";
        listings += "</div>";
        listings += "</th>";
        listings += "<td class='align-middle'>";
        listings += "<div class='input-group position-relative shop-quantity'>";
        listings += "<a href='javascript:void(0)' class='shop-down position-absolute z-index-2 qty-left-minus' data-pid='" + products[i].ProductId + "' data-prid='" + products[i].PriceId +"'><i class='far fa-minus' aria-hidden='true'></i></a>";
        listings += "<input name='quantity' class='form-control form-control-sm px-10 py-4 fs-6 text-center border-0 input-number qty-input' value='" + qty +"' readonly>";
        listings += "<a href='javascript:void(0);' class='shop-up position-absolute z-index-2 qty-right-plus' data-pid='" + products[i].ProductId + "' data-prid='" + products[i].PriceId +"'><i class='far fa-plus' aria-hidden='true'></i></a>";
        listings += "</div>";
        listings += "</td>";
        listings += "<td class='align-middle'>";
        listings += "<p class='mb-0 text-body-emphasis fw-bold mr-xl-11 new-price-add'>" + ttlDiscount +"</p>";
        listings += "</td>";
        listings += "<td class='align-middle text-end pe-8'>";
        listings += "<a href='javascript:void(0);' data-pid='" + products[i].ProductId + "' data-prid='" + products[i].PriceId +"' class='d-block text-secondary RemoveFromCartBtn'><i class='fa fa-times'></i></a>";
        listings += "</td>";
        listings += "</tr>";
    }
    if (products.length > 0) {
        var mincart = products[0].MinCartPrice == "" ? 0 : products[0].MinCartPrice;
        var shipAmount = products[0].ShippingCharge == "" ? 0 : products[0].ShippingCharge;
        var sh = grandTotalPrice > mincart ? 0 : shipAmount;
        grandTotalPrice = parseFloat(grandTotalPrice) + parseFloat(sh);

        let ship = parseFloat(sh).toLocaleString("en-IN", { style: "currency", currency: "INR" });

        ttlGrandTotal = ttlGrandTotal.toFixed(2);
        ttlGrandTotal = parseFloat(ttlGrandTotal).toLocaleString('en-IN', {
            maximumFractionDigits: 2,
            style: 'currency',
            currency: 'INR'
        });

        grandTotalPrice = grandTotalPrice.toFixed(2);
        grandTotalPrice = parseFloat(grandTotalPrice).toLocaleString('en-IN', {
            maximumFractionDigits: 2,
            style: 'currency',
            currency: 'INR'
        });
        subtotal = subtotal.toFixed(2);
        subtotal = parseFloat(subtotal).toLocaleString('en-IN', {
            maximumFractionDigits: 2,
            style: 'currency',
            currency: 'INR'
        });

       
        $(".fullCartBag").css("display", "flex");
       // var bag = "<p>My Shopping Bag (" + products.length + " Items) <span>Total: <span class='TotalPrice''>" + grandTotalPrice + "</span></span></p>";
        $(".TotalPrice").empty();
        $(".TotalPrice").append(subtotal);
    //    $(".BagDetails").empty();
       // $(".BagDetails").append(bag);
        $("#allCartItem").empty();
        $("#allCartItem").append(listings);
        $(".DiscountEleClass").empty();
        $(".DiscountEleClass").text(grandTotalPrice);
        $(".shippingPrice").empty();
        $(".shippingPrice").text(ship);
      //  $(".ActualEleClass").empty();
       // $(".ActualEleClass").text(ttlGrandTotal);
        $(".DifferenceEleClass").empty();
        $(".DifferenceEleClass").text(ttlGrandDiff);
        $(".divNoItem").css("display", "none");
    }
    else {
        $(".tableContent").css("display", "none");
        $("#allCartItem").empty();
        $(".pricecard").css("display", "none");
        $(".divNoItem").css("display", "block");
        //var noItem = "";
        //noItem += "<td colspan='100%' style='text-align: center; height: 50px; vertical-align: middle;'>No cart Found</td>";
        //$(".NoCartItemFound").html(noItem);
    }
    
}