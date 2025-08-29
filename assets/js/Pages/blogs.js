
$(document).ready(function () {
    BindAllBlogs();

    $(document.body).on('click', ".pPVClick", function () {
        var ele = $(this);
        $(".pPagination a").removeClass("current");
        ele.addClass("current");
        BindAllBlogs();

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
        BindAllBlogs();

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
        BindAllBlogs();

    });

});

function BindAllBlogs() {


    var pno = "1";
    if ($(".pPagination a").hasClass("current")) {
        pno = $(".pPagination .current").attr('id').split('_')[1];
    }
    var dts = { pno: pno };
    $.ajax({
        type: 'POST',
        url: 'blogs.aspx/allBlogs',
        data: JSON.stringify(dts),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        async: false,
        success: function (res) {
            var products = res.d;
            var listings = "";
            var pLength = "";
            for (var i = 0; i < products.length; i++) {
                var blogurl = "/blog/" + products[i].Url;
                var img = "/" + products[i].ImageUrl;
                pLength = products[0].TotalCount;

                listings += "<div class='col-md-6 col-lg-4 mb-2'>";
                listings += "   <article class='card card-post-grid-1 blog-card bg-transparent border-0' data-animate='fadeInUp'>";
                listings += "       <figure class='card-img-top position-relative mb-5'>";
                listings += "           <a href='" + blogurl +"' class='hover-shine hover-zoom-in d-block' title='" + products[i].Title + "'>";
                listings += "               <img src='" + img + "' class='img-fluid w-100 loaded' alt='" + products[i].Title + "' width='370' height='450' loading='lazy'>";
                listings += "           </a>";
                listings += "       </figure>";
                listings += "       <div class='card-body text-center px-md-9 py-0'>";
                listings += "           <h4 class='card-title lh-base mb-9'>";
                listings += "               <a class='text-decoration-none' href='" + blogurl +"' title='" + products[i].Title + "'>" + products[i].Title + "</a>";
                listings += "           </h4>";
                listings += "       </div>";
                listings += "   </article>";
                listings += "</div>";

            }

            $("#BlogListBindingSec").empty();
            if (products.length > 0) {
                $("#BlogListBindingSec").append(listings);
                BindPPage(6, parseInt(pno), pLength);
                var maxHeight = Math.max.apply(null, $(".post-item .post__title a").map(function () {
                    return $(this).height();
                }).get());
                $(".post-item .post__title a").css("min-height", maxHeight);
                var maxHeight1 = Math.max.apply(null, $(".mainBlogPage .post-item .post__body .post__desc").map(function () {
                    return $(this).height();
                }).get());
                $(".mainBlogPage .post-item .post__body .post__desc").css("min-height", maxHeight1);
            }

        },
        error: function (err) {

            $("#BlogListBindingSec").empty();

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

    const topOffset = $("#BlogListBindingSec").offset().top;
    $(document).scrollTop(topOffset - 150);
}
