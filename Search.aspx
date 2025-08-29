<%@ Page Title="Search | ARCHIDPLY DECOR LTD" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <section class="breadcrumb__area include-bg grey-bg-2 ptb-30">
        <div class="container">
            <div class="row">
                <div class="col-xxl-12">
                    <div class="breadcrumb__content p-relative z-index-1">
                      <h1>Search Result - <%=Request.QueryString["query"] %></h1>
                        <div class="breadcrumb__list">
                            <span><a href="/Default.aspx">Home</a></span>
                            <span>Products</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
<section class="tp-product-area ptb-50">
        <div class="container">
            <div class="row align-items-end">
              
<%--                <%=StrProductlist %>
       --%>
             <%--            <div class='col-xl-3 col-lg-3 col-sm-6'>
                    <div class='tp-product-item p-list p-relative transition-3 mb-25'>
                        <div class='tp-product-thumb p-relative fix m-img'>
                            <a href='#'>
                                <img src='assets/img/products/p1.webp' alt='product-electronic'>
                            </a>
                            <!-- product badge -->
                            <div class='tp-product-badge'>
                                <span class='product-offer'>Sale</span>
                            </div>
                        </div>
                        <!-- product content -->
                        <div class='tp-product-content'>
                            <h3 class='tp-product-title'>
                                <a href='#'>ShipWall15W Square(5 inch) LED Panel Light - Plasto Series
            
                                </a>
                            </h3>
                            <div class='plogin'>
                                <h5><a href='login.aspx'>Login for Price</a></h5>
                            </div>
                            <div class='tp-blog-btn'>
                                <a href='#' class='tp-btn-2 tp-btn-border-2'>Buy Now
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-lg-3 col-sm-6">
                    <div class="tp-product-item p-list p-relative transition-3 mb-25">
                        <div class="tp-product-thumb p-relative fix m-img">
                            <a href="#">
                                <img src="assets/img/products/p2.webp" alt="product-electronic">
                            </a>
                        </div>
                        <!-- product content -->
                        <div class="tp-product-content">
                            <h3 class="tp-product-title">
                                <a href="#">ShipWall20W Round(6 inch) LED Panel Light- Plasto series
                                </a>
                            </h3>
                            <div class="plogin">
                                <h5><a href="login.aspx">Login for Price</a></h5>
                            </div>
                            <div class="tp-blog-btn">
                                <a href="#" class="tp-btn-2 tp-btn-border-2">Buy Now
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-lg-3 col-sm-6">
                    <div class="tp-product-item p-list p-relative transition-3 mb-25">
                        <div class="tp-product-thumb p-relative fix m-img">
                            <a href="#">
                                <img src="assets/img/products/p3.webp" alt="product-electronic">
                            </a>
                        </div>
                        <!-- product content -->
                        <div class="tp-product-content">
                            <h3 class="tp-product-title">
                                <a href="#">ShipWall8W Round(4 inch) LED Panel Light- Plasto series
                                </a>
                            </h3>
                            <div class="plogin">
                                <h5><a href="login.aspx">Login for Price</a></h5>
                            </div>
                            <div class="tp-blog-btn">
                                <a href="#" class="tp-btn-2 tp-btn-border-2">Buy Now
                                </a>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="col-xl-3 col-lg-3 col-sm-6">
                    <div class="tp-product-item p-list p-relative transition-3 mb-25">
                        <div class="tp-product-thumb p-relative fix m-img">
                            <a href="#">
                                <img src="assets/img/products/p4.webp" alt="product-electronic">
                            </a>
                            <!-- product badge -->
                            <div class="tp-product-badge">
                                <span class="product-hot">Sold out</span>
                            </div>
                        </div>
                        <!-- product content -->
                        <div class="tp-product-content">
                            <h3 class="tp-product-title">
                                <a href="#">ShipWall15W Round(5 inch) LED Panel Light- Plasto series
                                </a>
                            </h3>
                            <div class="plogin">
                                <h5><a href="login.aspx">Login for Price</a></h5>
                            </div>
                            <div class="tp-blog-btn">
                                <a href="#" class="tp-btn-2 tp-btn-border-2">Buy Now
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-lg-3 col-sm-6">
                    <div class="tp-product-item p-list p-relative transition-3 mb-25">
                        <div class="tp-product-thumb p-relative fix m-img">
                            <a href="#">
                                <img src="assets/img/products/p5.webp" alt="product-electronic">
                            </a>
                            <!-- product badge -->
                            <div class="tp-product-badge">
                                <span class="product-offer">Sale</span>
                            </div>
                        </div>
                        <!-- product content -->
                        <div class="tp-product-content">
                            <h3 class="tp-product-title">
                                <a href="#">ShipWall15W Round(6 inch) LED Panel Light- Plasto series
                                </a>
                            </h3>
                            <div class="plogin">
                                <h5><a href="login.aspx">Login for Price</a></h5>
                            </div>
                            <div class="tp-blog-btn">
                                <a href="#" class="tp-btn-2 tp-btn-border-2">Buy Now
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-lg-3 col-sm-6">
                    <div class="tp-product-item p-list p-relative transition-3 mb-25">
                        <div class="tp-product-thumb p-relative fix m-img">
                            <a href="#">
                                <img src="assets/img/products/p6.webp" alt="product-electronic">
                            </a>
                        </div>
                        <!-- product content -->
                        <div class="tp-product-content">

                            <h3 class="tp-product-title">
                                <a href="#">ShipWall15W Square(6 inch) LED Panel Light - Plasto Series
                                </a>
                            </h3>
                            <div class="plogin">
                                <h5><a href="login.aspx">Login for Price</a></h5>
                            </div>

                            <div class="tp-blog-btn">
                                <a href="#" class="tp-btn-2 tp-btn-border-2">Buy Now
                                      
                                </a>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="col-xl-3 col-lg-3 col-sm-6">
                    <div class="tp-product-item p-list p-relative transition-3 mb-25">
                        <div class="tp-product-thumb p-relative fix m-img">
                            <a href="#">
                                <img src="assets/img/products/p2.webp" alt="product-electronic">
                            </a>
                        </div>
                        <!-- product content -->
                        <div class="tp-product-content">
                            <h3 class="tp-product-title">
                                <a href="#">ShipWall20W Square(6 inch) LED Panel Light - Plasto Series
                                </a>
                            </h3>
                            <div class="plogin">
                                <h5><a href="login.aspx">Login for Price</a></h5>
                            </div>
                            <div class="tp-blog-btn">
                                <a href="#" class="tp-btn-2 tp-btn-border-2">Buy Now
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-lg-3 col-sm-6">
                    <div class="tp-product-item p-list p-relative transition-3 mb-25">
                        <div class="tp-product-thumb p-relative fix m-img">
                            <a href="#">
                                <img src="assets/img/products/p1.webp" alt="product-electronic">
                            </a>
                            <!-- product badge -->
                            <div class="tp-product-badge">
                                <span class="product-offer">Sale</span>
                            </div>
                        </div>
                        <!-- product content -->
                        <div class="tp-product-content">
                            <h3 class="tp-product-title">
                                <a href="#">ShipWall15W Square(5 inch) LED Panel Light - Plasto Series
            
                                </a>
                            </h3>
                            <div class="plogin">
                                <h5><a href="login.aspx">Login for Price</a></h5>
                            </div>
                            <div class="tp-blog-btn">
                                <a href="#" class="tp-btn-2 tp-btn-border-2">Buy Now
                                </a>
                            </div>
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
    </section>

</asp:Content>

