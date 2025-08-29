<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="dealers-list.aspx.cs" Inherits="Admin_dealers_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Manage Dealers</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="dashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Settings</a></li>
                                <li class="breadcrumb-item active">Add Dealers</li>
                            </ol>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex">
                            <h4 class="card-title mb-0 flex-grow-1">Add Dealers</h4>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblStatus" runat="server" Visible="false" Width="100%"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 mb-2">
                                    <label>Name<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2" ID="txtName" />
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtName" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-3 mb-2">
                                    <label>Phone No<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2" ID="txtPhone" />
                                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtPhone" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-sm-3 mb-2">
                                    <label>Emailn ID<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2" ID="txtEmail" />
                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-3 mb-2">
                                    <label>City<sup class="text-danger">*</sup></label>
                                    <asp:DropDownList runat="server" class="form-control mb-2 mr-sm-2" ID="ddlCity">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ControlToValidate="ddlCity" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-12 mt-2">
                                    <label>Address<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="200" TextMode="MultiLine" Rows="4" class="form-control mb-2 mr-sm-2" ID="txtAddress" />
                                    <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="txtAddress" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <%-- <div class="col-sm-4 mb-2 d-none">
                                    <label>Title<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2" ID="txtTitle" />
                                    <asp:RequiredFieldValidator ID="reqProductUrl" runat="server" ControlToValidate="txtTitle" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-4 mb-2 d-none">
                                    <label>Logo<sup>*</sup></label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control"></asp:FileUpload>
                                    <small style="color: red;">.png, .jpeg, .jpg, .gif, .webp formats are required, Image Size Should be 128*128px</small><br />
                                    <%=strThumbImage %>
                                </div>
                                <div class="col-sm-4 mb-2 d-none">
                                    <label>External Link</label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2" ID="txtlink" />
                                </div>--%>
                            </div>
                            <div class="row mt-3">
                                <div class="col-lg-5">
                                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary waves-effect waves-light m-t-25" ValidationGroup="Save" Text="Save" Style="margin-top: 5px;" OnClick="btnSave_Click" />
                                    <asp:Button runat="server" ID="AddNew" CssClass="btn btn-outline-danger waves-effect waves-light m-t-25" Visible="false" Text="Clear All" Style="margin-top: 5px;" OnClick="addNew_Click" />
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
                            <h4 class="card-title mb-0 flex-grow-1">Manage Dealers</h4>
                        </div>
                        <div class="card-body">
                            <table id="alternative-pagination" class="table table-striped table-bordered myTable" style="width: 100%;">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Name</th>
                                        <th>Phone No</th>
                                        <th>Email ID</th>
                                        <th>City</th>
                                        <th>Address</th>
                                        <th>Added On</th>
                                        <th class="text-center">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%=strImages %>
                                </tbody>
                            </table>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="assets/js/pages/dealers-list.js"></script>
</asp:Content>

