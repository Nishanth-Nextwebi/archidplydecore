<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="product-detail.aspx.cs" Inherits="product_detail" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Admin/assets/plugins/snackbar/snackbar.min.css" rel="stylesheet" />
    <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
    <style>
        .slick-prev {
            right: 97%;
            margin-right: 0.5rem;
        }

        .slick-next {
            left: 97%;
            margin-left: 0.5rem;
        }

        .bg-new-color {
            background: #f4986d;
            color: #fff;
        }

            .bg-new-color:hover {
                border-color: #f4986d;
                color: #f4986d;
                background-color: #fff;
            }

        @media (max-width: 991px) {
            .accordion-flush .accordion-item {
                padding-right: 10px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="asp" runat="server"></asp:ToolkitScriptManager>


    <main id="content" class="wrapper layout-page">
        <section class="z-index-2 d-xl-block d-none position-relative pb-2 mb-12 new-hide-section">
        </section>
        <section class="container container-xxl pt-6 pb-13 pb-lg-15">
            <div class="container">
                <nav class="py-4 lh-30px" aria-label="breadcrumb">
                    <ol class="breadcrumb justify-content-start py-1 mb-0">
                        <li class="breadcrumb-item"><a title="Home" href="/">Home</a></li>
                        <li class="breadcrumb-item"><a title="Shop" href="javascript:void(0)">Products</a></li>
                        <li class="breadcrumb-item active" aria-current="page"><%=strProductName %></li>
                    </ol>
                </nav>
            </div>

            <%=strProducts %>
            <%--<div class='row justify-content-center'>
                <div class='col-md-6 pe-lg-13'>
                    <div class='position-sticky top-0'>
                        <div class='row'>
                            <div class='col-xl-2 pe-xl-0 order-1 order-xl-0 mt-5 mt-xl-0'>
                                <div id='vertical-slider-thumb' class='slick-slider slick-slider-thumb ps-1 ms-n3 me-n4 mx-xl-0' data-slick-options='{&#34;arrows&#34;:false,&#34;asNavFor&#34;:&#34;#vertical-slider-slides&#34;,&#34;dots&#34;:false,&#34;focusOnSelect&#34;:true,&#34;responsive&#34;:[{&#34;breakpoint&#34;:1260,&#34;settings&#34;:{&#34;vertical&#34;:false}}],&#34;slidesToShow&#34;:4,&#34;vertical&#34;:true}'><img src='#' data-src='assets/imgs/gurjan/archidplyclubplus1.jpg' class='cursor-pointer lazy-image mx-3 mx-xl-0 px-0 mb-xl-7' width='75' height='100' title='' alt=''><img src='#' data-src='assets/imgs/gurjan/archidplyclubplus2.jpg' class='cursor-pointer lazy-image mx-3 mx-xl-0 px-0 mb-xl-7' width='75' height='100' title='' alt=''><img src='#' data-src='assets/imgs/gurjan/archid-flexible.jpg' class='cursor-pointer lazy-image mx-3 mx-xl-0 px-0 mb-xl-7' width='75' height='100' title='' alt=''><img src='#' data-src='assets/imgs/gurjan/archidplyclub1.jpg' class='cursor-pointer lazy-image mx-3 mx-xl-0 px-0 mb-xl-7' width='75' height='100' title='' alt=''></div>
                            </div>
                            <div class='col-xl-10 ps-xl-8 pe-xl-0 order-0 order-xl-1'>
                                <div id='vertical-slider-slides' class='slick-slider slick-slider-arrow-inside slick-slider-dots-inside slick-slider-dots-light g-0' data-slick-options='{&#34;arrows&#34;:false,&#34;asNavFor&#34;:&#34;#vertical-slider-thumb&#34;,&#34;dots&#34;:false,&#34;slidesToShow&#34;:1,&#34;vertical&#34;:true}'><a href='assets/imgs/gurjan/archidplyclubplus1.jpg' data-gallery='product-gallery' data-thumb-src='assets/imgs/gurjan/archidplyclubplus1.jpg'><img src='#' data-src='assets/imgs/gurjan/archidplyclubplus1.jpg' width='540' height='720' title='' class='h-auto lazy-image' alt=''></a><a href='assets/imgs/gurjan/archidplyclubplus2.jpg' data-gallery='product-gallery' data-thumb-src='assets/imgs/gurjan/archidplyclubplus2.jpg'><img src='#' data-src='assets/imgs/gurjan/archidplyclubplus2.jpg' width='540' height='720' title='' class='h-auto lazy-image' alt=''></a><a href='assets/imgs/gurjan/archid-flexible.jpg' data-gallery='product-gallery' data-thumb-src='assets/imgs/gurjan/archid-flexible.jpg'><img src='#' data-src='assets/imgs/gurjan/archid-flexible.jpg' width='540' height='720' title='' class='h-auto lazy-image' alt=''></a><a href='assets/imgs/gurjan/archidplyclub1.jpg' data-gallery='product-gallery' data-thumb-src='assets/imgs/gurjan/archidplyclub1.jpg'><img src='#' data-src='assets/imgs/gurjan/archidplyclub1.jpg' width='540' height='720' title='' class='h-auto lazy-image' alt=''></a></div>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class='col-md-4 pt-md-0 pt-10'>
                    <h1 class='mb-4 pb-2 fs-3'>Archidply Club Plus</h1>
                    <p class='fs-17px'>
                        Archidply Club Plus is a magnificent addition to the Archidply plywood range. It is made using advanced technology that exceeds industry standards of durability and reliability.
                    </p>
                    <h4 class='text-left'>Features</h4>
                    <div class='product-features'>
                        <div class='row'>
                            <div class='col-md-6'>
                                <ul>
                                    <li>
                                        <img src='assets/imgs/strong.png' width='45' class='img-fluid' />
                                        <span class='icon-box-title card-title'>Strong/Durable</span>
                                    </li>
                                    <li>
                                        <img src='assets/imgs/durable.png' width='45' class='img-fluid' />
                                        <span class='icon-box-title card-title'>High-density</span>
                                    </li>
                                    <li>
                                        <img src='assets/imgs/veneer.png' width='45' class='img-fluid' />
                                        <span class='icon-box-title card-title'>Core composed veneer</span>
                                    </li>
                                </ul>
                            </div>
                            <div class='col-md-6'>
                                <ul>
                                    <li>
                                        <img src='assets/imgs/surface.png' width='45' class='img-fluid' />
                                        <span class='icon-box-title card-title'>100% Gurjan</span>
                                    </li>
                                    <li>
                                        <img src='assets/imgs/boiling.png' width='45' class='img-fluid' />
                                        <span class='icon-box-title card-title'>BWP grade</span>
                                    </li>
                                    <li>
                                        <img src='assets/imgs/termite-proof.png' width='45' class='img-fluid' />
                                        <span class='icon-box-title card-title'>Termite-proof</span>
                                    </li>
                                </ul>
                            </div>

                        </div>
                    </div>
                    <div class='card border border-2  rounded mb-8' style='box-shadow: 0 0 10px 0 rgba(0,0,0,0.1)'>
                        <div class='card-body py-0 px-0'>
                            <div class='product-features'>
                                <div class='row align-items-center'>
                                    <div class='col-md-12'>
                                        <div class='card border-2 mb-0'>
                                            <div class='card-body px-0 pt-0 pb-0'>
                                                <div class='d-flex align-items-center justify-content-between mb-0 bg-transparent-blue'>
                                                    <span>Available sizes (In ft):</span>
                                                    <span class='d-block ml-auto text-body-emphasis fw-bold fea'>8x4</span>
                                                </div>
                                                <div class='d-flex align-items-center justify-content-between bg-transparent-white'>
                                                    <span class='font-bold'>Available thickness (In mm)</span>
                                                    <span class='d-block ml-auto text-body-emphasis fw-bold fea'>6, 9, 12, 16, 19</span>
                                                </div>
                                                <div class='d-flex align-items-center justify-content-between bg-transparent-blue'>
                                                    <span>Warranty</span>
                                                    <span class='d-block ml-auto text-body-emphasis fw-bold fea'>Life Time Warranty</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class='row justify-content-center'>
                        <div class='col-md-5'>
                            <a href='#' class='btn w-100 green-btn' btn-hover-bg-primary btn-hover-border-primary' title='Check Out'>Enquiry </a>
                        </div>
                    </div>


                </div>
            </div>--%>
        </section>
        <div class="border-top w-100 h-1px"></div>
        <section style="background-color: #f8f8f8">
            <div class="container container-xxl pt-10 pb-10 pt-lg-10 pb-lg-20">
                <div class="text-lg-center text-center">
                    <h2 class="mb-12">Related Products</h2>
                </div>
                <div class="slick-slider related-products" data-slick-options="{&#34;arrows&#34;:true,&#34;centerMode&#34;:true,&#34;centerPadding&#34;:&#34;calc((100% - 1440px) / 2)&#34;,&#34;dots&#34;:true,&#34;infinite&#34;:true,&#34;responsive&#34;:[{&#34;breakpoint&#34;:1200,&#34;settings&#34;:{&#34;arrows&#34;:false,&#34;dots&#34;:false,&#34;slidesToShow&#34;:3}},{&#34;breakpoint&#34;:992,&#34;settings&#34;:{&#34;arrows&#34;:false,&#34;dots&#34;:true,&#34;slidesToShow&#34;:2}},{&#34;breakpoint&#34;:576,&#34;settings&#34;:{&#34;arrows&#34;:false,&#34;dots&#34;:true,&#34;slidesToShow&#34;:2}}],&#34;slidesToShow&#34;:4}">

                    <%=strRelatedProducts %>
                    <%--   <div class="mb-6">
                        <div class="card card-product grid-2 bg-transparent border-0">
                            <figure class="card-img-top position-relative mb-0 overflow-hidden">
                                <a href="#" class="hover-zoom-in d-block" title="Shield Conditioner">
                                    <img src="#" data-src="assets/imgs/gurjan/born-vivant2.jpg" class="img-fluid lazy-image w-100" alt="Shield Conditioner" width="330" height="440">
                                </a>
                            </figure>
                            <div class="card-body text-center p-0">
                                <h4 class="product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3"><a class="text-decoration-none text-reset" href="#">Bon vivant premium plywood</a></h4>
                                <a href="#" class="btn btn-link p-0 mt-2 text-decoration-none text-primary fw-semibold">
                                    Read More<i class="far fa-arrow-right ps-2 fs-13px"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="mb-6">
                        <div class="card card-product grid-2 bg-transparent border-0">
                            <figure class="card-img-top position-relative mb-0 overflow-hidden">
                                <a href="#" class="hover-zoom-in d-block" title="Perfecting Facial Oil">
                                    <img src="#" data-src="assets/imgs/gurjan/archidplyclubplus1.jpg" class="img-fluid lazy-image w-100" alt="Perfecting Facial Oil" width="330" height="440">
                                </a>
                            </figure>
                            <div class="card-body text-center p-0">
                                <h4 class="product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3"><a class="text-decoration-none text-reset" href="#">Archidply club plus</a></h4>
                                <a href="#" class="btn btn-link p-0 mt-2 text-decoration-none text-primary fw-semibold">
                                    Read More<i class="far fa-arrow-right ps-2 fs-13px"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="mb-6">
                        <div class="card card-product grid-2 bg-transparent border-0">
                            <figure class="card-img-top position-relative mb-0 overflow-hidden">
                                <a href="#" class="hover-zoom-in d-block" title="Enriched Hand &amp; Body Wash">
                                    <img src="#" data-src="assets/imgs/gurjan/archidplyclub1.jpg" class="img-fluid lazy-image w-100" alt="Enriched Hand &amp; Body Wash" width="330" height="440">
                                </a>
                            </figure>
                            <div class="card-body text-center p-0">
                                <h4 class="product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3"><a class="text-decoration-none text-reset" href="#">Archidply club </a></h4>
                                <a href="#" class="btn btn-link p-0 mt-2 text-decoration-none text-primary fw-semibold">
                                    Read More<i class="far fa-arrow-right ps-2 fs-13px"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="mb-6">
                        <div class="card card-product grid-2 bg-transparent border-0">
                            <figure class="card-img-top position-relative mb-0 overflow-hidden">
                                <a href="#" class="hover-zoom-in d-block" title="Shield Shampoo">
                                    <img src="#" data-src="assets/imgs/gurjan/archid-flexible.jpg" class="img-fluid lazy-image w-100" alt="Shield Shampoo" width="330" height="440">
                                </a>
                            </figure>
                            <div class="card-body text-center p-0">
                                <h4 class="product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3"><a class="text-decoration-none text-reset" href="#">Archid Flexible</a></h4>
                                <a href="#" class="btn btn-link p-0 mt-2 text-decoration-none text-primary fw-semibold">
                                    Read More<i class="far fa-arrow-right ps-2 fs-13px"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="mb-6">
                        <div class="card card-product grid-2 bg-transparent border-0">
                            <figure class="card-img-top position-relative mb-0 overflow-hidden">
                                <a href="#" class="hover-zoom-in d-block" title="Shield Conditioner">
                                    <img src="#" data-src="assets/imgs/gurjan/born-vivant2.jpg" class="img-fluid lazy-image w-100" alt="Shield Conditioner" width="330" height="440">
                                </a>
                            </figure>
                            <div class="card-body text-center p-0">
                                <h4 class="product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3"><a class="text-decoration-none text-reset" href="#">Bon vivant premium plywood</a></h4>
                                <a href="#" class="btn btn-link p-0 mt-2 text-decoration-none text-primary fw-semibold">
                                    Read More<i class="far fa-arrow-right ps-2 fs-13px"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="mb-6">
                        <div class="card card-product grid-2 bg-transparent border-0">
                            <figure class="card-img-top position-relative mb-0 overflow-hidden">
                                <a href="#" class="hover-zoom-in d-block" title="Perfecting Facial Oil">
                                    <img src="#" data-src="assets/imgs/gurjan/archidplyclubplus1.jpg" class="img-fluid lazy-image w-100" alt="Perfecting Facial Oil" width="330" height="440">
                                </a>
                            </figure>
                            <div class="card-body text-center p-0">
                                <h4 class="product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3"><a class="text-decoration-none text-reset" href="#">Archidply club plus</a></h4>
                                <a href="#" class="btn btn-link p-0 mt-2 text-decoration-none text-primary fw-semibold">
                                    Read More<i class="far fa-arrow-right ps-2 fs-13px"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="mb-6">
                        <div class="card card-product grid-2 bg-transparent border-0">
                            <figure class="card-img-top position-relative mb-0 overflow-hidden">
                                <a href="#" class="hover-zoom-in d-block" title="Enriched Hand &amp; Body Wash">
                                    <img src="#" data-src="assets/imgs/gurjan/archidplyclub1.jpg" class="img-fluid lazy-image w-100" alt="Enriched Hand &amp; Body Wash" width="330" height="440">
                                </a>
                            </figure>
                            <div class="card-body text-center p-0">
                                <h4 class="product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3"><a class="text-decoration-none text-reset" href="#">Archidply club </a></h4>
                                <a href="#" class="btn btn-link p-0 mt-2 text-decoration-none text-primary fw-semibold">
                                    Read More<i class="far fa-arrow-right ps-2 fs-13px"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="mb-6">
                        <div class="card card-product grid-2 bg-transparent border-0">
                            <figure class="card-img-top position-relative mb-0 overflow-hidden">
                                <a href="#" class="hover-zoom-in d-block" title="Shield Shampoo">
                                    <img src="#" data-src="assets/imgs/gurjan/archid-flexible.jpg" class="img-fluid lazy-image w-100" alt="Shield Shampoo" width="330" height="440">
                                </a>
                            </figure>
                            <div class="card-body text-center p-0">
                                <h4 class="product-title card-title text-primary-hover text-body-emphasis fs-15px fw-500 mb-3"><a class="text-decoration-none text-reset" href="#">Archid Flexible</a></h4>
                                <a href="#" class="btn btn-link p-0 mt-2 text-decoration-none text-primary fw-semibold">
                                    Read More<i class="far fa-arrow-right ps-2 fs-13px"></i>
                                </a>
                            </div>
                        </div>

                    </div>--%>
                </div>
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </div>

            <div class="modal fade" id="contactUsModal" tabindex="-1" data-bs-backdrop="static" aria-labelledby="contactUsModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content">
                        <div class="modal-header text-center border-0 pb-0">
                            <button type="button" class="btn-close position-absolute end-5 top-5" id="btn-closee" data-bs-dismiss="modal" aria-label="Close"></button>
                            <h3 class="modal-title w-100" id="contactUsModalLabel">Quick Enquiry</h3>
                        </div>
                        <div class="modal-body px-sm-13 px-8">
                            <p class="text-center fs-16 mb-10">
                                Have questions or want to know more about this product? Feel free to reach out to us.
                            </p>
                            <div id="enquiryForm">
                                <div class="mb-3">
                                    <input type="text" id="txtFullName" class="form-control border-1 mb-5 acceptOnlyAlpha" placeholder="Full Name*">
                                    <span class="error-message text-danger"></span>
                                </div>
                                <div class="mb-3">
                                    <input type="text" id="txtPhone" class="form-control border-1 mb-5 onlyNum" placeholder="Phone Number*" maxlength="10">
                                    <span class="error-message text-danger"></span>
                                </div>
                                <div class="mb-3">
                                    <input type="email" id="txtEmail" class="form-control border-1 mb-5" placeholder="Your Email*">
                                    <span class="error-message text-danger"></span>
                                </div>
                                <div class="mb-3">
                                    <input type="text" id="txtCity" class="form-control border-1 mb-5" placeholder="Your City*">
                                    <span class="error-message text-danger"></span>
                                </div>
                                <div class="mb-3">
                                    <textarea id="txtMessage" class="form-control border-1 mb-5" rows="4" placeholder="Your Message"></textarea>
                                </div>
                                <button type="submit" id="btnSendMessage" class="btn w-100 green-btn btn-hover-bg-primary btn-hover-border-primary">Send Message</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <%--<div class="modal" id="contactUsModal" tabindex="-1" aria-labelledby="contactUsModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content">
                        <div class="modal-header text-center border-0 pb-0">
                            <button type="button"
                                class="btn-close position-absolute end-5 top-5"
                                data-bs-dismiss="modal"
                                aria-label="Close">
                            </button>
                            <h3 class="modal-title w-100" id="contactUsModalLabel">Quick Enquiry</h3>
                        </div>
                        <div class="modal-body px-sm-13 px-8">
                            <p class="text-center fs-16 mb-10">
                                Have questions or Know more about this product? Feel free to reach out to us.
               
                            </p>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div>
                                        <asp:TextBox ID="txtFullName" runat="server" MaxLength="100" CssClass="form-control border-1 mb-5 acceptOnlyAlpha" Placeholder="Full Name*"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtFullName" SetFocusOnError="true" ValidationGroup="prodEnq" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtPhone" runat="server" MaxLength="10" CssClass="form-control border-1 mb-5 onlyNum" Placeholder="Phone Number*"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv4" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhone" SetFocusOnError="true" ValidationGroup="prodEnq" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPhone" Display="Dynamic" ValidationGroup="prodEnq" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Enter 10 Digit Valid Phone Number" ValidationExpression="^\d{10}$"></asp:RegularExpressionValidator>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="150" CssClass="form-control border-1 mb-5" Placeholder="Your Email*"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmail" SetFocusOnError="true" ValidationGroup="prodEnq" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="prodEnq" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Invalid E-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtMessage" runat="server" MaxLength="300" CssClass="form-control border-1 mb-5" TextMode="MultiLine" Rows="4" Placeholder="Your Message"></asp:TextBox>
                                    </div>
                                    <div>
                                        <asp:Button ID="btnSendMessage" runat="server" ValidationGroup="prodEnq" CssClass="btn w-100 green-btn btn-hover-bg-primary btn-hover-border-primary" Text="Send Message" OnClick="btnSendMessage_click" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSendMessage" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>--%>

            <%--Resource dowmnlod modal--%>
            <div class="modal fade sucess" id="exampleModal1" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content">
                        <div class="modal-header text-center border-0 pb-0">
                            <button type="button"
                                class="btn-close position-absolute end-5 top-5"
                                data-bs-dismiss="modal"
                                aria-label="Close">
                            </button>
                            <h3 class="modal-title w-100" id="exampleModalLabel">Download Resources</h3>
                        </div>
                        <div class="modal-body px-sm-13 px-8">
                            <p class="text-center fs-16 mb-10">
                                By filling the below form you can download resource
                            </p>
                            <div action="#">
                                <div class="error-message alert alert-danger d-block d-none fw-bold"></div>

                                <asp:TextBox ID="textFname" runat="server" class="form-control textFname mb-7" MaxLength="100" placeholder="Full Name"></asp:TextBox>
                                <asp:TextBox ID="txtemailAdress" class="form-control txtemailAdress mb-7" runat="server" MaxLength="100" placeholder="Email"></asp:TextBox>
                                <asp:TextBox ID="txtContact" class="form-control txtContact mb-7" runat="server" MaxLength="10" placeholder="Contact Number"></asp:TextBox>
                                <asp:TextBox ID="txtProfession" class="form-control txtProfession mb-7" runat="server" MaxLength="100" placeholder="Profession"></asp:TextBox>

                                <asp:LinkButton runat="server" ID="BtnSubmit" CssClass="btn btn-dark btn-hover-bg-primary btn-hover-border-primary mt-10 w-100 mb-6 submitdata BtnSubmit" ValidationGroup="Save">Download<i class="far fa-download ps-2 fs-13px"></i></asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal for certificate-->
        </section>

    </main>
    <script src="/Admin/assets/js/jquery-3.6.0.min.js"></script>
    <script src="/Admin/assets/plugins/snackbar/snackbar.min.js"></script>
    <script src="/assets/js/Pages/product-details.min.js"></script>
</asp:Content>

