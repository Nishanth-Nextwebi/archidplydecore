<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="products.aspx.cs" Inherits="products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
    <style>
         .bg-overlay::before{
     background:unset !important;
 }
         .new-banner-no-bg{
             height:300px;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main id="content" class="wrapper layout-page product-category">
        <section class="position-relative custom-overlay  new-banner-no-bg" id="about_introduction" >
            <div class="lazy-bg bg-overlay position-absolute z-index-1 w-100 h-100  light-mode-img" data-bg-src="/assets/imgs/ban1/2.png">
            </div>
        </section>
        <div class="container">
            <nav class="py-0 lh-30px" aria-label="breadcrumb">
                <ol class="breadcrumb justify-content-start py-1">
                    <li class="breadcrumb-item"><a href="/">Home</a></li>
                    <li class="breadcrumb-item"><a href="#">Products</a></li>
                    <li class="breadcrumb-item active" aria-current="page"><%=strHeading %></li>
                </ol>
            </nav>
        </div>
        <section class="pt-14 pb-md-12 pb-8 pb-lg-12 pt-lg-12" style="background: #F1EFEC url('assets/imgs/bg-img3.png'); background-repeat: no-repeat; background-position: left; background-attachment: fixed">
            <div class="container container-xxl pb-16 pb-lg-5 pt-lg-5 mb-lg-3">
                <div class="row align-items-center justify-content-center">

                    <div class="col-md-10 col-lg-8">
                        <div class="text-center new-product-head" data-animate="fadeInUp">
                            <h2 class="mb-10"><%=strHeading %></h2>
                           <%=strCategoryDesc %>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="container container-xxl">
                            <div class="row gy-4 justify-content-center">
                                <%=strCategoriesDetails %>
                            </div>
                        </div>
                    </div>
                </div>
                <!--<div class="row justify-content-center">
            <div class="col-lg-12">
                <div class="row gy-11">
                    <div class="col-6">
                        <div class="card flex-md-row card-product list bg-transparent border-0" data-animate="fadeInUp">
                            <figure class="card-img-top position-relative mb-md-0 mb-7 overflow-hidden flex-md-shrink-0">
                                <a href="../shop/product-details-v1.html" class="hover-zoom-in d-block" title="Shield Conditioner">
                                    <img src="#" data-src="assets/imgs/gurjan/born-vivant1.jpg" class="img-fluid lazy-image w-100" alt="Shield Conditioner" width="330" height="440">
                                </a>
                            </figure>
                            <div class="card-body flex-md-grow-1 p-0 pt-md-9">

                                <h5 class="card-titletext-decoration-none fs-4 mb-4 d-block fw-semibold"><a class="color-inherit text-decoration-none" href="#">Bon vivant premium plywood</a></h5>
                                <p class="product-desc mb-10">
                                    The Bon Vivant Premium Plywood collection is characterized by a vast array of options ...
                                </p>


                                <div class="d-flex flex-wrap horizontal">
                                    <a href="#" class="btn btn-add-to-cart btn-dark btn-hover-bg-primary btn-hover-border-primary ">Know more</a>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="card flex-md-row card-product list bg-transparent border-0" data-animate="fadeInUp">
                            <figure class="card-img-top position-relative mb-md-0 mb-7 overflow-hidden flex-md-shrink-0">
                                <a href="../shop/product-details-v1.html" class="hover-zoom-in d-block" title="Shield Conditioner">
                                    <img src="#" data-src="assets/imgs/gurjan/born-vivant1.jpg" class="img-fluid lazy-image w-100" alt="Shield Conditioner" width="330" height="440">
                                </a>
                            </figure>
                            <div class="card-body flex-md-grow-1 p-0 pt-md-9">
                                <h5 class="card-titletext-decoration-none fs-4 mb-4 d-block fw-semibold">
                                    <a class="color-inherit text-decoration-none" href="#">Archidply club plus</a>
                                </h5>
                                <p class="product-desc mb-10">
                                    Archidply Club Plus is a magnificent addition to the Archidply plywood range...

                                </p>



                                <div class="d-flex flex-wrap horizontal">
                                    <a href="#" class="btn btn-add-to-cart btn-dark btn-hover-bg-primary btn-hover-border-primary ">Know more</a>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="card flex-md-row card-product list bg-transparent border-0" data-animate="fadeInUp">
                            <figure class="card-img-top position-relative mb-md-0 mb-7 overflow-hidden flex-md-shrink-0">
                                <a href="../shop/product-details-v1.html" class="hover-zoom-in d-block" title="Shield Conditioner">
                                    <img src="#" data-src="assets/imgs/gurjan/born-vivant1.jpg" class="img-fluid lazy-image w-100" alt="Shield Conditioner" width="330" height="440">
                                </a>
                            </figure>
                            <div class="card-body flex-md-grow-1 p-0 pt-md-9">
                                <h5 class="card-titletext-decoration-none fs-4 mb-4 d-block fw-semibold"><a class="color-inherit text-decoration-none" href="#">Archidply club</a></h5>
                                <p class="product-desc mb-10">
                                    The Archidply Club range boasts elegance and exquisite craftsmanship...

                                </p>


                                <div class="d-flex flex-wrap horizontal">
                                    <a href="#" class="btn btn-add-to-cart btn-dark btn-hover-bg-primary btn-hover-border-primary ">Know more</a>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="card flex-md-row card-product list bg-transparent border-0" data-animate="fadeInUp">
                            <figure class="card-img-top position-relative mb-md-0 mb-7 overflow-hidden flex-md-shrink-0">
                                <a href="../shop/product-details-v1.html" class="hover-zoom-in d-block" title="Shield Conditioner">
                                    <img src="#" data-src="assets/imgs/gurjan/born-vivant1.jpg" class="img-fluid lazy-image w-100" alt="Shield Conditioner" width="330" height="440">
                                </a>
                            </figure>
                            <div class="card-body flex-md-grow-1 p-0 pt-md-9">
                                <h5 class="card-titletext-decoration-none fs-4 mb-4 d-block fw-semibold">
                                    <a class="color-inherit text-decoration-none" href="#">Archidply flexible plywood</a>
                                </h5>
                                <p class="product-desc mb-10">
                                    Archidply’s Flexible Plywood is a versatile product with high bending properties...
                                </p>


                                <div class="d-flex flex-wrap horizontal">
                                    <a href="#" class="btn btn-add-to-cart btn-dark btn-hover-bg-primary btn-hover-border-primary ">Know more</a>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>-->
            </div>
            <!--<div class="container container-xxl">


        <div class="row">
            <div class="col-md-3 mb-8 mb-md-0" data-animate="fadeInUp">
                <div class="card border-0 mb-4">
                    <div class="image-box-4">


                        <img class="lazy-image img-fluid lazy-image light-mode-img" src="#" data-src="./assets/imgs/gurjan/born-vivant2.jpg" width="960" height="640" alt="">
                    </div>
                    <div class="card-body text-body-emphasis  pt-9 mt-2">
                        <h5 class="card-titletext-decoration-none fs-4 mb-4 d-block fw-semibold"><a class="color-inherit text-decoration-none" href="#">Bon vivant premium plywood</a></h5>

                        <p>The Bon Vivant Premium Plywood collection is characterized by a vast array of options ...</p>

                        <a href="#" class="btn btn-link p-0 mt-2 text-decoration-none text-primary fw-semibold">
                            Read More<i class="far fa-arrow-right ps-2 fs-13px"></i>
                        </a>
                    </div>
                </div>

            </div>
            <div class="col-md-3 mb-8 mb-md-0" data-animate="fadeInUp">
                <div class="card border-0 mb-4">
                    <div class="image-box-4">


                        <img class="lazy-image img-fluid lazy-image light-mode-img" src="#" data-src="./assets/imgs/gurjan/archidplyclubplus2.jpg" width="960" height="640" alt="">
                    </div>
                    <div class="card-body text-body-emphasis  pt-9 mt-2">
                        <h5 class="card-titletext-decoration-none fs-4 mb-4 d-block fw-semibold"><a class="color-inherit text-decoration-none" href="#">Archidply club plus</a></h5>

                        <p>Archidply Club Plus is a magnificent addition to the Archidply plywood range...</p>
                        <a href="#" class="btn btn-link p-0 mt-2 text-decoration-none text-primary fw-semibold">
                            Read More<i class="far fa-arrow-right ps-2 fs-13px"></i>
                        </a>
                    </div>
                </div>

            </div>

            <div class="col-md-3 mb-8 mb-md-0" data-animate="fadeInUp">
                <div class="card border-0 mb-4">
                    <div class="image-box-4">


                        <img class="lazy-image img-fluid lazy-image light-mode-img" src="#" data-src="./assets/imgs/gurjan/archidplyclub1.jpg" width="960" height="640" alt="">
                    </div>
                    <div class="card-body text-body-emphasis  pt-9 mt-2">
                        <h5 class="card-titletext-decoration-none fs-4 mb-4 d-block fw-semibold"><a class="color-inherit text-decoration-none" href="#">Archidply club</a></h5>
                        <p>The Archidply Club range boasts elegance and exquisite craftsmanship...</p>

                        <a href="#" class="btn btn-link p-0 mt-2 text-decoration-none text-primary fw-semibold">
                            Read More<i class="far fa-arrow-right ps-2 fs-13px"></i>
                        </a>
                    </div>
                </div>

            </div>

            <div class="col-md-3 mb-8 mb-md-0" data-animate="fadeInUp">
                <div class="card border-0 mb-4">
                    <div class="image-box-4">


                        <img class="lazy-image img-fluid lazy-image light-mode-img" src="#" data-src="./assets/imgs/gurjan/archid-flexible.jpg" width="960" height="640" alt="">
                    </div>
                    <div class="card-body text-body-emphasis  pt-9 mt-2">
                        <h5 class="card-titletext-decoration-none fs-4 mb-4 d-block fw-semibold"><a class="color-inherit text-decoration-none" href="#">Archidply flexible plywood</a></h5>
                        <p>
                            Archidply’s Flexible Plywood is a versatile product with high bending properties...

                        </p>
                        <a href="#" class="btn btn-link p-0 mt-2 text-decoration-none text-primary fw-semibold">
                            Read More<i class="far fa-arrow-right ps-2 fs-13px"></i>
                        </a>
                    </div>
                </div>

            </div>
        </div>
    </div>-->
        </section>





    </main>
</asp:Content>

