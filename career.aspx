<%@ Page Title="Careers at ArchidPly Decor " Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="career.aspx.cs" Inherits="career" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="keywords" content="career in plywood, jobs in plywood company, ArchidPly Decor career, plywood manufacturers in india, best plywood in india, jobs in bangalore, laminated plywood, plywood supplier in india, top plywood company, factory jobs" />

    <meta name="description" content="Join ArchidPly Decor – top plywood manufacturer in India. Explore jobs in design, sales & manufacturing at our Bangalore offices. " />
    <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <main id="content" class="wrapper layout-page">
        <section data-animated-id="1">

            <div class="bg-body-secondary py-5">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb breadcrumb-site py-0 d-flex justify-content-center">
                        <li class="breadcrumb-item"><a class="text-decoration-none text-body" href="#">Home</a>
                        </li>
                        <li class="breadcrumb-item active pl-0 d-flex align-items-center" aria-current="page">Career</li>
                    </ol>
                </nav>
            </div>
            <div class="container">
                <div class="text-center pt-13 mb-13 mb-lg-15">
                    <div class="text-center">
                        <h1 class="fs-36px mb-7">Archidply Decor Career</h1>
                        <p class="fs-18px mb-0 w-lg-60 w-xl-50 mx-md-13 mx-lg-auto">Discover endless possibilities and unleash your potential. Join a team where your dreams turn into achievements.</p>
                    </div>
                </div>
            </div>
        </section>
        <section>
            <div class="container">
                <div class="row align-items-center mb-10">
                    <div class="col-xl-6 col-lg-6 col-md-12 col-sm-6  mb-13 mb-lg-0 animate__fadeInUp animate__animated " data-animate="fadeInUp">
                        <div class="text-left new-career">
                            <p class="font-primary fs-5 text-body-emphasis mb-5">Archidply Decor career.....</p>
                            <h2 class="text-uppercase fs-32px fw-bold mb-6">Shape Your Future with Us!</h2>
                            <p class="fs-18px mb-2">Welcome to Archidply Decor Careers! We’re not just a workplace; we’re a community of innovators, dreamers, and achievers. Whether you're a seasoned professional or just starting your journey, we offer opportunities to grow, learn, and make a difference. Join us in shaping the future and achieving greatness together.</p>
                        </div>
                        <p class="font-primary fs-5 text-body-emphasis mb-5">Want Join Us?</p>
                        <p class="fs-18px mb-12">Ready to take the next step in your career? Explore opportunities and join a team that values your unique creativity and perspectives</p>
                    </div>
                    <div class="col-lg-6 card mb-4 rounded-4 p-7 mb-5" data-animate="fadeInUp">
                        <h4 class="font-primary fs-5 text-body-emphasis mb-12">Please fill out the form below to apply for a job</h4>
                        <div class="row gx-9">
                            <div class="col-lg-6  col-12 mb-10">
                                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control acceptOnlyAlpha" MaxLength="100" Placeholder="Full Name*"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtFullName" SetFocusOnError="true" ValidationGroup="jobapply" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg-6  col-12 mb-10">
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control onlyNum" MaxLength="10" Placeholder="Phone Number*"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhone" SetFocusOnError="true" ValidationGroup="jobapply" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPhone" Display="Dynamic" ValidationGroup="jobapply" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Enter 10 Digit Valid Phone Number" ValidationExpression="^\d{10}$"></asp:RegularExpressionValidator>

                            </div>
                            <div class="col-lg-6  col-12 mb-10">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="100" Placeholder="Your Email*"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmail" SetFocusOnError="true" ValidationGroup="jobapply" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="jobapply" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Invalid E-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-lg-6  col-12 mb-10">
                                <asp:TextBox ID="txtInterst" runat="server" CssClass="form-control acceptOnlyAlpha" MaxLength="100" Placeholder="Interested Field*"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtInterst" SetFocusOnError="true" ValidationGroup="jobapply" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg-6  col-12 mb-10">
                                <asp:TextBox ID="txtExp" runat="server" CssClass="form-control onlyNum" MaxLength="10" Placeholder="Year of Experience*"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtExp" SetFocusOnError="true" ValidationGroup="jobapply" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-lg-6  col-12 mb-10">
                                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control acceptOnlyAlpha" MaxLength="100" Placeholder="City*"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCity" SetFocusOnError="true" ValidationGroup="jobapply" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12 mb-10">
                                <label class="text-dark">Cover Letter<sup class="text-danger">*</sup></label>
                                <asp:FileUpload CssClass="form-control" ID="FileUpload1" runat="server" />
                            </div>
                        </div>
                        <div>
                            <asp:Button ID="btnApply" runat="server" CssClass="btn btn-primary mb-10 mt-3" Text="Apply" ValidationGroup="jobapply" OnClick="btnApplyJob" />
                            <asp:Label ID="lblpdf" runat="server" Visible="false"></asp:Label>
                        </div>

                    </div>
                </div>
            </div>
        </section>


    </main>
</asp:Content>

