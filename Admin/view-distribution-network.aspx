<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="view-distribution-network.aspx.cs" Inherits="Admin_view_distribution_network" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Distribution Network Request</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/admin/dashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0)">Requests</a></li>
                                <li class="breadcrumb-item active">View Distribution Network Request</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="card">

                <div class="card-header">
                    <h5 class="card-title">Manage Distribution Network Requests</h5>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <table id="alternative-pagination" class="table  align-middle table-striped table-bordered myTable" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Name</th>
                                        <th>Email Id</th>
                                        <th>Phone Number</th>
                                        <th>City</th>
                                        <th>Added On</th>
                                        <th class="text-center">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                         <%=strRequests%>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
<script src="assets/js/pages/view-distribution-network.js"></script>
</asp:Content>