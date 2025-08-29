<%@ Page Title="Top Plywood & Laminates in India | Shop Archidply Products" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="shop-products.aspx.cs" Inherits="shop_products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <meta name="description" content="Shop premium plywood, veneers, doors and wood products online at Archidply Decor. Leading manufacturer and supplier across India with quality assured.">
     <meta name="keywords" content="buy plywood online india, wood supplier india, plywood manufacturers online, veneer sheets supplier, decorative wood doors online, premium plywood shop india, archidply wood products, plywood supplier india, plywood factory online, laminates and veneers india" />
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
    <style>
        .pagination {
            display: flex;
            list-style: none;
            padding: 0;
        }

        .product-category {
            overflow: hidden;
        }

        .page-item {
            margin: 0 5px;
        }

            .page-item.active a {
                background-color: #4e7661;
                color: white;
                border-color: #4e7661;
            }

            .page-item.disabled a {
                pointer-events: none;
                opacity: 0.6;
            }

        #BindAllShopProductList div {
            display: block !important;
            visibility: visible !important;
            opacity: 1 !important;
        }

        .custom-align-right {
            text-align: right;
        }

        .product-category {
            overflow: hidden;
        }

        .bg-overlay::before {
            background: unset !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="position-relative custom-overlay new-banner-head" id="about_introduction">
        <div class="lazy-bg bg-overlay  position-absolute z-index-1 w-100 h-100   light-mode-img" data-bg-src="assets/imgs/ban1/2.png">
        </div>
        <div class="lazy-bg bg-overlay dark-mode-img position-absolute z-index-1 w-100 h-100" data-bg-src="assets/imgs/ban1/2.png">
        </div>

        <div class="position-relative z-index-2 container py-10 py-md-15 py-lg-22">

            <h1 class="fs-56px mb-0 text-white text-center">All Our Products
            </h1>
        </div>
    </section>
    <main id="content" class="wrapper layout-page product-category">


        <section class="shop-product-layout" style="background: #F1EFEC url('assets/imgs/bg-img3.png'); background-repeat: no-repeat; background-position: left; background-attachment: fixed">

            <div class="container">
                <nav class="py-0 lh-30px" aria-label="breadcrumb">
                    <ol class="breadcrumb justify-content-start py-1">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/shop">Shop</a></li>
                        <li class="breadcrumb-item active CatName" aria-current="page">All Our Products</li>
                    </ol>
                </nav>
            </div>
            <div class="container container-xxl pt-5  mt-10">
                <div class="row d-flex justify-content-between align-items-center">
                    <div class="col-lg-6 col-ms-6 col-sm-12 col-12">
                        <h6 class="text-danger">NOTE : DELIVERY IS CURRENTLY AVAILABLE ONLY IN BENGALURU</h6>

                    </div>
                    <div class="col-lg-4 col-sm-12 text-end">
                        <div class="" data-animate="fadeInUp">
                            <div class="input-group form-border-transparent d-flex">
                                <asp:DropDownList runat="server" class="form-select" ID="ddlCategory">
                                    <asp:ListItem Value="">Select Category</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <center>
                <div class="container container-xxl pb-16 pb-lg-18 mb-lg-3 pt-0  mt-10 divNoItem">
                    <img src="/images_/not-found.png" alt="img" width="300" height="300" />
                    <h3 style="color: #F4986D"><strong>No products found for your search</strong></h3>
                    <a href="/contact-us" class="btn btn-outline-dark text-nowrap mt-2">Send Request</a>
                </div>
            </center>
            <div class="container container-xxl pb-16 pb-lg-18 mb-lg-3 pt-5 mainProductList">
                <%--    <div class="text-center pt-13 pb-7">
                    <div class="container AboutCategory">
                        <p class="fs-18px">
                            <%=strAboutCategory %>
                       </p>
                    </div>
                </div>--%>
                <div class="row gy-lg-50px gy-4  justify-content-center" id="BindAllShopProductList">

                    <%-- <div class="col-sm-6  col-lg-4 col-xl-3">
                      <div class="card card-product grid-1 bg-transparent border-0" data-animate="fadeInUp">
                          <figure class="card-img-top position-relative mb-0 overflow-hidden ">
                              <a href="#" class="hover-zoom-in d-block" title="Shield Conditioner">
                                  <img src="#" data-src="https://archidplydecor.com/wp-content/uploads/2018/11/Wonder-Grain.jpg" class="img-fluid lazy-image w-100" alt="Shield Conditioner" width="330" height="440">
                              </a>
                          </figure>
                          <div class="card-body text-center p-0">
                              <span class="d-flex align-items-center price text-body-emphasis fw-bold justify-content-center mb-3 fs-6">
                                  <del class=" text-body fw-500 me-4 fs-13px">₹400.00</del>
                                  <ins class="text-decoration-none">₹301.00</ins>
                              </span>
                              <h4 class="product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3"><a class="text-decoration-none text-reset" href="shop-product-detail.aspx">Wonder Grain </a></h4>
                              <a href="shop-product-detail.aspx" class="btn btn-add-to-cart green-btn btn-hover-bg-primary btn-hover-border-primary" title="Check Out"> View More</a>
                          </div>
                      </div>
                  </div>

                  <div class='col-sm-6  col-lg-4 col-xl-3'>
                      <div class='card card-product grid-1 bg-transparent border-0' data-animate='fadeInUp'>
                          <figure class='card-img-top position-relative mb-0 overflow-hidden '>
                              <a href='#' class='hover-zoom-in d-block' title='Shield Conditioner'>
                                  <img src='#' data-src='https://archidplydecor.com/wp-content/uploads/2018/11/Metallic.jpg' class='img-fluid lazy-image w-100' alt='Shield Conditioner' width='330' height='440'>
                              </a>
                          </figure>
                          <div class='card-body text-center p-0'>
                              <span class='d-flex align-items-center price text-body-emphasis fw-bold justify-content-center mb-3 fs-6'>
                                  <del class=' text-body fw-500 me-4 fs-13px'>₹400.00</del>
                                  <ins class='text-decoration-none'>₹301.00</ins>
                              </span>
                              <h4 class='product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3'><a class='text-decoration-none text-reset' href='#'>Metallic</a></h4>
                              <a href='#' class='btn btn-add-to-cart green-btn btn-hover-bg-primary btn-hover-border-primary' title='Check Out'> View More</a>
                          </div>
                      </div>
                  </div>


                  <div class="col-sm-6  col-lg-4 col-xl-3">
                      <div class="card card-product grid-1 bg-transparent border-0" data-animate="fadeInUp">
                          <figure class="card-img-top position-relative mb-0 overflow-hidden ">
                              <a href="#" class="hover-zoom-in d-block" title="Shield Conditioner">
                                  <img src="#" data-src="https://archidplydecor.com/wp-content/uploads/2018/11/10x4.jpg" class="img-fluid lazy-image w-100" alt="Shield Conditioner" width="330" height="440">
                              </a>
                          </figure>
                          <div class="card-body text-center p-0">
                              <span class="d-flex align-items-center price text-body-emphasis fw-bold justify-content-center mb-3 fs-6">
                                  <del class=" text-body fw-500 me-4 fs-13px">₹400.00</del>
                                  <ins class="text-decoration-none">₹301.00</ins>
                              </span>

                              <h4 class="product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3"><a class="text-decoration-none text-reset" href="#">10×4</a></h4>
                              <a href="#" class="btn btn-add-to-cart green-btn btn-hover-bg-primary btn-hover-border-primary" title="Check Out"> View More</a>
                          </div>
                      </div>
                  </div>


                  <div class="col-sm-6  col-lg-4 col-xl-3">
                      <div class="card card-product grid-1 bg-transparent border-0" data-animate="fadeInUp">
                          <figure class="card-img-top position-relative mb-0 overflow-hidden ">
                              <a href="#" class="hover-zoom-in d-block" title="Shield Conditioner">
                                  <img src="#" data-src="https://archidplydecor.com/wp-content/uploads/2018/11/Textured.jpg" class="img-fluid lazy-image w-100" alt="Shield Conditioner" width="330" height="440">
                              </a>
                          </figure>
                          <div class="card-body text-center p-0">
                              <span class="d-flex align-items-center price text-body-emphasis fw-bold justify-content-center mb-3 fs-6">
                                  <del class=" text-body fw-500 me-4 fs-13px">₹400.00</del>
                                  <ins class="text-decoration-none">₹301.00</ins>
                              </span>

                              <h4 class="product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3"><a class="text-decoration-none text-reset" href="#">Textured </a></h4>
                              <a href="#" class="btn btn-add-to-cart green-btn btn-hover-bg-primary btn-hover-border-primary" title="Check Out"> View More</a>
                          </div>
                      </div>
                  </div>

                  <div class="col-sm-6  col-lg-4 col-xl-3">
                      <div class="card card-product grid-1 bg-transparent border-0" data-animate="fadeInUp">
                          <figure class="card-img-top position-relative mb-0 overflow-hidden ">
                              <a href="#" class="hover-zoom-in d-block" title="Shield Conditioner">
                                  <img src="#" data-src="https://archidplydecor.com/wp-content/uploads/2018/11/Vintage.jpg" class="img-fluid lazy-image w-100" alt="Shield Conditioner" width="330" height="440">
                              </a>
                          </figure>
                          <div class="card-body text-center p-0">
                              <span class="d-flex align-items-center price text-body-emphasis fw-bold justify-content-center mb-3 fs-6">
                                  <del class=" text-body fw-500 me-4 fs-13px">₹400.00</del>
                                  <ins class="text-decoration-none">₹301.00</ins>
                              </span>

                              <h4 class="product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3"><a class="text-decoration-none text-reset" href="#">Vintage </a></h4>
                              <a href="#" class="btn btn-add-to-cart green-btn btn-hover-bg-primary btn-hover-border-primary" title="Check Out"> View More</a>
                          </div>
                      </div>
                  </div>
                  <div class="col-sm-6  col-lg-4 col-xl-3">
                      <div class="card card-product grid-1 bg-transparent border-0" data-animate="fadeInUp">
                          <figure class="card-img-top position-relative mb-0 overflow-hidden ">
                              <a href="#" class="hover-zoom-in d-block" title="Shield Conditioner">
                                  <img src="#" data-src="https://archidplydecor.com/wp-content/uploads/2018/11/Rare-Exotic.jpg" class="img-fluid lazy-image w-100" alt="Shield Conditioner" width="330" height="440">
                              </a>
                          </figure>
                          <div class="card-body text-center p-0">
                              <span class="d-flex align-items-center price text-body-emphasis fw-bold justify-content-center mb-3 fs-6">
                                  <del class=" text-body fw-500 me-4 fs-13px">₹400.00</del>
                                  <ins class="text-decoration-none">₹301.00</ins>
                              </span>

                              <h4 class="product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3"><a class="text-decoration-none text-reset" href="#">Rare Exotic </a></h4>
                              <a href="#" class="btn btn-add-to-cart green-btn btn-hover-bg-primary btn-hover-border-primary" title="Check Out"> View More</a>
                          </div>
                      </div>
                  </div>

                  <div class="col-sm-6  col-lg-4 col-xl-3">
                      <div class="card card-product grid-1 bg-transparent border-0" data-animate="fadeInUp">
                          <figure class="card-img-top position-relative mb-0 overflow-hidden ">
                              <a href="#" class="hover-zoom-in d-block" title="Shield Conditioner">
                                  <img src="#" data-src="https://archidplydecor.com/wp-content/uploads/2018/11/Life-Tree.jpg" class="img-fluid lazy-image w-100" alt="Shield Conditioner" width="330" height="440">
                              </a>
                          </figure>
                          <div class="card-body text-center p-0">
                              <span class="d-flex align-items-center price text-body-emphasis fw-bold justify-content-center mb-3 fs-6">
                                  <del class=" text-body fw-500 me-4 fs-13px">₹400.00</del>
                                  <ins class="text-decoration-none">₹301.00</ins>
                              </span>

                              <h4 class="product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3"><a class="text-decoration-none text-reset" href="#">Life Tree </a></h4>
                              <a href="#" class="btn btn-add-to-cart green-btn btn-hover-bg-primary btn-hover-border-primary" title="Check Out"> View More</a>
                          </div>
                      </div>
                  </div>


                  <div class="col-sm-6  col-lg-4 col-xl-3">
                      <div class="card card-product grid-1 bg-transparent border-0" data-animate="fadeInUp">
                          <figure class="card-img-top position-relative mb-0 overflow-hidden ">
                              <a href="#" class="hover-zoom-in d-block" title="Shield Conditioner">
                                  <img src="#" data-src="https://archidplydecor.com/wp-content/uploads/2018/11/Burls-and-Crotches.jpg" class="img-fluid lazy-image w-100" alt="Shield Conditioner" width="330" height="440">
                              </a>
                          </figure>
                          <div class="card-body text-center p-0">
                              <span class="d-flex align-items-center price text-body-emphasis fw-bold justify-content-center mb-3 fs-6">
                                  <del class=" text-body fw-500 me-4 fs-13px">₹400.00</del>
                                  <ins class="text-decoration-none">₹301.00</ins>
                              </span>

                              <h4 class="product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3"><a class="text-decoration-none text-reset" href="#">Burls and Crotches </a></h4>
                              <a href="#" class="btn btn-add-to-cart green-btn btn-hover-bg-primary btn-hover-border-primary" title="Check Out"> View More</a>
                          </div>
                      </div>
                  </div>
                  <div class="col-sm-6  col-lg-4 col-xl-3">
                      <div class="card card-product grid-1 bg-transparent border-0" data-animate="fadeInUp">
                          <figure class="card-img-top position-relative mb-0 overflow-hidden ">
                              <a href="#" class="hover-zoom-in d-block" title="Shield Conditioner">
                                  <img src="#" data-src="https://archidplydecor.com/wp-content/uploads/2018/11/Designer.jpg" class="img-fluid lazy-image w-100" alt="Shield Conditioner" width="330" height="440">
                              </a>
                          </figure>
                          <div class="card-body text-center p-0">
                              <span class="d-flex align-items-center price text-body-emphasis fw-bold justify-content-center mb-3 fs-6">
                                  <del class=" text-body fw-500 me-4 fs-13px">₹400.00</del>
                                  <ins class="text-decoration-none">₹301.00</ins>
                              </span>

                              <h4 class="product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3"><a class="text-decoration-none text-reset" href="#">Designer </a></h4>
                              <a href="#" class="btn btn-add-to-cart green-btn btn-hover-bg-primary btn-hover-border-primary" title="Check Out"> View More</a>
                          </div>
                      </div>
                  </div>

                  <div class="col-sm-6  col-lg-4 col-xl-3">
                      <div class="card card-product grid-1 bg-transparent border-0" data-animate="fadeInUp">
                          <figure class="card-img-top position-relative mb-0 overflow-hidden ">
                              <a href="#" class="hover-zoom-in d-block" title="Shield Conditioner">
                                  <img src="#" data-src="https://archidplydecor.com/wp-content/uploads/2018/11/Rotary-Cut.jpg" class="img-fluid lazy-image w-100" alt="Shield Conditioner" width="330" height="440">
                              </a>
                          </figure>
                          <div class="card-body text-center p-0">
                              <span class="d-flex align-items-center price text-body-emphasis fw-bold justify-content-center mb-3 fs-6">
                                  <del class=" text-body fw-500 me-4 fs-13px">₹400.00</del>
                                  <ins class="text-decoration-none">₹301.00</ins>
                              </span>

                              <h4 class="product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3"><a class="text-decoration-none text-reset" href="#">Rotary Cut </a></h4>
                              <a href="#" class="btn btn-add-to-cart green-btn btn-hover-bg-primary btn-hover-border-primary" title="Check Out"> View More</a>
                          </div>
                      </div>
                  </div>--%>
                </div>
            </div>
            <div class="row mt-1">
                <div class="col-12 text-center">
                    <ul class="pagination pPagination justify-content-center">
                    </ul>
                </div>
            </div>
            <asp:HiddenField runat="server" ID="HiddenCatId" />
        </section>
        <script src="/assets/js/Pages/shop-product.min.js"></script>
    </main>
</asp:Content>

