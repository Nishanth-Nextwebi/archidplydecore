<%@ Page Title="Contact ArchidPly Decor | Plywood Manufacturer in India" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="contact-us.aspx.cs" Inherits="contact_us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <meta name="keywords" content="plywood company in india, plywood manufacturers in bangalore, contact ArchidPly Decor, flush doors, laminated plywood, veneer doors, furniture plywood, door manufacturers, top plywood brand, support" />
    <meta name="description" content="Connect with ArchidPly Decor for plywood & door solutions in Bangalore and across India. Call today for expert support and enquiries. ">
    <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
    <link rel="stylesheet" href="https://unpkg.com/leaflet/dist/leaflet.css" />
    <script src="https://unpkg.com/leaflet/dist/leaflet.js"></script>
    <style>
        #map {
            height: 100vh;
            width: 100%;
            background: #f1f1f1
        }

        .input-error {
            border: 2px solid red !important;
            box-shadow: 0 0 5px red;
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }

        th, td {
            border: 1px solid black;
            padding: 8px;
            text-align: left;
        }

        .marketing h2 {
            font-size: 24px !important;
            margin-bottom: 20px;
        }

        th {
            background-color: #f2f2f2;
        }

        .card-style1 {
            position: relative;
            display: block;
            z-index: 1;
            margin-top: 65px;
        }

            .card-style1 .card-main-img {
                position: relative;
                display: block;
                z-index: 1;
            }

        .leaflet-container .leaflet-marker-pane img {
            height: 28px !important;
            width: 28px !important;
        }

        .card-style1 .card-image {
            position: relative;
            display: block;
            overflow: hidden;
            border-radius: 10px;
            width: 400px;
        }

        .card-style1 .card-content {
            position: relative;
            display: block;
            background-color: #fff;
            text-align: center;
            padding-top: 40px;
            padding-bottom: 40px;
            padding-left: 80px;
            padding-right: 30px;
            margin-left: -216px;
            margin-right: 20px;
            margin-top: 0px;
            text-align: left;
            z-index: 0;
            box-shadow: 0px 20px 20px 0px rgba(0, 0, 0, 0.03);
            border-radius: 10px;
            min-height: 350px;
            width: 100%;
        }

        .card-style1 .card-content1 {
            position: relative;
            display: block;
            background-color: #fff;
            text-align: center;
            padding-top: 40px;
            padding-bottom: 40px;
            padding-left: 40px;
            padding-right: 30px;
            margin-left: 0px;
            margin-right: -216px;
            margin-top: 0px;
            text-align: left;
            z-index: 0;
            box-shadow: 0px 20px 20px 0px rgba(0, 0, 0, 0.03);
            border-radius: 10px;
            min-height: 350px;
            width: 100%;
        }

        .contact-card {
            padding: 40px 20px;
            background: #fff;
            display: flex;
            box-shadow: rgba(99, 99, 99, 0.2) 0px 2px 8px 0px;
            flex-direction: column;
            align-items: center;
        }

            .contact-card span {
                font-size: 18px;
                display: block;
                font-weight: 600;
            }

            .contact-card h3 {
                font-size: 28px ;
                text-align:center;
            }

            .contact-card p {
                font-size: 20px;
                margin-top: 5px;
            }

                .contact-card p a {
                    color: #31ade3;
                }

        .new-sticky {
            position: sticky;
            top: 0px !important;
        }
                .bg-overlay::before{
    background:unset !important;
}
    </style>    
    <link href="Admin/assets/libs/snackbar/snackbar.min.css" rel="stylesheet" />
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
      "name": "Contact Us",
      "item": "https://archidplydecor.com/contact-us"
    }
  ]
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main id="content" class="wrapper layout-page investor-contact pt-0 pb-10" alt="Background image" style="background: #f1efec url('assets/imgs/bg-img2.png'); background-repeat: no-repeat; background-position: right; background-attachment: fixed">
        <section class="position-relative custom-overlay new-banner-head" id="about_introduction">

            <div class="lazy-bg bg-overlay position-absolute z-index-1 w-100 h-100   light-mode-img" data-bg-src="assets/imgs/ban1/2.png" alt="Soft green-blue curves">
            </div>
            <div class="lazy-bg bg-overlay dark-mode-img position-absolute z-index-1 w-100 h-100" data-bg-src="assets/imgs/ban1/2.png" alt="Soft green-blue curves">
            </div>
    <div class="position-relative z-index-2 container py-10 py-md-15 py-lg-22">
                <h2 class="fs-56px mb-0 text-white text-center">Contact Us</h2>
            </div>
        </section>
        <div class="container">
            <div class="py-5">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb breadcrumb-site py-0 d-flex justify-content-start">
                        <li class="breadcrumb-item">
                            <a class="text-decoration-none text-body" href="/">Home</a>
                        </li>
                        <li class="breadcrumb-item active pl-0 d-flex align-items-center" aria-current="page">Contact Us</li>
                    </ol>
                </nav>
            </div>
        </div>
        <section class="pt-5 pb-16 pt-lg-17 pb-lg-18 mx-auto" style="max-width: 750px" data-animated-id="3">
            <div class="container text-center">
                <h2 class="mb-10 fs-3">Send a message</h2>
                <div class="contact-form" method="post" action="#">
                    <div class="row mb-8 mb-md-10">
                        <div class="col-md-6 col-12 mb-8 mb-md-0">
                            <asp:TextBox runat="server" MaxLength="100" ID="txtName" CssClass="form-control acceptOnlyAlpha" placeholder="Name*" data-val-id="rfv1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtName" SetFocusOnError="true" ValidationGroup="ContactUs" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6 col-12">
                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control onlyNum" MaxLength="10" placeholder="Phone*" data-val-id="rfv2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv4" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhone" SetFocusOnError="true" ValidationGroup="ContactUs" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPhone" Display="None" ValidationGroup="ContactUs" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Enter 10 Digit Valid Phone Number" ValidationExpression="^\d{10}$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-md-12 mt-8 col-12">
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" MaxLength="100" placeholder="Email*" data-val-id="rfv2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmail" SetFocusOnError="true" ValidationGroup="ContactUs" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="ContactUs" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Invalid E-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <asp:TextBox ID="txtMessage" TextMode="MultiLine" Rows="6" MaxLength="250" placeholder="Message*" runat="server" CssClass="form-control mb-6" data-val-id="rfv3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv3" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtMessage" SetFocusOnError="true" ValidationGroup="ContactUs" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                    <div>
                    <asp:Button runat="server" ValidationGroup="ContactUs" OnClick="submit_Click" ID="submit" CssClass="btn btn-dark btn-hover-bg-primary btn-hover-border-primary px-11" Text="Submit" />
                    </div>
                </div>
            </div>
        </section>
        <section class="section-padding">
            <div class="container">
                <div class="row gy-4">
                    <div class="col-lg-6 col-md-6 col-12">
                        <div class="contact-card">
                            <span>Contact Us</span>
                            <h3>Resident Representatives – South India


                            </h3>
                            <p class="">
                                Ph :
                           <a href="tel:+919591849977">+91 9591849977 </a>/ <a href="tel: 7022012573
