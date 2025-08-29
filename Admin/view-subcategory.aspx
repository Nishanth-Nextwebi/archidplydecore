<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="view-subcategory.aspx.cs" Inherits="Admin_view_subcategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">SubCategory</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="dashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">View SubCategory</li>
                            </ol>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex">
                            <h4 class="card-title mb-0 flex-grow-1">Manage SubCategory</h4>
                        </div>
                        <div class="card-body">
                            <table id="alternative-pagination" class="table nowrap dt-responsive align-middle table-striped table-bordered myTable" style="width: 100%;">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Image</th>
                                        <th>Category</th>
                                        <th>Sub Category</th>
                                        <th>Display Home</th>
                                        <th>Display Order</th>
                                        <th>Updated On</th>
                                        <th class="text-center">Action</th>
                                    </tr>
                                </thead>
                                <tbody id="tbdy">
                                    <%=strCategory %>
                                </tbody>

                            </table>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
     <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="assets/js/pages/subcategory.js"></script>
    <script>
        $(document).ready(function () {
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

