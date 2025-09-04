<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="my-profile.aspx.cs" Inherits="my_profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
    <style>
        .form-control{
            background:#fff !important;
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
      "name": "My Profile",
      "item": "https://archidplydecor.com/my-profile"
    }
  ]
}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row mb-9 align-items-center justify-content-between">

        <div class="col-sm-6 mb-8 mb-sm-0">
            <h2 class="fs-4 mb-0 text-dark">Profile settings</h2>
        </div>
    </div>


    <div class="card mb-4 rounded-4 p-7">
        <div class="card-body pt-0 pb-0 px-0">
            <div class="row mx-n8">

                <div class="col-lg-9 px-8">
                    <section class="p-xl-8">
                        <form class="form-border-1" runat="server">
                            <div class="row">
                                <div class="col-lg-8">
                                    <div class="row gx-9">
                                        <div class="col-6 mb-6">
                                            <label class="mb-2 fs-13px ls-1 fw-semibold text-uppercase" for="first-name">First Name</label>
                                            <asp:TextBox ID="txtFirstName" runat="server" MaxLength="100" CssClass="form-control acceptOnlyAlpha" Placeholder="Type here"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First name is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-6 mb-6">
                                            <label class="mb-2 fs-13px ls-1 fw-semibold text-uppercase" for="last-name">Last Name</label>
                                            <asp:TextBox ID="txtLastName" runat="server" MaxLength="100" CssClass="form-control acceptOnlyAlpha" Placeholder="Type here"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last name is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-lg-6 mb-6">
                                            <label class="mb-2 fs-13px ls-1 fw-semibold text-uppercase" for="email">Email</label>
                                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" CssClass="form-control" Placeholder="example@mail.com" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-6 mb-6">
                                            <label class="mb-2 fs-13px ls-1 fw-semibold text-uppercase" for="phone">Phone</label>
                                            <asp:TextBox ID="txtPhone" runat="server" MaxLength="12" CssClass="form-control onlyNum" Placeholder="+91 234567890"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone number is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Invalid phone number format" CssClass="text-danger" ValidationExpression="^\+?\d{10,15}$" Display="Dynamic"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col-lg-12 mb-6">
                                            <label class="mb-2 fs-13px ls-1 fw-semibold text-uppercase" for="address">Address</label>
                                            <asp:TextBox ID="txtAdd" runat="server" TextMode="MultiLine" Rows="3" MaxLength="250" CssClass="form-control" Placeholder="Type here"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAdd" ErrorMessage="Address is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                </div>
                                <aside class="col-lg-4">
                                    <div class="text-lg-center">
                                        <div class="mx-auto">
                                            <%=strProfileimg %>
                                            <%--                                            <asp:Image ID="imgUserPhoto" runat="server" CssClass="mb-9 rounded-pill" ImageUrl="../assets/imgs/avatar-1.png" AlternateText="User Photo" Height="196px" Width="196px" />--%>
                                        </div>
                                        <small class="d-block link-danger mt-2">.png, .jpeg, .jpg,.webp formats are required</small>
                                        <div>
                                            <asp:LinkButton ID="btnUpload" runat="server" CssClass="btn border hover-white bg-hover-primary border-hover-primary" OnClientClick="triggerFileUpload(); return false;">
    <i class="fas fa-cloud-upload"></i> Upload
                                            </asp:LinkButton>
                                            <asp:FileUpload ID="fileUploadPhoto" runat="server" CssClass="d-none" />
                                        </div>
                                    </div>
                                </aside>
                            </div>

                            <br>
                            <asp:Button runat="server" class="btn btn-primary" ID="btnUpdate" Text="Save Changes" OnClick="UpdateProfile_Click" />
                            <asp:Label ID="lblProfile" runat="server" Visible="false"></asp:Label>

                        </form>
                        <hr class="my-8">
                        <div class="row">
                            <div class="col-md-12">
                                <article class=" d-flex justify-content-between align-items-center  ">
                                    <div class="mr-auto ">
                                        <h4 class="fs-24px mb-0 text-dark font-weight-500">Change Password</h4>
                                    </div>
                                    <div>
                                        <a class="btn border btn-hover-text-light btn-hover-bg-primary btn-hover-border-primary btn-sm" href="/change-password">Change Password</a>
                                    </div>

                                </article>
                            </div>

                            <!--<div class="col-md-5">
                                    <article class="d-flex p-6 mb-6 bg-body-tertiary border rounded">
                                        <div class="mr-auto">
                                            <h6 class="fs-14px mb-0 font-weight-500">Remove account</h6>
                                            <small class="text-muted d-block" style="width: 70%">Once you delete your account, there is no going back.</small>
                                        </div>
                                        <div>
                                            <a class="btn border btn-hover-text-light btn-hover-bg-primary btn-hover-border-primary btn-sm" href="#">Deactivate</a>
                                        </div>

                                    </article>
                                </div>-->

                        </div>

                    </section>
                </div>

            </div>

        </div>

    </div>
    <script type="text/javascript">
        function triggerFileUpload() {
            document.getElementById('<%= fileUploadPhoto.ClientID %>').click();
        }
    </script>

</asp:Content>
