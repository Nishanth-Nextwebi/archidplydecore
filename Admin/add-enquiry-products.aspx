<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="add-enquiry-products.aspx.cs" Inherits="Admin_add_enquiry_products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0"><%=Request.QueryString["id"]==null?"Add":"Update" %> Enquiry Products</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="dashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Enquiry Products</a></li>
                                <li class="breadcrumb-item active"><%=Request.QueryString["id"]==null?"Add":"Update" %> Enquiry Products</li>
                            </ol>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex">
                            <h4 class="card-title mb-0 flex-grow-1"><%=Request.QueryString["id"]==null?"Add":"Update" %> Enquiry Products</h4>
                        </div>
                        <!-- end card header -->
                        <div class="card-body">

                            <div class="row mb-2">
                                <div class="col-lg-4 mb-3">
                                    <label>Category<sup>*</sup></label>
                                    <asp:DropDownList runat="server" class="form-control mb-2 mr-sm-2 basic SubCategory" ID="ddlCategory">
                                        <asp:ListItem Value="">Select Category</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCategory" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4 mb-3 d-none">
                                    <label class="form-label" for="project-title-input">Sub Category <sup>*</sup></label>
                                    <asp:DropDownList runat="server" class="form-control mb-2 mr-sm-2" ID="ddlSubCategory">
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="ddlSubCategory" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-lg-4 mb-3 d-none">
                                    <label class="form-label" for="project-title-input">Brands <sup>*</sup></label>
                                    <asp:DropDownList runat="server" class="form-control mb-2 mr-sm-2" ID="ddlBrand">
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlBrand" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-lg-4 mb-3">
                                    <label>Product Name<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="200" class="form-control mb-2 mr-sm-2 txtProdName" ID="txtProdName" />
                                    <asp:RequiredFieldValidator ID="rfv01" runat="server" ControlToValidate="txtProdName" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-lg-4 mb-3">
                                    <label>Product Url<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="200" class="form-control mb-2 mr-sm-2 txtURL" ID="txtURL" />
                                    <asp:RequiredFieldValidator ID="rfv02" runat="server" ControlToValidate="txtURL" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-lg-4 mb-3 d-none">
                                    <label>Tags</label>
                                    <asp:ListBox ID="drpTag" runat="server" CssClass="form-control lvTest  mb-2 mr-sm-2" SelectionMode="Multiple" MaxLength="100"></asp:ListBox>
                                </div>
                                <div class="col-lg-4 mb-3">
                                    <label>Features<sup>*</sup></label>
                                    <asp:ListBox ID="ddlFeatures" runat="server" CssClass="form-control lvTest  mb-2 mr-sm-2" SelectionMode="Multiple" MaxLength="100"></asp:ListBox>
                                    <asp:RequiredFieldValidator ID="rfv03" runat="server" ControlToValidate="ddlFeatures" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4 mb-3">
                                    <label>Item Number</label>
                                    <asp:TextBox runat="server" ID="txtItemNum" CssClass="form-control" />
                                </div>
                                <div class="col-lg-4 mb-3">
                                    <label>Page Title</label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2 textcount1" ID="txtPTitle" />
                                </div>
                                <div class="col-lg-6 mb-3">
                                    <label>Meta Keys</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" Style="height: 100px !important;" class="form-control mb-2 mr-sm-2 textcount1" ID="txtMKeys" />
                                </div>
                                <div class="col-lg-6 mb-3">
                                    <label>Meta Desc</label>
                                    <asp:TextBox runat="server" data-id="MetaDesc" TextMode="MultiLine" Style="height: 100px !important;" class="form-control mb-2 mr-sm-2 textcount1" ID="txtMetaDesc" />
                                </div>
                                <div class="col-lg-12 mb-3">
                                    <label>Product Description<sup>*</sup></label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" class="form-control mb-2 mr-sm-2 summernote" Style="height: 100px !important;" ID="txtShort" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtShort" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <%--<div class="col-lg-12 mb-3">
                                    <label>Features</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" class="form-control mb-2 mr-sm-2 summernote" Style="height: 100px !important;" ID="txtFeatures" />
                                </div>--%>
                                <%--<div class="col-lg-12 mb-3">
                                    <label>Full Description<sup>*</sup></label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" class="form-control mb-2 mr-sm-2 summernote" Style="height: 100px !important;" ID="txtProductDesc" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtProductDesc" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>--%>
                                <div class="col-lg-4 mb-3">
                                    <label>Product Image<sup>*</sup></label>
                                    <asp:FileUpload CssClass="form-control" ID="FileUpload1" runat="server" />
                                    <small style="color: red;">.png, .jpeg, .jpg, .gif, .webp formats to be uploaded and size should be 500x500 px</small>
                                    <br />
                                    <br />
                                    <%=strBannerImage %>
                                </div>

                                <div class="col-lg-3 mb-3">
                                    <label>Display Order</label>
                                    <asp:TextBox runat="server" ID="txtOrder" CssClass="form-control numberOnlyInput" />
                                </div>
                                <div class="col-lg-2 mb-3">
                                    <label>Display Home ?</label>
                                    <asp:CheckBox CssClass="form-control" ID="chbDispHome" runat="server" />
                                </div>
                                <%--<div class="col-lg-2 mb-3 d-none">
                                    <label>Featured</label>
                                    <asp:CheckBox CssClass="form-control" ID="chkFeatured" runat="server" />
                                </div>--%>
                            </div>
                            <div class="row mt-3">
                                <div class="col-lg-5">
                                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" ValidationGroup="Save" />
                                    <asp:Button runat="server" ID="addProduct" class="btn btn-outline-danger waves-effect waves-light" Text="Clear All" Visible="false" OnClick="addProduct_Click" />
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
        </div>
    </div>
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $(".txtProdName").change(function () {
                $(".txtURL").val($(".txtProdName").val().toLowerCase().replace(/\./g, '').replace(/\//g, '').replace(/\&/g, 'and').replace(/\\/g, '').replace(/\*/g, '').replace(/\?/g, '').replace(/\~/g, '').replace(/\ /g, '-'));
            });
            $(document).on("keypress", function (e) {
                if (e.shiftKey) {
                    return;
                }
                if (e.which == 13) { document.getElementById("<%=btnSave.ClientID %>").click(); }
            });
            $(document).ready(function () {
                $('.lstChks').fSelect();
                $(".fs-wrap").each(function () {
                    var wrap = $(this).width();
                    $(this).find(".fs-dropdown").css("width", wrap);
                });
            });
            $('.lvTest').fSelect();
        });
    </script>

</asp:Content>

