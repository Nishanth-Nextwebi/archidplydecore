<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="shop-checkout.aspx.cs" Inherits="shop_checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
    <style>
        .small-green-text {
            font-size: small;
            color: green;
        }
        @media (max-width: 1299px) {
    input#ContentPlaceHolder1_btnApply {
        padding: 10px 9px !important;
        font-size:12px;
    }
}

        .coupons-text{
            color:#ff0000;
            text-decoration:underline;
            margin-top:10px;
            display:block !important;
        }
    </style>
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
    },{
      "@type": "ListItem",
      "position": 2,
      "name": "Checkout,
      "item": "https://archidplydecor.com/checkout"
    }
  ]
}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main id="content" class="wrapper layout-page" style="background: #fbf1e9">
        <section class="z-index-2 position-relative pb-2 mb-5">

            <div class="mb-3">
                <div class="container">
                    <nav class="py-4 lh-30px" aria-label="breadcrumb">
                        <ol class="breadcrumb justify-content-start py-1 mb-0">
                            <li class="breadcrumb-item"><a title="Home" href="/">Home</a></li>
                            <li class="breadcrumb-item"><a title="Shop" href="/shop">Shop</a></li>
                            <li class="breadcrumb-item active" aria-current="page">Check Out</li>
                        </ol>
                    </nav>
                </div>
            </div>
        </section>
        <section class="container pb-14 pb-lg-19">
            <div class="text-center">
                <h2 class="mb-6">Check out</h2>
            </div>
            <div class="pt-12  pe-lg-10 pe-5 pb-10 px-lg-10 px-5 bg-white new-check-pad">
                <div class="row">
                    <div class="col-lg-4 pb-lg-0 pb-14 order-lg-last">
                        <div class="card border-0 rounded-0 shadow">

                            <div class="card-header px-0 mx-lg-5 mx-xl-10 margin-fix-md mx-5 bg-transparent py-8">
                                <h4 class="fs-4 mb-8">Order Summary</h4>
                                <%=strCarts %>
                            </div>
                            <div class="card-body px-10 py-8">
                                <div class="d-flex align-items-center mb-2">
                                    <span>Subtotal:</span>
                                    <span class="d-block ms-auto text-body-emphasis fw-bold DiscountEleClass"><%=strSubTotal %></span>
                                </div>
                                <div class="d-flex align-items-center">
                                    <span>Shipping:</span>
                                    <span class="d-block ms-auto text-body-emphasis fw-bold Shipping"><%=strShipping %></span>
                                </div>
                                <div class="d-flex align-items-center">
                                    <span>Discount:</span>
                                    <span class="d-block ms-auto text-body-emphasis fw-bold">- <%=strDiscount %></span>
                                </div>
                            </div>
                            <div class="card-footer bg-transparent py-5 px-0 mx-10">
                                <div class="d-flex align-items-center fw-bold mb-6">
                                    <span class="text-body-emphasis p-0">Total price:</span>
                                    <span class="d-block ms-auto text-body-emphasis fs-4 fw-bold DiscountEleClass"><%=strTotal %></span>
                                </div>
                            </div>
                        </div>
                        <div class="card border-0 mt-10 rounded-0 shadow">
                            <asp:Label ID="lblCoupon" runat="server" Visible="false"></asp:Label>

                            <div class="card-body py-10 px-8 my-0">
                                <p class="card-text text-body-emphasis mb-8">
                                    If you have a coupon code, please apply it below.
       
                                </p>
                                <div class="input-group position-relative">
                                    <asp:TextBox runat="server" ID="txtCoupon" MaxLength="100" class="form-control bg-body rounded-end" placeholder="Coupon Code*"> </asp:TextBox>
                                    <asp:Button type="submit" runat="server" ID="btnApply" OnClick="btnApply_Click" Visible="true" Text="Apply Coupon" class="btn btn-dark btn-hover-bg-primary btn-hover-border-primary" />
                                    <asp:Button type="submit" runat="server" ID="btnRemove" OnClick="btnRemove_Click" Visible="false" Text="Remove Coupon" class="btn btn-dark btn-hover-bg-primary btn-hover-border-primary" />
                                </div>
                                <a href="javascrip:void(0);" class="d-block text-end  coupons-text"  data-bs-target="#Couponmodal" data-bs-toggle="modal">View Coupons</a>
                            </div>
                        </div>

                    </div>
                    <div class="col-lg-8 order-lg-first pe-xl-20 pe-lg-6">
                        <div class="checkout">
                            <p class="fs-18px mb-5" runat="server" id="Loginlink" visible="false">
                                <span class="fs-20px">Returning customer?</span>
                                <a href="/login.aspx" class="text-primary text-decoration-underline">Click here to login</a>
                            </p>
                            <%--                      <p>
                                Have a coupon?
                               
                                <a data-bs-toggle="collapse" href="#collapsecoupon" role="button" aria-expanded="false" aria-controls="collapsecoupon">Click here to enter your code</a>
                            </p>
                            <div class="collapse" id="collapsecoupon">
                                
                            </div>--%>
                            <h4 class="fs-4 pt-4 mb-7">Billing Information</h4>
                            <h5>
                                <asp:Label runat="server" ID="lblCityValidation" class="text-danger mb-1"></asp:Label>
                            </h5>
                            <div class="mb-5">
                                <div class="row gy-4">
                                    <div class="col-md-6 mb-md-0 mb-2">
                                        <label class="mb-5 fs-13px letter-spacing-01 fw-semibold text-uppercase">First name<span class="text-danger">*</span></label>
                                        <asp:TextBox runat="server" MaxLength="100" ID="txtFName" CssClass="form-control acceptOnlyAlpha" placeholder="First Name"> </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtFName" SetFocusOnError="true" ValidationGroup="order" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="mb-5 fs-13px letter-spacing-01 fw-semibold text-uppercase">Last name<span class="text-danger">*</span></label>

                                        <asp:TextBox runat="server" MaxLength="100" ID="txtLName" CssClass="form-control acceptOnlyAlpha" placeholder="Last Name"> </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtLName" SetFocusOnError="true" ValidationGroup="order" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="mb-5">
                                <div class="row gy-4">
                                    <div class="col-md-12 mb-md-0 mb-2">
                                        <label for="street-address" class="mb-5 fs-13px letter-spacing-01 fw-semibold text-uppercase">Address Line1<span class="text-danger">*</span></label>
                                        <asp:TextBox runat="server" MaxLength="250" ID="txtAddress1" CssClass="form-control mb-5"> </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv3" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtAddress1" SetFocusOnError="true" ValidationGroup="order" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-12">
                                        <label for="street-address" class="mb-5 fs-13px letter-spacing-01 fw-semibold text-uppercase">Address Line2</label>
                                        <asp:TextBox runat="server" MaxLength="250" ID="txtAddress2" CssClass="form-control mb-5"> </asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfv4" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtAddress2" SetFocusOnError="true" ValidationGroup="order" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="mb-5">
                                <div class="row gy-4">
                                    <div class="col-md-4">
                                        <label for="zip" class="mb-5 fs-13px letter-spacing-01 fw-semibold text-uppercase">PinCode<span class="text-danger">*</span></label>
                                        <asp:TextBox runat="server" MaxLength="6" ID="txtZip" CssClass="form-control mb-5 numberOnlyInput"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ForeColor="Red" ControlToValidate="txtZip" SetFocusOnError="true" ValidationGroup="order" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-4 mb-md-0 mb-5">
                                        <label for="city" class="mb-5 fs-13px letter-spacing-01 fw-semibold text-uppercase">City<span class="text-danger">*</span></label>
                                        <%-- <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-select bg-body-secondary rounded p-5 text-secondary">
                                            <asp:ListItem Text="Bengaluru" Value="Bengaluru" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                        <asp:TextBox runat="server" MaxLength="100" ID="txtCity"   CssClass="form-control mb-5 acceptOnlyAlpha"> </asp:TextBox>

                                        <%--<asp:RequiredFieldValidator ID="rfv5" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCity" SetFocusOnError="true" ValidationGroup="order" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="col-md-4 mb-md-0 mb-5">
                                        <label for="state" class="mb-5 fs-13px letter-spacing-01 fw-semibold text-uppercase">State<span class="text-danger">*</span></label>
                                        <%--   <asp:DropDownList ID="ddlState" runat="server" CssClass="form-select bg-body-secondary rounded p-5 text-secondary">
                                            <asp:ListItem Text="Karnataka" Value="Karnataka" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                        <asp:TextBox runat="server" MaxLength="100"  ID="txtState" CssClass="form-control mb-5 acceptOnlyAlpha"> </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv6" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtState" SetFocusOnError="true" ValidationGroup="order" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>

                                    </div>
                                    <%--<label id="lblCityValidation" class="text-danger mb-3"></label>--%>
                                </div>
                            </div>
                            <div class="mb-5">
                                <asp:Label ID="lblCountry" runat="server" CssClass="mb-5 fs-13px letter-spacing-01 fw-semibold text-uppercase" Text="Country"></asp:Label>
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-select bg-body-secondary rounded p-5 text-secondary">
                                    <asp:ListItem Text="India" Value="India" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="mb-5">
                                <label class="mb-5 fs-13px letter-spacing-01 fw-semibold text-uppercase">info</label>
                                <div class="row gy-4">
                                    <div class="col-md-6 mb-md-0 mb-7">
                                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" MaxLength="100" placeholder="Email ID"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv8" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmail" SetFocusOnError="true" ValidationGroup="order" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rev1" runat="server" Display="Dynamic" ControlToValidate="txtEmail" ValidationGroup="order" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Invalid E-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control numberOnlyInput" MaxLength="10" placeholder="Phone number"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv9" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhone" SetFocusOnError="true" ValidationGroup="order" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                    </div>
                                    <label class="mb-5 fs-13px letter-spacing-01 fw-semibold text-uppercase">Payment Type</label>

                                    <div class="d-flex gap-5 align-items-center">
                                        <asp:RadioButton ID="rbOnlinePayment" runat="server" Text="Online Payment" GroupName="PaymentMethod" CssClass="form-control new-right" Checked="true" />
                                        <asp:RadioButton ID="rbCOD" runat="server" Text="COD (Cash on Delivery)" GroupName="PaymentMethod" CssClass="form-control new-right" />
                                    </div>
                                </div>
                            </div>
                            <div class="mt-6 mb-5 form-check p-0">
                                <asp:CheckBox ID="customCheck5" runat="server" CssClass="rounded-0 me-4" Checked="true" />
                                <label class="text-body-emphasis" for="customCheck5">
                                    <span class="text-body-emphasis">Shipping address is the same as billing</span>
                                </label>


                            </div>
                            <div runat="server" id="DeliveryDiv">
                                <h4 class="fs-4 pt-4 mb-7">shipping Information</h4>
                                <div class="row gy-4">
                                    <div class="col-md-6 mb-md-0 mb-7">
                                        <asp:TextBox runat="server" ID="txtDelEmailID" CssClass="form-control" MaxLength="100" placeholder="Email ID"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtDelEmailID" SetFocusOnError="true" ValidationGroup="order" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ControlToValidate="txtDelEmailID" ValidationGroup="order" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Invalid E-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="txtDelPhone" CssClass="form-control numberOnlyInput" MaxLength="10" placeholder="Phone number"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtDelPhone" SetFocusOnError="true" ValidationGroup="order" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="mb-5">
                                    <div class="row gy-4">
                                        <div class="col-md-12 mb-md-0 mb-2">
                                            <label for="street-address" class="mb-5 fs-13px letter-spacing-01 fw-semibold text-uppercase">Address Line1<span class="text-danger">*</span></label>
                                            <asp:TextBox runat="server" MaxLength="250" ID="txtDelAddress1" CssClass="form-control mb-5"> </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ForeColor="Red" ControlToValidate="txtDelAddress1" SetFocusOnError="true" ValidationGroup="order" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-12">
                                            <label for="street-address" class="mb-5 fs-13px letter-spacing-01 fw-semibold text-uppercase">Address Line2</label>
                                            <asp:TextBox runat="server" MaxLength="250" ID="txtDelAddress2" CssClass="form-control mb-5"> </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="mb-5">
                                    <div class="row gy-4">
                                        <div class="col-md-4">
                                            <label for="zip" class="mb-5 fs-13px letter-spacing-01 fw-semibold text-uppercase">PinCode<span class="text-danger">*</span></label>
                                            <asp:TextBox runat="server" MaxLength="6" ID="txtDelZip" CssClass="form-control mb-5 numberOnlyInput"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" runat="server" ForeColor="Red" ControlToValidate="txtDelZip" SetFocusOnError="true" ValidationGroup="order" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-4 mb-md-0 mb-5">
                                            <label for="city" class="mb-5 fs-13px letter-spacing-01 fw-semibold text-uppercase">City<span class="text-danger">*</span></label>
                                            <%--   <asp:DropDownList ID="txtDelCity" runat="server" CssClass="form-select bg-body-secondary rounded p-5 text-secondary">
                                                <asp:ListItem Text="Bengaluru" Value="Bengaluru" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>--%>
                                            <asp:TextBox runat="server" MaxLength="100"  ID="txtDelCity" CssClass="form-control mb-5"> </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ForeColor="Red" ControlToValidate="txtDelCity" SetFocusOnError="true" ValidationGroup="order" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4 mb-md-0 mb-5">
                                            <label for="state" class="mb-5 fs-13px letter-spacing-01 fw-semibold text-uppercase">State<span class="text-danger">*</span></label>
                                            <%--    <asp:DropDownList ID="txtDelState" runat="server" CssClass="form-select bg-body-secondary rounded p-5 text-secondary">
                                                <asp:ListItem Text="Karnataka" Value="Karnataka" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>--%>
                                            <asp:TextBox runat="server" MaxLength="100"  ID="txtDelState" CssClass="form-control mb-5"> </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ForeColor="Red" ControlToValidate="txtDelState" SetFocusOnError="true" ValidationGroup="order" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>

                                        </div>

                                    </div>
                                </div>


                            </div>
                            <asp:Button runat="server" ValidationGroup="order" OnClick="submit_Click" ID="submit" CssClass="btn green-btn btn-hover-border-primary px-11 mt-md-7 mt-4" Text="Place Order" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>

      <div class="modal fade sucess" id="Couponmodal" tabindex="-1" aria-labelledby="Couponmodal" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
        <div class="modal-content">
            <div class="modal-header text-center border-0 pb-0">
                <button type="button"
                    class="btn-close position-absolute end-5 top-5"
                    data-bs-dismiss="modal"
                    aria-label="Close">
                </button>
                <h3 class="modal-title w-100" id="signInModalLabel">All  Coupons</h3>
            </div>
            <div class="modal-body px-sm-13 px-8">
                <div class="group-discount">
                    <%=strAvailableCoupons %>
                               <%-- <div class="box-discount">
                                    <div class="discount-top">
                                        <div class="discount-off">
                                            <div class="text-caption-1">Discount</div>
                                            <span class="sale-off text-btn-uppercase">10% OFF</span>
                                        </div>
                                        <div class="discount-from">
                                            <p class="text-caption-1">For all orders <br> from 200$</p>
                                        </div>
                                    </div>
                                    <div class="discount-bot">
                                        <span class="text-btn-uppercase">Mo234231</span>
                                        <button class="tf-btn"><span class="text">Code Coupon</span></button>
                                    </div>
                                </div>
                                <div class="box-discount active">
                                    <div class="discount-top">
                                        <div class="discount-off">
                                            <div class="text-caption-1">Discount</div>
                                            <span class="sale-off text-btn-uppercase">10% OFF</span>
                                        </div>
                                        <div class="discount-from">
                                            <p class="text-caption-1">For all orders <br> from 200$</p>
                                        </div>
                                    </div>
                                    <div class="discount-bot">
                                        <span class="text-btn-uppercase">ADRFGT1232324</span>
                                        <button class="tf-btn"><span class="text">Code Coupon</span></button>
                                    </div>
                                </div>
                                <div class="box-discount">
                                    <div class="discount-top">
                                        <div class="discount-off">
                                            <div class="text-caption-1">Discount</div>
                                            <span class="sale-off text-btn-uppercase">10% OFF</span>
                                        </div>
                                        <div class="discount-from">
                                            <p class="text-caption-1">For all orders <br> from 200$</p>
                                        </div>
                                    </div>
                                    <div class="discount-bot">
                                        <span class="text-btn-uppercase">Akas!232</span>
                                        <button class="tf-btn"><span class="text">Code Coupon</span></button>
                                    </div>
                                </div>--%>
                            </div>
            </div>
        </div>
    </div>
</div>
    <script src="/assets/js/Pages/shop-checkout.js"></script>

    
    <script>
document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".tf-btn").forEach(button => {
        button.addEventListener("click", function () {
            let couponCode = this.parentElement.querySelector("span.text-btn-uppercase").innerText;

            navigator.clipboard.writeText(couponCode).then(() => {
            }).catch(err => {
                console.error("Failed to copy: ", err);
            });
        });
    });
});
    </script>

</asp:Content>

