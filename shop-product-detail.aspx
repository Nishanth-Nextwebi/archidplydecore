<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="shop-product-detail.aspx.cs" Inherits="shop_product_detail" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
    <style>
        .submitbtn {
            margin-left: 0px;
            margin-top: 20px;
        }

        .rating1 {
            margin-top: 0px;
            border: none;
            float: left;
        }

            .rating1 > label {
                color: #90A0A3;
                float: right;
                margin-bottom: 0px;
            }

                .rating1 > label:before {
                    margin: 5px;
                    font-size: 1em;
                    font-family: FontAwesome;
                    content: "\f005";
                    display: inline-block;
                }

            .rating1 > input {
                display: none;
            }

                .rating1 > input:checked ~ label,
                .rating1:not(:checked) > label:hover,
                .rating1:not(:checked) > label:hover ~ label {
                    color: #4e7661;
                }

                    .rating1 > input:checked + label:hover,
                    .rating1 > input:checked ~ label:hover,
                    .rating1 > label:hover ~ input:checked ~ label,
                    .rating1 > input:checked ~ label:hover ~ label {
                        color: #4e7661;
                    }

        .new-rating {
            display: flex;
            width: 100%;
            margin-bottom: 10px;
            justify-content: start;
            gap: 1rem;
            align-items: center;
        }

            .new-rating h6 {
                font-size: 16px;
                font-weight: 700;
                padding-left: 10px;
                margin: 0px;
            }

        .ItemNotAvailable {
            margin-left: 365px;
            background: red;
            color: white;
            border-radius: 25px;
        }

        .ItemAvailable {
            margin-left: 365px;
            background: Green;
            color: white;
            border-radius: 25px;
        }

        .star svg {
            height: 24px !important;
            width: 24px !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="asp" runat="server"></asp:ToolkitScriptManager>

    <main id="content" class="wrapper layout-page">


        <section class="container pt-6 pb-13 pb-lg-20">
            <div class="row justify-content-center">
                <div class="col-lg-11 col-md-12">
                    <nav class="py-0 lh-30px" aria-label="breadcrumb">
                        <ol class="breadcrumb justify-content-start py-1">
                            <li class="breadcrumb-item"><a href="/">Home</a></li>
                            <li class="breadcrumb-item"><a href="/shop">Shop</a></li>
                            <li class="breadcrumb-item active" aria-current="page"><%=strProductName %></li>
                        </ol>
                    </nav>
                </div>
            </div>
            <div class="row justify-content-center mt-2">
                <div class="col-md-12 col-lg-6 pe-lg-13">
                    <div class="new-gallery">
                        <%=strProductGallery %>
                        <%--       <a href="https://archidplydecor.com/wp-content/uploads/2018/11/Wonder-Grain.jpg" data-gallery="gallery3" data-thumb-src="https://archidplydecor.com/wp-content/uploads/2018/11/Wonder-Grain.jpg">
                        <img src="#" data-src="https://archidplydecor.com/wp-content/uploads/2018/11/Wonder-Grain.jpg" class="lazy-image mb-7 img-fluid h-auto" width="540" height="720" alt=""></a>
                    <a href="https://archidplydecor.com/wp-content/uploads/2018/11/Metallic.jpg" data-gallery="gallery3" data-thumb-src="https://archidplydecor.com/wp-content/uploads/2018/11/Metallic.jpg">
                        <img src="#" data-src="https://archidplydecor.com/wp-content/uploads/2018/11/Metallic.jpg" class="lazy-image mb-7 img-fluid h-auto" width="540" height="720" alt=""></a>
                    <a href="https://archidplydecor.com/wp-content/uploads/2018/11/10x4.jpg" data-gallery="gallery3" data-thumb-src="https://archidplydecor.com/wp-content/uploads/2018/11/10x4.jpg">
                        <img src="#" data-src="https://archidplydecor.com/wp-content/uploads/2018/11/10x4.jpg" class="lazy-image mb-7 img-fluid h-auto" width="540" height="720" alt=""></a>
                    <a href="https://archidplydecor.com/wp-content/uploads/2018/11/Vintage.jpg" data-gallery="gallery3" data-thumb-src="https://archidplydecor.com/wp-content/uploads/2018/11/Vintage.jpg">
                        <img src="#" data-src="https://archidplydecor.com/wp-content/uploads/2018/11/Vintage.jpg" class="lazy-image mb-7 img-fluid h-auto" width="540" height="720" alt=""></a>
                        --%>
                    </div>
                </div>
                <div class="col-md-12 col-lg-6 pt-md-0 pt-10">
                    <div class="position-sticky top-0 new-details">
                        <asp:HiddenField ID="HiddenStockStatus" runat="server" />
                        <h1 class="mb-4 pb-2 fs-2"><%=strProductName %></h1>
                        <p class="d-flex align-items-center new-price-flex mb-6">
                            <span class="fs-18px text-body-emphasis fw-bold fs-32px  disprice">₹<%=strDiscountPrice %></span>
                            <span class="text-decoration-line-through ps-6 actualprce">₹<%=strActalPrice %></span>
                            <span class="badge text-bg-primary fs-6 fw-semibold ms-7 px-6 py-3 discountPercent"><%=strDiscountpercentage %></span>
                        </p>
                        <div class="d-flex align-items-center fs-15px mb-6">
                            <p class="mb-0 fw-semibold text-body-emphasis"><%=strRatingTotalCount %></p>
                            <div class="d-flex align-items-center fs-12px justify-content-center mb-0 px-6 rating-result">
                                <div class="rating">
                                    <%=strAvgRating %>
                                </div>
                            </div>
                            <a data-scroll="#reviews" runat="server" id="AddReview" class="border-start ps-6 text-body">Read reviews</a>
                        </div>
                        <p class="fs-15px"><%=strShordDesc %></p>
                        <ul class="list-unstyled">
                            <div class="row gy-15px align-items-center spacing-02">
                                <div class="col-auto dropdown skin-goal px-4">
                                    <label for="image_video" class="visually-hidden">Size </label>
                                    <%--<select name="image_video" id="image_video" class="form-select" fdprocessedid="66z9tc">
                                        <option>Size</option>
                                        <option>2400mm</option>
                                        <option>1200mm</option>
                                    </select>--%>
                                    <asp:DropDownList runat="server" class="form-select" ID="ddlSize">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hfProductId" runat="server" />
                                </div>
                                <div class="col-auto dropdown skin-goal px-4">
                                    <label for="sort_by" class="visually-hidden">Thickness</label>
                                    <select name="ddlThickness" id="ddlThickness" class="form-select">
                                    </select>
                                    <%--<asp:DropDownList runat="server" class="form-select" ID="ddlThickness">
                                    </asp:DropDownList>--%>
                                </div>
                            </div>
                        </ul>




                        <div class="mb-9 pb-2">
                            <div class="row align-items-end">
                                <div class="form-group col-sm-4" runat="server" id="quantityDiv" visible="false">
                                    <label class="text-body-emphasis fw-semibold fs-15px pb-6" for="number">Quantity: </label>
                                    <div class="input-group position-relative w-100 input-group-lg">
                                        <a href="javascript:void(0)" class="shop-down position-absolute translate-middle-y top-50 start-0 ps-7 product-info-2-minus qty-left-minus">
                                            <i class="far fa-minus"></i>
                                        </a>
                                        <input type="text" name="quantity" id="product_quantity" class="form-control form-control-sm px-10 py-4 fs-6 text-center border-0 input-number qty-input"
                                            value="1" required readonly />
                                        <a href="javascript:void(0)" class="shop-up position-absolute translate-middle-y top-50 end-0 pe-7 product-info-2-plus qty-right-plus">
                                            <i class="far fa-plus"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="col-sm-8 pt-9 mt-2 mt-sm-0 pt-sm-0">
                                    <%=strProdStatus %>
                                    <asp:HiddenField ID="productIdHidden" runat="server" />
                                </div>
                            </div>
                            <span class="quantity-error text-danger d-block mt-1"></span>
                            <small class="text-danger">Delivery is currently available only in Bengaluru</small>
                        </div>

                        <div class="accordion accordion-flush" id="accordionFlushExample">
                            <div class="accordion-item pb-4">
                                <h2 class="accordion-header" id="flush-headingOne">
                                    <a class="product-info-accordion text-decoration-none" href="#" data-bs-toggle="collapse" data-bs-target="#flush-collapseOne" aria-expanded="false" aria-controls="flush-collapseOne">
                                        <span class="fs-4">Product Detail</span>
                                    </a>
                                </h2>
                            </div>
                            <div id="flush-collapseOne" class="accordion-collapse collapse show" aria-labelledby="flush-headingOne" data-bs-parent="#accordionFlushExample">
                                <div class="pt-8 pb-3">
                                    <p class="mb-2 pb-4"><%=strProdDetails %></p>
                                </div>
                            </div>
                            <div class="accordion-item pb-4 mt-7">
                                <h2 class="accordion-header" id="flush-headingTwo">
                                    <a class="product-info-accordion collapsed text-decoration-none" href="#" data-bs-toggle="collapse" data-bs-target="#flush-collapseTwo" aria-expanded="false" aria-controls="flush-collapseTwo">
                                        <span class="fs-4">Specifications</span>
                                    </a>
                                </h2>
                            </div>
                            <div id="flush-collapseTwo" class="accordion-collapse collapse" aria-labelledby="flush-headingTwo" data-bs-parent="#accordionFlushExample">
                                <div class="pt-8 pb-3"><%=strProdSpecifications %></div>
                            </div>
                            <div class="accordion-item pb-4 mt-7">
                                <h2 class="accordion-header" id="flush-headingThree">
                                    <a class="product-info-accordion collapsed text-decoration-none" href="#" data-bs-toggle="collapse" data-bs-target="#flush-collapseThree" aria-expanded="false" aria-controls="flush-collapseThree">
                                        <span class="fs-4">Enquiry Form</span>
                                    </a>
                                </h2>
                            </div>
                            <div id="flush-collapseThree" class="accordion-collapse collapse" aria-labelledby="flush-headingThree" data-bs-parent="#accordionFlushExample">
                                <div class="pt-8 pb-3">
                                    <div class="row mb-8 mb-md-10">
                                        <div class="col-md-6 col-12 mb-8 mb-md-0">
                                            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control input-focus acceptOnlyAlpha" Placeholder="Name"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="NameValidator" runat="server" ControlToValidate="txtFullName" ErrorMessage="Name is required." CssClass="text-danger" Display="Dynamic" />
                                        </div>
                                        <div class="col-md-6 col-12">
                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control input-focus onlyNum" MaxLength="10" Placeholder="Contact Number" TextMode="Phone"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="ContactNumberValidator" runat="server" ControlToValidate="txtPhone" ErrorMessage="Contact number is required." CssClass="text-danger" Display="Dynamic" />
                                            <asp:RegularExpressionValidator ID="ContactNumberFormatValidator" runat="server" ControlToValidate="txtPhone"
                                                ValidationExpression="^\d{10}$" ErrorMessage="Contact number must be 10 digits." CssClass="text-danger" Display="Dynamic" />
                                        </div>
                                    </div>
                                    <div class="row mb-8 mb-md-10">
                                        <div class="col-md-12 col-12 mb-0 mb-md-0">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control input-focus" MaxLength="100" Placeholder="Email" TextMode="Email"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="EmailValidator" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required." CssClass="text-danger" Display="Dynamic" />
                                            <asp:RegularExpressionValidator ID="EmailFormatValidator" runat="server" ControlToValidate="txtEmail"
                                                ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ErrorMessage="Invalid email format." CssClass="text-danger" Display="Dynamic" />
                                        </div>
                                    </div>
                                    <div class="row mb-8 mb-md-10">
                                        <div class="col-md-12 col-12 mb-8 mb-md-0">
                                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control input-focus acceptOnlyAlpha" Placeholder="City"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCity" ErrorMessage="City is required." CssClass="text-danger" Display="Dynamic" />
                                        </div>
                                    </div>

                                    <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control mb-6 input-focus" MaxLength="300" Placeholder="Messenger" TextMode="MultiLine" Rows="7"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="MessageValidator" runat="server" ControlToValidate="txtMessage" ErrorMessage="Message is required." CssClass="text-danger" Display="Dynamic" />
                                    <br />
                                    <div class="text-start">
                                        <asp:Button ID="SubmitButton" runat="server" CssClass="btn btn-dark btn-hover-bg-primary btn-hover-border-primary text-start px-11 submitbtn" Text="Submit" OnClick="EnquiryButton_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </section>
        <%=strRelatedProducts %>

        <%=strFAQs %>

        <%--<section class="container pt-15 pb-15 pt-lg-10 pb-lg-10">
            <div class="text-center">
                <h2 class="mb-12" runat="server">FAQ's</h2>
            </div>
            <div class="col-lg-12">
                <div class="accordion accordion-flush" id="accordionFlushExample2">
						<div class="accordion-item pb-5 pt-11 pt-md-0">
							<h2 class="accordion-header" id="flush-headingReturnsOne">
								<a class="product-info-accordion collapsed text-decoration-none" href="#"
								   data-bs-toggle="collapse" data-bs-target="#flush-collapseReturnsOne" aria-expanded="false"
								   aria-controls="flush-collapseReturnsOne">
									<span class="fs-18px">How do I know my package has shipped?</span>
								</a>
							</h2>
						</div>
						<div id="flush-collapseReturnsOne" class="accordion-collapse collapse"
							 aria-labelledby="flush-headingReturnsOne" data-bs-parent="#accordionFlushExample2">
							<div class="py-8">
								<p>Complexion-perfecting natural foundation enriched with antioxidant-packed superfruits,
									vitamins, and
									other skin-nourishing nutrients. Creamy liquid formula sets with a pristine matte finish
									for
									soft,
									velvety smooth skin.</p>
								<p class="mb-0">
									Say hello to flawless, long-lasting foundation that comes in 7 melt-into-your-skin
									shades.
									This
									lightweight, innovative formula creates a smooth, natural matte finish that won’t settle
									into lines.
									It’s the perfect fit for your skin. 1 fl. oz.
								</p>
							</div>
						</div>

                        
						<div class="accordion-item pb-4 pt-11">
							<h2 class="accordion-header" id="flush-headingReturnsTwo">
								<a class="product-info-accordion collapsed text-decoration-none" href="#"
								   data-bs-toggle="collapse" data-bs-target="#flush-collapseReturnsTwo" aria-expanded="false"
								   aria-controls="flush-collapseReturnsTwo">
								<span
									class="fs-18px">Why are certain products unavailable to ship to Internationally?</span>
								</a>
							</h2>
						</div>
						<div id="flush-collapseReturnsTwo" class="accordion-collapse collapse" aria-labelledby="flush-headingTwo"
							 data-bs-parent="#accordionFlushExample2">
							<div class="py-8">
								<p>Complexion-perfecting natural foundation enriched with antioxidant-packed superfruits,
									vitamins, and
									other skin-nourishing nutrients. Creamy liquid formula sets with a pristine matte finish
									for
									soft,
									velvety smooth skin.</p>
								<p class="mb-0">
									Say hello to flawless, long-lasting foundation that comes in 7 melt-into-your-skin
									shades.
									This
									lightweight, innovative formula creates a smooth, natural matte finish that won’t settle
									into lines.
									It’s the perfect fit for your skin. 1 fl. oz.
								</p>
							</div>
						</div>
                        
						<div class="accordion-item pb-4 pt-11">
							<h2 class="accordion-header" id="flush-headingReturnsThree">
								<a class="product-info-accordion collapsed text-decoration-none" href="#"
								   data-bs-toggle="collapse" data-bs-target="#flush-collapseReturnsThree" aria-expanded="false"
								   aria-controls="flush-collapseReturnsThree">
									<span class="fs-18px">Why is my tracking number not updating?</span>
								</a>
							</h2>
						</div>
						<div id="flush-collapseReturnsThree" class="accordion-collapse collapse"
							 aria-labelledby="flush-headingThree" data-bs-parent="#accordionFlushExample2">
							<div class="py-8">
								<p>Complexion-perfecting natural foundation enriched with antioxidant-packed superfruits,
									vitamins, and
									other skin-nourishing nutrients. Creamy liquid formula sets with a pristine matte finish
									for
									soft,
									velvety smooth skin.</p>
								<p class="mb-0">
									Say hello to flawless, long-lasting foundation that comes in 7 melt-into-your-skin
									shades.
									This
									lightweight, innovative formula creates a smooth, natural matte finish that won’t settle
									into lines.
									It’s the perfect fit for your skin. 1 fl. oz.
								</p>
							</div>
						</div>
            </div>
                </div>
        </section>--%>





        <section class="container pt-15 pb-15 pt-lg-10 pb-lg-10" id="reviews">
            <div class="text-center">
                <h2 class="mb-12" runat="server">Customer Reviews</h2>
            </div>

            <div class="mb-11">
                <div class=" d-md-flex justify-content-between align-items-center">
                    <div class=" d-flex align-items-center">
                        <h4 class="fs-1 me-9 mb-0"><%=strRatingTotalCount %></h4>
                        <div class="p-0">
                            <div class="d-flex align-items-center fs-6 ls-0 mb-4">
                                <div class="rating">
                                    <div class="rating1">
                                        <input type="radio" id="star5" name="rating" value="5" />
                                        <label class="star" for="star5" title="Awesome" aria-hidden="true"></label>
                                        <input type="radio" id="star4" name="rating" value="4" />
                                        <label class="star" for="star4" title="Great" aria-hidden="true"></label>
                                        <input type="radio" id="star3" name="rating" value="3" />
                                        <label class="star" for="star3" title="Very good" aria-hidden="true"></label>
                                        <input type="radio" id="star2" name="rating" value="2" />
                                        <label class="star" for="star2" title="Good" aria-hidden="true"></label>
                                        <input type="radio" id="star1" name="rating" value="1" />
                                        <label class="star" for="star1" title="Bad" aria-hidden="true"></label>
                                    </div>
                                    <%--                                    <div class="empty-stars">
                                        <span class="star">
                                            <svg class="icon star-o">
                                                <use xlink:href="#star-o"></use>
                                            </svg>
                                        </span>
                                        <span class="star">
                                            <svg class="icon star-o">
                                                <use xlink:href="#star-o"></use>
                                            </svg>
                                        </span>
                                        <span class="star">
                                            <svg class="icon star-o">
                                                <use xlink:href="#star-o"></use>
                                            </svg>
                                        </span>
                                        <span class="star">
                                            <svg class="icon star-o">
                                                <use xlink:href="#star-o"></use>
                                            </svg>
                                        </span>
                                        <span class="star">
                                            <svg class="icon star-o">
                                                <use xlink:href="#star-o"></use>
                                            </svg>
                                        </span>
                                    </div>
                                    <div class="filled-stars"
                                        style="width: 96%">
                                        <span class="star">
                                            <svg class="icon star text-primary">
                                                <use xlink:href="#star"></use>
                                            </svg>
                                        </span>
                                        <span class="star">
                                            <svg class="icon star text-primary">
                                                <use xlink:href="#star"></use>
                                            </svg>
                                        </span>
                                        <span class="star">
                                            <svg class="icon star text-primary">
                                                <use xlink:href="#star"></use>
                                            </svg>
                                        </span>
                                        <span class="star">
                                            <svg class="icon star text-primary">
                                                <use xlink:href="#star"></use>
                                            </svg>
                                        </span>
                                        <span class="star">
                                            <svg class="icon star text-primary">
                                                <use xlink:href="#star"></use>
                                            </svg>
                                        </span>
                                    </div>--%>
                                </div>
                            </div>
                            <p class="mb-0">Please rate your experience with our product (1-5 stars)</p>
                        </div>
                    </div>
                    <div class="text-md-end mt-md-0 mt-7">
                        <a href="#customer-review" class="btn btn-outline-dark" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="customer-review">Post Review</a>
                    </div>
                </div>
            </div>

            <div class="collapse mb-14" id="customer-review">
                <div class="product-review-form">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row">

                                <div class="col-sm-4">
                                    <div class="form-group mb-7">
                                        <label class="mb-4 fs-6 fw-semibold text-body-emphasis" for="reviewName">Name</label>
                                        <asp:TextBox ID="reviewName" class="form-control alphaonly" runat="server" placeholder="Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ControlToValidate="reviewName" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="rat" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group mb-4">
                                        <label class="mb-4 fs-6 fw-semibold text-body-emphasis" for="reviewEmail">Email</label>
                                        <asp:TextBox ID="reviewEmail" class="form-control" placeholder="Email" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="rev1" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="reviewEmail" SetFocusOnError="true" ValidationGroup="rat" ErrorMessage="Invalid e-mail address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="reviewEmail" SetFocusOnError="true" ValidationGroup="rat" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group mb-7">
                                        <label class="mb-4 fs-6 fw-semibold text-body-emphasis" for="reviewPhone">Phone number</label>
                                        <asp:TextBox ID="reviewPhone" class="form-control" MaxLength="10" runat="server" placeholder="Phone Number"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="reviewPhone" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="rat" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group mb-4">
                                        <label class="mb-4 fs-6 fw-semibold text-body-emphasis" for="reviewSubject">Subject</label>
                                        <asp:TextBox ID="reviewSubject" class="form-control" runat="server" placeholder="Subject"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="reviewSubject" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="rat" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="form-group mb-7">
                                        <label class="mb-4 fs-6 fw-semibold text-body-emphasis" for="reviewMessage">Message</label>
                                        <asp:TextBox ID="reviewMessage" runat="server" class="form-control" Rows="5" placeholder="Your Reviews *" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="reviewMessage" SetFocusOnError="true" ValidationGroup="rat" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-sm-8">
                                    <asp:Button ID="btnRating" runat="server" Text="Submit" CssClass="btn green-btn" OnClick="btnRating_Click" ValidationGroup="rat"></asp:Button>
                                    <asp:HiddenField ID="SelectedRating" runat="server" />
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnRating" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="mt-12">
                <%=strReatings %>
                <%--<div class="border-bottom pb-7 pt-10">
                    <div class="d-flex align-items-center mb-6">
                        <div class="d-flex align-items-center fs-15px ls-0">
                            <div class="rating">
                                <div class="empty-stars">
                                    <span class="star">
                                        <svg class="icon star-o">
                                            <use xlink:href="#star-o"></use>
                                        </svg>
                                    </span>
                                    <span class="star">
                                        <svg class="icon star-o">
                                            <use xlink:href="#star-o"></use>
                                        </svg>
                                    </span>
                                    <span class='star'>
                                        <svg class='icon star-o'>
                                            <use xlink:href='#star-o'></use>
                                        </svg>
                                    </span>
                                    <span class="star">
                                        <svg class="icon star-o">
                                            <use xlink:href="#star-o"></use>
                                        </svg>
                                    </span>
                                    <span class="star">
                                        <svg class="icon star-o">
                                            <use xlink:href="#star-o"></use>
                                        </svg>
                                    </span>
                                </div>
                                <div class="filled-stars"
                                    style="width: 100%">
                                    <span class="star">
                                        <svg class="icon star text-primary">
                                            <use xlink:href="#star"></use>
                                        </svg>
                                    </span>
                                    <span class="star">
                                        <svg class="icon star text-primary">
                                            <use xlink:href="#star"></use>
                                        </svg>
                                    </span>
                                    <span class="star">
                                        <svg class="icon star text-primary">
                                            <use xlink:href="#star"></use>
                                        </svg>
                                    </span>
                                    <span class="star">
                                        <svg class="icon star text-primary">
                                            <use xlink:href="#star"></use>
                                        </svg>
                                    </span>
                                    <span class="star">
                                        <svg class="icon star text-primary">
                                            <use xlink:href="#star"></use>
                                        </svg>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <span class="fs-3px mx-5"><i class="fas fa-circle"></i></span>
                        <span class="fs-14">5 day ago</span>
                    </div>

                    <div class="d-flex justify-content-start align-items-center mb-5">
                        <img src="#" data-src="../assets/images/others/single-product-01.png" class="me-6 lazy-image rounded-circle" width="60" height="60" alt="Avatar">
                        <div class="">
                            <h5 class="mt-0 mb-4 fs-14px text-uppercase ls-1">Name.</h5>
                            <p class="mb-0">Location</p>
                        </div>
                    </div>
                    <p class="fw-semibold fs-6 text-body-emphasis mb-5">Favorite Foundation</p>
                    <p class="mb-10 fs-6">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. </p>



                </div>
                <div class="border-bottom pb-7 pt-10">
                    <div class="d-flex align-items-center mb-6">
                        <div class="d-flex align-items-center fs-15px ls-0">
                            <div class="rating">
                                <div class="empty-stars">
                                    <span class="star">
                                        <svg class="icon star-o">
                                            <use xlink:href="#star-o"></use>
                                        </svg>
                                    </span>
                                    <span class="star">
                                        <svg class="icon star-o">
                                            <use xlink:href="#star-o"></use>
                                        </svg>
                                    </span>
                                    <span class="star">
                                        <svg class="icon star-o">
                                            <use xlink:href="#star-o"></use>
                                        </svg>
                                    </span>
                                    <span class="star">
                                        <svg class="icon star-o">
                                            <use xlink:href="#star-o"></use>
                                        </svg>
                                    </span>
                                    <span class="star">
                                        <svg class="icon star-o">
                                            <use xlink:href="#star-o"></use>
                                        </svg>
                                    </span>
                                </div>
                                <div class="filled-stars"
                                    style="width: 80%">
                                    <span class="star">
                                        <svg class="icon star text-primary">
                                            <use xlink:href="#star"></use>
                                        </svg>
                                    </span>
                                    <span class="star">
                                        <svg class="icon star text-primary">
                                            <use xlink:href="#star"></use>
                                        </svg>
                                    </span>
                                    <span class="star">
                                        <svg class="icon star text-primary">
                                            <use xlink:href="#star"></use>
                                        </svg>
                                    </span>
                                    <span class="star">
                                        <svg class="icon star text-primary">
                                            <use xlink:href="#star"></use>
                                        </svg>
                                    </span>
                                    <span class="star">
                                        <svg class="icon star text-primary">
                                            <use xlink:href="#star"></use>
                                        </svg>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <span class="fs-3px mx-5"><i class="fas fa-circle"></i></span>
                        <span class="fs-14">3  day ago</span>
                    </div>

                    <div class="d-flex justify-content-start align-items-center mb-5">
                        <img src="#" data-src="../assets/images/others/product-review-avatar-01.jpg" class="me-6 lazy-image rounded-circle" width="60" height="60" alt="Avatar">
                        <div class="">
                            <h5 class="mt-0 mb-4 fs-14px text-uppercase ls-1">Name</h5>
                            <p class="mb-0">Location</p>
                        </div>
                    </div>
                    <p class="fw-semibold fs-6 text-body-emphasis mb-5">Good as light coverage</p>
                    <p class="mb-10 fs-6">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. </p>
                </div>--%>
            </div>

        </section>




    </main>
    <script src="/assets/js/Pages/shop-product-details.js"></script>


    <script>
        $(document).ready(function () {
            $('input[name="rating"]').on('change', function () {
                const value = $('input[name="rating"]:checked').val();
                $("#<%=SelectedRating.ClientID%>").val(value);
            });
        });
        document.addEventListener('DOMContentLoaded', function () {
            const scrollLinks = document.querySelectorAll('[data-scroll]');

            scrollLinks.forEach(link => {
                link.addEventListener('click', function (e) {
                    e.preventDefault();

                    const targetId = this.getAttribute('data-scroll');
                    const targetElement = document.querySelector(targetId);

                    if (targetElement) {
                        targetElement.scrollIntoView({ behavior: 'smooth', block: 'start' });
                    }
                });
            });
        });
    </script>

</asp:Content>

