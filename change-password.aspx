<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="change-password.aspx.cs" Inherits="change_password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                            <li class="breadcrumb-item"><a title="Shop" href="/my-profile">My Account</a></li>
                            <li class="breadcrumb-item active" aria-current="page">Change Password</li>
                        </ol>
                    </nav>
                </div>
            </div>
        </section>
        <section class="container pb-14 pb-lg-19">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="text-center">
                        <h2 class="mb-6">Change Password</h2>
                    </div>

                    <div class="pt-12 pe-10 px-10 pb-12 bg-white b-r-10">
                        <asp:Label runat="server" Visible="false" ID="lblStatus"></asp:Label>

                        <asp:TextBox runat="server" class="form-control border-1 mb-7" MaxLength="30" placeholder="Current Password" TextMode="Password" ID="txtCurrent" />
                        <asp:RequiredFieldValidator ID="rfvtxtCurrent" runat="server" ControlToValidate="txtCurrent" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="change" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" class="form-control border-1 mb-7 passwordn" MaxLength="30" placeholder="New Password" TextMode="Password" ID="txtNew" />
                        <asp:RequiredFieldValidator ID="rfvtxtNew" runat="server" ControlToValidate="txtNew" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="change" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                        <%--<asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtNew" ErrorMessage="Password needs 8+ chars, 1 uppercase, 1 lowercase, 1 digit, 1 special." ValidationGroup="change" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"></asp:RegularExpressionValidator>--%>
                        <asp:TextBox runat="server" class="form-control border-1 mb-7  password" placeholder="Confirm New Password" MaxLength="30" TextMode="Password" ID="txtConfirm" />
                        <asp:RequiredFieldValidator ID="rfvtxtConfirm" runat="server" ControlToValidate="txtConfirm" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="change" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cmptxtConfirm" runat="server" ControlToValidate="txtConfirm" ControlToCompare="txtNew" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="change" ErrorMessage="Password doesn't match"> </asp:CompareValidator>
                        <div class="form-check ps-0 m-0 remember-box mb-7">
                            <input class="checkbox_animated check-box" type="checkbox" id="flexCheckDefault">
                            <label class="form-check-label" for="flexCheckDefault">View Password</label>
                        </div>
                        <%-- <input name="email" type="password" class="" placeholder="Current Password" required="" fdprocessedid="cpot6r">
                    <input name="email" type="password" class="form-control border-1 mb-5" placeholder="New Password" required="" fdprocessedid="cpot6r">
                    <input name="email" type="password" class="form-control border-1 mb-5" placeholder="Confirm New Password" required="" fdprocessedid="cpot6r">--%>
                        <asp:Button CssClass="btn green-btn btn-forgot fs-18px btn-hover-bg-primary btn-hover-border-primary w-100 mt-7" ValidationGroup="change" ID="BtnchnagePwd" Text="Change Password" runat="server" OnClick="BtnchnagePwd_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </section>

    </main>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#flexCheckDefault").change(function () {
                if ($(this).is(":checked")) {
                    $("#<%=txtCurrent.ClientID%>").attr("type", "text");
                    $("#<%=txtNew.ClientID%>").attr("type", "text");
                    $("#<%=txtConfirm.ClientID%>").attr("type", "text");
                } else {
                    $("#<%=txtCurrent.ClientID%>").attr("type", "password");
                    $("#<%=txtNew.ClientID%>").attr("type", "password");
                    $("#<%=txtConfirm.ClientID%>").attr("type", "password");

                }
            });
        });
    </script>
</asp:Content>

