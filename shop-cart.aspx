<%@ Page Title="Checkout Cart | Complete Your Order Now" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="shop-cart.aspx.cs" Inherits="shop_cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <meta name="keywords" content="plywood cart, laminates checkout, doors shopping cart, buy plywood online, home decor cart, secure payment, finalize order, building materials cart, Archidply products, complete purchase" />

    <meta name="description" content="Review your Archidply Decor cart items – plywood, laminates, doors & more. Proceed to checkout for a seamless purchase. Secure & fast delivery across India. Order now!" />
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
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
      "name": "cart,
      "item": "https://archidplydecor.com/cart"
    }
  ]
}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main id="content" class="wrapper layout-page pb-15" style="background: #fbf1e9">
        <section class="z-index-2 position-relative pb-2 mb-8">

            <div class="mb-3">
                <div class="container">
                    <nav class="py-2 lh-30px" aria-label="breadcrumb">
                        <ol class="breadcrumb justify-content-start py-1 mb-0">
                            <li class="breadcrumb-item"><a title="Home" href="/">Home</a></li>
                            <li class="breadcrumb-item"><a title="Shop" href="/shop">Shop</a></li>
                            <li class="breadcrumb-item active" aria-current="page">Shopping Cart</li>
                        </ol>
                    </nav>
                </div>
            </div>
        </section>
        <section class="container">
            <h1 class="text-center mt-4 mb-7">Shopping Cart</h1>
            <div class="shopping-cart bg-white">
                <center>
                    <div class="divNoItem">
                        <img src="/assets/imgs/empty-cart.png" width="200" height="200" alt="img"/>
                        <h3 style="color: #F4986D"><strong>There is no item(s) in your shopping cart</strong></h3>
                        <div class="mt-4">
                            <a href="/shop" class="btn btn-outline-dark me-8 text-nowrap my-5">Continue shopping</a>
                        </div>
                    </div>
                </center>
                <div class="table-responsive-md pb-0 pb-lg-0 tableContent">
                    <table class="table border">
                        <thead class="bg-body-secondary">
                            <tr class="fs-15px letter-spacing-01 fw-semibold text-uppercase text-body-emphasis">
                                <th scope="col" class="fw-semibold border-1 ps-11">products</th>
                                <th scope="col" class="fw-semibold border-1">quantity</th>
                                <th colspan="2" class="fw-semibold border-1">Price</th>
                            </tr>
                        </thead>
                        <tbody id="allCartItem">
                        </tbody>
                        <tfoot>
                            <tr>
                                <td class="pt-5 pb-10 ps-8 position-relative bg-body ps-0 left">
                                    <a href="/shop" title="Countinue Shopping"
                                        class="btn btn-outline-dark me-8 text-nowrap my-5 footerpart">Countinue Shopping
                                    </a>
                                </td>
                                <%--   <td colspan="3" class="text-end ps-10 pt-5 pb-10 position-relative bg-body right">
                                    <a value="Clear Shopping Cart"
                                        class="btn btn-link p-0 border-0 border-bottom border-secondary text-decoration-none rounded-0 my-5 fw-semibold" id="deleteSelected">
                                        <i class="fa fa-times me-3"></i>
                                        Clear Shopping Cart
</a>
                                </td>--%>
                            </tr>
                        </tfoot>
                    </table>
                </div>

                <div class="row pt-8 pt-lg-11 pb-16 pb-lg-15 justify-content-center">
                    <div class="col-lg-4 pt-lg-0 pt-10 pricecard">
                        <div class="card border-0  mr-xl-11 new-shadow" style="box-shadow: 0 0 10px 0 rgba(0,0,0,0.1)">
                            <div class="card-body px-9 pt-6">
                                <div class="d-flex align-items-center justify-content-between mb-5">
                                    <span>Subtotal:</span>
                                    <span class="d-block ml-auto text-body-emphasis fw-bold TotalPrice"></span>
                                </div>
                                <div class="d-flex align-items-center justify-content-between">
                                    <span>Shipping:</span>
                                    <span class="d-block ml-auto text-body-emphasis fw-bold shippingPrice"></span>
                                </div>
                            </div>
                            <div class="card-footer bg-transparent px-0 pt-5 pb-7 mx-9">
                                <div class="d-flex align-items-center justify-content-between fw-bold mb-7">
                                    <span class="text-secondary text-body-emphasis">Total pricre:</span>
                                    <span class="d-block ml-auto text-body-emphasis fs-4 fw-bold DiscountEleClass">₹</span>
                                </div>
                                <a href="/checkout"
                                    class="btn w-100 btn-dark green-btn btn-hover-border-primary"
                                    title="Check Out">Check Out</a>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>

    </main>
    <script src="/assets/js/Pages/shop-cart.js"></script>
</asp:Content>

