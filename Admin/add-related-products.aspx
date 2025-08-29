<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="add-related-products.aspx.cs" Inherits="Admin_add_related_products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0"><%=Request.QueryString["id"]==null?"Add":"Update" %> Related Products</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="dashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Products</a></li>
                                <li class="breadcrumb-item active"><%=Request.QueryString["id"]==null?"Add":"Update" %> Related Products</li>
                            </ol>
                        </div>

                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex">
                            <h4 class="card-title mb-0 flex-grow-1"><%=Request.QueryString["id"]==null?"Add":"Update" %> Related Products</h4>
                        </div>
                        <!-- end card header -->
                        <div class="card-body">
                            <div class="row mb-2">
                                <div class="col-lg-4">
                                    <label>Select Category <sup>*</sup></label>
                                    <asp:DropDownList runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true" class="form-control mb-2 mr-sm-2 basic" ID="DropDownList1">
                                        <asp:ListItem Value="0">Select Category</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ControlToValidate="DropDownList1" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4">
                                    <label>Select Related Products <sup>*</sup></label>
                                    <asp:DropDownList runat="server" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack="true" class="form-control mb-2 mr-sm-2 basic" ID="ddlProduct">
                                        <asp:ListItem Value="0">Select Product</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="0" ControlToValidate="ddlProduct" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4">
                                    <label>Map Products <sup>*</sup></label>
                                    <asp:ListBox ID="chkMapProducts" runat="server" CssClass="lstChks" SelectionMode="Multiple"></asp:ListBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="chkMapProducts" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-lg-5">
                                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" ValidationGroup="Save" />
                                </div>
                                <div class="col-lg-12 mt-2">
                                    <p style="color: red; font-weight: bold;">Note : <sup>*</sup> are required fields</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $(document).on("keypress", function (e) {
                if (e.shiftKey) {
                    return;
                }
                if (e.which == 13) { document.getElementById("<%=btnSave.ClientID %>").click(); }
            });
            $(document).ready(function () {
                $('.lstChks').fSelect();
                $(".fs-wrap").each(function () {
                    var wrap = $(this).width();
                    $(this).find(".fs-dropdown").css("width", wrap);
                });
            });
        });
    </script>
</asp:Content>



