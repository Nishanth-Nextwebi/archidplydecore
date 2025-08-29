<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="investors-list.aspx.cs" Inherits="Admin_investors_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Manage Investors</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="dashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Settings</a></li>
                                <li class="breadcrumb-item active">Add Investors</li>
                            </ol>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex">
                            <h4 class="card-title mb-0 flex-grow-1">Add Investors</h4>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblStatus" runat="server" Visible="false" Width="100%"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4 mb-2">
                                    <label>Title<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2" ID="txtTitle" />
                                    <asp:RequiredFieldValidator ID="reqProductUrl" runat="server" ControlToValidate="txtTitle" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-4 mb-2">
                                    <label>Logo<sup>*</sup></label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control"></asp:FileUpload>
                                    <small style="color: red;">.png, .jpeg, .jpg, .gif, .webp formats are required, Image Size Should be 128*128px</small><br />
                                    <%=strThumbImage %>
                                </div>
                                <div class="col-sm-4 mb-2">
                                    <label>External Link</label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2" ID="txtlink" />
                                </div>
                            </div>
                            <div class="row">
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
                            <h4 class="card-title mb-0 flex-grow-1">Manage Investors</h4>
                        </div>
                        <div class="card-body">
                            <table id="alternative-pagination" class="table nowrap dt-responsive align-middle table-striped table-bordered myTable" style="width: 100%;">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Title</th>
                                        <th>Logo</th>
                                        <th>Link</th>
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
    <script src="assets/js/pages/investers-list.js"></script>
</asp:Content>