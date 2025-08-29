<%@ Page Title="Shipping & Refund Policy " Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="shipping-refund-policy.aspx.cs" Inherits="shipping_refund_policy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <meta name="keywords" content="shipping policy, refund policy, ArchidPly Decor, plywood delivery, best plywood in india, laminated plywood, plywood company in india, plywood in bangalore, furniture plywood, flush doors" />

<meta name="description" content="Learn about shipping and refund policies at ArchidPly Decor. Hassle-free delivery of plywood & doors across Bangalore and India. " />
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
    <style>
        .policy-wrap p {
            margin-bottom: 1.5rem; /* Adds space between paragraphs */
        }

        .policy-wrap ul {
            margin-top: 1rem;
            padding-left: 1.5rem; /* Adjusts spacing for bullet points */
        }

        .policy-wrap li {
            margin-bottom: 1rem; /* Adds space between list items */
        }

        h2 {
            margin-bottom: 2.5rem; /* Adds space below the main heading */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main id="content" class="wrapper layout-page">
        <div class="container">
            <nav class="py-4 lh-30px" aria-label="breadcrumb">
                <ol class="breadcrumb justify-content-start py-1">
                    <li class="breadcrumb-item"><a href="/">Home</a></li>
                    <li class="breadcrumb-item active" aria-current="page">Shipping and Refund Policy</li>
                </ol>
            </nav>

            <section class="py-5 mb-10 policy-wrap">
                <div class="container">
                    <h1 class="text-center mb-lg-12 mb-10 mt-lg-8 mt-5">Shipping and Delivery</h1>
                    <p>
                        We currently offer shipping and delivery for domestic buyers; orders are shipped through domestic courier 
companies and /or Transporters only. Freight / Handling charges will be extra. Unloading arrangement 
and charges shall be paid by the buyers. Orders are shipped within 0-7 days or as per the delivery date 
agreed at the time of order confirmation and delivering of the shipment subject to Courier Company / 
Transporters norms. Orders received on Sunday or during holidays are dispatched the following Monday 
or next working day. In an event of natural or manmade calamities or civil disturbance, the scheduled 
delivery will be delayed and the purchaser will be informed accordingly. During busy times, such as 
holiday period, there can be processing and shipping delays. ARCHIDPLY DECOR LTD is not liable for 
any delay in delivery by the courier company / Transport authorities and only guarantees to hand over the 
consignment to the courier company or Transporter authorities within 0-7 days of the order and payment
or as per the delivery date agreed at the time of order confirmation. Delivery of all orders will be to the 
address provided by the buyer. Delivery of our services will be confirmed on your mail ID as specified
during registration. For any issues in utilizing our services you may contact our helpdesk on 8880130901 
or adltrading@archidply.com
                    </p>
                </div>
            </section>
            <section class="py-5 mb-10 policy-wrap">
                <div class="container">
                    <h2 class="text-center mb-lg-12 mb-10 mt-lg-8 mt-5">Cancellation and Refund</h2>
                    <p class="text-center">No cancellations & Refunds are entertained.</p>
                </div>
            </section>
        </div>

    </main>
</asp:Content>
