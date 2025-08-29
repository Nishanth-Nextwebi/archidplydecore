<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="add-coupon.aspx.cs" Inherits="Admin_add_coupon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">

            <!-- start page title -->
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0"><%=Request.QueryString["id"]==null?"Add":"Update" %> Coupon Code</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="dashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Settings</a></li>
                                <li class="breadcrumb-item active"><%=Request.QueryString["id"]==null?"Add":"Update" %> Coupon Code</li>
                            </ol>
                        </div>

                    </div>
                </div>
            </div>
            <!-- end page title -->

            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex">
                            <h4 class="card-title mb-0 flex-grow-1"><%=Request.QueryString["id"]==null?"Add":"Update" %> Coupon Code</h4>
                        </div>
                        <!-- end card header -->
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-4 mb-3">
                                    <label>Coupon Type<sup>*</sup></label>
                                    <asp:DropDownList runat="server" class="form-control mb-2 mr-sm-2" ID="ddlCouponType">
                                        <asp:ListItem Value=" ">Select Coupon Type</asp:ListItem>
                                        <asp:ListItem Value="Percentage">Percentage</asp:ListItem>
                                        <asp:ListItem Value="Value">Value</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCouponType" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4 mb-3">
                                    <label>Coupon Value<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="6" class="form-control mb-2 mr-sm-2 decimalInput" ID="txtValue" />
                                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtValue" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4 mb-3">
                                    <label>Expiry Date<sup>*</sup></label>
                                    <asp:TextBox runat="server"  class="form-control mb-2 mr-sm-2 datepickerCurrent flatpickr-input" ID="txtDate" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDate" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-3 mb-3">
                                    <label>Coupon Code<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="50" class="form-control mb-2 mr-sm-2" ID="txtCode" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCode" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-3 mb-3">
                                    <label>Minimum Cart Value</label>
                                    <asp:TextBox runat="server" MaxLength="6" class="form-control mb-2 mr-sm-2 decimalInput" ID="txtMinValue" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMinValue" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-3 mb-3">
                                    <label>No of Usage<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="500" class="form-control mb-2 mr-sm-2 onlyNum" ID="txtNoOfUsage" />
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtNoOfUsage" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-3 mb-3">
                                    <label>Customer Id</label>
                                    <asp:TextBox runat="server" MaxLength="50" class="form-control mb-2 mr-sm-2" ID="txtCustomerId" />
                                    <%--<asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="txtCustomerId" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-lg-12 mb-3">
                                    <label>Coupon description</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" Rows="3" MaxLength="200" class="form-control mb-2 mr-sm-2" ID="txtDesc" />
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDesc" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-lg-12 mb-3">
                                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary waves-effect waves-light m-t-25" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />
                                    <asp:Button runat="server" ID="addCoupon" CssClass="btn btn-outline-danger waves-effect waves-light m-t-25" Visible="false" Text="Clear All" OnClick="addCoupon_Click" />
                                </div>
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
                            <h4 class="card-title mb-0 flex-grow-1">Manage Coupon Code</h4>
                        </div>
                        <div class="card-body">
                            <table id="alternative-pagination" class="table nowrap dt-responsive align-middle table-striped table-bordered myTable" style="width: 100%;">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Coupon Type</th>
                                        <th>Coupon Value</th>
                                        <th>Number of Usage</th>
                                        <th>Total Used</th>
                                        <th>Customer Id</th>
                                        <th>Coupon Code</th>
                                        <th>Expiry Date</th>
                                        <th>Added On</th>
                                        <th class="text-center">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%=strColors %>
                                </tbody>
                            </table>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
    <script src="assets/js/pages/add-coupon.js"></script>
</asp:Content>

