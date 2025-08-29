<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="reset-password.aspx.cs" Inherits="reset_password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                            <li class="breadcrumb-item active" aria-current="page">Reset Password</li>
                        </ol>
                    </nav>
                </div>
            </div>
        </section>
        <section class="container pb-14 pb-lg-19">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="text-center">
                        <h2 class="mb-6">Reset Password</h2>
                    </div>
                    <div class="pt-12 pe-10 px-10 pb-12 bg-white b-r-10">
                        <div class="row g-4">
                                    <asp:Label runat="server" ID="lblStatus"></asp:Label>
                                    <div class="col-12">
                                        <div class="PasswordBox">
                                            <div class="InputBox">
                                                <asp:TextBox runat="server" Type="password" placeholder="Enter New Password" CssClass="form-control border-1 mb-5 passwordInput" MaxLength="30" ID="txtPwd"></asp:TextBox>

                                            </div>

                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtPwd" SetFocusOnError="true" ValidationGroup="LogIn" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegExp1" runat="server" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                            ErrorMessage="Password length must be between 5 to 16 characters"
                                            ControlToValidate="txtPwd"
                                            ValidationExpression="^[a-zA-Z0-9'@&#.\s]{5,16}$" />

                                    </div>
                                    <div class="col-12">
                                        <div class="PasswordBox">
                                            <div class="InputBox">
                                                <asp:TextBox runat="server" Type="password" placeholder="Re-Enter New Password" CssClass="form-control border-1 mb-5 passwordInput" MaxLength="30" ID="txtRePwd"></asp:TextBox>
                                            </div>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtRePwd" SetFocusOnError="true" ValidationGroup="LogIn" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                            ErrorMessage="Password length must be between 5 to 16 characters"
                                            ControlToValidate="txtRePwd"
                                            ValidationExpression="^[a-zA-Z0-9'@&#.\s]{5,16}$" />
                                    </div>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                                        ControlToCompare="txtPwd" ControlToValidate="txtRePwd"
                                        ErrorMessage="Password does not match." Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ></asp:CompareValidator>
                                    <div class="col-12">
                                        <div class="forgot-box">
                                            <div class="form-check ps-0 m-0 remember-box">
                                                <input class="checkbox_animated check-box" type="checkbox" id="flexCheckDefault">
                                                <label class="form-check-label" for="flexCheckDefault">View Password</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">

                                        <asp:Button runat="server" ID="btnSubmit" CssClass="btn green-btn btn-forgot fs-18px btn-hover-bg-primary btn-hover-border-primary w-100" OnClick="btnSubmit_Click" Text="Submit" />
                                    </div>
                                </div>
                        </div>
                </div>
            </div>
        </section>

    </main>
     <script type="text/javascript">
     $(document).ready(function () {
         $("#flexCheckDefault").change(function () {
           if( $(this).is(":checked")){
               $("#<%=txtPwd.ClientID%>").attr("type","text");
               $("#<%=txtRePwd.ClientID%>").attr("type","text");
           }else{
                 $("#<%=txtPwd.ClientID%>").attr("type","password");
               $("#<%=txtRePwd.ClientID%>").attr("type","password");

           }
         });
     });
     </script>
</asp:Content>

