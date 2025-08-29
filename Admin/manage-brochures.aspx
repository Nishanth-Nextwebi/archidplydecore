<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="manage-brochures.aspx.cs" Inherits="Admin_manage_brochures" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Brochures</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Brochures</a></li>
                                <li class="breadcrumb-item active">Manage Brochures</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <asp:Label ID="lblStatus" runat="server" Visible="false" Width="100%"></asp:Label>
                    <div class="card">
                        <div class="card-header">
                            <h5 class="card-title"><%=Request.QueryString["id"] !=null?"Update":"Add" %> Brochures</h5>
                        </div>
                        <div class="card-body">
                            <div class="row gy-4">
                                <div class="col-lg-4">
                                    <label>Title<sup>*</sup></label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txtTitle" placeholder="Title" MaxLength="100" />
                                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtTitle" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4">
                                    <label>Image<sup>*</sup></label>
                                    <asp:FileUpload runat="server" class="form-control mb-2 mr-sm-2" ID="FileUpload1" />
                                 <small style="color: red;">.png, .jpeg, .jpg formats to be uploaded and size should be 128*128 px</small><br />
                                 <%=strImage %>
                                </div>

                                <div class="col-lg-4">
                                    <label>Upload PDF<sup>*</sup></label>
                                    <asp:FileUpload ID="UploadPDF" runat="server" CssClass="form-control mb-2 mr-sm-2" />
                                    <small style="color: red;">.pdf, .doc, formats are required.</small><br />
                                    <div id="divpdf" runat="server" visible="false">
                                    <a href="/<%=strPDF %>" target="_blank">
                                        <img src="assets/images/pdf.png" alt="" width="65" height="60"></a><br />
                                        Check PDF
                                    </div>
                                </div>

                                <div class="col-lg-4">
                                    <div>
                                        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary waves-effect waves-light m-t-25" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />
                                        <asp:Button runat="server" ID="btnNew" CssClass="btn btn-outline-danger waves-effect waves-light m-t-25" Text="Clear All" Visible="false" OnClick="addNew_Click" />

                                        <asp:Label ID="lblThumb" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblpdf" runat="server" Visible="false"></asp:Label>
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
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex">
                            <h4 class="card-title mb-0 flex-grow-1">Manage Brochures </h4>
                        </div>
                        <div class="card-body">
                            <div class="card-body">
                                <table id="alternative-pagination" class="table nowrap dt-responsive align-middle table-striped table-bordered myTable" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Title</th>
                                            <th>Image</th>
                                            <th>PDF</th>
                                            <th>AddedOn</th>
                                            <th class="text-center">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                          <%=strBrochures %>
                                    </tbody>

                                </table>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="assets/js/pages/manage-brochures.js"></script>
</asp:Content>

