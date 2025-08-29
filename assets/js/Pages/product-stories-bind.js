
$(document).ready(function () {
    BindAllStories();


    $(document.body).on('click', ".pPVClick", function () {
        var ele = $(this);
        $(".pPagination a").removeClass("current");
        ele.addClass("current");
        BindAllStories();
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
        BindAllStories();
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
        BindAllStories();
    });

    function BindAllStories() {
        var pno = "1";
        if ($(".pPagination a").hasClass("current")) {
            pno = $(".pPagination .current").attr('id').split('_')[1];
        }
        var dts = { pno: pno };

        $.ajax({
            type: 'POST',
            url: '/product-stories.aspx/allProductStories',
            data: JSON.stringify(dts),
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            async: false,
            success: function (res) {
                var products = res.d;
                var listings = "";

                for (var i = 0; i < products.length; i++) {
                    const product = products[i];
                    const img = `/${product.Image}`;
                    const detail = product.FullDesc;

                    //const vlink = product.Link
                    //    ? `<a href='${product.Link}' data-lity><img src='${img}' alt='Youtube Video' title='Click to play video'></a>`
                    //    : `<a href='${img}' data-lity><img src='${img}' alt='Youtube Video'></a>`;

       //             const vlink = product.Link
       //                 ? `<img style='height:355px !important' src='${img}' data-src='${img}' class='img-fluid lazy-image h-auto' alt=''>`
       //                 : `<a href='${img}' data-gallery='gallery00001' data-thumb-src='${img}'>
       //  <img style='height:355px !important' src='${img}' data-src='${img}' class='img-fluid lazy-image h-auto' alt=''>
       //</a>`;

//                    const vlink = product.Link ? `<div style='height:355px !important' class='bg-image video-01 d-flex justify-content-center align-items-center h-lg-85 position-relative py-18 py-lg-0 py-md-23 lazy-bg'
// data-bg-src='${img}'>
//<a href='${product.Link}' class='view-video iframe-link video-btn d-flex justify-content-center align-items-center fs-30px lh-115px btn btn-outline-light border border-white border-2 rounded-circle transition-all-1'><svg class='icon'><use xlink:href='#icon-play-fill'></use></svg></a></div>` : `<a href='${img}' data-gallery='gallery00001' data-thumb-src='${img}'><img style='height:355px !important' src='${img}' data-src='${img}' class='img-fluid lazy-image h-auto' alt=''></a>`

                    const vlink = product.Link == '' ? '' : `<a href='${product.Link}' data-lity><img src='${img}' alt='Youtube Video' title='Click to play video'></a>`;
                    const StoreImg = product.StoriesGal?.map(gal => `<a href='/${gal.Images}' data-gallery='${gal.StoryGuid}' data-thumb-src='/${gal.Images}'><img src='/${gal.Images}' data-src='/${gal.Images}' class='img-fluid lazy-image h-auto' alt=''></a>` ).join("") || "";

                    if (i % 2 === 0) {
                        listings += `
                    <section class='why-choose-us section-padding'>
                        <div class='container container-xxl'>
                            <div class='row align-items-center justify-content-between '>
                                <div class='col-lg-6 px-lg-10 py-lg-0 py-10 order-lg-0 order-1 productStoryWrap'>
                                    <div class='text-left'>
                                        <div class='section-title'>
                                            <h2>"${product.Title}"</h2>
                                        </div>
                                        ${detail}
                                    </div>
                                </div>
                                <div class='col-lg-6 order-lg-1 order-0 position-relative'>
                                    <div class='about-img'>
                                        <div class='container-fluid'>
                                            <div class='px-md-6'>
                                                <div class='mx-n6 slick-slider slick-slider-product-stories' 
                                                    data-slick-options='{
                                                        "slidesToShow": 1,
                                                        "infinite": false,
                                                        "autoplay": true,
                                                        "dots": true,
                                                        "arrows": false,
                                                        "responsive": [
                                                            {"breakpoint": 1366, "settings": {"slidesToShow": 1}},
                                                            {"breakpoint": 992, "settings": {"slidesToShow": 1}},
                                                            {"breakpoint": 768, "settings": {"slidesToShow": 1}},
                                                            {"breakpoint": 576, "settings": {"slidesToShow": 1}}
                                                        ]
                                                    }'>
                                                    ${StoreImg}
                                                    ${vlink}
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>
                `;
                    } else {
                        listings += `
                    <section class='why-choose-us section-padding'>
                        <div class='container container-xxl'>
                            <div class='row align-items-center justify-content-between '>
                                <div class='col-lg-6 position-relative'>
                                    <div class='about-img'>
                                        <div class='container-fluid'>
                                            <div class='px-md-6'>
                                                <div class='mx-n6 slick-slider slick-slider-product-stories'
                                                    data-slick-options='{
                                                        "slidesToShow": 1,
                                                        "infinite": false,
                                                        "autoplay": true,
                                                        "dots": true,
                                                        "arrows": false,
                                                        "responsive": [
                                                            {"breakpoint": 1366, "settings": {"slidesToShow": 1}},
                                                            {"breakpoint": 992, "settings": {"slidesToShow": 1}},
                                                            {"breakpoint": 768, "settings": {"slidesToShow": 1}},
                                                            {"breakpoint": 576, "settings": {"slidesToShow": 1}}
                                                        ]
                                                    }'>
                                                    ${StoreImg}
                                                    ${vlink}
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class='col-lg-6 px-lg-10 py-lg-0 py-10 productStoryWrap'>
                                    <div class='text-left'>
                                        <div class='section-title'>
                                            <h2>"${product.Title}"</h2>
                                        </div>
                                        ${detail}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>
                `;
                    }
                }

                $("#StoryListBindingSec").empty();
                if (products.length > 0) {
                    $("#StoryListBindingSec").append(listings);
                    BindPPage(5, parseInt(pno), products[0].TotalCount);
                }

                $(".slick-slider-product-stories").slick({
                    slidesToShow: 1,
                    infinite: false,
                    autoplay: true,
                    dots: true,
                    arrows: false,
                    responsive: [
                        { breakpoint: 1366, settings: { slidesToShow: 1 } },
                        { breakpoint: 992, settings: { slidesToShow: 1 } },
                        { breakpoint: 768, settings: { slidesToShow: 1 } },
                        { breakpoint: 576, settings: { slidesToShow: 1 } }
                    ]
                });

                $(".slick-dots li").empty();
                $(".slick-dots li").append("<span></span>");
 
           
            },
            error: function (xhr, status, error) {
                $("#StoryListBindingSec").empty();
            }
        });
    }
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

        const topOffset = $("#StoryListBindingSec").offset().top;
        $(document).scrollTop(topOffset - 150);
    }

});


