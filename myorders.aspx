<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="myorders.aspx.cs" Inherits="myorders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
       <style>
       .form-control{
           background:#fff !important;
           color:#000 !important;
       }
        @media (min-width: 320px) and (max-width: 991px) {
    table.table.table-hover.align-middle.table-nowrap.mb-0.table-borderless {
    width: 1000px;
}

 }
         .dashboard-wrapper table thead th{
             background:#f1f1f1 !important;
             color:#000 !important;
         }
         .dashboard-wrapper .table > :not(caption) > * > *{
             color:#000 !important;
         }
         .dashboard-wrapper .table > td> a{
            color:#000 !important;
         }
         
         td a{
 color:#000 !important;
         }
         .btn-dark{
             background-color:transparent !important;
         }
         a.btn.btn-primary.py-4.fs-13px.btn-xs.me-4 {
    background: #000 !important;
    color: #fff !important;
}
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main id="content" class="bg-body-tertiary-01 d-flex flex-column main-content">
        <div class="dashboard-page-content" data-animated-id="1">
            <div class="card mb-4 rounded-4 p-7">
                <div class="card-header bg-transparent px-0 pt-0 pb-7">
                    <div class="row align-items-center justify-content-between">
                        <div class="col-md-4 col-12 mr-auto mb-md-0 mb-6">
                            <h2 class="fs-4 mb-0 text-dark">My Orders History </h2>
                            <p class="mb-0 text-dark">View your past orders Below.</p>

                        </div>
                        <div class="col-md-8">
                            <div class="row justify-content-end flex-nowrap d-flex">
                                <div class="col-lg-4 col-md-6 col-12">
                                    <select class="form-select" ID="ddlStatus">
                                        <option value="">All</option>
                                        <option value="Initiated">Initiated</option>
                                        <option value="In-Process">In-Process</option>
                                        <option value="Dispatched">Dispatched</option>
                                        <option value="Delivered">Delivered</option>
                                        <option value="Cancelled">Cancelled</option>
                                    </select>
                                </div>
                                <div class="col-lg-4 col-md-6 col-6 d-none">
                                    <select class="form-select" id="ddlPageSize">
                                        <option value="10">Show 10</option>
                                        <option value="20">Show 20</option>
                                        <option value="30">Show 30</option>
                                        <option value="50">Show 50</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body px-0 pt-7 pb-0">
                    <div class="table-responsive">
                        <table class="table table-hover align-middle table-nowrap mb-0 table-borderless">
                            <thead class="table-light">
                                <tr>
                                    <th class="align-middle" scope="col">#ID
                                </th>
                                    <th class="align-middle" scope="col">Product Name
                                </th> 
                                    <th class="align-middle" scope="col">Order Id
                                </th>

                                    <th class="align-middle" scope="col">Total Price
                                </th>
                                    <th class="align-middle" scope="col">Status
                                </th>
                                    <th class="align-middle" scope="col">Ordered On
                                </th>
                                    <th class="align-middle" scope="col">Expected Delivery Date
                                </th>
                                    <th class="align-middle text-center" scope="col">Actions
                                </th>

                                </tr>
                            </thead>
                            <tbody id="tblBodyLoadingFrame">
                            </tbody>
                            <tbody id="tblBody">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <script src="/assets/js/Pages/my-orders.js"></script>
</asp:Content>

