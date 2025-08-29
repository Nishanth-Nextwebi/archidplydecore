<%@ Page Title="Visit ArchidPly Showroom in Bangalore" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="showroom.aspx.cs" Inherits="showroom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="Explore ArchidPly Decor showrooms in Bangalore. Discover premium plywood, doors & laminates in person. Plywood showroom in bangalore">
    <meta name="keywords" content="plywood showroom in bangalore, ArchidPly Decor, laminated plywood, veneer plywood, best plywood in india, top plywood company, door manufacturers, flush doors, ply manufacturer, furniture plywood" />
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main id="content" class="wrapper layout-page">
        <section class="position-relative custom-overlay new-banner-head" id="about_introduction">

            <div class="lazy-bg bg-overlay position-absolute z-index-1 w-100 h-100   light-mode-img" data-bg-src="assets/imgs/ban1/2.png" alt="img">
            </div>
            <div class="lazy-bg bg-overlay dark-mode-img position-absolute z-index-1 w-100 h-100" data-bg-src="assets/imgs/ban1/2.png" alt="img">
            </div>

    <div class="position-relative z-index-2 container py-10 py-md-15 py-lg-22">

                <h2 class="fs-56px mb-0 text-white text-center">Showroom</h2>
            </div>
        </section>



        <div class="container">
            <nav class="py-4 lh-30px" aria-label="breadcrumb">
                <ol class="breadcrumb justify-content-start py-1">
                    <li class="breadcrumb-item"><a href="/">Home</a></li>


                    <li class="breadcrumb-item active" aria-current="page">Showroom</li>
                </ol>
            </nav>
        </div>
        <section class="container  pt-6 pb-13 pb-lg-20">
            <div class="row new-bg align-items-center">
                <div class="col-lg-7">
                    <div class="text-start">
                        <div class="text-start new-fact-head">
                            <h1 class="fs-28px mb-3">ARCHIDPLY DECOR  LIMITED</h1>
                            <p class="fs-18px d-none">Archidply Décor is a unique concept store for premium veneers with different finishes, Plywood decorative doors, Flooring, laminates, acrylic & many more under one roof , it is one stop shop for interior. It is located on the prime location of Queens Road with lot space of parking available .Step into the store to explore more.</p>
                            <p class="fs-18px">Bonvivant Collective  is a unique concept store for premium veneers with different textures , plywood ,decorative doors, engineered flooring, laminates, acrylic & many more products under one roof . It is a one stop shop for all interior needs and is located on the prime location of Queens Road with basement parking available .Step into the store to explore more.</p>
                        </div>

                    </div>
                    <div class="d-flex align-items-start">
                        <div>
                            <h3 class="fs-5 mb-6">Address</h3>
                            <div class="fs-6">
                                <p class="mb-2 pb-4 fs-6">
                                    Bonvivant Collective 
                                <br>
                                    Ground Floor & 1st Floor No 50, Millenium Towers,
                            <br />
                                    Queens Rd, Swamy Shivanandapuram, Shivaji Nagar,

                            <br />
                                    Karnataka 560051 

                                </p>
                                <p>
                                    Phone no : 080-43714281 , 7022012573
                                </p>
                            </div>
                            <a href="https://maps.app.goo.gl/9JXcAdPUSZivcJuA8" class="text-decoration-none border-bottom border-currentColor fw-semibold fs-6" contenteditable="false" style="cursor: pointer;">Get Direction</a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-5">
                    <div class="new-left">
                        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1943.8901861742593!2d77.59673463861387!3d12.98589461044749!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3bae16664f73ae43%3A0x1229bdf01a8ea51a!2sMillennium%20Towers!5e0!3m2!1sen!2sin!4v1738070048297!5m2!1sen!2sin" width="100%" height="394" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
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
                                    <a href="images_/s1/SDP_4544.jpg" data-gallery="gallery111" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/s1/SDP_4544.jpg" alt="instagram-01" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>
                                    <a href="images_/s1/SDP_4575.jpg" data-gallery="gallery111" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/s1/SDP_4575.jpg" alt="instagram-01" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>

                                    <%--<a href="images_/s1/SDP_4602.jpg" data-gallery="gallery111" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/s1/SDP_4602.jpg" alt="instagram-01" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>--%>
                                    <a href="images_/s1/SDP_4622.jpg" data-gallery="gallery111" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/s1/SDP_4622.jpg" alt="instagram-01" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>
                                    <a href="images_/s1/SDP_4678.jpg" data-gallery="gallery111" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/s1/SDP_4678.jpg" alt="instagram-01" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>
                                    <a href="images_/s1/SDP_4712.jpg" data-gallery="gallery111" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/s1/SDP_4712.jpg" alt="instagram-01" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>
                                    <a href="images_/s1/SDP_4728.jpg" data-gallery="gallery111" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/s1/SDP_4728.jpg" alt="instagram-01" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>
                                    <a href="images_/s1/SDP_4731.jpg" data-gallery="gallery111" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/s1/SDP_4731.jpg" alt="instagram-01" src="#">
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

        <section class="container  pt-6 pb-13 pb-lg-20">
            <div class="row new-bg align-items-center">
                <div class="col-lg-7">
                    <div class="text-start">
                        <div class="text-start new-fact-head">
                            <h2 class="fs-28px mb-3">ARCHIDPLY DECOR  LIMITED</h2>
                            <p class="fs-18px d-none">Archidply Décor  showroom is a retail space designed to display and sell various types of plywood and veneer materials used for furniture, interior decor, and construction. It’s typically a well-organized, well-lit area where customers can view the range of products available in different sizes, thicknesses, textures, and finishes.</p>
                            <p class="fs-18px">Bonvivant collective  showroom is a retail space designed to display and sell various types of plywood and veneer materials used for furniture, interior decor, and construction. It’s typically a well-organized, well-lit area where customers can view the range of products available in different sizes, thicknesses, textures, and finishes.</p>
                        </div>

                    </div>
                    <div class="d-flex align-items-start">


                        <div>
                            <h3 class="fs-5 mb-6">Address</h3>
                            <div class="fs-6">
                                <p class="mb-2 pb-4 fs-6">
                                    Bonvivant Collective 
                                <br>
                                    G15 Ground Floor Scheme no. 54 PU -333
                            <br />
                                    Commercial Princess Business Sky Park,Block No.22,23,24

                            <br />
                                    AB Road Indore 452010
                                </p>
                                <p>
                                    Phone no : 0731-4202546

                                </p>
                            </div>
                            <a href="https://maps.app.goo.gl/d8k2ubTMrJiLZrgu6" class="text-decoration-none border-bottom border-currentColor fw-semibold fs-6" contenteditable="false" style="cursor: pointer;">Get Direction</a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-5">
                    <div class="new-left">
                        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3679.511307815712!2d75.89087702530558!3d22.746398529368324!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3962fd56993c50d5%3A0xbdcc27d0aa89f45b!2sPrinces&#39;%20Business%20Skypark!5e0!3m2!1sen!2sin!4v1738070308284!5m2!1sen!2sin" width="100%" height="394" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
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
                                    <a href="images_/s2/s1.jpg" data-gallery="gallery222" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/s2/s1.jpg" alt="instagram-01" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>
                                    <a href="images_/s2/s2.jpg" data-gallery="gallery222" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/s2/s2.jpg" alt="instagram-01" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>
                                    <a href="images_/s2/s3.jpg" data-gallery="gallery222" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/s2/s3.jpg" alt="instagram-01" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>
                                    <a href="images_/s2/s4.jpg" data-gallery="gallery222" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/s2/s4.jpg" alt="instagram-01" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>
                                    <a href="images_/s2/s5.jpeg" data-gallery="gallery222" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/s2/s5.jpeg" alt="instagram-01" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>
                                    <a href="images_/s2/s6.jpeg" data-gallery="gallery222" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/s2/s6.jpegg" alt="instagram-01" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>

                                    <a href="images_/s2/s7.jpeg" data-gallery="gallery222" class=" hover-zoom-in hover-shine  card-img-overlay-hover hover-zoom-in hover-shine d-block">
                                        <img class="lazy-image img-fluid w-100" width="314" height="314" data-src="images_/s2/s7.jpegg" alt="instagram-01" src="#">
                                        <span class="card-img-overlay bg-dark bg-opacity-30"></span>
                                    </a>

                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>

    </main>





</asp:Content>

