<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="pincode.aspx.cs" Inherits="Admin_pincode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0"><%=Request.QueryString["id"]==null?"Add":"Update" %> Pincode</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="dashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Settings</a></li>
                                <li class="breadcrumb-item active"><%=Request.QueryString["id"]==null?"Add":"Update" %> Pincode</li>
                            </ol>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex">
                            <h4 class="card-title mb-0 flex-grow-1"><%=Request.QueryString["id"]==null?"Add":"Update" %> Pincode</h4>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-4 mb-2">
                                    <label>Pincode<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="6" class="form-control mb-2 mr-sm-2 onlyNum" ID="txtPinCode" />
                                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtPinCode" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revPinCode" runat="server" Display="Dynamic" ControlToValidate="txtPinCode" ValidationGroup="Save" ValidationExpression="^[1-9][0-9]{5}$" ErrorMessage="Enter a valid 6-digit PIN code" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-lg-4 mb-2">
                                    <label>City<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2 acceptOnlyAlpha" ID="txtcity" />
                                    <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtcity" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpValidator3" runat="server" ErrorMessage="Enter a valid Name." ValidationExpression="^[a-zA-Z ]*$" ControlToValidate="txtcity" ValidationGroup="Save" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-lg-4 mb-2 d-none">
                                    <label>Shipping Price<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2 decimalInput" ID="txtShip" />
                                </div>
                                <div class="col-lg-4  col-md-6">
                                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary waves-effect waves-light m-t-25" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />
                                    <asp:Button runat="server" ID="addPincode" CssClass="btn btn-outline-danger waves-effect waves-light m-t-25" Text="Clear All" Visible="false" OnClick="addPincode_Click" />
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
                            <h4 class="card-title mb-0 flex-grow-1">Manage Pincode</h4>
                        </div>
                        <div class="card-body">
                            <table id="alternative-pagination" class="table nowrap dt-responsive align-middle table-striped table-bordered myTable" style="width: 100%;">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Pincode</th>
                                        <th>City</th>
                                        <th>Updated On</th>
                                        <th class="text-center">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%=strAllPincode %>
                                </tbody>
                            </table>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
    <script src="assets/js/pages/pincode.js"></script>
</asp:Content>

