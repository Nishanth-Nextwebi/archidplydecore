$(document).ready(function () {

    var stockStatus=$('[id*=HiddenStockStatus]').val();
    if (stockStatus === "Yes") {
        $(".btnAddToCartP").removeClass("d-none");
    } else {
        $(".btnAddToCartP").addClass("d-none");
        $(".Enquirybtn").removeClass("d-none");
    }

    $('[id*=ddlSize]').change(handleSizeChange);
    handleSizeChange();

    $('[id*=ddlThickness]').change(GetPriceBySizeAndThick);
    GetPriceBySizeAndThick();


    $(document.body).on("click", ".shop-up", function () {
        let quantity = parseInt($('#product_quantity').val(), 10);

        if (quantity < 20) {
            quantity++;  
            $('#product_quantity').val(quantity);  
            $('.quantity-error').text("");  
        }

        if (quantity >= 20) {
            $('#product_quantity').val(20); 
            $('.quantity-error').text("Maximum quantity is 20");
        }
    });

    $(document.body).on("click", ".shop-down", function () {
        let quantity = parseInt($('#product_quantity').val(), 10);

        if (quantity > 1) {
            quantity--;
            $('#product_quantity').val(quantity);  
            $('.quantity-error').text(""); 
        }

        if (quantity <= 1) {
            $('#product_quantity').val(1);  
            $('.quantity-error').text("Minimum quantity is 1");
        }
    });



    $(document.body).on("click", ".btnAddToCartP", function (e) {
        e.preventDefault();
       // var $btnAddToCartP = $(this);
        //$btnAddToCartP.prop('disabled', true).html("Please wait...");
        var pdid = $('[id*=productIdHidden]').val();
        var price = $('.actualprce').html().replace('₹', '').trim();
        var size = $('[id*=ddlSize]').val();
        var Thickness = $('#ddlThickness').val();
        var qty = $("#product_quantity").val();
        var pncd = "";
        qty = qty === "" || qty === undefined ? "1" : qty;

        $.ajax({
            type: 'POST',
            url: '/WebService.asmx/AddToCart',
            data: "{pdid:'" + pdid + "',price:'" + price + "',size:'" + size + "',Thickness:'" + Thickness + "',pncd:'" + pncd + "',qty:'" + qty + "'}",
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            async: false,
            success: function (res) {
                if (res.d.toString() == "Success") {
                    Snackbar.show({ text: 'Added to cart successfully.', pos: 'top-right', textColor: '#ffffff', actionTextColor: '#dd6a9d', actionText: 'Go to cart', onActionClick: function (element) { window.location.href = '/cart'; }, backgroundColor: '#4e7661' });
                  //  $btnAddToCartP.prop('disabled', true).html("Add To Bag");
                    GetQuantity();
                }
                else if (res.d.toString() == "Not found") {
                    Snackbar.show({ text: 'Product does not exist. Please refresh the page.', pos: 'top-right', textColor: '#ffffff', actionTextColor: '#ff0000', backgroundColor: '#000000' });
                   // $btnAddToCartP.prop('disabled', true).html("Add To Bag");
                }
            },
            error: function (err) {
                Snackbar.show({ text: 'An error occurred. Please try again.', pos: 'top-right', textColor: '#ffffff', actionTextColor: '#ff0000', backgroundColor: '#000000' });
               // $btnAddToCartP.prop('disabled', true).html("Add To Bag");
            }
        });
    });


});
function handleSizeChange() {
    var elem = $('[id*=ddlSize]');
    var pid = $('[id*=hfProductId]').val();
    var size = elem.val();

    $.ajax({
        type: 'POST',
        url: "/shop-product-detail.aspx/GetProductPrice",
        data: JSON.stringify({ size: size, pid: pid }),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        async: false,
        success: function (response) {
            var products = response.d;
            var thicknessDropdown = $('#ddlThickness');
            thicknessDropdown.empty();
            $.each(products, function (index, item) {
                thicknessDropdown.append('<option value="' + item.ProductThickness + '">' + item.ProductThickness + '</option>');
            });
           /* if (products.length > 0) {
                var selectedProduct = products[0];
                var percentage = ((selectedProduct.ActualPrice - selectedProduct.DiscountPrice) / selectedProduct.ActualPrice) * 100;
                $('.disprice').text('₹' + selectedProduct.DiscountPrice);
                $('.actualprce').text('₹' + selectedProduct.ActualPrice);
                $('.discountPercent').text(percentage.toFixed(2) + '%');
            }*/
            GetPriceBySizeAndThick();
        },
        error: function (xhr, status, error) {
            Snackbar.show({ text: 'An error occurred. Please try again.', pos: 'top-right', textColor: '#ffffff', actionTextColor: '#ff0000', backgroundColor: '#000000' });
        }
    });
}

function GetPriceBySizeAndThick() {
    var elem = $('[id*=ddlSize]');
    var elem2 = $('[id*=ddlThickness]');
    var pid = $('[id*=hfProductId]').val();
    var size = elem.val();
    var thic = elem2.val();

    $.ajax({
        type: 'POST',
        url: "/shop-product-detail.aspx/GetPriceBySizeAndThick",
        data: JSON.stringify({ size: size, pid: pid, thic: thic }),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        async: false,
        success: function (response) {
            var products = response.d;
            if (products.length > 0) {
                var selectedProduct = products[0];
                var percentage = ((selectedProduct.ActualPrice - selectedProduct.DiscountPrice) / selectedProduct.ActualPrice) * 100;
                $('.disprice').text('₹' + selectedProduct.DiscountPrice);
                $('.actualprce').text('₹' + selectedProduct.ActualPrice);
                $('.discountPercent').text(percentage.toFixed(2) + '%');
            }
        },
        error: function (xhr, status, error) {
            Snackbar.show({ text: 'An error occurred. Please try again.', pos: 'top-right', textColor: '#ffffff', actionTextColor: '#ff0000', backgroundColor: '#000000' });
        }
    });
}


