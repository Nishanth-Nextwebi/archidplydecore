<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="Admin_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        span.clsord {
            text-align: center !important;
        }

        .filterRev.selected {
            background: #3577f1 !important;
            color: #fff !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">

            <!-- start page title -->
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Dashboard</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Dashboard</a></li>
                                <li class="breadcrumb-item active">Dashboard</li>
                            </ol>
                        </div>

                    </div>
                </div>
            </div>
            <!-- end page title -->

            <div class="row">
                <div class="col">
                    <div class="h-100">
                        <div class="row mb-3 pb-1">
                            <div class="col-12">
                                <div class="d-flex align-items-lg-center flex-lg-row flex-column">
                                    <div class="flex-grow-1">
                                        <h4 class="fs-16 mb-1">Hello, <%=Strusername %>!</h4>
                                        <p class="text-muted mb-0">Here's what's happening with your store today.</p>
                                    </div>
                                    <div class="mt-3 mt-lg-0 d-none">
                                        <div class="row g-3 mb-0 align-items-center">
                                            <div class="col-auto">
                                                <a href="view-all.aspx" class="btn btn-soft-danger shadow-none"><i class="ri-message-2-line align-middle me-1"></i>View Architects</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xl-3 col-md-6">
                    <div class="card card-animate">
                        <div class="card-body">
                            <div class="d-flex align-items-center">
                                <div class="flex-grow-1 overflow-hidden">
                                    <p class="text-uppercase fw-medium text-muted text-truncate mb-0">Total Sales</p>
                                </div>
                            </div>
                            <div class="d-flex align-items-end justify-content-between mt-4">
                                <div>
                                    <h4 class="fs-22 fw-semibold ff-secondary mb-4"><span>₹</span><span class="counter-value" data-target="<%=strTotalSales %>"><%=strTotalSales %></span></h4>
                                    <a href="/Admin/order-report.aspx" class="text-decoration-underline">View Total Sales</a>
                                </div>
                                <div class="avatar-sm flex-shrink-0">
                                    <span class="avatar-title bg-info rounded fs-3">
                                        <i class="bx bx-rupee"></i>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-md-6">
                    <div class="card card-animate custom-bg">
                        <div class="card-body">
                            <div class="d-flex align-items-center">
                                <div class="flex-grow-1 overflow-hidden">
                                    <p class="text-uppercase fw-medium text-muted text-truncate mb-0">Products</p>
                                </div>
                            </div>
                            <div class="d-flex align-items-end justify-content-between mt-4">
                                <div>
                                    <h4 class="fs-22 fw-semibold ff-secondary mb-4"><span class="counter-value" data-target="<%=strTotalProduct %>"><%=strTotalProduct %></span></h4>
                                    <a href="/Admin/view-products.aspx" class="text-decoration-underline">View Products</a>
                                </div>
                                <div class="avatar-sm flex-shrink-0">
                                    <span class="avatar-title bg-warning rounded fs-3">
                                        <i class="mdi mdi-archive"></i>
                                    </span>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-md-6">
                    <div class="card card-animate">
                        <div class="card-body">
                            <div class="d-flex align-items-center">
                                <div class="flex-grow-1 overflow-hidden">
                                    <p class="text-uppercase fw-medium text-muted text-truncate mb-0">Total Orders</p>
                                </div>
                            </div>
                            <div class="d-flex align-items-end justify-content-between mt-4">
                                <div>
                                    <h4 class="fs-22 fw-semibold ff-secondary mb-4"><span class="counter-value" data-target="<%=strTotalOrder %>"><%=strTotalOrder %></span></h4>
                                    <a href="/Admin/order-report.aspx" class="text-decoration-underline">View Total Orders</a>
                                </div>
                                <div class="avatar-sm flex-shrink-0">
                                    <span class="avatar-title bg-success rounded fs-3">
                                        <i class="bx bx-shopping-bag"></i>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-3 col-md-6">
                    <div class="card card-animate">
                        <div class="card-body">
                            <div class="d-flex align-items-center">
                                <div class="flex-grow-1 overflow-hidden">
                                    <p class="text-uppercase fw-medium text-muted text-truncate mb-0">Customers</p>
                                </div>

                            </div>
                            <div class="d-flex align-items-end justify-content-between mt-4">
                                <div>
                                    <h4 class="fs-22 fw-semibold ff-secondary mb-4"><span class="counter-value" data-target="<%=strTotalCustomer %>"><%=strTotalCustomer %></span></h4>
                                    <a href="/Admin/view-customers.aspx" class="text-decoration-underline">View All Customers</a>
                                </div>
                                <div class="avatar-sm flex-shrink-0">
                                    <span class="avatar-title bg-danger rounded fs-3">
                                        <i class="mdi mdi-account-multiple"></i>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-md-6 d-none">
                    <div class="card card-animate">
                        <div class="card-body">
                            <div class="d-flex align-items-center">
                                <div class="flex-grow-1 overflow-hidden">
                                    <p class="text-uppercase fw-medium text-muted text-truncate mb-0">Blogs</p>
                                </div>

                            </div>
                            <div class="d-flex align-items-end justify-content-between mt-4">
                                <div>
                                    <h4 class="fs-22 fw-semibold ff-secondary mb-4"><span class="counter-value" data-target="<%=strBlogs %>"><%=strBlogs %></span></h4>
                                    <a href="/Admin/view-blogs.aspx" class="text-decoration-underline">View All Blogs</a>
                                </div>
                                <div class="avatar-sm flex-shrink-0">
                                    <span class="avatar-title bg-primary rounded fs-3">
                                        <i class="mdi mdi-wechat"></i>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-md-6 d-none">
                    <div class="card card-animate">
                        <div class="card-body">
                            <div class="d-flex align-items-center">
                                <div class="flex-grow-1 overflow-hidden">
                                    <p class="text-uppercase fw-medium text-muted text-truncate mb-0">Contact Request</p>
                                </div>

                            </div>
                            <div class="d-flex align-items-end justify-content-between mt-4">
                                <div>
                                    <h4 class="fs-22 fw-semibold ff-secondary mb-4"><span class="counter-value" data-target="<%=strContact %>"><%=strContact %></span></h4>
                                    <a href="/Admin/view-contactus.aspx" class="text-decoration-underline">View Contact Requests</a>
                                </div>
                                <div class="avatar-sm flex-shrink-0">
                                    <span class="avatar-title bg-dark rounded fs-3">
                                        <i class="mdi mdi-message-alert"></i>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
     <div class="col-xl-8">
         <div class="card">
             <div class="card-header border-0 align-items-center d-flex">
                 <h4 class="card-title mb-0 flex-grow-1">Revenue</h4>

                 <div>
                     <button type="button" class="btn btn-soft-primary btn-sm shadow-none filterRev" data-val="All">
                         ALL
     
                     </button>
                     <button type="button" class="btn btn-soft-primary btn-sm shadow-none filterRev" data-val="1W">
                         1W
     
                     </button>
                     <button type="button" class="btn btn-soft-primary btn-sm shadow-none filterRev" data-val="1M">
                         1M
     
                     </button>
                     <button type="button" class="btn btn-soft-primary btn-sm shadow-none filterRev" data-val="6M">
                         6M
     
                     </button>
                     <button type="button" class="btn btn-soft-primary btn-sm shadow-none filterRev selected" data-val="1Y">
                         1Y 
                     </button>
                 </div>
             </div>
             <!-- end card header -->

             <div class="card-header p-0 border-0 bg-soft-light">
                 <div class="row g-0 text-center">
                     <div class="col-6 col-sm-3">
                         <div class="p-3 border border-dashed border-start-0">
                             <h5 class="mb-1"><span class="counter-value totalSales" data-target="0">0</span></h5>
                             <p class="text-muted mb-0">Total Sales</p>
                         </div>
                     </div>
                     <!--end col-->
                     <div class="col-6 col-sm-3">
                         <div class="p-3 border border-dashed border-start-0">
                             <h5 class="mb-1">₹<span class="counter-value confirmedSale" data-target="0">0</span></h5>
                             <p class="text-muted mb-0">Confirmed Order</p>
                         </div>
                     </div>
                     <!--end col-->
                     <div class="col-6 col-sm-3">
                         <div class="p-3 border border-dashed border-start-0">
                             <h5 class="mb-1">₹<span class="counter-value initiatedSale" data-target="0">0</span></h5>
                             <p class="text-muted mb-0">Initiated Order</p>
                         </div>
                     </div>
                     <!--end col-->
                     <div class="col-6 col-sm-3">
                         <div class="p-3 border border-dashed border-start-0 border-end-0">
                             <h5 class="mb-1 text-success"><span class="counter-value convRatio" data-target="0">0</span>%</h5>
                             <p class="text-muted mb-0">Conversation Ratio</p>
                         </div>
                     </div>
                     <!--end col-->
                 </div>
             </div>
             <!-- end card header -->

             <div class="card-body p-0 pb-2">
                 <div class="w-100">
                     <div id="customer_impression_charts" data-colors='["--vz-success", "--vz-primary", "--vz-danger"]' class="apex-charts" dir="ltr"></div>
                 </div>
             </div>
             <!-- end card body -->
         </div>
         <!-- end card -->
     </div>
     <div class="col-xl-4">
         <div class="card card-height-100">
             <div class="card-header align-items-center d-flex">
                 <h4 class="card-title mb-0 flex-grow-1">Order Status</h4>
                 <div>
                     <button type="button" class="btn btn-soft-primary btn-sm shadow-none filterSts" data-val="All">
                         ALL
     
                     </button>
                     <button type="button" class="btn btn-soft-primary btn-sm shadow-none filterSts" data-val="1W">
                         1W
     
                     </button>
                     <button type="button" class="btn btn-soft-primary btn-sm shadow-none filterSts" data-val="1M">
                         1M
     
                     </button>
                     <button type="button" class="btn btn-soft-primary btn-sm shadow-none filterSts" data-val="6M">
                         6M
     
                     </button>
                     <button type="button" class="btn btn-soft-primary btn-sm shadow-none filterSts selected" data-val="1Y">
                         1Y 
                     </button>
                 </div>
             </div>
             <!-- end card header -->

             <div class="card-body">
                 <div id="store-visits-source" class="apex-charts" dir="ltr"></div>
             </div>
         </div>
         <!-- .card-->
     </div>
 </div>
 <div class="row">
     <div class="col-xl-12">
         <div class="card">
             <div class="card-header align-items-center d-flex">
                 <h4 class="card-title mb-0 flex-grow-1">Recent Orders</h4>
                 <div class="flex-shrink-0">
                     <a href="order-report.aspx" class="btn btn-soft-info btn-sm shadow-none">
                         <i class="ri-file-list-3-line align-middle text-ligt"></i>View Reports</a>
                 </div>
             </div>
             <div class="card-body">
                 <div class="table-responsive table-card">
                     <table class="table table-bordered table-centered align-middle table-nowrap mb-0">
                         <thead class="text-muted table-light">
                             <tr>
                                 <th>#</th>
                                 <th>Order Id</th>
                                 <th>Name</th>
                                 <th>Email</th>
                                 <th>Mobile</th>
                                 <th>Sub Total</th>
                                 <th>Order Status</th>
                                 <th>Payment Status</th>
                                 <th>Payment Id</th>
                                 <th>Ordered On</th>
                             </tr>
                         </thead>
                         <tbody>
                             <%=strOrders %>
                         </tbody>
                     </table>
                 </div>
             </div>
         </div>
     </div>
 </div>
        </div>
    </div>
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="assets/libs/apexcharts/apexcharts.min.js"></script>
    <script src="assets/js/pages/dashboard-ecommerce.init.js"></script>

</asp:Content>

