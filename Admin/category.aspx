<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="category.aspx.cs" Inherits="Admin_create_category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0"><%=Request.QueryString["id"]==null?"Add":"Update" %> Enquiry Product Category</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="dashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Categories</a></li>
                                <li class="breadcrumb-item active"><%=Request.QueryString["id"]==null?"Add":"Update" %> Enquiry Product Category</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <asp:Label runat="server" ID="lblStatus" Visible="false"></asp:Label>
                    <div class="card">
                        <div class="card-header align-items-center d-flex">
                            <h4 class="card-title mb-0 flex-grow-1"><%=Request.QueryString["id"]==null?"Add":"Update" %> Enquiry Product Category</h4>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-4 mb-2">
                                    <label>Category<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2 txtName" ID="txtCategory" />
                                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtCategory" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter a valid Name." ValidationExpression="^[a-zA-Z ]*$" ControlToValidate="txtCategory" ValidationGroup="Contact" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers" ValidChars="-" TargetControlID="txtCategory"></asp:FilteredTextBoxExtender>--%>
                                </div>
                                <div class="col-lg-4 mb-2">
                                    <label>Category Url<sup>*</sup></label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2 txtURL" ID="txtURL" />
                                    <asp:RequiredFieldValidator ID="reqProductUrl" runat="server" ControlToValidate="txtURL" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter a valid Name." ValidationExpression="^[a-zA-Z ]*$" ControlToValidate="txtUrl" ValidationGroup="Contact" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                </div>

                                <div class="col-lg-4 mb-2">
                                    <label>Page Title</label>
                                    <asp:TextBox runat="server" data-id="Title" class="form-control mb-2 mr-sm-2 textcount1" ID="txtPageTitle" />
                                </div>
                                <div class="col-lg-6 mb-2">
                                    <label>Meta Desc</label>
                                    <asp:TextBox runat="server" data-id="MetaDesc" TextMode="MultiLine" class="form-control mb-2 mr-sm-2 textcount1" ID="txtMetaDesc" />
                                </div>
                                <div class="col-lg-6">
                                    <label>Meta Keys</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" class="form-control mb-2 mr-sm-2" ID="txtMetaKeys" />
                                </div>


                                <div class="col-lg-12 mb-2 d-none">
                                    <label>Short Description<sup>*</sup></label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" class="form-control mb-2 mr-sm-2" ID="txtShortDesc" />

                                </div>
                                <div class="col-lg-12 mb-2">
                                    <label>Description<sup>*</sup></label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" class="form-control mb-2 mr-sm-2 summernote" Style="height: 100px !important;" ID="txtCategoryDesc" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCategoryDesc" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-lg-4 mb-2 d-none">
                                    <label>Category Image<sup>*</sup></label>
                                    <asp:FileUpload CssClass="form-control" ID="FileUpload1" runat="server" />
                                    <small style="color: red;">.png, .jpeg, .jpg formats to be uploaded and size should be 300*300 px</small>
                                    <br />
                                    <br />
                                    <%=strCatImage %>
                                </div>
                                <div class="col-lg-3 mb-2">
                                    <label>Display Home ?</label>
                                    <asp:CheckBox CssClass="form-control" ID="chbDispHome" runat="server" />
                                </div>
                                <div class="col-lg-3 mb-2">
                                    <label>Display Order</label>
                                    <asp:TextBox ID="txtDisplayOrder" MaxLength="5" runat="server" CssClass="form-control mb-2 mr-sm-2 numberOnlyInput" onkeypress="return isNumber(event)"></asp:TextBox>
                                </div>
                                <div class="col-lg-8 mb-2">
                                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary waves-effect waves-light m-t-25" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />
                                    <asp:Button runat="server" ID="btnAddCategory" CssClass="btn btn-outline-danger waves-effect waves-light m-t-25" Text="Clear All" Visible="false" OnClick="btnAddCategory_Click" />
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
    <script>
        $(".txtName").change(function () {
            $(".txtURL").val($(".txtName").val().toLowerCase().replace(/\./g, '').replace(/\//g, '').replace(/\\/g, '').replace(/\*/g, '').replace(/\?/g, '').replace(/\~/g, '').replace(/\ /g, '-'));
        });
    </script>
    <script src="assets/js/pages/category.js"></script>
</asp:Content>

