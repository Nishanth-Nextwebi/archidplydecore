<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="add-products.aspx.cs" Inherits="Admin_assets_add_products" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #left-defaults {
            padding-left: 0px;
        }

            #left-defaults .maindiv {
                border: 1px solid #ddd;
                border-radius: 5px;
                margin: 15px;
                padding: 15px;
            }


        #GalleryModal .card {
            box-shadow: unset;
        }

        .ImageUploadBox {
            border: 1px solid #ddd;
            border-radius: 10px;
            padding: 15px;
            position: sticky;
            top: 30px;
        }

        .stickyGalleryOrder {
            display: flex;
            align-items: center;
            justify-content: space-between;
            position: sticky;
            top: 0px;
            background: #ffffff;
            z-index: 999;
        }

        #GalleryModal .modal-body {
            padding-top: 0px;
        }

        .error {
            color: red;
        }
    </style>
    <!-- dropzone css -->
    <link rel="stylesheet" href="assets/libs/dropzone/dropzone.css" type="text/css" />
    <link href="assets/libs/dragula/dragula.min.css" rel="stylesheet" />
    <link href="assets/libs/dragula/example.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="sc1" runat="server"></asp:ToolkitScriptManager>
    <div class="page-content">
        <div class="container-fluid">

            <!-- start page title -->
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0"><%=Request.QueryString["id"]==null?"Add":"Update" %> Product</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="dashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Products</a></li>
                                <li class="breadcrumb-item active"><%=Request.QueryString["id"]==null?"Add":"Update" %> Product</li>
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
                            <div class="flex-grow-1 oveflow-hidden">
                                <ul class="nav justify-content-start nav-tabs-custom rounded card-header-tabs border-bottom-0" role="tablist">
                                    <li id="GeneralTab" runat="server" class="nav-item" role="presentation">
                                        <a class="nav-link active GeneralTab" data-bs-toggle="tab" href="#tabsNavigationSimple1" role="tab" aria-selected="true">General</a>
                                    </li>
                                    <li id="idTabPrices" runat="server" visible="false" class="nav-item" role="presentation">
                                        <a class="nav-link idTabPrices" data-id="<%=strCategory%>" data-bs-toggle="tab" href="#tabsNavigationSimple2" role="tab" aria-selected="false" tabindex="-1">Price</a>
                                    </li>
                                    <li id="idTabSeo" runat="server" visible="false" class="nav-item" role="presentation">
                                        <a class="nav-link idTabSeo" data-bs-toggle="tab" href="#tabsNavigationSimple3" role="tab" aria-selected="false" tabindex="-1">SEO</a>
                                    </li>
                                    <li id="idTabGallery" runat="server" visible="false" class="nav-item" role="presentation">
                                        <a class="nav-link idTabGallery" data-bs-toggle="tab" href="#tabsNavigationSimple4" role="tab" aria-selected="false" tabindex="-1">Gallery</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tabsNavigationSimple1">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server"
                                        UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <asp:Label ID="lblStatus" runat="server" Visible="false" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-4 mb-3 d-none">
                                                    <label>Category</label>
                                                    <asp:DropDownList runat="server" class="form-control mb-2 mr-sm-2 basic SubCategory" ID="ddlCategory">
                                                        <asp:ListItem Value="">Select Category</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCategory" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="col-lg-4 mb-3">
                                                    <label class="form-label" for="project-title-input">Category <sup>*</sup></label>
                                                    <asp:DropDownList runat="server" class="form-control mb-2 mr-sm-2" ID="ddlSubCategory">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="ddlSubCategory" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
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
                                                    <asp:RequiredFieldValidator ID="reqProductName" runat="server" ControlToValidate="txtProdName" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-lg-4 mb-3">
                                                    <label>Product Url<sup>*</sup></label>
                                                    <asp:TextBox runat="server" MaxLength="200" class="form-control mb-2 mr-sm-2 txtURL" ID="txtURL" />
                                                    <asp:RequiredFieldValidator ID="reqProductUrl" runat="server" ControlToValidate="txtURL" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter a valid Name." ValidationExpression="^[a-zA-Z ]*$" ControlToValidate="txtUrl" ValidationGroup="Contact" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers" ValidChars="-" TargetControlID="txtUrl"></asp:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-lg-4 mb-3 d-none">
                                                    <label>Tag</label>
                                                    <asp:DropDownList ID="drpTag" runat="server" CssClass="form-control mb-2 mr-sm-2"></asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="drpTag" InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="col-lg-4 mb-3 d-none">
                                                    <label>SKU Code</label>
                                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2" ID="txtSKUCode" />
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSKUCode" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12 mb-3">
                                                    <label>Short Description<sup>*</sup></label>
                                                    <asp:TextBox runat="server" TextMode="MultiLine" MaxLength="700" Rows="4" class="form-control mb-2 mr-sm-2" ID="txtShort" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtShort" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="col-lg-12 mb-3">
                                                    <label>Specifications<sup>*</sup></label>
                                                    <asp:TextBox runat="server"  TextMode="MultiLine" class="form-control mb-2 mr-sm-2 summernote" ID="txtIngr" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtIngr" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>

                                                </div>


                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12 mb-3">
                                                    <label>Product Details<sup>*</sup></label>
                                                    <asp:TextBox runat="server" TextMode="MultiLine" class="form-control mb-2 mr-sm-2 summernote" Style="height: 100px !important;" ID="txtProductDesc" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProductDesc" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="col-lg-4 mb-3 d-none">
                                                    <label>Place of Origin</label>
                                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2 acceptOnlyAlpha" ID="txtOrigin" />
                                                </div>
                                                <div class="col-lg-4 mb-3 d-none">
                                                    <label>Review Keyword</label>
                                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2" ID="txtReview" />
                                                </div>
                                                <div class="col-lg-3 mb-3">
                                                    <label>Item Number</label>
                                                    <asp:TextBox runat="server" ID="txtItemNum" MaxLength="100" CssClass="form-control numOnly" />
                                                </div>
                                                <div class="col-lg-3 mb-3">
                                                    <label>Product Order</label>
                                                    <asp:TextBox runat="server" ID="txtProductOrder" MaxLength="100" CssClass="form-control numOnly" />
                                                </div>
                                                <div class="col-lg-2 mb-3">
                                                    <label>In Stock</label>
                                                    <asp:CheckBox CssClass="form-control" ID="chkInStock" runat="server" />
                                                    <span></span>
                                                </div>
                                                <div class="col-lg-2 mb-3">
                                                    <label>Display Home ?</label>
                                                    <asp:CheckBox CssClass="form-control" ID="chbDispHome" runat="server" />
                                                    <span></span>
                                                </div>
                                                <div class="col-lg-2 mb-3 d-none">
                                                    <label>Best Seller</label>
                                                    <asp:CheckBox CssClass="form-control" ID="chkBestSeller" runat="server" />
                                                </div>
                                                <div class="col-lg-2 mb-3">
                                                    <label>Featured</label>
                                                    <asp:CheckBox CssClass="form-control" ID="chkFeatured" runat="server" />
                                                </div>
                                                <div class="col-lg-2 mb-3 d-none">
                                                    <label>Enquiry</label>
                                                    <asp:CheckBox CssClass="form-control" ID="chkEnquiry" runat="server" />
                                                </div>
                                                <div class="col-lg-2 mb-3 d-none">
                                                    ">
                                                    <label>Shop</label>
                                                    <asp:CheckBox CssClass="form-control" ID="chkShop" runat="server" />
                                                </div>
                                                <%--<div class="col-lg-2">
                                    <label>Display Order<small class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='Entered display order will be assigned for the product'><i style="color: blue;">info</i></small></label>
                                    <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="form-control mb-2 mr-sm-2 numberOnlyInput"></asp:TextBox>
                                </div>--%>
                                            </div>
                                            <div class="row mb-3 mb-3">
                                                <div class="col-lg-3 mt-3 d-none">
                                                    <label>Expected Delivery Days</label>
                                                    <asp:TextBox runat="server" MaxLength="100" placeholder="Enter No of Days" class="form-control mb-2 mr-sm-2 onlyNum" ID="txtDelDate" />
                                                </div>
                                                <div class="col-lg-4 mb-3 mt-3">
                                                    <label>Product Image<sup>*</sup></label>
                                                    <asp:FileUpload CssClass="form-control" ID="FileUpload1" runat="server" />
                                                    <small style="color: red;">.png, .jpeg, .jpg, .gif, .webp formats to be uploaded and size should be 500x500 px</small>
                                                    <br />
                                                    <br />
                                                    <%=strBannerImage %>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" ValidationGroup="Save" />
                                                    <asp:Button runat="server" ID="addProduct" class="btn btn-outline-danger waves-effect waves-light" Text="Clear All" Visible="false" OnClick="addProduct_Click" />
                                                    <asp:Label ID="lblThumb" runat="server" Visible="false"></asp:Label>
                                                    <input type="hidden" id="idPid" runat="server" />
                                                </div>
                                                <div class="col-lg-12 mt-2">
                                                    <p style="color: red; font-weight: bold;">Note : <sup>*</sup> are required fields</p>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSave" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane" id="tabsNavigationSimple2">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server"
                                        UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <label id="lblProdPrices" style="width: 100%;" class="d-none"></label>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-lg-3">
                                                    <label>Product Size<sup>*</sup></label>
                                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txtSize" />
                                                    <asp:RequiredFieldValidator ID="rfvSize" runat="server" Style="color: Red;" ControlToValidate="txtSize" ValidationGroup="Price" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-lg-3">
                                                    <label>Product Thickness<sup>*</sup></label>
                                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txt_thickness" />
                                                    <asp:RequiredFieldValidator ID="rfvthickness" runat="server" Style="color: Red;" ControlToValidate="txt_thickness" ValidationGroup="Price" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="col-lg-3">
                                                    <label>MRP<sup>*</sup></label>
                                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2 decimalInput" ID="txtactual" />
                                                    <asp:RequiredFieldValidator ID="reqtxtactual" runat="server" Style="color: Red;" ControlToValidate="txtactual" ValidationGroup="Price" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-lg-3">
                                                    <label>Offer Price<sup>*</sup></label>
                                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2 decimalInput" ID="txtdiscount" />
                                                    <asp:RequiredFieldValidator ID="reqtxtdiscount" runat="server" Style="color: Red;" ControlToValidate="txtdiscount" ValidationGroup="Price" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-lg-12">
                                                    <a id="idBtnProdPrices" class="btn btn-primary" data-url="<%=strCategory %>">Save</a>
                                                    <a id="cancel" class="btn btn-outline-danger waves-effect waves-light" style="display: none;">Cancel</a>
                                                    <input type="hidden" id="hdPrId" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row mt-2">
                                                <div class="col-lg-12 mt-2">
                                                    <p style="color: red; font-weight: bold;">Note : <sup>*</sup> are required fields</p>
                                                </div>
                                            </div>
                                            <hr />

                                            <div class="row">
                                                <div class="col-lg-12 layout-top-spacing">
                                                    <div class="statbox widget box box-shadow">
                                                        <div class="widget-header">
                                                            <div class="row">
                                                                <div class="col-xl-12 col-md-12 col-sm-12 col-12">
                                                                    <h4>Manage Prices</h4>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="widget-content widget-content-area">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="table-responsive mb-4 mt-4" id="PriceTable">
                                                                        <%--<table class="table table-hover myTable" style="width: 100%;">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Product Size</th>
                                                                    <th>Actual Price</th>
                                                                    <th>Discount Price</th>
                                                                    <th>Updated On</th>
                                                                    <th class="text-center">Action</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody id="tbdy">
                                                            </tbody>
                                                            <tfoot>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Product Size</th>
                                                                    <th>Actual Price</th>
                                                                    <th>Discount Price</th>
                                                                    <th>Updated On</th>
                                                                    <th class="text-center">Action</th>
                                                                </tr>
                                                            </tfoot>
                                                        </table>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane" id="tabsNavigationSimple3">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server"
                                        UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <asp:Label ID="lblProdSeo" runat="server" TextMode="MultiLine" Visible="false" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6 mb-3">
                                                    <label>Page Title</label>
                                                    <asp:TextBox runat="server" TextMode="MultiLine" class="form-control mb-2 mr-sm-2 textcount1" ID="txtPTitle" />
                                                </div>
                                                <div class="col-lg-6 mb-3">
                                                    <label>Meta Desc</label>
                                                    <asp:TextBox runat="server" data-id="MetaDesc" TextMode="MultiLine" class="form-control mb-2 mr-sm-2 textcount1" ID="txtMetaDesc" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6 mb-3">
                                                    <label>Meta Keys</label>
                                                    <asp:TextBox runat="server" TextMode="MultiLine" class="form-control mb-2 mr-sm-2 textcount1" ID="txtMKeys" />
                                                </div>
                                            </div>
                                            <div class="row mt-2">
                                                <div class="col-lg-5">
                                                    <asp:Button runat="server" ID="btnSeo" CssClass="btn btn-primary" Text="Update" OnClick="btnSeo_Click" />
                                                </div>
                                                <div class="col-lg-12 mt-2">
                                                    <p style="color: red; font-weight: bold;">Note : <sup>*</sup> are required fields</p>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnSeo" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane" id="tabsNavigationSimple4" role="tabpanel">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="row d-flex">
                                                <div class="col-lg-8">
                                                    <div class="card">
                                                        <div class="card-header stickyGalleryOrder">
                                                            <h4 class="card-title mb-0">Gallery Images</h4>
                                                            <asp:Label ID="lblProdImg" runat="server" Visible="false" Width="100%"></asp:Label>
                                                            <input type="button" style="margin-top: 10px;" value="Update Image Order" id="UpdateImgOrder" class="btn btn-primary">
                                                        </div>
                                                        <div class="card-body">
                                                            <ul id="left-defaults" class="row dragula sortablev ImagesLoaded">
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-lg-4">
                                                    <div class="card">
                                                        <div class="ImageUploadBox">
                                                            <div class="card-header">
                                                                <h4 class="card-title mb-0">Add Images</h4>
                                                            </div>
                                                            <!-- end card header -->
                                                            <div class="card-body">
                                                                <p class="text-muted">You can add preview images for the package here</p>

                                                                <div class="dropzone">
                                                                    <div class="fallback">
                                                                        <input name="file" type="file" multiple="multiple">
                                                                    </div>
                                                                    <div class="dz-message needsclick">
                                                                        <div class="mb-3">
                                                                            <i class="display-4 text-muted ri-upload-cloud-2-fill"></i>
                                                                        </div>

                                                                        <h5>Drop files here or click to upload.</h5>
                                                                    </div>
                                                                </div>
                                                                <small class="d-block link-danger mt-2">.png, .jpeg, .jpg, .gif, .webp formats are required, Image Size Should be 1000*1000 px</small>

                                                                <ul class="list-unstyled mb-0" id="dropzone-preview">
                                                                    <li class="mt-2" id="dropzone-preview-list">
                                                                        <!-- This is used as the file preview template -->
                                                                        <div class="border rounded">
                                                                            <div class="d-flex p-2">
                                                                                <div class="flex-shrink-0 me-3">
                                                                                    <div class="avatar-sm bg-light rounded">
                                                                                        <img data-dz-thumbnail class="img-fluid rounded d-block" src="#" alt="Dropzone-Image" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="flex-grow-1">
                                                                                    <div class="pt-1">
                                                                                        <h5 class="fs-14 mb-1" data-dz-name>&nbsp;</h5>
                                                                                        <p class="fs-13 text-muted mb-0" data-dz-size></p>
                                                                                        <strong class="error text-danger" data-dz-errormessage></strong>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="flex-shrink-0 ms-3">
                                                                                    <button data-dz-remove class="btn btn-sm btn-danger">Delete</button>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </li>
                                                                </ul>
                                                                <div class="text-end">
                                                                    <button type="button" id="AddToGallery" data-pid="" class="btn btn-lg btn-primary">Upload</button>
                                                                </div>
                                                                <!-- end dropzon-preview -->
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <!-- dropzone min -->
    <script src="assets/libs/dropzone/dropzone-min.js"></script>
    <script src="assets/js/form-file-upload.init.js"></script>
    <script src="assets/libs/dragula/dragula.min.js"></script>
    <script src="assets/libs/dragula/custom-dragula.js"></script>
    <script src="assets/js/pages/add-products.js"></script>
</asp:Content>
