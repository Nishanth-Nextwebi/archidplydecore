<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="add-job.aspx.cs" Inherits="Admin_add_job" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Add Job</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Careers</a></li>
                                <li class="breadcrumb-item active"><%=Request.QueryString["id"] !=null?"Update":"Add" %> Job</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="card-title"><%=Request.QueryString["id"] !=null?"Update":"Add" %> Job Details</h5>
                        </div>
                        <div class="card-body">
                            <asp:Label ID="lblStatus" runat="server" Visible="false" Width="100%"></asp:Label>
                            <div class="row">
                                <div class="col-lg-4 mb-2">
                                    <label>Job Title<sup>*</sup></label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2 txtName" ID="txtTitle" placeholder="Job Title" MaxLength="100" />
                                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtTitle" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4 mb-2">
                                    <label>Job URL<sup>*</sup></label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2 txtUrl" ID="txtURL" placeholder="Job Url" />
                                    <asp:RequiredFieldValidator ID="rfv16" runat="server" ControlToValidate="txtURL" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4 mb-2">
                                    <label>Posted On<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2 datepicker" ID="txtDate" placeholder="Posted On" />
                                    <asp:RequiredFieldValidator ID="rfv22" runat="server" ControlToValidate="txtDate" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-lg-3 mb-2">
                                    <label>Location<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2 acceptOnlyAlpha" ID="txtLocation" placeholder="Location" />
                                    <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtLocation" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-3 mb-2">
                                    <label>Salary<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2 onlyNum" ID="txtSalary" placeholder="Salary" />
                                    <asp:RequiredFieldValidator ID="rfv11" runat="server" ControlToValidate="txtSalary" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-3 mb-2">
                                    <label>No of Openings<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2 onlyNum" ID="txtNoOfPositions" placeholder="No Of Positions" />
                                    <asp:RequiredFieldValidator ID="rfv13" runat="server" ControlToValidate="txtNoOfPositions" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-lg-3 mb-2">
                                    <label>Experience<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2" ID="txtExperience" placeholder="Experience" />
                                    <asp:RequiredFieldValidator ID="rfv12" runat="server" ControlToValidate="txtExperience" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-6 mb-2">
                                    <label>Skills<sup>*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="250" TextMode="MultiLine" class="form-control mb-2 mr-sm-2" Rows="3" ID="txtSkill" placeholder="Skills" />
                                    <asp:RequiredFieldValidator ID="rfv14" runat="server" ControlToValidate="txtSkill" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-lg-6 mb-2">
                                    <label>Short Description<sup>*</sup></label>
                                    <asp:TextBox ID="txtShortDesc" MaxLength="250" TextMode="MultiLine" class="form-control mb-2 mr-sm-2" Rows="3" runat="server" placeholder="Short Description"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtShortDesc" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-lg-12">
                                    <label>Full Description<sup>*</sup></label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" MaxLength="250" class="form-control mb-2 mr-sm-2 summernote" ID="txtDesc" Placeholder="Full Description ....." />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDesc" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 mb-3">
                  <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary waves-effect waves-light m-t-25" Text="Save" OnClick="btnSave_Click" OnClientClick="tinyMCE.triggerSave(false,true);" ValidationGroup="Save" />
                    <asp:Button runat="server" ID="addNew" CssClass="btn btn-outline-danger waves-effect waves-light m-t-25" Visible="false" Text="Clear All" OnClick="btnNew_Click" />
                  
                    <asp:Label ID="lblThumb" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblThumb1" runat="server" Visible="false"></asp:Label>
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

