<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="add-products-stories.aspx.cs" Inherits="Admin_add_products_stories" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="sc1" runat="server"></asp:ToolkitScriptManager>
    <div class="page-content">
        <div class="container-fluid">
            <!-- start page title -->
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0"><%=Request.QueryString["id"] == null ? "Add" : "Update" %> Product Stories</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Dashboard</a></li>
                                <li class="breadcrumb-item active"><%=Request.QueryString["id"] == null ? "Add" : "Update" %> Product Stories</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="card-title mb-0"><%=Request.QueryString["id"] == null ? "Add" : "Update" %> Product Stories</h5>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-4 mb-3">
                                    <label>Title<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2 txtTitle" ID="txtTitle" />
                                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtTitle" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4 mb-3 d-none">
                                    <label>Url<sup>*</sup></label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2 txtUrl" ID="txtUrl" />
                                </div>

                                <div class="col-md-4 mb-3">
                                    <label class="form-label">Youtube Link</label>
                                    <asp:TextBox runat="server" MaxLength="50" class="form-control mb-2 mr-sm-2" ID="txtLink" />
                                </div>
                                <div class="col-md-4 mb-3">
                                    <label>Featured ?</label>
                                    <asp:CheckBox CssClass="form-control" ID="chbFeatured" runat="server" />
                                    <span></span>
                                </div>
                                <div class="col-md-12 mb-3">
                                    <label>Full Description</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" class="form-control mb-2 mr-sm-2 summernote" ID="txtDesc" Placeholder="Full Description ....." />
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDesc" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <label>Thumbnail Image<sup class="text-danger">*</sup></label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control"></asp:FileUpload>
                                    <small style="color: red;">.png, .jpeg, .jpg, .gif, .webp formats are required, Image Size Should be 800*500 px</small><br />
                                    <%=strImage %>
                                </div>

                            </div>
                            <!-- end card body -->
                            <div class="row">
                                <div class="text-start mb-2">
                                    <p style="color: red; font-weight: bold;">Note : <sup>*</sup> are required fields</p>
                                </div>
                                <div class="text-start mt-1">
                                    <div>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" CssClass="btn btn-primary waves-effect waves-light " OnClientClick="tinyMCE.triggerSave(false,true);" OnClick="btnSave_Click" />
                                        <asp:Label ID="lblImage" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <!-- end col -->
                        </div>
                    </div>
                </div>
            </div>


        </div>
    </div>
    <script>
        $(document).ready(function () {
            $(".txtTitle").change(function () {
                $(".txtUrl").val($(".txtTitle").val().toLowerCase().replace(/\./g, '').replace(/\//g, '').replace(/\&/g, 'and').replace(/\\/g, '').replace(/\*/g, '').replace(/\?/g, '').replace(/\~/g, '').replace(/\ /g, '-'));
            });
        });
    </script>
</asp:Content>

