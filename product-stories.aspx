<%@ Page Title="ArchidPly Product Stories " Language="C#" MasterPageFile="./UserMaster.master" AutoEventWireup="true" CodeFile="product-stories.aspx.cs" Inherits="product_stories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <meta name="keywords" content="ArchidPly Decor, plywood company in india, premium plywood, product stories, laminated plywood, plywood innovation, furniture plywood, top plywood company, eco-friendly plywood, plywood manufacturers" />

<meta name="description" content="Explore product stories from ArchidPly Decor – innovation, durability & design insights for India's best plywood. Read our journey now " />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/lity/2.4.1/lity.min.css" integrity="sha512-UiVP2uTd2EwFRqPM4IzVXuSFAzw+Vo84jxICHVbOA1VZFUyr4a6giD9O3uvGPFIuB2p3iTnfDVLnkdY7D/SJJQ==" crossorigin="anonymous" referrerpolicy="no-referrer" />
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
    <style>
        .section-padding {
            padding: 20px 0px;
        }

        .why-choose-content h5 {
            margin-bottom: 15px;
        }

        .why-choose-content ul li {
            margin-bottom: 5px;
        }

        .why-choose-content ul {
            display: grid;
            grid-template-columns: repeat(1, 1fr);
            gap: 10px 10px;
            list-style: none;
            margin: 0;
            padding: 0;
        }

        .new-pad {
            padding: 40px 0px;
        }

        .section-title h4 {
            font-size: 20px !important;
            line-height: 30px !important;
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
         .bg-overlay::before{
     background:unset !important;
 }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main id="content" class="wrapper layout-page overflow-hidden investor-contact  pb-10" style="background: #f1efec url('assets/imgs/bg-img2.png'); background-repeat: no-repeat; background-position: right; background-attachment: fixed">

        <section class="position-relative custom-overlay  new-banner-head" id="about_introduction">

            <div class="lazy-bg bg-overlay position-absolute z-index-1 w-100 h-100   light-mode-img" data-bg-src="assets/imgs/ban1/2.png">
            </div>
            <div class="lazy-bg bg-overlay dark-mode-img position-absolute z-index-1 w-100 h-100" data-bg-src="assets/imgs/ban1/2.png">
            </div>

    <div class="position-relative z-index-2 container py-10 py-md-15 py-lg-22">

                <h1 class="fs-56px mb-0 text-white text-center">Product Stories
                </h1>
            </div>
        </section>
        <div class="new-pad">
            <div id="StoryListBindingSec"></div>
            <div class="row mt-12">
                <div class="col-12 text-center">
                    <ul class="pagination pPagination justify-content-center">
                    </ul>
                </div>
            </div>
        </div>
    </main>

    <%--  <div id="StoryListBindingSec"></div>
                                <div class="row mt-12">
    <div class="col-12 text-center">
        <ul class="pagination pPagination justify-content-center">
        </ul>
    </div>
</div>--%>

    <%--    <section class="why-choose-us section-padding ">
        <div class="container container-xxl">
            <div class="row align-items-center justify-content-between">

                <div class="col-lg-6 px-lg-10 py-lg-0 py-13">
                    <div class="text-left">
                        <div class="section-title">
                            <h2>Bungalow, Bhopal
                            </h2>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6 position-relative">
                    <div class="about-img">
                        <div class="container-fluid">
                            <div class="px-md-6">
                                <div class="mx-n6 slick-slider" data-slick-options='{"slidesToShow": 1,"infinite":false,"autoplay":true,"dots":true,"arrows":false,"responsive":[{"breakpoint": 1366,"settings": {"slidesToShow":1 }},{"breakpoint": 992,"settings": {"slidesToShow":1}},{"breakpoint": 768,"settings": {"slidesToShow": 1}},{"breakpoint": 576,"settings": {"slidesToShow": 1}}]}'>

                                    <a href="assets/imgs/ps/Bhopal1.jpg" data-lity>
                                        <img src="assets/imgs/ps/Bhopal1.jpg" alt="Your Image Description">
                                    </a>
                                    <a href="assets/imgs/ps/Bhopal3.jpg" data-lity>
                                        <img src="assets/imgs/ps/Bhopal3.jpg" alt="Your Image Description">
                                    </a>

                                    <a href="https://www.youtube.com/embed/4ORbpY6d9Zk?si=s_s67Cv7ao4yVchT" data-lity>
                                        <img src="assets/imgs/ps/bangalore1.jpg" alt="Your Image Description">
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="why-choose-us section-padding">
    <div class="container container-xxl">
        <div class="row align-items-center justify-content-between">

            <!-- Left Side Content -->
            <div class="col-lg-6 px-lg-10 py-lg-0 py-13">
                <div class="text-left">
                    <div class="section-title">
                        <h2>Bungalow, Bhopal</h2>
                        <p>At Archidply, we see every project as a challenge that allows us to create something novel
                            and breathtaking. And here’s how we used Archidply Club Plywood and Natural Decorative
                            Veneers to add a touch of magic to this property.</p>
                        <h5>Other Products Used:</h5>
                        <ul class="product-list">
                            <li><i class="fa fa-check-circle"></i> Royal Vintage Oak</li>
                            <li><i class="fa fa-check-circle"></i> Royal Burberry</li>
                            <li><i class="fa fa-check-circle"></i> Royal Euro Walnut</li>
                        </ul>
                    </div>
                </div>
            </div>

            <!-- Right Side Image Slider -->
            <div class="col-lg-6 position-relative">
                <div class="about-img">
                    <div class="container-fluid">
                        <div class="px-md-6">
                            <div class="mx-n6 slick-slider" 
                                 data-slick-options='{
                                     "slidesToShow": 1,
                                     "infinite": true,
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
                                <!-- Images -->
                                <a href="assets/imgs/ps/Bhopal1.jpg" data-lity>
                                    <img src="assets/imgs/ps/Bhopal1.jpg" alt="Bhopal Image 1">
                                </a>
                                <a href="assets/imgs/ps/Bhopal3.jpg" data-lity>
                                    <img src="assets/imgs/ps/Bhopal3.jpg" alt="Bhopal Image 2">
                                </a>
                                <!-- Video -->
                                <a href="https://www.youtube.com/embed/4ORbpY6d9Zk?si=s_s67Cv7ao4yVchT" data-lity>
                                    <img src="assets/imgs/ps/bangalore1.jpg" alt="Video Thumbnail">
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</section>--%>
    <%--    <section class="why-choose-us section-padding">
        <div class="container container-xxl">
            <div class="row align-items-center justify-content-between">

                <!-- Left Side Content -->
                <div class="col-lg-6 px-lg-10 py-lg-0 py-13">
                    <div class="text-left">
                        <div class="section-title">
                            <h2>Bungalow, Bhopal</h2>
                            <p>
                                At Archidply, we see every project as a challenge that allows us to create something novel
                            and breathtaking. And here’s how we used Archidply Club Plywood and Natural Decorative
                            Veneers to add a touch of magic to this property.
                            </p>
                            <h5>Other Products Used:</h5>
                            <ul class="product-list">
                                <li><i class="fa fa-check-circle"></i>Royal Vintage Oak</li>
                                <li><i class="fa fa-check-circle"></i>Royal Burberry</li>
                                <li><i class="fa fa-check-circle"></i>Royal Euro Walnut</li>
                            </ul>
                        </div>
                    </div>
                </div>

                <!-- Right Side Image Slider -->
                <div class="col-lg-6 position-relative">
                    <div class="about-img">
                        <div class="container-fluid">
                            <div class="px-md-6">
                                <div class="mx-n6 slick-slider"
                                    data-slick-options='{
                                     "slidesToShow": 1,
                                     "infinite": true,
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
                                    <!-- Images -->
                                    <a href="assets/imgs/ps/Bhopal1.jpg" data-lity>
                                        <img src="assets/imgs/ps/Bhopal1.jpg" alt="Bhopal Image 1">
                                    </a>
                                    <a href="assets/imgs/ps/Bhopal3.jpg" data-lity>
                                        <img src="assets/imgs/ps/Bhopal3.jpg" alt="Bhopal Image 2">
                                    </a>
                                    <!-- Video -->
                                    <a href="https://www.youtube.com/embed/4ORbpY6d9Zk?si=s_s67Cv7ao4yVchT" data-lity>
                                        <img src="assets/imgs/ps/bangalore1.jpg" alt="Video Thumbnail">
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </section>

    <section class="why-choose-us section-padding bg-light ">
        <div class="container container-xxl">
            <div class="row align-items-center justify-content-between">
                <div class="col-lg-6 position-relative">
                    <div class="about-img">
                        <div class="container-fluid">
                            <div class="px-md-6">
                                <div class="mx-n6 slick-slider"
                                    data-slick-options='{
          "slidesToShow": 1,
          "infinite": true,
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
                                    <!-- Images -->
                                    <a href="assets/imgs/ps/Bhopal1.jpg" data-lity>
                                        <img src="assets/imgs/ps/Bhopal1.jpg" alt="Bhopal Image 1">
                                    </a>
                                    <a href="assets/imgs/ps/Bhopal3.jpg" data-lity>
                                        <img src="assets/imgs/ps/Bhopal3.jpg" alt="Bhopal Image 2">
                                    </a>
                                    <!-- Video -->
                                    <a href="https://www.youtube.com/embed/4ORbpY6d9Zk?si=s_s67Cv7ao4yVchT" data-lity>
                                        <img src="assets/imgs/ps/bangalore1.jpg" alt="Video Thumbnail">
                                    </a>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6 px-lg-10 py-lg-0 py-13">
                    <div class="text-left">
                        <div class="section-title">
                            <h2>Bungalow, Raipur
                            </h2>
                        </div>

                    </div>


                </div>

            </div>

        </div>
    </section>--%>

    <%--    <section class="why-choose-us section-padding bg-light ">
        <div class="container container-xxl">
            <div class="row align-items-center justify-content-between">
                <div class="col-lg-6 position-relative">
                    <div class="about-img">
                        <div class="container-fluid">
                            <div class="px-md-6">
                                <div class="mx-n6 slick-slider" data-slick-options='{"slidesToShow": 1,"autoplaySpeed": 1000,"infinite":false,"autoplay":true,"dots":true,"arrows":false,"responsive":[{"breakpoint": 1366,"settings": {"slidesToShow":1 }},{"breakpoint": 992,"settings": {"slidesToShow":1}},{"breakpoint": 768,"settings": {"slidesToShow": 1}},{"breakpoint": 576,"settings": {"slidesToShow": 1}}]}'>

                                    <div class="px-6">
                                        <a href="assets/imgs/ps/bangalore1.jpg" title="instagram-01" data-gallery="instagram2" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/bangalore1.jpg" alt="instagram-01" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>

                                    <div class="px-6">
                                        <a href="assets/imgs/ps/bangalore2.jpg" title="instagram-02" data-gallery="instagram2" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/bangalore2.jpg" alt="instagram-02" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>

                                    <div class="px-6">
                                        <a href="assets/imgs/ps/bangalore3.jpg" title="instagram-03" data-gallery="instagram2" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/bangalore3.jpg" alt="instagram-03" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>
                                    <div class="px-6">
                                        <a href="assets/imgs/ps/banne8.jpg" title="instagram-03" data-gallery="instagram2" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/banne8.jpg" alt="instagram-03" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>


                                </div>


                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6 px-lg-10 py-lg-0 py-13">
                    <div class="text-left">
                        <div class="section-title">
                            <h2>Bungalow, Raipur

                            </h2>
                            <p>
                                Whenever we’re given a creative brief, we rush to our drawing boards to chalk out unique plans for our clients. And that’s exactly what we did at this
property — with the help of Archidply Club Plus and select veneers from our Decorative Veneer collection.

                            </p>
                        </div>
                        <div class="why-choose-content">
                            <h5 class="">Veneers used:
:
                            </h5>
                            <ul>
                                <li>
                                    <i class="fa-solid fa-check-circle"></i><span>Royale Cocobolo

                                    </span>
                                </li>
                                <li>
                                    <i class="fa-solid fa-check-circle"></i><span>Royale Pulp Tree

                                    </span>
                                </li>
                                <li>
                                    <i class="fa-solid fa-check-circle"></i><span>Royale Iron Wood

                                    </span>
                                </li>
                                <li>
                                    <i class="fa-solid fa-check-circle"></i><span>Fumed Eucalyptus FSC


                                    </span>
                                </li>
                                <li>
                                    <i class="fa-solid fa-check-circle"></i><span>Fumed Larch FSC


                                    </span>
                                </li>
                                <li>
                                    <i class="fa-solid fa-check-circle"></i><span>Royale Fumed Oak Moon Ring


                                    </span>
                                </li>

                            </ul>
                        </div>
                    </div>


                </div>

            </div>

        </div>
    </section>
    <section class="why-choose-us section-padding ">
        <div class="container container-xxl">
            <div class="row align-items-center justify-content-between">

                <div class="col-lg-6 px-lg-10 py-lg-0 py-13">
                    <div class="text-left">
                        <div class="section-title">
                            <h2>Residence, Mangalore

                            </h2>
                            <p>
                                When it comes to crafting residences, we make sure to leave behind an elegant finish and a homely warmth. And to do that, we used our Carbonised Teak and Royal Teak Veneers along with our Club Plywood at this property. And the results are here for you to see!



                            </p>
                        </div>

                    </div>


                </div>
                <div class="col-lg-6 position-relative">
                    <div class="about-img">
                        <div class="container-fluid">
                            <div class="px-md-6">
                                <div class="mx-n6 slick-slider" data-slick-options='{"slidesToShow": 1,"infinite":false,"autoplay":true,"autoplaySpeed":2000,"dots":true,"arrows":false,"responsive":[{"breakpoint": 1366,"settings": {"slidesToShow":1 }},{"breakpoint": 992,"settings": {"slidesToShow":1}},{"breakpoint": 768,"settings": {"slidesToShow": 1}},{"breakpoint": 576,"settings": {"slidesToShow": 1}}]}'>

                                    <div class="px-6">
                                        <a href="assets/imgs/ps/mangalore1.jpg" title="instagram-01" data-gallery="instagram3" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/mangalore1.jpg" alt="instagram-01" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>

                                    <div class="px-6">
                                        <a href="assets/imgs/ps/mangalore2.jpg" title="instagram-02" data-gallery="instagram3" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/mangalore2.jpg" alt="instagram-02" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>

                                    <div class="px-6">
                                        <a href="assets/imgs/ps/mangalore3.jpg" title="instagram-03" data-gallery="instagram3" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/mangalore3.jpg" alt="instagram-03" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>
                                    <div class="px-6">
                                        <a href="assets/imgs/ps/mangalore4.jpg" title="instagram-03" data-gallery="instagram3" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/mangalore4.jpg" alt="instagram-03" src="#">
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
    </section>
    <section class="why-choose-us section-padding bg-light ">
        <div class="container container-xxl">
            <div class="row align-items-center justify-content-between">
                <div class="col-lg-6 position-relative">
                    <div class="about-img">
                        <div class="container-fluid">
                            <div class="px-md-6">
                                <div class="mx-n6 slick-slider" data-slick-options='{"slidesToShow": 1,"infinite":false,"autoplay":true,"dots":true,"autoplaySpeed": 3000,"arrows":false,"responsive":[{"breakpoint": 1366,"settings": {"slidesToShow":1 }},{"breakpoint": 992,"settings": {"slidesToShow":1}},{"breakpoint": 768,"settings": {"slidesToShow": 1}},{"breakpoint": 576,"settings": {"slidesToShow": 1}}]}'>

                                    <div class="px-6">
                                        <a href="assets/imgs/ps/erode1.jpg" title="instagram-01" data-gallery="instagram4" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/erode1.jpg" alt="instagram-01" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>

                                    <div class="px-6">
                                        <a href="assets/imgs/ps/erode2.jpg" title="instagram-02" data-gallery="instagram4" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/erode2.jpg" alt="instagram-02" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>

                                    <div class="px-6">
                                        <a href="assets/imgs/ps/erode3.jpg" title="instagram-03" data-gallery="instagram4" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/erode3.jpg" alt="instagram-03" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>
                                    <div class="px-6">
                                        <a href="assets/imgs/ps/erode4.jpg" title="instagram-03" data-gallery="instagram4" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/erode4.jpg" alt="instagram-03" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>


                                </div>


                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6 px-lg-10 py-lg-0 py-13">
                    <div class="text-left">
                        <div class="section-title">
                            <h2>Residence, Erode


                            </h2>
                            <p>
                                All our products come with a transformational quality. For instance, when it came to transforming this residence into a world-class home we put our Smoked Sapeli Veneer to good use and gave it a stunning makeover!


                            </p>
                        </div>

                    </div>


                </div>

            </div>

        </div>
    </section>
    <section class="why-choose-us section-padding ">
        <div class="container container-xxl">
            <div class="row align-items-center justify-content-between">

                <div class="col-lg-6 px-lg-10 py-lg-0 py-13">
                    <div class="text-left">
                        <div class="section-title">
                            <h2>Resort, Havelock Islands, Andaman and Nicobar


                            </h2>
                            <p>
                                We turned a seaside resort into a cozy getaway with our 9mm Gurjan Club Plywood overlaid with our Golden Cedar Veneer.




                            </p>
                        </div>

                    </div>


                </div>
                <div class="col-lg-6 position-relative">
                    <div class="about-img">
                        <div class="container-fluid">
                            <div class="px-md-6">
                                <div class="mx-n6 slick-slider" data-slick-options='{"slidesToShow": 1,"infinite":false,"autoplay":true,"autoplaySpeed": 1000,"dots":true,"arrows":false,"responsive":[{"breakpoint": 1366,"settings": {"slidesToShow":1 }},{"breakpoint": 992,"settings": {"slidesToShow":1}},{"breakpoint": 768,"settings": {"slidesToShow": 1}},{"breakpoint": 576,"settings": {"slidesToShow": 1}}]}'>

                                    <div class="px-6">
                                        <a href="assets/imgs/ps/product-story5-1.jpg" title="instagram-01" data-gallery="instagram5" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/product-story5-1.jpg" alt="instagram-01" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>

                                    <div class="px-6">
                                        <a href="assets/imgs/ps/product-story5-2.jpg" title="instagram-02" data-gallery="instagram5" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/product-story5-2.jpg" alt="instagram-02" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>

                                    <div class="px-6">
                                        <a href="assets/imgs/ps/product-story5-3.jpg" title="instagram-03" data-gallery="instagram5" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/product-story5-3.jpg" alt="instagram-03" src="#">
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
    </section>
    <section class="why-choose-us section-padding bg-light ">
        <div class="container container-xxl">
            <div class="row align-items-center justify-content-between">
                <div class="col-lg-6 position-relative">
                    <div class="about-img">
                        <div class="container-fluid">
                            <div class="px-md-6">
                                <div class="mx-n6 slick-slider" data-slick-options='{"slidesToShow": 1,"infinite":false,"autoplay":true,"dots":true,"arrows":false,"responsive":[{"breakpoint": 1366,"settings": {"slidesToShow":1 }},{"breakpoint": 992,"settings": {"slidesToShow":1}},{"breakpoint": 768,"settings": {"slidesToShow": 1}},{"breakpoint": 576,"settings": {"slidesToShow": 1}}]}'>

                                    <div class="px-6">
                                        <a href="assets/imgs/ps/Image1-820x658.jpeg" title="instagram-01" data-gallery="instagram6" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/Image1-820x658.jpeg" alt="instagram-01" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>

                                    <div class="px-6">
                                        <a href="assets/imgs/ps/Image10-576x658.jpeg" title="instagram-02" data-gallery="instagram6" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/Image10-576x658.jpeg" alt="instagram-02" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>

                                    <div class="px-6">
                                        <a href="assets/imgs/ps/Image11.jpeg" title="instagram-03" data-gallery="instagram6" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/Image11.jpeg" alt="instagram-03" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>
                                    <div class="px-6">
                                        <a href="assets/imgs/ps/Image12-1024x658.jpeg" title="instagram-03" data-gallery="instagram6" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/Image12-1024x658.jpeg" alt="instagram-03" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>
                                    <div class="px-6">
                                        <a href="assets/imgs/ps/Image13-682x658.jpeg" title="instagram-03" data-gallery="instagram6" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/Image13-682x658.jpeg" alt="instagram-03" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>
                                    <div class="px-6">
                                        <a href="assets/imgs/ps/Image2-1024x658.jpeg" title="instagram-03" data-gallery="instagram6" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/Image2-1024x658.jpeg" alt="instagram-03" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>
                                    <div class="px-6">
                                        <a href="assets/imgs/ps/Image3-682x658.jpeg" title="instagram-03" data-gallery="instagram6" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/Image3-682x658.jpeg" alt="instagram-03" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>
                                    <div class="px-6">
                                        <a href="assets/imgs/ps/Image5-1024x658.jpeg" title="instagram-03" data-gallery="instagram6" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/Image5-1024x658.jpeg" alt="instagram-03" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>
                                    <div class="px-6">
                                        <a href="assets/imgs/ps/Image6.jpeg" title="instagram-03" data-gallery="instagram6" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/Image6.jpeg" alt="instagram-03" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>
                                    <div class="px-6">
                                        <a href="assets/imgs/ps/Image7-576x658.jpeg" title="instagram-03" data-gallery="instagram6" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/Image7-576x658.jpeg" alt="instagram-03" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>

                                    <div class="px-6">
                                        <a href="assets/imgs/ps/Image8-485x658.jpeg" title="instagram-03" data-gallery="instagram6" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/Image8-485x658.jpeg" alt="instagram-03" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>


                                    <div class="px-6">
                                        <a href="assets/imgs/ps/Image9-1024x658.jpeg" title="instagram-03" data-gallery="instagram4" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                            <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="assets/imgs/ps/Image9-1024x658.jpeg" alt="instagram-03" src="#">
                                            <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                        </a>
                                    </div>





                                </div>


                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6 px-lg-10 py-lg-0 py-13">
                    <div class="text-left">
                        <div class="section-title">
                            <h2>Residence, Belgaum & Goa



                            </h2>
                            <p>
                                We used our premium range of product i.e Carbonised Teak and Royal Teak Veneers for interior designing .



                            </p>
                        </div>

                    </div>


                </div>

            </div>

        </div>
    </section>--%>
        </div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/lity/2.4.1/lity.min.js" integrity="sha512-UU0D/t+4/SgJpOeBYkY+lG16MaNF8aqmermRIz8dlmQhOlBnw6iQrnt4Ijty513WB3w+q4JO75IX03lDj6qQNA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="/assets/js/Pages/product-stories-bind.js"></script>

</asp:Content>

