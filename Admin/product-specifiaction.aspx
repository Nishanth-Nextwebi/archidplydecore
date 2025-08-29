<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="product-specifiaction.aspx.cs" Inherits="Admin_product_specifiaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Product Specifications</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="dashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Products</a></li>
                                <li class="breadcrumb-item active"><%=Request.QueryString["id"]==null?"Add":"Update" %> Specifications </li>
                            </ol>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex">
                            <h4 class="card-title mb-0 flex-grow-1">Product Specifications - <%=strProductName %></h4>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblStatus" runat="server" Visible="false" Width="100%"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <label>Title<span class="text-danger">*</span></label>
                                    <asp:TextBox runat="server"  class="form-control mb-2 mr-sm-2" ID="txtTitle" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-5 d-none">
                                    <label>Grade</label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txtGrade" />
                                    <%--<asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtGrade" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-lg-6">
                                    <label>Display Order</label>
                                    <asp:TextBox runat="server" MaxLength="5" class="form-control mb-2 mr-sm-2 numberOnlyInput" ID="txtOrder" />
                                    <%--<asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtOrder" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-lg-12">
                                    <label>Desc<span class="text-danger">*</span></label>
                                    <asp:TextBox runat="server" Rows="4" TextMode="MultiLine" class="form-control mb-2 mr-sm-2 " ID="txtDesc" />
                                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtDesc" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>

                                <div class="row">
                                    <div class="col-lg-2">
                                        <asp:Label ID="lblThumb" runat="server" Visible="false"></asp:Label>
                                        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary" Text="Save" Style="margin-top: 35px;" ValidationGroup="Save" OnClick="btnSave_Click" />
                                        <asp:Button runat="server" ID="btnclear" class="btn btn-outline-danger waves-effect waves-light" Text="Clear All" Style="margin-top: 35px;" OnClick="btnclear_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 mt-2">
                                    <p style="color: red; font-weight: bold;">Note : <sup>*</sup> are required fields</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex">
                            <h4 class="card-title mb-0 flex-grow-1">Manage Product Specifications</h4>
                        </div>
                        <div class="card-body">
                            <table id="alternative-pagination" class="table nowrap dt-responsive align-middle table-striped table-bordered myTable" style="width: 100%;">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Title</th>
                                        <th>DisplayOrder</th>
                                        <th>Description</th>
                                        <th>Added On</th>
                                        <th class="text-center">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%=strProductSpecificationss %>
                                </tbody>
                            </table>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
    <script src="assets/js/pages/product-specifiaction.js"></script>
</asp:Content>