">7022012573</a>
                            </p>

                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-12">
                        <div class="contact-card">
                            <span>Contact Us</span>
                            <h3 >Resident Representatives – Rest of India

                            </h3>
                            <p class="">
                                Ph :
                           <a href="tel:+919591849977">+91 9513325133  </a>/ <a href="tel:+917314202546
">7314202546</a>
                            </p>

                        </div>
                    </div>
                </div>
            </div>
        </section>
        <div class="section-padding map-box">
            <div class="container">
                <div class="row">
                    <div class="col-lg-7">
                        <div class="new-sticky">
                            <div id="map"></div>
                        </div>


                    </div>
                    <div class="col-lg-5">
                        <div class="marketing">
                            <h2>Marketing Cities List</h2>
                            <table>
                                <tr>
                                    <th>Cities</th>
                                </tr>

                                <tr>
                                    <td>Ahmedabad</td>
                                </tr>
                                <tr>
                                    <td>Bangalore</td>
                                </tr>
                                <tr>
                                    <td>Bellary</td>
                                </tr>
                                <tr>
                                    <td>Chennai</td>
                                </tr>
                                <tr>
                                    <td>Chittor AP</td>
                                </tr>
                                <tr>
                                    <td>Coimbatore</td>
                                </tr>
                                <tr>
                                    <td>Hyderabad</td>
                                </tr>
                                <tr>
                                    <td>Indore</td>
                                </tr>
                                <tr>
                                    <td>Karimnagar</td>
                                </tr>

                                <tr>
                                    <td>Mumbai</td>
                                </tr>
                                <tr>
                                    <td>Namakkal</td>
                                </tr>
                                <tr>
                                    <td>Raipur</td>
                                </tr>
                                <tr>
                                    <td>Salem</td>
                                </tr>
                                <tr>
                                    <td>Tirupati</td>
                                </tr>
                                <tr>
                                    <td>Secundrabad</td>
                                </tr>
                                <tr>
                                    <td>Vijaywada</td>
                                </tr>
                                <tr>
                                    <td>Warrangal</td>
                                </tr>

                            </table>

                            <%--<table>
        <tr>
            <th>Sl No</th>
            <th>Marketing Team Name</th>
            <th>Place</th>
        </tr>
        <tr><td>1</td><td>Viresh Vyas</td><td>Indore</td></tr>
        <tr><td>2</td><td>BT Reddy</td><td>Bangalore</td></tr>
        <tr><td>3</td><td>Adavayya M</td><td>Bangalore</td></tr>
        <tr><td>4</td><td>Pradeep KR</td><td>Bangalore</td></tr>
        <tr><td>5</td><td>Vijaykumar Gonbal</td><td>Bangalore</td></tr>
        <tr><td>6</td><td>Ajeet Singh</td><td>Bangalore</td></tr>
        <tr><td>7</td><td>Nandeesh BS</td><td>Bellary</td></tr>
        <tr><td>8</td><td>Rajgopal Boga</td><td>Vijaywada / Tirupati</td></tr>
        <tr><td>9</td><td>M Thyagaraja</td><td>Chittor AP</td></tr>
        <tr><td>10</td><td>Viju David</td><td>Chennai</td></tr>
        <tr><td>11</td><td>Sridharan</td><td>Chennai</td></tr>
        <tr><td>12</td><td>V Silambarasan</td><td>Salem</td></tr>
        <tr><td>13</td><td>Suresh Kumar M</td><td>Coimbatore</td></tr>
        <tr><td>14</td><td>Venkat M</td><td>Namakkal</td></tr>
        <tr><td>15</td><td>Shravan Kumar</td><td>Hyderabad</td></tr>
        <tr><td>16</td><td>Salla Ramesh</td><td>Hyderabad</td></tr>
        <tr><td>17</td><td>Koteswar Rao</td><td>Warrangal / Karimnagar</td></tr>
        <tr><td>18</td><td>Mohammed Samuddin</td><td>Hyderabad</td></tr>
        <tr><td>19</td><td>Suresh N</td><td>Secundrabad</td></tr>
        <tr><td>20</td><td>Deepak Hardiya</td><td>Indore</td></tr>
        <tr><td>21</td><td>Sanket Kumar</td><td>Indore</td></tr>
        <tr><td>22</td><td>Vaibhav Srivastav</td><td>Indore</td></tr>
        <tr><td>23</td><td>Bhavesh Purohit</td><td>Raipur</td></tr>
        <tr><td>24</td><td>Hitesh Thaker</td><td>Ahmedabad</td></tr>
        <tr><td>25</td><td>Manoj Bhatt</td><td>Mumbai</td></tr>
        <tr><td>26</td><td>Manoj Hegde</td><td>Bangalore</td></tr>
        <tr><td>27</td><td>Milan Shah</td><td>Bangalore</td></tr>
        <tr><td>28</td><td>Shankar Narayan</td><td>Bangalore</td></tr>
        <tr><td>29</td><td>Sinclair</td><td>Bangalore</td></tr>
        <tr><td>30</td><td>Naveen Kumar V</td><td>Bangalore</td></tr>
    </table>--%>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <section data-animated-id="1" class="bg-light d-none">

            <div class="container pt-13">
                <div class="text-center">
                    <div class="text-center">
                        <h2 class="fs-36px mb-9">Keep In Touch with Us</h2>
                        <p class="fs-18px mb-3 w-lg-60 w-xl-50 mx-md-12 mx-lg-auto">We’re talking about clean beauty gift sets, of course – and we’ve got a bouquet of beauties for yourself or someone you love.</p>
                    </div>

                </div>
                <div class="row d-none">
                    <div class="col-md-4 mb-12 mb-md-0">
                        <div class="d-flex align-items-start">
                            <div class="pe-lg-9 pe-8 text-primary">
                                <svg class="icon fs-2">
                                    <use xlink:href="#icon-box-07"></use>
                                </svg>
                            </div>
                            <div>
                                <h3 class="fs-5 mb-6">Address</h3>
                                <div class="fs-6">
                                    <p class="mb-2 pb-4 fs-6">
                                        No.50, Millenium Towrs,
                                        <br>
                                        Shivajinagar Bangalore — 560051
                                    </p>
                                </div>
                                <div class="fs-6">
                                    <p class="mb-2 pb-4 fs-6">
                                        No.50, Millenium Towrs,
                                        <br>
                                        Shivajinagar Bangalore — 560051
                                    </p>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="col-md-4 mb-12 mb-md-0">
                        <div class="d-flex align-items-start">
                            <div class="pe-lg-9 pe-8 text-primary">
                                <svg class="icon fs-2">
                                    <use xlink:href="#icon-box-05"></use>
                                </svg>
                            </div>
                            <div>
                                <h3 class="fs-5 mb-6">Contact</h3>
                                <div class="fs-6">
                                    <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                    <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                    <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="col-md-4 mb-12 mb-md-0">
                        <div class="d-flex align-items-start">
                            <div class="pe-lg-9 pe-8 text-primary">
                                <svg class="icon fs-2">
                                    <use xlink:href="#icon-box-06"></use>
                                </svg>
                            </div>
                            <div>
                                <h3 class="fs-5 mb-6">Hour of operation</h3>
                                <div class="fs-6">
                                    <dl class="d-flex mb-0">
                                        <dt class="pe-3 fs-6 text-body-emphasis fw-500" style="width: 110px">Mon – Fri:</dt>
                                        <dd class="mb-0">08:30 – 20:00</dd>
                                    </dl>
                                    <dl class="d-flex mb-0">
                                        <dt class="pe-3 fs-6 text-body-emphasis fw-500" style="width: 110px">Sat &amp; Sun:</dt>
                                        <dd class="mb-0">09:30 – 21:30</dd>
                                    </dl>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="row justify-content-center">
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                            <div class="card-content">

                                <div class="text-end">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
                        <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
                        <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">

                            <div class="card-content1">

                                <div class="text-start">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
                       <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
                       <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                            <div class="card-content">

                                <div class="text-end">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
      <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
      <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">

                            <div class="card-content1">

                                <div class="text-start">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
     <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
     <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                            <div class="card-content">

                                <div class="text-end">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
      <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
      <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">

                            <div class="card-content1">

                                <div class="text-start">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
     <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
     <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                            <div class="card-content">

                                <div class="text-end">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
      <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
      <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">

                            <div class="card-content1">

                                <div class="text-start">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
     <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
     <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                            <div class="card-content">

                                <div class="text-end">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
      <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
      <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">

                            <div class="card-content1">

                                <div class="text-start">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
     <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
     <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                            <div class="card-content">

                                <div class="text-end">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
      <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
      <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">

                            <div class="card-content1">

                                <div class="text-start">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
     <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
     <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                            <div class="card-content">

                                <div class="text-end">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
      <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
      <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">

                            <div class="card-content1">

                                <div class="text-start">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
     <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
     <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                            <div class="card-content">

                                <div class="text-end">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
      <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
      <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">

                            <div class="card-content1">

                                <div class="text-start">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
     <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
     <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                            <div class="card-content">

                                <div class="text-end">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
      <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
      <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">

                            <div class="card-content1">

                                <div class="text-start">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
     <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
     <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                            <div class="card-content">

                                <div class="text-end">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
      <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
      <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">

                            <div class="card-content1">

                                <div class="text-start">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
     <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
     <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                            <div class="card-content">

                                <div class="text-end">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
      <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
      <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">

                            <div class="card-content1">

                                <div class="text-start">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
     <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
     <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                            <div class="card-content">

                                <div class="text-end">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
      <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
      <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">

                            <div class="card-content1">

                                <div class="text-start">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
     <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
     <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                            <div class="card-content">

                                <div class="text-end">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
      <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
      <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">

                            <div class="card-content1">

                                <div class="text-start">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
     <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
     <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                            <div class="card-content">

                                <div class="text-end">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
      <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
      <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="card-style1 d-flex  justify-content-center align-items-center">

                            <div class="card-content1">

                                <div class="text-start">
                                    <h3 class="fs-5 mb-6">Address</h3>
                                    <div class="fs-6">
                                        <p class="mb-2 pb-4 fs-6">
                                            3245 Abbot Kinney BLVD –
     <br>
                                            PH Venice, CA 124
                                        </p>
                                        <p>
                                            76 East Houston Street
     <br>
                                            PH Venice, CA 124
                                        </p>
                                    </div>
                                    <h3 class="fs-5 mb-6">Contact</h3>
                                    <div class="fs-6">
                                        <div class="fs-6">
                                            <p class="mb-3 fs-6">Mobile:<span class="text-body-emphasis"> +91 7022012573</span></p>
                                            <p class="mb-3 fs-6">Tollfree:<span class="text-body-emphasis"> 1900 26886</span></p>
                                            <p class="mb-0 fs-6">E-mail: info@archidplydecor.com</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-main-img">

                                <div class="card-image">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d317859.6089702069!2d-0.075949!3d51.508112!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48760349331f38dd%3A0xa8bf49dde1d56467!2sTower%20of%20London!5e0!3m2!1sen!2sus!4v1719221598456!5m2!1sen!2sus" width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>


                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>





    </main>
    <%--<script>
        var map = L.map('map', {
            center: [22.3511148, 78.6677428], // Center of India
            zoom: 5,
            minZoom: 4,
            maxZoom: 10,
            zoomControl: true,
            scrollWheelZoom: true,
            doubleClickZoom: true,
            touchZoom: true,
            dragging: true
        });

        // Restrict users to India's bounds
        var indiaBounds = [
            [6.746, 68.162],  // Southwest corner
            [35.674, 97.395]  // Northeast corner
        ];
        map.setMaxBounds(indiaBounds);
        map.on('drag', function () { map.panInsideBounds(indiaBounds, { animate: false }); });

        // Load India GeoJSON for state boundaries
        fetch("https://raw.githubusercontent.com/geohacker/india/master/state/india_telengana.geojson")
            .then(response => response.json())
            .then(data => {
                L.geoJson(data, {
                    style: function (feature) {
                        return {
                            color: "#242020",
                            weight: 1,
                            fillColor: "#fff",
                            fillOpacity: 0.7
                        };
                    }
                }).addTo(map);
            });

        // Define category colors
        var categories = {
            "STX": "blue",
            "HHX": "yellow" // Updated HHX to yellow
        };

        // Locations
        var locations = [
            { state: "Karnataka", lat: 13.4020, lon: 78.0551, cities: ["Chintamani, Karnataka, India"], category: "STX" },
            { state: "Karnataka", lat: 12.9716, lon: 77.5946, cities: ["Bangalore, India"], category: "HHX" },
            { state: "Madhya Pradesh", lat: 22.7196, lon: 75.8577, cities: ["Indore, Madhya Pradesh, India"], category: "HHX" }



        ];

        locations.forEach(location => {
            var iconColor = categories[location.category] || "red"; // Default to red if no category

            var icon = L.divIcon({
                className: "custom-marker",
                html: `<div style="width:12px; height:12px; background:${iconColor}; border-radius:50%;"></div>`,
                iconSize: [12, 12]
            });

            L.marker([location.lat, location.lon], { icon: icon })
                .addTo(map)
                .bindPopup(`<b>${location.state}</b><br>${location.cities.join(", ")}`);
        });

        // Legend for categories
        var legend = L.control({ position: 'bottomleft' });

        legend.onAdd = function (map) {
            var div = L.DomUtil.create('div', 'info legend');
            var categoriesList = ['STX', 'HHX'];

            categoriesList.forEach(category => {
                div.innerHTML += `<i style="background:${categories[category]}; width:15px; height:15px; display:inline-block; margin-right:5px;"></i> ${category}<br>`;
            });

            return div;
        };

        legend.addTo(map);
    </script>--%>

    <script>
        var map = L.map('map', {
            center: [22.3511148, 78.6677428], // Center of India
            zoom: 5,
            minZoom: 4,
            maxZoom: 10,
            zoomControl: true,
            scrollWheelZoom: true,
            doubleClickZoom: true,
            touchZoom: true,
            dragging: true
        });

        // Restrict users to India's bounds
        var indiaBounds = [
            [6.746, 68.162],  // Southwest corner
            [35.674, 97.395]  // Northeast corner
        ];
        map.setMaxBounds(indiaBounds);
        map.on('drag', function () { map.panInsideBounds(indiaBounds, { animate: false }); });

        // Load India GeoJSON for state boundaries
        fetch("https://raw.githubusercontent.com/geohacker/india/master/state/india_telengana.geojson")
            .then(response => response.json())
            .then(data => {
                L.geoJson(data, {
                    style: function (feature) {
                        return {
                            color: "#242020",
                            weight: 1,
                            fillColor: "#fff",
                            fillOpacity: 0.7
                        };
                    }
                }).addTo(map);
            });

        // Define category colors
        /* var categories = {
             "Registered Office": "red",
             "Marketing Office": "yellow"
         };*/



        // Locations
        var locations = [
            // Special category locations
            // { state: "Karnataka", lat: 13.416243, lon: 77.288418, cities: ["Chintamani, Karnataka, India"], category: "Marketing Office" },
            //{ state: "Karnataka", lat: 15.3173, lon: 75.7139, cities: ["Bangalore, India"], category: "Registered Office" },
            //{ state: "Madhya Pradesh", lat: 22.9734, lon: 78.6569, cities: ["Indore, Madhya Pradesh, India"], category: "Marketing Office" },

            // Normal locations (no categories)
            { state: "Tirupati", lat: 13.6288, lon: 79.4192, cities: ["Rajgopal Boga"] },
            { state: "Karimnagar", lat: 18.4386, lon: 79.1288, cities: ["Koteswar Rao"] },
            { state: "Mumbai", lat: 19.0760, lon: 72.8777, cities: ["Manoj Bhatt"] },
            { state: "Ahmedabad", lat: 23.0225, lon: 72.5714, cities: ["Hitesh Thaker"] },
            { state: "Raipur", lat: 21.2514, lon: 81.6296, cities: ["Bhavesh Purohit"] },
            { state: "Secundrabad", lat: 17.4399, lon: 78.4983, cities: ["Suresh N"] },
            { state: "Warrangal", lat: 18.0, lon: 79.58, cities: ["Koteswar Rao"] },
            { state: "Hyderabad", lat: 17.4065, lon: 78.4772, cities: ["Shravan Kumar", "Salla Ramesh", "Mohammed Samuddin"] },
            { state: "Namakkal", lat: 11.2194, lon: 78.1678, cities: ["Venkat M"] },
            { state: "Coimbatore", lat: 11.0168, lon: 76.9558, cities: ["Suresh Kumar M"] },
            { state: "Salem", lat: 11.6643, lon: 78.1460, cities: ["V Silambarasan"] },
            { state: "Chennai", lat: 13.0843, lon: 80.2705, cities: ["Viju David", "Sridharan"] },
            { state: "Indore", lat: 22.9734, lon: 78.6569, cities: ["Viresh Vyas", "Deepak Hardiya", "Sanket Kumar", "Vaibhav Srivastav"] },
            { state: "Bellary", lat: 15.1394, lon: 76.9214, cities: ["Nandeesh BS"] },
            { state: "Bangalore", lat: 14.520447, lon: 75.643303, cities: ["BT Reddy", "Adavayya M", "Pradeep KR", "Vijaykumar Gonbal", "Ajeet Singh", "Manoj Hegde", "Milan Shah", "Shankar Narayan", "Sinclair", "Naveen Kumar V"] },
            { state: "Vijaywada", lat: 16.515099, lon: 80.632095, cities: ["Rajgopal Boga"] },
            { state: "Chittoor", lat: 13.0827, lon: 80.2707, cities: ["M Thyagaraja"] }
        ];

        locations.forEach(location => {
            // var iconUrl = "https://unpkg.com/leaflet@1.9.4/dist/images/marker-icon.png"; // Default icon
            var iconUrl = "/images_/pin.png"; // Default icon

            /*                if (location.category && categories[location.category]) {
                                if (categories[location.category] == "yellow") {
                                    iconUrl = `/images_/yello.png`;
            
                                } else {
                                    iconUrl = `/images_/red.png`;
            
                                }
                              //  iconUrl = `https://maps.google.com/mapfiles/ms/icons/${categories[location.category]}-dot.png`;
                            }*/

            var icon = L.icon({
                iconUrl: iconUrl,
                iconSize: [32, 32]
            });

            L.marker([location.lat, location.lon], { icon: icon })
                .addTo(map)
                .bindPopup(`<b>${location.state}</b><br>${location.cities.join(", ")}`);
        });

        // Legend for categories
        //var legend = L.control({ position: 'bottomleft' });
        //legend.onAdd = function (map) {
        //    var div = L.DomUtil.create('div', 'info legend');
        //    var categoriesList = ['Registered Office', 'Marketing Office'];

        //    categoriesList.forEach(category => {
        //        div.innerHTML += `<i style="background:${categories[category]}; width:15px; height:15px; display:inline-block; margin-right:5px;"></i> ${category}<br>`;
        //    });

        //    return div;
        //};
        //legend.addTo(map);
    </script>

    <script src="Admin/assets/libs/snackbar/snackbar.min.js"></script>
</asp:Content>

