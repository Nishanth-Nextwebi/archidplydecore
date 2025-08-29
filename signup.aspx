<%@ Page Title="Sign Up | ArchidPly Decor Best plywood in bangalore, India" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="signup.aspx.cs" Inherits="signup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <meta name="keywords" content="signup ArchidPly, plywood supplier login, ArchidPly Decor account, best plywood in india, laminated plywood, veneer plywood, furniture plywood, plywood in bangalore, top plywood companies, dealer login" />

<meta name="description" content="Create your account with ArchidPly Decor to access premium plywood products, pricing, offers & more. " />
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main id="content" class="wrapper layout-page" style="background: #fbf1e9">
        <section class="z-index-2 position-relative pb-2 mb-5">

            <div class="mb-3">
                <div class="container">
                    <nav class="py-4 lh-30px" aria-label="breadcrumb">
                        <ol class="breadcrumb justify-content-start py-1 mb-0">
                            <li class="breadcrumb-item"><a title="Home" href="/">Home</a></li>
                            <li class="breadcrumb-item active" aria-current="page">Sign up</li>
                        </ol>
                    </nav>
                </div>
            </div>
        </section>
        <section class="container pb-14 pb-lg-19">
            <div class="row justify-content-center">
                <div class="col-md-8 col-lg-6">
                    <div class="text-center">
                        <h1 class="mb-6">Sign up</h1>
                    </div>
                    <div class="pt-12 pe-10 px-10 pb-12 bg-white b-r-10">
                         <p class="text-center new-hightlight fs-16 mb-10">Already have an account? <a href="/login.aspx" class="text-black">Log in</a></p>
                        <asp:Label runat="server" Visible="false" ID="lblSighUpStatus"></asp:Label>
                        <asp:TextBox ID="txtFirstName" runat="server" MaxLength="100" CssClass="form-control border-1 mb-5 acceptOnlyAlpha" Placeholder="First name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="fv3" runat="server" ForeColor="Red" ControlToValidate="txtFirstName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="signup1" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtLastName" runat="server" MaxLength="100" CssClass="form-control border-1 mb-5 acceptOnlyAlpha" Placeholder="Last name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv4" runat="server" ForeColor="Red" ControlToValidate="txtLastName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="signup1" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtSignUpEmail" runat="server" MaxLength="200" CssClass="form-control border-1 mb-5" Placeholder="Your email"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv5" runat="server" ForeColor="Red" ControlToValidate="txtSignUpEmail" SetFocusOnError="true" Display="Dynamic" ValidationGroup="signup1" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="txtSignUpEmail" ValidationGroup="signup1" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Invalid E-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtSignUpMobileNo" runat="server" MaxLength="10" CssClass="form-control border-1 mb-5 onlyNum" Placeholder="Your Phone Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv6" runat="server" ForeColor="Red" ControlToValidate="txtSignUpMobileNo" SetFocusOnError="true" Display="Dynamic" ValidationGroup="signup1" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSignUpMobileNo" Display="Dynamic" ValidationGroup="signup1" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Enter 10 Digit Valid Phone Number" ValidationExpression="^\d{10}$"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtSignUpPassword" runat="server" MaxLength="30" CssClass="form-control border-1" Placeholder="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv7" runat="server" ForeColor="Red" ControlToValidate="txtSignUpPassword" SetFocusOnError="true" Display="Dynamic" ValidationGroup="signup1" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                        <div class="d-flex align-items-center mt-8 mb-7">
                            <asp:CheckBox ID="chkAgreePolicy" runat="server" CssClass=" rounded-0 me-3" />
                            <label for="chkAgreePolicy" class="custom-control-label text-body">Yes, I agree with Grace <a href="/privacy-policy" class="text-black-new">Privacy Policy</a> and <a href="/privacy-policy" class="text-black">Terms of Use</a></label>
                        </div>
                        <asp:Button ID="btnSignUp" ValidationGroup="signup1" runat="server" CssClass="btn btn-dark btn-hover-bg-primary btn-hover-border-primary w-100" Text="Sign Up" OnClick="btnSignUp_Click" />
                        <asp:HiddenField ID="txtUGuid" runat="server" />
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>

