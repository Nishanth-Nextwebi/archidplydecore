<%@ Page Title="ArchidPly Factory" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="factory.aspx.cs" Inherits="factory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="Take a look inside ArchidPly Decor’s state-of-the-art plywood factory in Bangalore. High-quality products made in India. Call today at plywood factory in india">
    <meta name="keywords" content="plywood factory in india, plywood manufacturers in bangalore, ArchidPly Decor, best plywood in india, laminated plywood, furniture plywood, veneer plywood, top plywood companies, door manufacturers, flush doors" />
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
    <style>
        .new-bg {
            background: #f1f1f1;
            padding: 20px;
            border-radius: 12px;
        }

        .new-left {
            padding-left: 30px;
        }

            .new-left iframe {
                border-radius: 6px;
            }

        .bg-overlay::before {
            background: unset !important;
        }

        .bg-overlay::before {
            background: unset !important;
        }

        @media (max-width: 991px) {


            .hover-shine img {
                height: auto !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main id="content" class="wrapper layout-page">
        <section class="position-relative custom-overlay new-banner-head" id="about_introduction">

            <div class="lazy-bg bg-overlay position-absolute z-index-1 w-100 h-100   light-mode-img" data-bg-src="assets/imgs/ban1/2.png">
            </div>
            <div class="lazy-bg bg-overlay dark-mode-img position-absolute z-index-1 w-100 h-100" data-bg-src="assets/imgs/ban1/2.png">
            </div>

            <div class="position-relative z-index-2 container py-10 py-md-15 py-lg-22">

                <h1 class="fs-56px mb-0 text-white text-center">Factory</h1>
            </div>
        </section>



        <div class="container">
            <nav class="py-4 lh-30px" aria-label="breadcrumb">
                <ol class="breadcrumb justify-content-start py-1">
                    <li class="breadcrumb-item"><a href="/">Home</a></li>


                    <li class="breadcrumb-item active" aria-current="page">Factory</li>
                </ol>
            </nav>
        </div>








        <section class="container  pt-6 pb-13 pb-lg-20">
            <div class="row new-bg align-items-center">
                <div class="col-lg-7">
                    <div class="text-start">
                        <div class="text-start new-fact-head">
                            <h2 class="fs-28px mb-3">ARCHIDPLY DECOR LIMITED</h2>
                            <p class="fs-18px">All our products come to life in our cutting-edge facilities in  Chintamani, Karnataka. Our flagship facility at Chintamani is spread across 12.5 acres and is where all our decorative veneers and pre-laminated boards are manufactured.</p>
                        </div>

                    </div>
                    <div class="d-flex align-items-start">


                        <div>
                            <h3 class="fs-5 mb-6">Address</h3>
                            <div class="fs-6">
                                <p class="mb-2 pb-4 fs-6">
                                    Sy. No.19, KSSIDC Industrial Area, Bangalore,
                                        <br>
                                    Bangalore Rd, Katamachanhalli, Chintamani,
                                    <br />
                                    Karnataka 563125 
                                </p>
                                <p>
                                    Phone no : 094484 19394                               
                                </p>
                            </div>
                            <a href="https://maps.app.goo.gl/XZmJrFXwoQnYZDFg7" class="text-decoration-none border-bottom border-currentColor fw-semibold fs-6" contenteditable="false" style="cursor: pointer;">Get Direction</a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-5">
                    <div class="new-left">
                        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d15526.155300184078!2d78.03343199915298!3d13.378927391600614!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3bb204426dda505d%3A0x3cfa04e4ff68bf75!2sARCHIDPLY%20DECOR%20LIMITED!5e0!3m2!1sen!2sin!4v1738058577132!5m2!1sen!2sin" width="100%" height="394" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
                    </div>
                </div>
                <div class="col-lg-12">
                    <h4 class="fw-bold mt-3">Gallery</h4>
                    <div class="about-img">
                        <div class="">
                            <div class="">
                                <div class="mx-n6 slick-slider"
                                    data-slick-options='{
                     "slidesToShow": 4,
                     "infinite": false,
                     "autoplay": false,
                     "dots": true,
                     "arrows": false,
                     "responsive": [
                         {"breakpoint": 1366, "settings": {"slidesToShow": 4}},
                         {"breakpoint": 992, "settings": {"slidesToShow": 3}},
                         {"breakpoint": 768, "settings": {"slidesToShow": 2}},
                         {"breakpoint": 576, "settings": {"slidesToShow": 1}}
                     ]
                 }'>
                                    <!-- Images -->
                                    <a href="images_/show/fac-002.jpg" data-gallery="B1" class="hover-zoom-in hover-shine card-img-overlay-hover d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/show/fac-002.jpg" alt="instagram-01" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>
                                    <a href="images_/show/fac.jpg" data-gallery="B1" class="hover-zoom-in hover-shine card-img-overlay-hover d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/show/fac.jpg" alt="instagram-02" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>
                                    <a href="images_/show/fac005.jpg" data-gallery="B1" class="hover-zoom-in hover-shine card-img-overlay-hover d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/show/fac005.jpg" alt="instagram-03" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>
                                    <a href="images_/show/factory-00.jpg" data-gallery="B1" class="hover-zoom-in hover-shine card-img-overlay-hover d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/show/factory-00.jpg" alt="instagram-04" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>
                                    <%--<a href="images_/show/factory-07.jpg"  data-gallery="B1" class="hover-zoom-in hover-shine card-img-overlay-hover d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/show/factory-07.jpg" alt="instagram-05" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>--%>
                                    <a href="images_/show/Factory-pic.jpg" data-gallery="B1" class="hover-zoom-in hover-shine card-img-overlay-hover d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/show/Factory-pic.jpg" alt="instagram-06" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>
                                    <a href="images_/show/factory.jpg" data-gallery="B1" class="hover-zoom-in hover-shine card-img-overlay-hover d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/show/factory.jpg" alt="instagram-07" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>
                                    <a href="images_/show/factory01.jpg" data-gallery="B1" class="hover-zoom-in hover-shine card-img-overlay-hover d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/show/factory01.jpg" alt="instagram-08" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>

                                    <!-- Video -->

                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>

    </main>








</asp:Content>

