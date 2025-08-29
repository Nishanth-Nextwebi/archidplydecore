<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="banner-images.aspx.cs" Inherits="Admin_banner_images" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Upload Banner Images</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="dashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Settings</a></li>
                                <li class="breadcrumb-item active">Upload Banner Images</li>
                            </ol>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex">
                            <h4 class="card-title mb-0 flex-grow-1">Add Banner Images</h4>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblStatus" runat="server" Visible="false" Width="100%"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 mb-2">
                                    <label>Banner Title<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2" ID="txtTitle" />
                                    <asp:RequiredFieldValidator ID="reqProductUrl" runat="server" ControlToValidate="txtTitle" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-sm-6 mb-2">
                                    <label>External Link<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2" ID="txtlink" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtlink" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-sm-6 mb-2">
                                    <label>Desktop Image<sup>*</sup></label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control"></asp:FileUpload>
                                    <small style="color: red;">.png, .jpeg, .jpg, .gif, .webp formats are required, Image Size Should be 1520*650 px</small><br />
                                    <%=strThumbImage %>
                                </div>
                                <div class="col-sm-6 mb-2 d-none">
                                    <label>Mobile Image<sup>*</sup></label>
                                    <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control"></asp:FileUpload>
                                    <small style="color: red;">.png, .jpeg, .jpg, .gif, .webp formats are required, Image Size Should be 400*500 px</small><br />
                                    <%=strThumbImage1 %>
                                </div>
                            </div>
                             <div class="col-lg-12 mt-3">
     <label>Full Description<sup>*</sup></label>
     <asp:TextBox runat="server" TextMode="MultiLine" MaxLength="250" class="form-control mb-2 mr-sm-2 summernote" ID="txtDesc" Placeholder="Full Description ....." />
     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDesc" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
 </div>
                            <div class="row mt-3">
                                <div class="col-lg-5">
                                    <asp:Button runat="server" ID="btnUpload" CssClass="btn btn-primary waves-effect waves-light m-t-25" ValidationGroup="Save" Text="Save" Style="margin-top: 5px;" OnClick="btnUpload_Click" />
                                    <asp:Button runat="server" ID="addBanner" CssClass="btn btn-outline-danger waves-effect waves-light m-t-25" Visible="false" Text="Clear All" Style="margin-top: 5px;" OnClick="addBanner_Click" />
                                    <asp:Label ID="lblThumb" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblThumb1" runat="server" Visible="false"></asp:Label>
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
                            <h4 class="card-title mb-0 flex-grow-1">Manage Banners</h4>
                        </div>
                        <div class="card-body">
                            <table id="alternative-pagination" class="table nowrap dt-responsive align-middle table-striped table-bordered myTable" style="width: 100%;">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Banner Title</th>
                                        <th>Desktop Image</th>
                                        <th>Updated On</th>
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
    <script src="assets/js/pages/banner-images.js"></script>
</asp:Content>

