<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="write-blog.aspx.cs" Inherits="Admin_write_blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Blogs</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Blogs</a></li>
                                <li class="breadcrumb-item active"><%=Request.QueryString["id"] !=null?"Update":"Add" %> Blogs</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-8">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="card-title"><%=Request.QueryString["id"] !=null?"Update":"Add" %> Blog Details</h5>
                        </div>
                        <div class="card-body">
                          <asp:Label ID="lblStatus" runat="server" Visible="false" Width="100%"></asp:Label>
                            <div class="row mb-2">
                                <div class="col-lg-6">
                                    <label">Blog Title<sup>*</sup></label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2 txtName" ID="txtBlogName" placeholder="Blog Title" maxlength="100" />
                                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtBlogName" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-6">
                                    <label>Blog URL<sup>*</sup></label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2 txtUrl" ID="txtURL" placeholder="Blog Url" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtURL" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-6">
                                    <label>Posted By<sup>*</sup></label>
                                    <asp:TextBox runat="server"  MaxLength="100" class="form-control mb-2 mr-sm-2 onlyAlpha" ID="txtPostedBy" placeholder="Posted By" />
                                    <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtPostedBy" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-6">
                                    <label>Posted On<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2  datepickerCurrent datepicker" ID="txtDate" placeholder="Posted On" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDate" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                 <div class="col-lg-12 mb-2">
                                    <label>Short Description<sup>*</sup></label>
                                    <asp:TextBox ID="txtShortDesc" maxlength="250" TextMode="MultiLine" class="form-control mb-2 mr-sm-2" Rows="3" runat="server" placeholder="Short Description"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtShortDesc" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-lg-12">
                                    <label>Full Description<sup>*</sup></label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" maxlength="250" class="form-control mb-2 mr-sm-2 summernote" ID="txtDesc" Placeholder="Full Description ....." />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDesc" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="card">
                        <div class="card-header">
                            <h5 class=" card-title">Add Seo Details</h5>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <label class="text-muted">Page Title</label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2 textcount1" ID="txtPageTitle" placeholder="Page Title" />
                                </div>
                                <div class="col-lg-12">
                                    <label class="text-muted">Meta Keys</label>
                                    <asp:TextBox ID="txtMetaKeys" class="form-control mb-2 mr-sm-2 textcount1" runat="server" placeholder="Meta Keys" maxlength="100"></asp:TextBox>
                                </div>
                                <div class="col-lg-12">
                                    <label class="text-muted">Meta Description</label>
                                    <asp:TextBox ID="txtMetaDesc" maxlength="250" TextMode="MultiLine" class="form-control mb-2 mr-sm-2" Rows="3" runat="server" placeholder="Meta Description"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <h5 class="card-title">Add Image Details</h5>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12 mb-3">
                                    <label class="text-muted">Thumb Image<sup class="text-danger">*</sup></label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Maxmimum 1 MB file size" CssClass="form-control"></asp:FileUpload>
                                    <small class="text-danger">.png, .jpeg, .jpg, .gif, .webp formats are required, Image Size Should be 1000 × 800 px</small><br />
                                    <br/>
                                    <asp:RequiredFieldValidator ID="reqFileUpload1" runat="server" ControlToValidate="FileUpload1" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Please select file to upload"></asp:RequiredFieldValidator>
                                     <%=strThumbImage %>
                                </div>
                                <div class="col-lg-12 mb-3">
                                    <label class="text-muted">Blog Image<sup class="text-danger">*</sup></label>
                                    <asp:FileUpload ID="FileUpload2" runat="server" ToolTip="Maxmimum 1 MB file size" CssClass="form-control"></asp:FileUpload>
                                    <small class="text-danger">.png, .jpeg, .jpg, .gif, .webp formats are required, Image Size Should be 1200 × 900 px</small><br />
                                   <asp:RequiredFieldValidator ID="reqFileUpload2" runat="server" ControlToValidate="FileUpload2" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Please select file to upload"></asp:RequiredFieldValidator>
                                     <%=strThumbImage2 %>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 mb-3">
                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary waves-effect waves-light m-t-25" Text="Save" OnClick="btnSave_Click" OnClientClick="tinyMCE.triggerSave(false,true);" ValidationGroup="Save" />
                    <asp:Button runat="server" ID="addBlog" CssClass="btn btn-outline-danger waves-effect waves-light m-t-25" Visible="false" Text="Clear All" OnClick="btnNew_Click" />
                  <asp:Label ID="lblThumb" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblThumb1" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
                                            <div class="row">
    <div class="col-lg-12 mt-2">
        <p style="color: red; font-weight: bold;">Note : <sup>*</sup> are required fields</p>
    </div>
</div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $(".txtName").change(function () {
                $(".txtUrl").val($(".txtName").val().toLowerCase().replace(/\./g, '').replace(/\//g, '').replace(/\&/g, 'and').replace(/\\/g, '').replace(/\*/g, '').replace(/\?/g, '').replace(/\~/g, '').replace(/\ /g, '-'));
            });
            var f1 = flatpickr(document.getElementsByClassName('datepickerCurrent'), {
                minDate: "today",
                dateFormat: "d/M/Y",
            });
            $(".textcount1").on('keyup', function (event) {
                var elem = $(this);
                var tps = elem.attr("data-id");
                var len = elem.val().length;
                elem.siblings('span').text("Character count : " + len);
                if (tps === "Title") {
                    if (len > 60) {
                        elem.siblings('span').css("color", "red");
                    }
                    else {
                        elem.siblings('span').css("color", "green");
                    }
                }
                else if (tps === "MetaDesc") {
                    if (len > 160) {
                        elem.siblings('span').css("color", "red");
                    }
                    else {
                        elem.siblings('span').css("color", "green");
                    }
                }
            });
        });
    </script>
</asp:Content>


