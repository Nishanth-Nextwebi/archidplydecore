<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="add-brand.aspx.cs" Inherits="add_brand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Brand</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Categories</a></li>
                                <li class="breadcrumb-item active"><%=Request.QueryString["id"] == null ? "Add" : "Update" %> Brand</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-8">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="card-title mb-0"><%=Request.QueryString["id"] == null ? "Add" : "Update" %> Brand</h5>
                        </div>
                        <div class="card-body">
                            <div class="row">

                                <div class="col-lg-6 mb-3">
                                    <label class="form-label" for="project-title-input">Brand<sup>*</sup></label>
                                    <asp:TextBox runat="server" placeHolder="Enter Brand" MaxLength="100" class="form-control mb-2 mr-sm-2 txtProdName" ID="txtBrand" />
                                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtBrand" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-lg-6 mb-3">
                                    <label class="form-label" for="project-title-input">Brand Url <sup>*</sup></label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2 txtURL" placeHolder="Auto Generated" ID="txtURL" />
                                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtURL" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-lg-12 mb-3">
                                    <label class="form-label" for="project-title-input">Short Description</label>
                                    <asp:TextBox runat="server" MaxLength="500" PlaceHolder="Enter Short Description" TextMode="MultiLine" class="form-control mb-2 mr-sm-2" Rows="2" ID="txtShortDesc" />
                                </div>
                                <div class="col-lg-6 mb-3">
                                    <label class="form-label">Thumbnail Image</label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" class="form-control mb-2 mr-sm-2" />
                                    <small style="color: red;">Image format .png, .jpeg, .jpg, .webp with 300 × 300 px</small>
                                    <br />
                                    <br />
                                    <%=strCatImage %>
                                </div>
                                <div class="col-lg-3 mb-3">
                                    <label>Display Order</label>
                                    <asp:TextBox ID="txtDisplayOrder" MaxLength="5" runat="server" CssClass="form-control mb-2 mr-sm-2 numberOnlyInput" onkeypress="return isNumber(event)"></asp:TextBox>
                                </div>

                                <div class="col-lg-3 mb-3">
                                    <br />
                                    <br />
                                    <div class="form-check form-check-success">
                                        <input class="form-check-input" type="checkbox" id="chkDisplayHome" runat="server">
                                        <label class="form-check-label" for="<%=chkDisplayHome.ClientID %>">Display Home ?</label>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12 mb-3">
                                    <label>Full Desc </label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" class="form-control mb-2 mr-sm-2 summernote" Style="height: 100px !important;" ID="txtBrandDesc" />
                                    <%--<asp:RequiredFieldValidator ID="rfv6" runat="server" ControlToValidate="txtBrandDesc" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="card-title mb-0">SEO</h5>
                        </div>
                        <div class="card-body">
                            <div class="col-lg-12 mb-3">
                                <label>Page Title</label>
                                <asp:TextBox runat="server" data-id="Title" TextMode="MultiLine" class="form-control mb-2 mr-sm-2 textcount1" ID="txtPageTitle" />
                            </div>
                            <div class="col-lg-12 mb-3">
                                <label>Meta Keys</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" class="form-control mb-2 mr-sm-2" ID="txtMetaKeys" />
                            </div>
                            <div class="col-lg-12 mb-3">
                                <label>Meta Desc</label>
                                <asp:TextBox runat="server" data-id="MetaDesc" TextMode="MultiLine" class="form-control mb-2 mr-sm-2 textcount1" ID="txtMetaDesc" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 mb-3">
                    <div>
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" CssClass="btn btn-primary waves-effect waves-light" OnClick="btnSave_Click" />
                        <asp:Button ID="btnNew" runat="server" Text="Add New Brand" CssClass="btn btn-danger waves-effect waves-light" Visible="false" OnClick="btnNew_Click" />
                        <asp:Label ID="lblThumb" runat="server" Visible="false"></asp:Label>
                    </div>
                    <div class="text-start mt-2">
                        <p style="color: red; font-weight: bold;">Note : <sup>*</sup> are required fields</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="assets/js/pages/add-brand.js"></script>
</asp:Content>

