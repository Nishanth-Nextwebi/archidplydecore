<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Tags.aspx.cs" Inherits="Admin_space" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="page-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0"><%=Request.QueryString["id"]==null?"Add":"Update" %> Tags</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="dashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Settings</a></li>
                                <li class="breadcrumb-item active"><%=Request.QueryString["id"]==null?"Add":"Update" %>Tags</li>
                            </ol>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex">
                            <h4 class="card-title mb-0 flex-grow-1"><%=Request.QueryString["id"]==null?"Add":"Update" %> Tags</h4>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <asp:Label ID="lblStatus" runat="server" Visible="false" Width="100%"></asp:Label>
                                <div class="col-lg-4 mb-3">
                                    <label>Tag Name<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2 txtTag" ID="txtTag" />
                                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtTag" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4 mb-3">
                                    <label>Tag Url<sup>*</sup></label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2 txtUrl" ID="txtUrl" />
                                    <asp:RequiredFieldValidator ID="reqProductUrl" runat="server" ControlToValidate="txtUrl" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter a valid Name." ValidationExpression="^[a-zA-Z ]*$" ControlToValidate="txtUrl" ValidationGroup="Contact" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>

                                </div>

                                <div class="col-lg-2 mb-3">
                                    <label>Display Order</label>
                                    <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="form-control mb-2 mr-sm-2 numberOnlyInput"></asp:TextBox>
                                </div>
                                <div class="col-lg-2 mb-3">
                                    <label>Display Home ?</label>
                                    <asp:CheckBox CssClass="form-control" ID="chbDispHome" runat="server" />
                                    <span></span>
                                </div>
                                <div class="col-lg-4 mb-3">
                                    <label>Tag Image<sup>*</sup></label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control"></asp:FileUpload>
                                    <small style="color: red;">.png, .jpeg, .jpg, .gif, .webp formats are required, Image Size Should be 300*300 px</small><br />
                                    <%=strImage %>
                                </div>
                                <div class="col-lg-6 mb-3">
                                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary waves-effect waves-light m-t-25" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />
                                    <asp:Button runat="server" ID="AddTags" CssClass="btn btn-outline-danger waves-effect waves-light m-t-25" Text="Clear All" Visible="false" OnClick="AddTags_Click" />
                                    <asp:Label ID="lblThumb" runat="server" Visible="false"></asp:Label>
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
                            <h4 class="card-title mb-0 flex-grow-1">Manage Tags</h4>
                        </div>
                        <div class="card-body">
                            <table id="alternative-pagination" class="table nowrap dt-responsive align-middle table-striped table-bordered myTable" style="width: 100%;">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Tag Image</th>
                                        <th>Tag Title</th>
                                        <th>DisplayHome</th>
                                        <th>Updated On</th>
                                        <th class="text-center">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%=strTag %>
                                </tbody>
                            </table>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
    <script src="assets/js/pages/tags.js"></script>
</asp:Content>
