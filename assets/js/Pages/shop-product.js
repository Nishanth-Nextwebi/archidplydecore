
$(document).ready(function () {
    var CatId = $('[id*=HiddenCatId]').val();
    if (CatId && CatId !== "0") {
        $('[id*=ddlCategory]').val(CatId).change();
        BindAllShopProducts();
    }
    BindAllShopProducts();

    $(document.body).on('click', ".pPVClick", function () {
        var ele = $(this);
        $(".pPagination a").removeClass("current");
        ele.addClass("current");
        BindAllShopProducts();

    });
    $(document.body).on('click', ".prPVClick", function () {
        var ele = $(this);
        var activeIndex = $(".pPagination li.active a").attr("id").split('_')[1];
        var currentIndex = ele.attr("id").split('_')[1];
        if (activeIndex == currentIndex) {
            $(".pPagination li a.dNonePrev").css("display", "none");
            return;
        }
        $(".pPagination a").removeClass("current");
        ele.addClass("current");
        BindAllShopProducts();

    });
    $(document.body).on('click', ".nxPVClick", function () {
        $(".pPagination li.dNonePrev").css("display", "flex");
        var ele = $(this);

        var currentIndex = ele.attr("id").split('_')[1];
        var activeIndex = $(".pPagination li.active a").attr("id").split('_')[1];

        if (currentIndex == activeIndex) {
            $(".pPagination li a.dNoneNext").css("display", "none");
            return;
        }

        $(".pPagination a").removeClass("current");
        ele.addClass("current");
        BindAllShopProducts();

    });

});

$(document.body).on('change', "[id*=ddlCategory]", function () {
    var Pcat = $(this).find('option:selected').text();
    $(".CatName").text(Pcat);
    BindAllShopProducts();
});

$(document.body).on('text', "[id*=HiddenCatId]", function () {
    var Pcat = $(this).find('option:selected').text();
    $(".CatName").text(Pcat);
    BindAllShopProducts();
});


function BindAllShopProducts() {
    var pno = "1";
    if ($(".pPagination a").hasClass("current")) {
        pno = $(".pPagination .current").attr('id').split('_')[1];
    }

    var Pcat = $('[id*=ddlCategory]').val() == "" ? "" : $('[id*=ddlCategory]').val();
    var currentURL = window.location.href;
    var urlSegments = currentURL.split('/');
    var PTag = urlSegments[urlSegments.length - 1] == "shop" ? "" : urlSegments[urlSegments.length - 1];
  
    var dts = { pno: pno, Pcat: Pcat};
    $.ajax({
        type: 'POST',
        url: '/shop-products.aspx/AllShopProducts',
        data: JSON.stringify(dts),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        async: false,
        success: function (res) {
            var products = res.d;
            var listings = "";
            var pLength = "";
            for (var i = 0; i < products.length; i++) {
                var producturl = "/shop-products/" + products[i].ProductUrl;
                var img = "/" + products[i].ProductImage;
                pLength = products[0].TotalCount;

                listings += "  <div class=' col-lg-4 col-xl-3 col-6 p-lg-5 p-3'>";
                listings += "      <div class='card card-product grid-1 bg-transparent border-0' >";
                listings += "         <figure class='card-img-top position-relative mb-0 overflow-hidden'>";
                listings += "               <a href='" + producturl + "' class='hover-zoom-in d-block' title='" + products[i].ProductName +"'>";
                listings += "            <img src='" + img + "' class='img-fluid lazy-image w-100' alt='" + products[i].ProductName +"' width='330' height='440'>";
                listings += "               </a>";
                listings += "         </figure>";
                listings += "         <div class='card-body text-center p-0'>";
                listings += "              <span class='d-flex align-items-center price text-body-emphasis fw-bold justify-content-center mb-3 fs-6'>";
                listings += "                  <ins class='text-decoration-none'>₹" + products[i].productprs[0].MinPrice +"</ins>  ";
                listings += "                  <ins class='text-decoration-none'>  - " + products[i].productprs[0].MaxPrice +"</ins>";
                listings += "              </span>";
                listings += "              <h4 class='product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3'><a class='text-decoration-none text-reset' href='" + producturl + "'>" + products[i].ProductName +"</a></h4>";
                listings += "              <a href='" + producturl +"' class='btn btn-add-to-cart green-btn btn-hover-bg-primary btn-hover-border-primary' title='Check Out'>View More</a>";
                listings += "          </div>";
                listings += "  </div>";
                listings += "  </div>";
            }

            $("#BindAllShopProductList").empty();
            if (products.length > 0) {
                $("#BindAllShopProductList").append(listings);
                BindPPage(8, parseInt(pno), pLength);
                var maxHeight = Math.max.apply(null, $(".post-item .post__title a").map(function () {
                    return $(this).height();
                }).get());
                $(".post-item .post__title a").css("min-height", maxHeight);

                var maxHeight1 = Math.max.apply(null, $(".mainProductList .post-item .post__body .post__desc").map(function () {
                    return $(this).height();
                }).get());
                $(".mainProductList .post-item .post__body .post__desc").css("min-height", maxHeight1);
                $(".divNoItem").css("display", "none");
                $(".AboutCategory").css("display", "block");

            } else {
                $(".AboutCategory").css("display", "none");
                $(".divNoItem").css("display", "block");
                $(".pPagination").empty();
            }
        },
        error: function (err) {
            $("#BindAllShopProductList").empty();
        }
    });
};

function BindPPage(pageS, cPage, pCount) {
    var noOfPagesCreated = Math.ceil(parseFloat(pCount) / parseInt(pageS));

    $(".pPagination").empty();

    let pagesss = "";

    const groupSize = 5;
    const groupStart = Math.floor((cPage - 1) / groupSize) * groupSize + 1;
    const groupEnd = Math.min(groupStart + groupSize - 1, noOfPagesCreated);

    for (let i = groupStart; i <= groupEnd; i++) {
        const activeClass = i === parseInt(cPage) ? "active" : "";
        pagesss += `<li class="page-item ${activeClass}">
                        <a class="page-link pPVClick" href="javascript:void(0);" id="pno_${i}">${i}</a>
                    </li>`;
    }

    if (groupEnd < noOfPagesCreated) {
        pagesss += `<li class="page-item">
                        <a class="page-link pPVClick" href="javascript:void(0);" id="pno_${groupEnd + 1}">...</a>
                    </li>`;
        pagesss += `<li class="page-item">
                        <a class="page-link pPVClick LastIndex" href="javascript:void(0);" id="pno_${noOfPagesCreated}">${noOfPagesCreated}</a>
                    </li>`;
    }

    const prevPage = cPage > 1 ? cPage - 1 : 1;
    const nextPage = cPage < noOfPagesCreated ? cPage + 1 : noOfPagesCreated;

    const pgnPrev = `<li class="page-item ${cPage === 1 ? "disabled" : ""}">
                        <a class="page-link prPVClick" href="javascript:void(0);" id="pnon_${prevPage}" aria-label="Previous">
                            <i class="fa fa-angle-left"></i>
                        </a>
                    </li>`;

    const pgnNext = `<li class="page-item ${cPage === noOfPagesCreated ? "disabled" : ""}">
                        <a class="page-link nxPVClick" href="javascript:void(0);" id="pnon_${nextPage}" aria-label="Next">
                            <i class="fa fa-angle-right"></i>
                        </a>
                    </li>`;

    $(".pPagination").append(pgnPrev + pagesss + pgnNext);

    //const topOffset = $("#BindAllShopProductList").offset().top;
    //$(document).scrollTop(topOffset - 150);
}


