<%@ Page Title="Archidply Decor| Best Manufacturer and Supplier of Plywood and Veneers in India" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<meta name="description" content="Discover Archidply Decor’s range of plywood, veneers, doors & decorative panels—perfect for homes, offices and interiors.">--%>
    <meta name="description" content="Archidply Decor,a leading manufacturer and supplier of plywood and veneers in India, offers premium quality decorative surfaces, laminates, and interior solutions for residential and commercial spaces.">
    <meta name="keywords" content="Archidply Decor, premium plywood, decorative veneers, flush doors, interior panels" />
    <link rel="canonical" href="https://archidplydecor.com/" />
    <style>
        modal-backdrop {
            background: rgba(0, 0, 0, 0.5);
        }

        .hero .hero-title-2 {
            font-size: 52px;
            line-height: 58px;
            font-weight: 400;
        }

        .m-content {
            background: #f8f9fa;
            border-radius: 15px;
            box-shadow: 0px 8px 24px rgba(0, 0, 0, 0.2);
        }

        .m-header {
            background: #e9f5ff;
            color: #0d6efd;
            border-bottom: none;
        }

        .m-title {
            font-size: 1.75rem;
            font-weight: bold;
        }

        .btn-close {
            color: #0d6efd;
        }

        .m-body {
            padding: 2rem;
        }

        .form-control {
            border-radius: 10px;
            border: 1px solid #ddd;
            padding: 10px;
        }

            .form-control:focus {
                box-shadow: 0px 0px 8px rgba(13, 110, 253, 0.5);
                border-color: #0d6efd;
            }

        .btn-primary {
            background: #0d6efd;
            border: none;
            border-radius: 10px;
            padding: 10px 20px;
            transition: background 0.3s ease;
        }

            .btn-primary:hover {
                background: #0056b3;
            }

        .fade.modal.show .modal-dialog {
            transform: translateY(-20px);
            opacity: 1;
            transition: all 0.3s ease-out;
        }

        .customeHR {
            margin-top: 1.5rem !important;
            margin-bottom: 1.5rem !important;
            background: linear-gradient(to right, #ff7e5f, #feb47b);
            width: 100%;
            margin: 20px 0;
            display: block;
        }

        .highlightword {
            color: #D4AF37;
            font-weight: bold;
        }

        .normalbanner {
            color: #8A6D3B;
            font-weight: bold;
        }

        .bg-overlay::before {
            z-index: 1 !important;
        }

        .productStoryWrap h2 {
            font-size: 24px !important;
            margin-bottom: 15px;
        }

        .productStoryWrap h4 {
            font-size: 16px !important;
        }

        .productStoryWrap ul {
            display: flex;
            justify-content: start;
            gap: 15px;
            padding-left: 0px !important;
            flex-wrap: wrap;
        }

        .productStoryWrap li {
            background: #804501;
            display: inline-block;
            border-radius: 3px;
            padding: 5px 10px;
            color: #fff;
        }

        #mainCTASec {
            position: relative;
        }

            #mainCTASec .textWrap {
                position: absolute;
                top: 0px;
                width: 100%;
                height: 100%;
            }

            #mainCTASec .bg-overlay:before {
                background: rgba(0, 0, 0, 0.2);
            }


        @media (max-width:767px) {


            #mainCTASec .textWrap h3 {
                font-size: 20px !important;
                margin-bottom: 20px !important;
            }

            #mainCTASec .textWrap .orange-btn {
                font-size: 12px !important;
            }
        }
    </style>
    <link href="/Admin/assets/libs/snackbar/snackbar.min.css" rel="stylesheet" />

    <link
        rel="preload"
        as="image"
        href="https://archidplydecor.com/UploadImages/013393a0-79e1-4726-9691-d20e8adc3c90-deskBanImg.webp" alt="Plywood furniture & wooden floor finish"
        type="image/webp"
        fetchpriority="high" />
    <link
        rel="preload"
        as="image"
        href="https://archidplydecor.com/UploadImages/7840fc96-4881-4668-abf5-7190e17173c2-deskBanImg.webp" alt="Wooden wall panel wardrobe doors"
        type="image/webp"
        fetchpriority="high" />

    <link
        rel="preload"
        as="image"
        href="https://archidplydecor.com/UploadImages/f8baab95-d01b-4d4d-84dd-6f5d30011e09-deskBanImg.webp" alt="Wooden console Decor"
        type="image/webp"
        fetchpriority="high" />

    <link
        rel="preload"
        as="image"
        href="https://archidplydecor.com/UploadImages/cb840c86-075d-45cb-afff-61234b0e3c14-deskBanImg.webp" alt="Office desks"
        type="image/webp"
        fetchpriority="high" />

    <link
        rel="preload"
        as="image"
        href="https://archidplydecor.com/UploadImages/d4b951a8-526b-479d-8931-13e2cda164ea-deskBanImg.webp" alt="Plywood wall panels"
        type="image/webp"
        fetchpriority="high" />
    <script type="application/ld+json">
{
  "@context": "https://schema.org",
  "@type": "BreadcrumbList",
  "itemListElement": [
    {
      "@type": "ListItem",
      "position": 1,
      "name": "Home",
      "item": "https://archidplydecor.com/"
    }
  ]
}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="scriptManager"></asp:ScriptManager>
    <main id="content" class="wrapper layout-page">
        <%=strBannerImages %>
        <%=strMobileBanner %>

        <%--<section class='home-banner'>
            <div class='slick-slider hero hero-header-02 slick-slider-dots-inside'
                data-slick-options='{&#34;arrows&#34;:true,&#34;autoplay&#34;:true,&#34;autoplaySpeed&#34;:9000,&#34;cssEase&#34;:&#34;ease-in-out&#34;,&#34;dots&#34;:false,&#34;fade&#34;:true,&#34;infinite&#34;:true,&#34;slidesToShow&#34;:1,&#34;speed&#34;:600}'>
                <div class='vh-100 d-flex align-items-center'>
                    <div class='z-index-2 container container-xxl py-21 pt-xl-10 pb-xl-11'>
                        <div class='hero-content text-start'>
                            <div data-animate='fadeInDown'>
                                <span class='welcome-text'>Welcome to Archidply! </span>
                                <h1 class='mb-15 text-white hero-title-2'>Sustainable <span>Plywood</span> for Modern Homes
                                </h1>
                            </div>
                            <div class='cta-btn' data-animate='fadeInDown'>
                                <a href='/shop' class='btn orange-btn'>Explore Now <i class='fa-solid fa-arrow-circle-right'></i>

                                </a>
                            </div>
                        </div>
                    </div>
                    <div class='w-100 h-100'>
                        <img src="assets/imgs/banner03.png" class="w-100 h-100 lazy-bg bg-overlay position-absolute z-index-1" />
                    </div>
                </div>
                <div class='vh-100 d-flex align-items-center'>
                    <div class='z-index-2 container container-xxl py-21 pt-xl-10 pb-xl-11'>
                        <div class='hero-content text-start'>
                            <div data-animate='fadeInDown'>
                                <span class='welcome-text'>Welcome to Archidply! </span>
                                <h1 class='mb-15 text-white hero-title-2'>Versatile <span>Plywood</span> for Every Project
                                </h1>
                            </div>
                            <div class='cta-btn' data-animate='fadeInDown'>
                                <a href='/shop' class='btn orange-btn'>Explore Now <i class='fa-solid fa-arrow-circle-right'></i>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class='w-100 h-100 bg-overlay position-absolute  z-index-1'>
                        <img src="assets/imgs//banner-2.png" class="w-100 h-100 lazy-bg " />
                    </div>

                </div>
            </div>
        </section>--%>
        <section class="container container-xxl section-padding-top">
            <div class="row gy-30px">
                <div class="col-lg-6 col-md-6" data-animate="fadeInUp">
                    <div class="card border-0 rounded-0 banner-01 hover-zoom-in hover-shine">
                        <img class="lazy-image object-fit-cover card-img light-mode-img"
                            src="#"
                            data-src="assets/imgs/pureply5.png"
                            alt="Wooden cabinet" />
                        <div class="card-img-overlay d-inline-flex flex-column p-md-5 p-lg-12 m-md-2 p-8 justify-content-start align-items-start">
                            <!-- <h6
                          class="card-subtitle ls-1 fs-15px mb-5 fw-semibold text-body-calculate"
                        >
                          NEW COLLECTION
                        </h6> -->
                            <!-- <h3 class="card-title lh-45px mw-md-60 pe-xl-15 fs-3 pe-0">
                          Intensive Glow C&#43; Serum
                        </h3> -->
                            <img src="/assets/imgs/pplogo.png"
                                alt="Archidply Decor Pureply logo"
                                class="sister-company" />
                            <div class="mt-7">
                                <a href="/products-categories/pureply" class="btn btn-white px-6 shadow-sm">Explore More</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6 col-md-6" data-animate="fadeInUp">
                    <div class="card border-0 rounded-0 banner-01 hover-zoom-in hover-shine">
                        <img class="lazy-image object-fit-cover card-img light-mode-img"
                            src="#"
                            data-src="assets/imgs/bonvivantImg.png"
                            alt="Bon Vivant veneer tabletop" />
                        <div class="card-img-overlay d-inline-flex flex-column p-md-5 p-lg-12 m-md-2 p-8 justify-content-start align-items-start">
                            <!-- <h3 class="card-title lh-45px fs-3 pe-15">
                          25% off Everything
                        </h3> -->
                            <!-- <p
                          class="card-text fs-15px text-body-emphasis mw-md-60 pe-xl-20"
                        >
                          Makeup with extended range in colors for every human.
                        </p> -->
                            <img src="assets/imgs/bonvivant-decorative2.png"
                                alt="Bon Vivant decorative veneers and laminates"
                                class="sister-company" />
                            <div class="mt-7">
                                <a href="products-categories/bon-vivant" class="btn btn-white shadow-sm">Explore More</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="section-padding product-category">
            <div class="container container-xxl">
                <div class="row align-items-center">
                    <div class="col-md-6 mb-5 mb-md-11" data-animate="fadeInUp">
                        <div class="section-title">
                            <h2>Shop Categories</h2>
                        </div>
                    </div>
                    <div class="col-md-6 mb-11 d-md-block d-none text-md-end" data-animate="fadeInUp">
                        <a href="/shop"
                            class="btn btn-link p-0 mt-2 text-decoration-none orange-text fw-semibold">Shop All Categories<i class="far fa-arrow-right ps-2 fs-13px"></i>
                        </a>
                    </div>
                </div>
                <div class="row ">

                    <div class="slick-slider"
                        data-slick-options="{&#34;arrows&#34;:false,&#34;centerMode&#34;:true,&#34;centerPadding&#34;:&#34;calc((100% - 1440px) / 2)&#34;,&#34;dots&#34;:true,&#34;infinite&#34;:true,&#34;responsive&#34;:[{&#34;breakpoint&#34;:1200,&#34;settings&#34;:{&#34;arrows&#34;:false,&#34;dots&#34;:false,&#34;slidesToShow&#34;:3}},{&#34;breakpoint&#34;:992,&#34;settings&#34;:{&#34;arrows&#34;:false,&#34;dots&#34;:true,&#34;slidesToShow&#34;:2}},{&#34;breakpoint&#34;:576,&#34;settings&#34;:{&#34;arrows&#34;:false,&#34;dots&#34;:true,&#34;slidesToShow&#34;:2}}],&#34;slidesToShow&#34;:4}"
                        data-animate="fadeInUp">

                        <%=strTags %>
                    </div>
                </div>
                <div class="row d-md-none d-block">
                    <div class="col-md-6 mt-11 text-md-start" data-animate="fadeInUp">
                        <a href="/shop"
                            class="btn btn-link p-0 mt-2 text-decoration-none orange-text fw-semibold">Shop All Categories<i class="far fa-arrow-right ps-2 fs-13px"></i>
                        </a>
                    </div>
                </div>
            </div>
        </section>

        <section class="section-padding our-products" style="background: #fbeff1">
            <div class="container container-xxl mb-10 mb-lg-13 d-xl-flex">
                <div class="section-title flex-grow-1 text-left"
                    data-animate="fadeInUp">
                    <h2>Explore Our Products</h2>

                </div>
            </div>
            <div class="container container-xxl mb-4">
                <div class="slick-slider our-best-seller-4"
                    data-slick-options="{&#34;arrows&#34;:true,&#34;centerMode&#34;:true,&#34;centerPadding&#34;:&#34;calc((100% - 1440px) / 2)&#34;,&#34;dots&#34;:true,&#34;infinite&#34;:true,&#34;responsive&#34;:[{&#34;breakpoint&#34;:1200,&#34;settings&#34;:{&#34;arrows&#34;:false,&#34;dots&#34;:false,&#34;slidesToShow&#34;:3}},{&#34;breakpoint&#34;:992,&#34;settings&#34;:{&#34;arrows&#34;:false,&#34;dots&#34;:false,&#34;slidesToShow&#34;:2}},{&#34;breakpoint&#34;:576,&#34;settings&#34;:{&#34;arrows&#34;:false,&#34;dots&#34;:true,&#34;slidesToShow&#34;:2}}],&#34;slidesToShow&#34;:4}">
                    <%=strFeatureProducts %>
                    <%-- <div data-animate="fadeInUp">
                    <div class="card card-product grid-1 bg-transparent border-0">
                        <figure class="card-img-top position-relative mb-7 overflow-hidden">
                            <a href="#"
                               class="hover-zoom-in d-block"
                               title="Shield Conditioner">
                                <img src="#"
                                     data-src="assets/imgs/products/product-1.jpg"
                                     class="img-fluid lazy-image w-100"
                                     alt="Shield Conditioner"
                                     width="330"
                                     height="440" />
                            </a>
                        </figure>
                        <div class="card-body text-center p-0">
                            <div class="product-details">
                                <h3>AT Crystal Z+ 1296</h3>
                            </div>
                        </div>
                    </div>
                </div>
                <div data-animate='fadeInUp'>
                    <div class='card card-product grid-1 bg-transparent border-0'>
                        <figure class='card-img-top position-relative mb-7 overflow-hidden'>
                            <a href='#'
                               class='hover-zoom-in d-block'
                               title='Perfecting Facial Oil'>
                                <img src='#'
                                     data-src='assets/imgs/products/product-2.jpg'
                                     class='img-fluid lazy-image w-100'
                                     alt='Perfecting Facial Oil'
                                     width='330'
                                     height='440' />
                            </a>
                        </figure>
                        <div class='card-body text-center p-0'>
                            <div class='product-details'>
                                <h3>AT Crystal Z+ 1296</h3>
                            </div>
                        </div>
                    </div>
                </div>
                <div data-animate="fadeInUp">
                    <div class="card card-product grid-1 bg-transparent border-0">
                        <figure class="card-img-top position-relative mb-7 overflow-hidden">
                            <a href="#"
                               class="hover-zoom-in d-block"
                               title="Enriched Hand &amp; Body Wash">
                                <img src="#"
                                     data-src="assets/imgs/products/product-3.jpg"
                                     class="img-fluid lazy-image w-100"
                                     alt="Enriched Hand &amp; Body Wash"
                                     width="330"
                                     height="440" />
                            </a>

                            <div class="position-absolute product-flash z-index-2">
                                <span class="badge badge-product-flash on-new">New</span>
                            </div>

                        </figure>
                        <div class="card-body text-center p-0">
                            <div class="product-details">
                                <h3>AT Crystal Z+ 1296</h3>
                            </div>
                        </div>
                    </div>
                </div>
                <div data-animate="fadeInUp">
                    <div class="card card-product grid-1 bg-transparent border-0">
                        <figure class="card-img-top position-relative mb-7 overflow-hidden">
                            <a href="#"
                               class="hover-zoom-in d-block"
                               title="Shield Shampoo">
                                <img src="#"
                                     data-src="assets/imgs/products/product-4.jpg"
                                     class="img-fluid lazy-image w-100"
                                     alt="Shield Shampoo"
                                     width="330"
                                     height="440" />
                            </a>



                        </figure>
                        <div class="card-body text-center p-0">
                            <div class="product-details">
                                <h3>AT Crystal Z+ 1296</h3>
                            </div>
                        </div>
                    </div>
                </div>
                <div data-animate="fadeInUp">
                    <div class="card card-product grid-1 bg-transparent border-0">
                        <figure class="card-img-top position-relative mb-7 overflow-hidden">
                            <a href="#"
                               class="hover-zoom-in d-block"
                               title="Enriched Hand Wash">
                                <img src="#"
                                     data-src="assets/imgs/products/product-5.jpg"
                                     class="img-fluid lazy-image w-100"
                                     alt="Enriched Hand Wash"
                                     width="330"
                                     height="440" />
                            </a>



                        </figure>
                        <div class="card-body text-center p-0">
                            <div class="product-details">
                                <h3>AT Crystal Z+ 1296</h3>
                            </div>
                        </div>
                    </div>
                </div>
                <div data-animate="fadeInUp">
                    <div class="card card-product grid-1 bg-transparent border-0">
                        <figure class="card-img-top position-relative mb-7 overflow-hidden">
                            <a href="#"
                               class="hover-zoom-in d-block"
                               title="Enriched Duo">
                                <img src="#"
                                     data-src="assets/imgs/products/product-6.jpg"
                                     class="img-fluid lazy-image w-100"
                                     alt="Enriched Duo"
                                     width="330"
                                     height="440" />
                            </a>


                        </figure>
                        <div class="card-body text-center p-0">
                            <div class="product-details">
                                <h3>AT Crystal Z+ 1296</h3>
                            </div>
                        </div>
                    </div>
                </div>
                <div data-animate="fadeInUp">
                    <div class="card card-product grid-1 bg-transparent border-0">
                        <figure class="card-img-top position-relative mb-7 overflow-hidden">
                            <a href="#"
                               class="hover-zoom-in d-block"
                               title="Shield Spray">
                                <img src="#"
                                     data-src="assets/imgs/products/product-7.jpg"
                                     class="img-fluid lazy-image w-100"
                                     alt="Shield Spray"
                                     width="330"
                                     height="440" />
                            </a>

                        </figure>
                        <div class="card-body text-center p-0">
                            <div class="product-details">
                                <h3>AT Crystal Z+ 1296</h3>
                            </div>
                        </div>
                    </div>
                </div>--%>
                </div>
            </div>
        </section>

        <section class="why-choose-us section-padding pb-md-5 d-none">
            <div class="container container-xxl">
                <div class="row align-items-center justify-content-between">
                    <div class="col-lg-6 position-relative order-lg-0 order-1" data-animate="fadeIn">
                        <div class="about-img">
                            <img class="img-1" src="assets/imgs/about-img-1.png" alt="Premium wood veneers" />
                            <img class="img-2" src="assets/imgs/about-img-2.png" alt="Wooden wall panel and furniture" />
                            <img class="img-3" src="assets/imgs/about-img-3.png" alt="Wood veneer cabinet" />
                        </div>
                    </div>
                    <div class="col-lg-6 px-lg-10 py-lg-0 py-0 order-lg-1 order-0 new-bottom-why" data-animate="fadeInUp">
                        <div class="text-left ">
                            <div class="section-title">
                                <h2>Why Shop with Archidply Decor?</h2>
                                <p>
                                    We give you a million of reasons to choose us as your
                                interior designing partner! The optimum quality of products,
                                prices that fit your budget, the look that ravishes your
                                visitors, and the guarantees with which you can invest with
                                us, all is offered interactively to you and hundreds of
                                architects across the globe.­
                                </p>
                            </div>
                            <div class="why-choose-content">
                                <h4>Archidlam Surfaces</h4>
                                <ul>
                                    <li>
                                        <i class="fa-solid fa-check-circle"></i><span>Streamlined Shipping Experience</span>
                                    </li>
                                    <li>
                                        <i class="fa-solid fa-check-circle"></i><span>Affordable Modern Design</span>
                                    </li>
                                    <li>
                                        <i class="fa-solid fa-check-circle"></i><span>Competitive Price & Easy To Shop</span>
                                    </li>
                                    <li>
                                        <i class="fa-solid fa-check-circle"></i><span>We Made Awesome Products</span>
                                    </li>
                                </ul>
                            </div>
                        </div>

                        <a href="about-us" class="mt-3 btn green-btn shadow-sm">About Us <i class="fa-solid fa-arrow-circle-right"></i>
                        </a>
                    </div>
                </div>

            </div>
        </section>



        <section class="ap-testimonials testimonial-home" style="background-color: #f8f8f8">
            <div class="container  pt-10 pb-15 pt-lg-10 pb-lg-10">
                <div class="text-center">
                    <h2 class="mb-6 new-head-sm">What Our Client's Says</h2>
                </div>

                <div class="slick-slider related-products" data-slick-options="{&#34;arrows&#34;:true,&#34;centerPadding&#34;:&#34;calc((100% - 1440px) / 2)&#34;,&#34;dots&#34;:true,&#34;infinite&#34;:true,&#34;responsive&#34;:[{&#34;breakpoint&#34;:1200,&#34;settings&#34;:{&#34;arrows&#34;:false,&#34;dots&#34;:false,&#34;slidesToShow&#34;:3}},{&#34;breakpoint&#34;:992,&#34;settings&#34;:{&#34;arrows&#34;:false,&#34;dots&#34;:false,&#34;slidesToShow&#34;:2}},{&#34;breakpoint&#34;:576,&#34;settings&#34;:{&#34;arrows&#34;:false,&#34;dots&#34;:true,&#34;slidesToShow&#34;:1}}],&#34;slidesToShow&#34;:4}">

                    <%=strClientStories%>
                    <%-- <div class="mb-6">
                    <div class="card card-product grid-2 bg-transparent border-0">
                        <div class="testimonial-card" data-animate="fadeInUp">
                            <p>
                                I’ve been associated with Archidply for 40 years, and
                                throughout this time, they’ve treated me like family. I take
                                great pride in being their dealer for such a lengthy period.
                            </p>
                            <div class="meta-details">
                                <h4>Ashish Kamra</h4>
                                <span> Keshav Infra, Mohali</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="mb-6">
                    <div class="card card-product grid-2 bg-transparent border-0">

                        <div class="testimonial-card" data-animate="fadeInUp">
                            <p>
                                For more than 20 years, my association with Archidply
                                Industries has been marked by satisfaction and appreciation
                                for their ethical and transparent practices.
                            </p>
                            <div class="meta-details">
                                <h4>Mr.Vaishal Shah</h4>
                                <span> Adinath Plywood Centre, Ahmedabad</span>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="mb-6">
                    <div class="card card-product grid-2 bg-transparent border-0">
                        <div class="testimonial-card" data-animate="fadeInUp">
                            <p>
                                For the past two decades, Perfect Ply n Wood has been in
                                collaboration with Archidply Industries. While we have
                                partnered with various top brands, our strongest allegiance
                                lies with Archidply.
                            </p>
                            <div class="meta-details">
                                <h4>Bhavesh Sandha</h4>
                                <span> Perfect ply n wood Mumbai</span>  <br />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="mb-6">
                    <div class="testimonial-card" data-animate="fadeInUp">
                        <p>
                            We have been associated with Archidply since a long time ,
                            since they are having entire product range, quality of the
                            products are excellent.
                        </p>
                        <div class="meta-details">
                            <h4>Mr. Ajuvindra Singh</h4>
                            <span> Faquih & Associates India, Mumbai</span>
                            <br />
                        </div>
                    </div>
                </div>
                <div class="mb-6">
                    <div class="testimonial-card" data-animate="fadeInUp">
                        <p>
                            We have been associated with Archidply since a long time ,
                            since they are having entire product range, quality of the
                            products are excellent.
                        </p>
                        <div class="meta-details">
                            <h4>Mr. Ajuvindra Singh</h4>
                            <span> Faquih & Associates India, Mumbai</span>  <br />
                        </div>
                    </div>
                </div>


                <div class="mb-6">
                    <div class="testimonial-card" data-animate="fadeInUp">
                        <p>
                            We have been associated with Archidply since a long time ,
                            since they are having entire product range, quality of the
                            products are excellent.
                        </p>
                        <div class="meta-details">
                            <h4>Mr. Ajuvindra Singh</h4>
                            <span> Faquih & Associates India, Mumbai</span>  <br />
                        </div>
                    </div>
                </div>--%>
                </div>

            </div>
        </section>


        <section class="our-counters">
            <div class="video-wrapper">
                <video src="assets/imgs/ban1/bgvideo.mp4" muted autoplay loop playsinline></video>
            </div>
            <div class="container container-xxl">
                <div class="ap-counters">
                    <div class="ap-counter-card">
                        <div class="new-counter">
                            <div class="counter">40</div>
                            <span>+</span>
                        </div>
                        <div class="counter-text">Years of existence</div>
                    </div>
                    <div class="ap-counter-card">
                        <div class="new-counter">
                            <div class="counter">5</div>
                            <span>Lac +</span>
                        </div>
                        <div class="counter-text">Veneer sheet per annum</div>
                    </div>
                    <div class="ap-counter-card">
                        <div class="new-counter">
                            <div class="counter">3</div>
                            <span>Lac +</span>
                        </div>
                        <div class="counter-text">Prelam Boards per Annum </div>
                    </div>
                    <div class="ap-counter-card">
                        <div class="new-counter">
                            <div class="counter">100</div>
                            <span>+</span>
                        </div>
                        <div class="counter-text">Trucks plywood per month</div>
                    </div>
                </div>
            </div>
        </section>

        <section id="with_client_logo_1" class="certification section-padding">
            <div class="container">
                <div class="row mt-5 mb-15">
                    <div class="col-lg-9 offset-lg-1 col-xl-8 offset-xl-2">
                        <div class="section-title">
                            <h2 class="text-center green-text">Certifications</h2>
                        </div>
                    </div>
                    <div class="col-lg-12 ">
                        <div class="text-center" data-animate="fadeInUp">
                            <h4 class="mb-0 new-style1  ">"Precision and perfection in every detail"
                            </h4>
                        </div>
                    </div>
                </div>

                <div class="slick-slider thumb"
                    data-slick-options='{"slidesToShow": 6,"focusOnSelect": true,"arrows": false, "dots": false, "responsive":[{"breakpoint":992,"settings":{"dots":true,"slidesToShow":4}},{"breakpoint":768,"settings":{"dots":true,"slidesToShow":3}},{"breakpoint":576,"settings":{"dots":true,"slidesToShow":3}}] }'>

                    <%--multi imgs--%>
                    <div class="client-logo-item" data-animate="fadeInUp">
                        <a class="lh-1 color-inherit text-decoration-none certi-trigger"
                            href="#"
                            data-bs-toggle="modal"
                            data-bs-target="#certimodal"
                            data-title="GREEN CERTIFICATES"
                            data-image="Plywood GreenPro|images_/certificates/PlygreenproC.jpg, 
                            Decorative Veneer GreenPro|images_/certificates/VengreenproC.jpg, 
                            Prelaminated MDF GreenPro|images_/certificates/PrelamgreenproCMDF.jpg, 
                           Doors GreenPro|images_/certificates/DoorgreenproC.jpg, 
                            Prelaminated Particle Board GreenPro|images_/certificates/PrelamPartgreenproC.jpg">
                            <img class="w-auto mx-auto light-mode-img"
                                src="images_/certificates/greenpro.jpg"
                                alt="GreenPro certified plywood" />
                        </a>
                    </div>
                    <%--pdf--%>
                    <div class="client-logo-item" data-animate="fadeInUp">
                        <a class="lh-1 color-inherit text-decoration-none certi-trigger"
                            href="#"
                            data-bs-toggle="modal"
                            data-bs-target="#certimodal"
                            data-title="BUREAU OF INDIAN STANDARDS"
                            data-image="License 2202 door|images_/certificates/License%202202%20door.pdf,
                            Licence 12823 Prelam|images_/certificates/Lisence%2012823%20Prelam.pdf,
                            License 710 BWP Plywood|images_/certificates/License%20710.pdf,
                            Licence 303 Plywood|images_/certificates/Licence%20303%20.pdf,
                        1328 licence Decorative Veneer|images_/certificates/1328%20lisence%20Decorative%20Veneer.pdf,
                            ">
                            <img class="w-auto mx-auto light-mode-img"
                                src="images_/certificates/bis.png"
                                alt="BIS certified plywood" />
                        </a>

                    </div>

                    <div class="client-logo-item" data-animate="fadeInUp">
                        <a class="lh-1 color-inherit text-decoration-none certi-trigger"
                            href="#"
                            data-bs-toggle="modal"
                            data-bs-target="#certimodal"
                            data-title="Certification"
                            data-image="Archidply Decor Ltd|images_/certificates/Archidply%20Decor%20Ltd-%20IGBC.pdf">
                            <img class="w-auto mx-auto light-mode-img"
                                src="images_/certificates/igbc.jpg"
                                alt="IGBC certified plywood" />
                        </a>
                    </div>
                    <div class="client-logo-item" data-animate="fadeInUp">
                        <a class="lh-1 color-inherit text-decoration-none certi-trigger"
                            href="#"
                            data-bs-toggle="modal"
                            data-bs-target="#certimodal"
                            data-title="Warranty certificates"
                            data-image="Pure Pro|images_/certificates/Warranty%20Certficate_Pure%20Pro.pdf,
    Pure Pro Plus|images_/certificates/Warranty%20Certficate_Pure%20Pro%20Plus_2024_A4.pdf,
    Pure Gold|images_/certificates/Warranty%20Certficate_Pure%20Gold_2024_A4.pdf,
    Pure 16|images_/certificates/Warranty%20Certficate_Pure%2016_2024_A4.pdf,
    Pure 16 Plus|images_/certificates/Warranty%20Certficate_Pure%2016%20Plus.pdf,
    Club Plus|images_/certificates/Lifetime_Warranty_ClubPlus_2024_A4.pdf,
    Archidply Club|images_/certificates/ArchidPly_Club_31%20Years_2024_A4.pdf,
    Bonvivant|images_/certificates/Lifetime_Warranty_Bonvivant_2024_A4.pdf,
    ">
                            <img class="w-auto mx-auto light-mode-img"
                                src="images_/certificates/warrenty4.png"
                                alt="Warranty assurance" />
                        </a>
                    </div>
                    <%-- <div class="client-logo-item" data-animate="fadeInUp">
                        <a class="lh-1 color-inherit text-decoration-none certi-trigger"
                            href="#"
                            data-bs-toggle="modal"
                            data-bs-target="#certimodal"
                            data-title="Certification"
                            data-image="Updated soon">
                            <img class="w-auto mx-auto light-mode-img"
                                src="images_/certificates/isi.png"
                                alt="ISI" />
                        </a>
                    </div>--%>
                    <div class="client-logo-item" data-animate="fadeInUp">
                        <a class="lh-1 color-inherit text-decoration-none certi-trigger"
                            href="javascript:void();"
                            data-bs-toggle="modal"
                            data-bs-target="javascript:void();"
                            data-title=" Certification"
                            data-image="Updated soon">
                            <img class="w-auto mx-auto light-mode-img"
                                src="images_/certificates/indonesian-legel-wood.png"
                                alt="Indonesian Legal Wood" />
                        </a>
                    </div>

                    <div class="client-logo-item" data-animate="fadeInUp">
                        <a class="lh-1 color-inherit text-decoration-none certi-trigger"
                            href="javascript:void();"
                            data-bs-toggle="modal"
                            data-bs-target="javascript:void();"
                            data-title="Certification"
                            data-image="Updated soon">
                            <img class="w-auto mx-auto light-mode-img"
                                src="images_/certificates/pefc.png"
                                alt="PEFC certified plywood" />
                        </a>
                    </div>
                </div>
            </div>
        </section>
        <section id="from_our_blog" class="our-blog section-padding" style="background: #f8f8f8">
            <div class="container">
                <div class="text-center section-title" data-animate="fadeInUp">
                    <h2 class="mb-6">Product Stories</h2>
                    <p class="new-style">
                        Elevate and revolutionize your space with our contemporary decorative veneers.
                    </p>
                </div>
            </div>
            <div class="container container-xxl mt-12 pt-3">
                <div class="slick-slider" data-slick-options="{&#34;arrows&#34;:false,&#34;dots&#34;:false,&#34;responsive&#34;:[{&#34;breakpoint&#34;:1200,&#34;settings&#34;:{&#34;slidesToShow&#34;:1}},{&#34;breakpoint&#34;:992,&#34;settings&#34;:{&#34;dots&#34;:true,&#34;slidesToShow&#34;:1}},{&#34;breakpoint&#34;:768,&#34;settings&#34;:{&#34;dots&#34;:true,&#34;slidesToShow&#34;:1}}],&#34;slidesToShow&#34;:1}">
                    <%=strProductStories %>
                    <%--<div class='new-bg-product'>
                        <div class='row align-items-center justify-content-between '>
                            <div class='col-lg-6 px-lg-10 py-lg-0 py-13'>
                                <div class='text-left new-pro-stories'>
                                    <div class='section-title'>
                                        <h2>Bungalow, Bhopal
                                        </h2>
                                    </div>
                                </div>
                            </div>
                            <div class='col-lg-6 position-relative'>
                                <div class='about-img'>
                                    <div class='container-fluid'>
                                        <div class='px-md-6'>
                                          
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>

                    <%--  <div class="new-bg-product">
                        <div class="row align-items-center justify-content-between ">

                            <div class="col-lg-6 px-lg-10 py-lg-0 py-13">
                                <div class="text-left new-pro-stories">
                                    <div class="section-title">
                                        <h2>Bungalow, Bhopal
                                        </h2>
                                        <p>
                                            At Archidply, we see every project as a challenge that allows us to create something novel and breathtaking. And here’s how we used Archidply Club Plywood and Natural Decorative Veneers to add a touch of magic to this property.


                                        </p>
                                    </div>
                                    <div class="why-choose-content">
                                        <h5 class="">Other products used:
                                        </h5>
                                        <ul>
                                            <li>
                                                <i class="fa-solid fa-check-circle"></i><span>Royal Vintage Oak
                                                </span>
                                            </li>
                                            <li>
                                                <i class="fa-solid fa-check-circle"></i><span>Royal Burberry
                                                </span>
                                            </li>
                                            <li>
                                                <i class="fa-solid fa-check-circle"></i><span>Royal Euro Walnut
                                                </span>
                                            </li>

                                        </ul>
                                    </div>
                                </div>


                            </div>
                            <div class="col-lg-6 position-relative">
                                <div class="about-img">
                                    <div class="container-fluid">
                                        <div class="px-md-6">
                                            <div class="mx-n6 slick-slider" data-slick-options='{"slidesToShow": 1,"infinite":false,"autoplay":true,"dots":true,"arrows":false,"responsive":[{"breakpoint": 1366,"settings": {"slidesToShow":1 }},{"breakpoint": 992,"settings": {"slidesToShow":1}},{"breakpoint": 768,"settings": {"slidesToShow": 1}},{"breakpoint": 576,"settings": {"slidesToShow": 1}}]}'>

                                                <div class="px-6">
                                                    <a href="assets/imgs/ps/05-1.jpg" title="instagram-01" data-gallery="instagram1" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/05-1.jpg" alt="instagram-01" src="#">
                                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                                    </a>
                                                </div>

                                                <div class="px-6">
                                                    <a href="assets/imgs/ps/06-1.jpg" title="instagram-02" data-gallery="instagram1" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/06-1.jpg" alt="instagram-02" src="#">
                                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                                    </a>
                                                </div>

                                                <div class="px-6">
                                                    <a href="assets/imgs/ps/Bhopal1.jpg" title="instagram-03" data-gallery="instagram1" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/Bhopal1.jpg" alt="instagram-03" src="#">
                                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                                    </a>
                                                </div>
                                                <div class="px-6">
                                                    <a href="assets/imgs/ps/Bhopal3.jpg" title="instagram-03" data-gallery="instagram1" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/Bhopal3.jpg" alt="instagram-03" src="#">
                                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                                    </a>
                                                </div>


                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="new-bg-product">
                        <div class="row align-items-center justify-content-between ">

                            <div class="col-lg-6 px-lg-10 py-lg-0 py-13">
                                <div class="text-left new-pro-stories">
                                    <div class="section-title">
                                        <h2>Bungalow, Bhopal
                                        </h2>
                                        <p>
                                            At Archidply, we see every project as a challenge that allows us to create something novel and breathtaking. And here’s how we used Archidply Club Plywood and Natural Decorative Veneers to add a touch of magic to this property.


                                        </p>
                                    </div>
                                    <div class="why-choose-content">
                                        <h5 class="">Other products used:
                                        </h5>
                                        <ul>
                                            <li>
                                                <i class="fa-solid fa-check-circle"></i><span>Royal Vintage Oak
                                                </span>
                                            </li>
                                            <li>
                                                <i class="fa-solid fa-check-circle"></i><span>Royal Burberry
                                                </span>
                                            </li>
                                            <li>
                                                <i class="fa-solid fa-check-circle"></i><span>Royal Euro Walnut
                                                </span>
                                            </li>

                                        </ul>
                                    </div>
                                </div>


                            </div>
                            <div class="col-lg-6 position-relative">
                                <div class="about-img">
                                    <div class="container-fluid">
                                        <div class="px-md-6">
                                            <div class="mx-n6 slick-slider" data-slick-options='{"slidesToShow": 1,"infinite":false,"autoplay":true,"dots":true,"arrows":false,"responsive":[{"breakpoint": 1366,"settings": {"slidesToShow":1 }},{"breakpoint": 992,"settings": {"slidesToShow":1}},{"breakpoint": 768,"settings": {"slidesToShow": 1}},{"breakpoint": 576,"settings": {"slidesToShow": 1}}]}'>

                                                <div class="px-6">
                                                    <a href="assets/imgs/ps/05-1.jpg" title="instagram-01" data-gallery="instagram1" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/05-1.jpg" alt="instagram-01" src="#">
                                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                                    </a>
                                                </div>

                                                <div class="px-6">
                                                    <a href="assets/imgs/ps/06-1.jpg" title="instagram-02" data-gallery="instagram1" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/06-1.jpg" alt="instagram-02" src="#">
                                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                                    </a>
                                                </div>

                                                <div class="px-6">
                                                    <a href="assets/imgs/ps/Bhopal1.jpg" title="instagram-03" data-gallery="instagram1" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/Bhopal1.jpg" alt="instagram-03" src="#">
                                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                                    </a>
                                                </div>
                                                <div class="px-6">
                                                    <a href="assets/imgs/ps/Bhopal3.jpg" title="instagram-03" data-gallery="instagram1" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/Bhopal3.jpg" alt="instagram-03" src="#">
                                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                                    </a>
                                                </div>


                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>

                    <%-- <%=strBlog %>--%>

                    <%-- <div>
                    <article class="card card-post-grid-3 bg-transparent border-0"
                             data-animate="fadeInUp">
                        <figure class="card-img-top mb-8 position-relative">
                            <a href="#"
                               class="hover-shine hover-zoom-in d-block"
                               title="How to Plop Hair for Bouncy, Beautiful Curls">
                                <img data-src="assets/imgs/blog-1.jpg"
                                     class="img-fluid lazy-image w-100"
                                     alt="How to Plop Hair for Bouncy, Beautiful Curls"
                                     width="450"
                                     height="290"
                                     src="#" />
                            </a>
                        </figure>
                        <div class="card-body p-0">
                            <ul class="post-meta list-inline lh-1 d-flex flex-wrap fs-13px text-uppercase ls-1 fw-semibold m-0">

                                <li class="border-start list-inline-item">
                                    Jan 19, 2024
                                </li>
                            </ul>
                            <h4 class="card-title lh-base mt-5 pt-2 mb-0 fs-20px">
                                <a class="text-decoration-none"
                                   href="#"
                                   title="How to Plop Hair for Bouncy, Beautiful Curls">
                                    The Benefits of High Gloss Laminates in Modern Interior
                                    Design
                                </a>
                            </h4>
                        </div>
                    </article>
                </div>
                <div>
                    <article class="card card-post-grid-3 bg-transparent border-0"
                             data-animate="fadeInUp">
                        <figure class="card-img-top mb-8 position-relative">
                            <a href="#"
                               class="hover-shine hover-zoom-in d-block"
                               title="Which foundation formula is right for your skin?">
                                <img data-src="assets/imgs/blog-2.jpg"
                                     class="img-fluid lazy-image w-100"
                                     alt="Which foundation formula is right for your skin?"
                                     width="450"
                                     height="290"
                                     src="#" />
                            </a>
                        </figure>
                        <div class="card-body p-0">
                            <ul class="post-meta list-inline lh-1 d-flex flex-wrap fs-13px text-uppercase ls-1 fw-semibold m-0">

                                <li class="border-start list-inline-item">
                                    Jan 19, 2024
                                </li>
                            </ul>
                            <h4 class="card-title lh-base mt-5 pt-2 mb-0 fs-20px">
                                <a class="text-decoration-none"
                                   href="#"
                                   title="Which foundation formula is right for your skin?">Top 5 White Laminates for a Modern and Sleek Interior</a>
                            </h4>
                        </div>
                    </article>
                </div>
                <div>
                    <article class="card card-post-grid-3 bg-transparent border-0"
                             data-animate="fadeInUp">
                        <figure class="card-img-top mb-8 position-relative">
                            <a href="#"
                               class="hover-shine hover-zoom-in d-block"
                               title="How To Choose The Right Sofa for your home">
                                <img data-src="assets/imgs/blog-3.jpg"
                                     class="img-fluid lazy-image w-100"
                                     alt="How To Choose The Right Sofa for your home"
                                     width="450"
                                     height="290"
                                     src="#" />
                            </a>
                        </figure>
                        <div class="card-body p-0">
                            <ul class="post-meta list-inline lh-1 d-flex flex-wrap fs-13px text-uppercase ls-1 fw-semibold m-0">

                                <li class="border-start list-inline-item">
                                    Jan 19, 2024
                                </li>
                            </ul>
                            <h4 class="card-title lh-base mt-5 pt-2 mb-0 fs-20px">
                                <a class="text-decoration-none"
                                   href="#"
                                   title="How To Choose The Right Sofa for your home">How to select perfect laminate finish for your door</a>
                            </h4>
                        </div>
                    </article>
                </div>--%>
                </div>
                <div class="text-center pt-2" data-animate="fadeInUp">
                    <a href="/product-stories" class="mt-12 btn green-btn">View All Product Stories <i class="fa-solid fa-check-circle"></i>
                    </a>
                </div>
            </div>
        </section>

        <section id="mainCTASec">
            <div class=" bg-overlay w-100 ">
                <img src="/assets/imgs/ban2/7.png" class="w-100" alt="Plywood sideboard decorative finish" />
            </div>
            <div class="text-center z-index-2 textWrap">
                <div class="d-flex justify-content-center align-items-center w-100 h-100">
                    <div>
                        <h3 class="mb-11 font-primary fw-bold text-white">Timeless designs, expertly executed!
                        </h3>
                        <a href="contact-us" class="btn orange-btn rounded ms-0">Get in Touch <i class="fa-solid fa-check-circle"></i>
                        </a>
                    </div>
                </div>
            </div>

        </section>

        <%=strResources %>
        <%--<section class="pt-lg-10 pb-lg-10 bg-warning-hover">
            <div class="container">
                <div class="text-center section-title" data-animate="fadeInUp">
                    <h2 class="mb-6">Resources</h2>
                    <p class="">
                        Our bundles were designed to conveniently package your tanning essentials while saving you money.
                    </p>
                </div>
            </div>
            <div class="container-xxl mt-10">
                <div class="row">
                    
                    <div class="col-xl-3 col-md-6 animate__fadeInUp animate__animated" data-animate="fadeInUp">
                        <div class="icon-box icon-box-style-1 card border-0 text-center">
                            <div class="icon-box-icon card-img text-primary">
                                <img src="assets/imgs/nveneer.png" width="65" />
                            </div>
                            <div class="icon-box-content card-body pt-4">
                                <h3 class="icon-box-title card-title fs-5 mb-0 pb-0">Natural Veneers</h3>
                                <a href="https://archidplydecor.com/pdf/Bon-Vivant-Premium-Decorative-Veneer-Collection.pdf" target="_blank" class="btn btn-link p-0 mt-2 text-decoration-none green-text fw-semibold">Download<i class="far fa-download ps-2 fs-13px"></i>
                                </a>
                            </div>
                        </div>

                    </div>
                    <div class="col-xl-3 col-md-6 animate__fadeInUp animate__animated" data-animate="fadeInUp">
                        <div class="icon-box icon-box-style-1 card border-0 text-center">
                            <div class="icon-box-icon card-img text-primary">
                                <img src="assets/imgs/sliding-door.png" width="65" />
                            </div>
                            <div class="icon-box-content card-body pt-4">
                                <h3 class="icon-box-title card-title fs-5 mb-0 pb-0">Teak Veneer</h3>
                                <a href="https://archidplydecor.com/pdf/Natural-Veneers.pdf" class="btn btn-link p-0 mt-2 text-decoration-none green-text fw-semibold">Download<i class="far fa-download ps-2 fs-13px"></i>
                                </a>
                            </div>
                        </div>

                    </div>
                    <div class="col-xl-3 col-md-6 animate__fadeInUp animate__animated" data-animate="fadeInUp">
                        <div class="icon-box icon-box-style-1 card border-0 text-center">
                            <div class="icon-box-icon card-img text-primary">
                                <img src="assets/imgs/sliding-door.png" width="65" />
                            </div>
                            <div class="icon-box-content card-body pt-4">
                                <h3 class="icon-box-title card-title fs-5 mb-0 pb-0">Materials, finishes, and services</h3>
                                <a href="https://archidplydecor.com/pdf/Archidply-Brochure.pdf" target="_blank" class="btn btn-link p-0 mt-2 text-decoration-none green-text fw-semibold">Download<i class="far fa-download ps-2 fs-13px"></i>
                                </a>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </section>--%>
        <%--<section class="bg-section-2 pt-lg-17 pb-lg-17 pt-15 pb-15" style="background: #fff2d6">
            <div class="container text-center">
                <div class="mx-auto mb-11 text-center" style="max-width: 500px" data-animate="fadeInUp">
                    <h3 class="mb-6 new-cta-font">Stay Up to Date with All News and Exclusive Offers</h3>
                </div>
                <div class="mx-auto" style="max-width: 546px" data-animate="fadeInUp">
                    <div class="text-center">
                        <asp:UpdatePanel runat="server" class="input-group position-relative mb-11 form-border-transparent">
                            <ContentTemplate>
                                <asp:TextBox type="text" runat="server" ID="txtSubscribeEmail" CssClass="form-control bg-body rounded-left" MaxLength="100" placeholder="Enter your email address" />
                                <asp:Button runat="server" Text="Subscribe" ID="Subscribe" OnClick="Subscribe_Click" ValidationGroup="sub" CssClass=" btn btn-dark btn-hover-bg-primary btn-hover-border-primary rounded ms-0" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtSubscribeEmail" SetFocusOnError="true" ValidationGroup="sub" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Enter a  valid Email address" ValidationGroup="sub" ControlToValidate="txtSubscribeEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Subscribe" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </section>--%>

        <section class="section-padding">
            <div class="container-fluid">
                <div class="row gy-4">
                    <div class="col-lg-6" data-animate="fadeInRight">
                        <div class="fact-card">
                            <div class="bg-image new-fact-img video-01 d-flex justify-content-center align-items-center  position-relative py-8 py-lg-0 py-md-23 lazy-bg"
                                data-bg-src="images_/show/factory.jpg" style="height: 400px" alt="Plywood factory">

                                <a href="https://youtu.be/PZIJHQk4Z9A" class="view-video iframe-link video-btn d-flex justify-content-center align-items-center fs-30px lh-115px btn btn-outline-light border border-white border-2 rounded-circle transition-all-1">
                                    <svg class="icon">
                                        <use xlink:href="#icon-play-fill"></use></svg></a>

                            </div>
                            <span>Factory</span>
                        </div>
                    </div>
                    <div class="col-lg-6" data-animate="fadeInRight">
                        <div class="fact-card">
                            <div class="bg-image new-fact-img video-01 d-flex justify-content-center align-items-center position-relative py-8  py-lg-0 py-md-23 lazy-bg"
                                data-bg-src="images_/s1/SDP_4622.jpg" alt="Plywood & laminate showroom display" style="height: 400px">
                                <a href="https://youtu.be/vwW0iTGBohU?si=L20-Yrq4jZyZmMxI" class="view-video iframe-link video-btn d-flex justify-content-center align-items-center fs-30px lh-115px btn btn-outline-light border border-white border-2 rounded-circle transition-all-1">
                                    <svg class="icon">
                                        <use xlink:href="#icon-play-fill"></use></svg></a>





                            </div>
                            <span>Showroom</span>
                        </div>
                    </div>
                </div>

            </div>
        </section>
        <div class="whatsapp-fixed">
            <div class="whatsapp-icon">
                <a href="https://wa.me/+917022012573" target="_blank"><i class="fa-brands fa-whatsapp"></i></a>
            </div>
        </div>
        <div class="whatsapp-fixed1 d-lg-none d-block">
            <div class="whatsapp-icon1">
                <a href="tel:+917022012573" target="_blank"><i class="fa-solid fa-phone"></i></a>
            </div>
        </div>
        <div class="call-fixed d-lg-block d-none">
            <div class="call-fixed-icon">
                <a href="tel:+917022012573" target="_blank">
                    <svg class="icon text-primary fs-1">
                        <use xlink:href="#icon-box-05"></use>
                    </svg>
                    +91 7022012573</a>
            </div>
        </div>


        <%--Qucik enquiry modal--%>
        <div class="modal fade" id="quickEnquiryModal" tabindex="-1" data-bs-backdrop="static" aria-labelledby="quickEnquiryModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content m-content">
                    <div class="modal-header m-header">
                        <h5 class="modal-title m-title mt-0" id="quickEnquiryModalLabel">Quick Enquiry</h5>
                        <button type="button" id="btn-closee" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body m-body">
                        <div id="enquiryForm">
                            <div class="mb-3">
                                <label class="form-label">Name<sup class="text-danger">*</sup></label>
                                <input type="text" id="txtName" class="form-control acceptOnlyAlpha" placeholder="Enter your name">
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Contact No<sup class="text-danger">*</sup></label>
                                <input type="text" id="txtPhone" class="form-control onlyNum" placeholder="Enter your contact no" maxlength="10">
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Email<sup class="text-danger">*</sup></label>
                                <input type="email" id="txtEmail" class="form-control" placeholder="Enter your email">
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Message</label>
                                <textarea id="txtMessage" class="form-control" rows="3" placeholder="Enter your message"></textarea>
                            </div>
                            <button type="submit" id="btnSendMessage" class="btn btn-primary w-100 mt-3">Send Message</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<div class="modal fade" id="quickEnquiryModal" tabindex="-1" data-bs-backdrop="static" aria-labelledby="quickEnquiryModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content m-content">
                    <div class="modal-header m-header">
                        <h5 class="modal-title m-title mt-0" id="quickEnquiryModalLabel">Quick Enquiry</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body m-body">
                         <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="enquiryForm">
                                    <div class="mb-6 mb-lg-3">
                                        <label for="name" class="form-label">Name<sup class="text-danger">*</sup></label>
                                        <asp:TextBox ID="txtName" runat="server" MaxLength="100" CssClass="form-control acceptOnlyAlpha" Placeholder="Enter your name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtName" SetFocusOnError="true" ValidationGroup="quick" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="mb-6 mb-lg-3">
                                        <label for="email" class="form-label">Contact No<sup class="text-danger">*</sup></label>
                                        <asp:TextBox ID="txtPhone" runat="server" MaxLength="10" CssClass="form-control onlyNum" Placeholder="Enter your contact no"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv4" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhone" SetFocusOnError="true" ValidationGroup="quick" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPhone" Display="Dynamic" ValidationGroup="quick" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Enter 10 Digit Valid Phone Number" ValidationExpression="^\d{10}$"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="mb-6 mb-lg-3">
                                        <label for="email" class="form-label">Email<sup class="text-danger">*</sup></label>
                                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="150" CssClass="form-control" Placeholder="Enter your email"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmail" SetFocusOnError="true" ValidationGroup="quick" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="quick" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Invalid E-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="mb-6 mb-lg-3">
                                        <label for="message" class="form-label">Message</label>
                                        <asp:TextBox ID="txtMessage" runat="server" MaxLength="300" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Enter your message"></asp:TextBox>
                                    </div>
                                    <asp:Button ID="btnSendMessage" ValidationGroup="quick" runat="server" CssClass="btn btn-primary w-100 mt-10" Text="Send Message" OnClick="btnSendMessage_click" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                 <asp:AsyncPostBackTrigger EventName="Click" ControlID="btnSendMessage" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>--%>

        <%--Resource dowmnlod modal--%>
        <div class="modal fade sucess" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                <div class="modal-content">
                    <div class="modal-header text-center border-0 pb-0">
                        <button type="button"
                            class="btn-close position-absolute end-5 top-5"
                            data-bs-dismiss="modal"
                            aria-label="Close">
                        </button>
                        <h3 class="modal-title w-100" id="signInModalLabel">Download Resources</h3>
                    </div>
                    <div class="modal-body px-sm-13 px-8">
                        <p class="text-center fs-16 mb-10">
                            By filling the below form you can download resource
                        </p>
                        <div action="#">
                            <div class="error-message alert alert-danger d-block d-none fw-bold">
                            </div>

                            <asp:TextBox ID="textFname" runat="server" class="form-control textFname mb-7 acceptOnlyAlpha" MaxLength="100" placeholder="Full Name*"></asp:TextBox>


                            <asp:TextBox ID="txtemailAdress" class="form-control txtemailAdress mb-7" runat="server" MaxLength="100" placeholder="Email*"></asp:TextBox>


                            <asp:TextBox ID="txtContact" class="form-control txtContact mb-7 numberOnlyInput" runat="server" MaxLength="10" placeholder="Contact Number*"></asp:TextBox>


                            <asp:TextBox ID="txtProfession" class="form-control txtProfession mb-7" runat="server" MaxLength="100" placeholder="Profession*"></asp:TextBox>

                            <asp:LinkButton runat="server" ID="BtnSubmit" ValidationGroup="dnlodRes" CssClass="btn btn-dark btn-hover-bg-primary btn-hover-border-primary mt-10 w-100 mb-6 submitdata BtnSubmit">Download<i class="far fa-download ps-2 fs-13px"></i></asp:LinkButton>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal for certificate-->
        <div class="modal fade" id="certimodal" tabindex="-1" aria-labelledby="certimodal" aria-hidden="true">
            <div class="modal-dialog modal-xl modal-dialog-centered modal-dialog-scrollable">
                <div class="modal-content">
                    <div class="modal-header text-center border-0 pb-0">
                        <button type="button"
                            class="btn-close position-absolute end-5 top-5"
                            data-bs-dismiss="modal"
                            aria-label="Close">
                        </button>
                        <h3 class="modal-title w-100 text-center" id="certiTitle">Certificate Title</h3>
                    </div>
                    <div class="modal-body px-sm-13 px-8 text-center">
                        <div id="certiContent" class="d-flex flex-column align-items-center">
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script src="/Admin/assets/libs/snackbar/snackbar.min.js"></script>
        <script src="/assets/js/Pages/defaultpage.min.js"></script>
        <script>

            document.addEventListener("DOMContentLoaded", function () {
                document.querySelectorAll(".certi-trigger").forEach(item => {
                    item.addEventListener("click", function () {
                        let modalTitle = this.getAttribute("data-title");
                        let imageData = this.getAttribute("data-image");
                        let certiContent = document.getElementById("certiContent");

                        document.getElementById("certiTitle").textContent = modalTitle;
                        certiContent.innerHTML = "";

                        let filesArray = imageData.split(",");

                        filesArray.forEach((fileData, index) => {
                            let [fileTitle, fileSrc] = fileData.trim().split("|");

                            let titleElement = document.createElement("h4");
                            titleElement.textContent = fileTitle;
                            titleElement.classList.add("text-center", "mt-10");

                            certiContent.appendChild(titleElement);

                            if (fileSrc.toLowerCase().endsWith(".pdf")) {
                                let pdfEmbed = document.createElement("embed");
                                pdfEmbed.src = fileSrc.trim();
                                pdfEmbed.type = "application/pdf";
                                pdfEmbed.width = "100%";
                                pdfEmbed.height = "500px";
                                certiContent.appendChild(pdfEmbed);
                            } else {
                                let imgElement = document.createElement("img");
                                imgElement.src = fileSrc.trim();
                                imgElement.classList.add("img-fluid", "mb-3");
                                certiContent.appendChild(imgElement);
                            }

                            if (index !== filesArray.length - 1) {
                                let hrElement = document.createElement("hr");
                                hrElement.classList.add("customeHR");
                                certiContent.appendChild(hrElement);
                            }
                        });
                    });
                });
            });


            document.addEventListener("DOMContentLoaded", function () {
                if (!sessionStorage.getItem("modalShown")) {
                    setTimeout(function () {
                        const myModal = new bootstrap.Modal(document.getElementById('quickEnquiryModal'));
                        myModal.show();
                        sessionStorage.setItem("modalShown", "true");
                    }, 8000);
                }
            });





        </script>
    </main>

</asp:Content>

