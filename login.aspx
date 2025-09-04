<%@ Page Title="Login | Access Your Account & Orders" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="keywords" content="plywood prices, flush doors cart, wooden doors cart, plywood supplier order, laminated plywood cart, best plywood India buy, plywood company cart, door manufacturers purchase, laminates in Bangalore cart, eco-friendly plywood checkout" />

    <meta name="description" content="Securely log in to your Archidply Decor account to manage orders, track shipments, and view your personalized decor selections. " />
    <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
    <style>
        .custom-control.d-flex.form-check {
            padding-left: 0px !important;
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
      "name": "Login",
      "item": "https://archidplydecor.com/login"
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
                            <li class="breadcrumb-item active" aria-current="page">Login</li>
                        </ol>
                    </nav>
                </div>
            </div>
        </section>
        <section class="container pb-14 pb-lg-19">
            <div class="row justify-content-center">
                <div class="col-md-8 col-lg-6">
                    <div class="text-center">
                        <h1 class="mb-6">Login</h1>
                    </div>
                    <div class="pt-12 pe-10 px-10 pb-12 bg-white b-r-10">
                        <p class="text-center new-hightlight fs-16 mb-10">
                            Don’t have an account yet?
                            <a href="/signup.aspx" class="text-black">Sign up</a>  for free
                        </p>
                        <asp:Label runat="server" Visible="false" ID="lblLoginStatus"></asp:Label>
                        <div>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mb-5" MaxLength="200" Placeholder="Your email"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEmail" SetFocusOnError="true" ValidationGroup="login1" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ValidationGroup="login1" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Invalid E-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </div>
                        <div>
                            <div class="position-relative">
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" MaxLength="30" Placeholder="Password" TextMode="Password"></asp:TextBox>
                                <button class="btn btn-link new-eye-hide position-absolute  text-decoration-none text-muted shadow-none password-addon" id="password-addon" type="button">
                                    <i class="fa-solid fa-eye"></i>
                                </button>
                            </div>
                            <asp:RequiredFieldValidator ID="rfv2" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPassword" SetFocusOnError="true" ValidationGroup="login1" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                        </div>

                        <div class="d-flex align-items-center justify-content-between mt-8 mb-7">
                            <div class="custom-control d-flex form-check">
                                <asp:CheckBox ID="chkStaySignedIn" runat="server" CssClass=" rounded-0 me-3" />
                                <label class="custom-control-label text-body" for="chkStaySignedIn">Stay signed in</label>
                            </div>
                            <a href="/forgot-password.aspx" class="text-secondary"><u>Forgot your password?</u></a>
                        </div>
                        <div>
                            <asp:Button ID="btnSubmit" runat="server" ValidationGroup="login1" CssClass="btn btn-dark btn-hover-bg-primary btn-hover-border-primary w-100 mb-6" Text="Log In" OnClick="btnLogin_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
    <script>
        document.getElementById('password-addon').addEventListener('click', function () {
            var passwordField = document.getElementById('<%= txtPassword.ClientID %>');
            var icon = this.querySelector('i');
            if (passwordField.type === 'password') {
                passwordField.type = 'text';
                icon.classList.remove('fa-eye');
                icon.classList.add('fa-eye-slash');
            } else {
                passwordField.type = 'password';
                icon.classList.remove('fa-eye-slash');
                icon.classList.add('fa-eye');
            }
        });
</script>
</asp:Content>

