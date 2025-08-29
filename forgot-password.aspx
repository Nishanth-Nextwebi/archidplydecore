<%@ Page Title="Reset Password | Plywood manufacturer in Bangalore, India" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="forgot-password.aspx.cs" Inherits="forgot_password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <meta name="keywords" content="ArchidPly Decor login, forgot password, plywood company in india, best plywood in india, account access, ply manufacturer in india, laminated plywood, veneer plywood, sign in plywood site, reset password" />

<meta name="description" content="Forgot your password? Easily reset your ArchidPly Decor account and continue exploring top plywood & veneer products in India. "/>
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
    <style>
        .card-product {
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            background: #fff !important;
            padding-right: 20px;
        }

        .bg-overlay::before {
            background: linear-gradient(8deg, rgba(0, 0, 0, 0.5) 46%, transparent 66%);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main id="content" class="wrapper layout-page" style="background: #fbf1e9">
        <section class="z-index-2 position-relative pb-2 mb-5">

            <div class="mb-3">
                <div class="container">
                    <nav class="py-4 lh-30px" aria-label="breadcrumb">
                        <ol class="breadcrumb justify-content-start py-1 mb-0">
                            <li class="breadcrumb-item"><a title="Home" href="/">Home</a></li>
                            <li class="breadcrumb-item"><a title="Shop" href="/my-profile">My Account</a></li>
                            <li class="breadcrumb-item active" aria-current="page">Forgot Password</li>
                        </ol>
                    </nav>
                </div>
            </div>
        </section>
        <section class="container pb-14 pb-lg-19">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="text-center">
                        <h1 class="mb-6">Forgot Password</h1>
                    </div>

                    <div class="pt-12 pe-10 px-10 pb-12 bg-white b-r-10">
                        <asp:Label runat="server" ID="lblStatus" Visible="false"></asp:Label>

                        <asp:TextBox runat="server" ID="txtBoxEmail" placeholder="Your email" CssClass="form-control border-1 mb-5" MaxLength="200"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtBoxEmail" SetFocusOnError="true" ValidationGroup="FrgtPwd" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpValidator1" runat="server" ControlToValidate="txtBoxEmail" Display="Dynamic" ValidationGroup="FrgtPwd" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Invalid E-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                        <div class="d-flex align-items-center mt-1 mb-10">
                            <div class="form-check p-lg-0">
                                Enter your email we will send the link to change the password 
                            </div>
                        </div>
                                                <asp:Button ValidationGroup="FrgtPwd" OnClick="btnReset_Click" Text="Forgot Password" runat="server" ID="btnReset" CssClass="btn green-btn btn-forgot fs-18px btn-hover-bg-primary btn-hover-border-primary w-100" />
                    </div>
                </div>
            </div>
        </section>

    </main>
</asp:Content>

