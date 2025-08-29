<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="order-report.aspx.cs" Inherits="order_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .error {
            color: red;
        }

        ul.shippingAddress_ul {
            font-size: 14px;
            background: aliceblue;
            padding: 20px;
            border-radius: 10px;
            margin-bottom: 0px;
        }

            ul.shippingAddress_ul li {
                font-size: 14px;
                margin-bottom: 10px;
            }

                ul.shippingAddress_ul li:last-child {
                    margin-bottom: 0px;
                }

        ul.shippingAddress_ul {
            list-style: none;
        }

            ul.shippingAddress_ul li strong {
                color: #000000;
            }

            ul.shippingAddress_ul li {
                margin-bottom: 20px;
                font-size: 14px;
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
                        <h4 class="mb-sm-0">Orders Report</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Report</a></li>
                                <li class="breadcrumb-item active">Orders Report</li>
                            </ol>
                        </div>

                    </div>
                </div>
            </div>
            <!-- end page title -->

        </div>
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex justify-content-between">
                            <h4 class="card-title mb-0 flex-grow-1">Filters</h4>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-3 mr-sm-2" placeholder="Search by Order Id/Name/Email/Phone No." ID="txtSearch" />
                                </div>
                                <div class="col-lg-2">
                                    <asp:DropDownList runat="server" MaxLength="100" class="form-control mb-3 mr-sm-2 fSelect ddlDay" ID="ddlDay">
                                        <asp:ListItem Value="">-Select Day's -</asp:ListItem>
                                        <asp:ListItem Value="1">Today's Orders</asp:ListItem>
                                        <asp:ListItem Value="2">Last 7 Days Orders</asp:ListItem>
                                        <asp:ListItem Value="3">Last 30 Days Orders</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-2">
                                    <asp:TextBox runat="server" placeholder="From Date" class="form-control mb-3 mr-sm-2 datepicker" ID="txtFrom" />
                                </div>
                                <div class="col-lg-2">
                                    <asp:TextBox runat="server" placeholder="To Date" class="form-control mb-3 mr-sm-2 datepicker" ID="txtTo" />
                                </div>
                                <div class="col-lg-2">
                                    <asp:DropDownList runat="server" class="form-control mb-3 mr-sm-2 fSelect" ID="ddlStatus">
                                        <asp:ListItem Selected="True" Value="">Order Status</asp:ListItem>
                                        <asp:ListItem>Initiated</asp:ListItem>
                                        <asp:ListItem>In-Process</asp:ListItem>
                                        <asp:ListItem>Dispatched</asp:ListItem>
                                        <asp:ListItem>Delivered</asp:ListItem>
                                        <asp:ListItem>Cancelled</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-2 ">
                                    <asp:DropDownList runat="server" class="form-control mb-3 mr-sm-2 fSelect" ID="ddlPayStatus">
                                        <asp:ListItem Value="">Payment Status</asp:ListItem>
                                        <asp:ListItem Value="Not Paid">Initiated</asp:ListItem>
                                        <asp:ListItem Value="Paid">Paid</asp:ListItem>
                                        <asp:ListItem Value="Failed">Failed</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4 ">
                                    <a id="btnSearch" class="btn btn-primary btn-dark btnSearch" href="javascript:void(0);">Search</a>
                                    <asp:LinkButton ID="btnExport" runat="server" Text="Search" OnClick="btnExport_Click" CssClass="btn btn-success waves-effect waves-light" Style="padding: 0px 5px; font-size: 24px;">
                                        <i class="mdi mdi-file-excel"></i>
                                    </asp:LinkButton>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">

                                <div class="col-lg-2 m-2 mb-0">
                                    <select id="ddlPageSize" class="form-select form-select-sm ">
                                        <option value="30">30</option>
                                        <option value="50">50</option>
                                        <option value="100">100</option>
                                        <option value="200">200</option>
                                    </select>
                                </div>

                                <div class="col-lg-12 mt-3 mb-2">
                                    <div class="table-responsive table-card m-2">

                                        <table class="table table-nowrap align-middle table-striped table-bordered" style="width: 100%;">
                                            <thead>
                                                <tr>
                                                    <th class="nosort">#</th>
                                                    <th class="nosort">Order Id</th>
                                                    <th class="nosort">Name</th>
                                                    <th class="nosort">Email</th>
                                                    <th class="nosort">Phone</th>
                                                    <th class="nosort">Order Status</th>
                                                    <th class="nosort">Shipping Info</th>
                                                    <th class="nosort">Ordered On</th>
                                                    <th class="nosort">Payment Type</th>
                                                    <th class="nosort">Payment Status</th>
                                                    <th class="nosort">PaymentId</th>
                                                    <th class="nosort">Advance Paid</th>
                                                    <th class="nosort">Balance Amount</th>
                                                    <th class="nosort">Total Price</th>
                                                    <th class="nosort">Last Updated On</th>
                                                    <th class="nosort fixed-column">Action</th>
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
                            <div class="row">
                                <div class="col-lg-12 mt-3">
                                    <span id="showDetails"></span>
                                </div>
                                <div class="col-lg-12">
                                    <ul class="pagination pagination-separated justify-content-center mb-0 vPagination">
                                    </ul>
                                </div>
                            </div>

                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>

    <div id="shippingModal" class="modal fade" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="myModalLabel">Shipping Address</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <ul class='shippingAddress_ul'>
                        <li>
                            <strong>Contact Person Name : </strong><span id="txtContactPersonName"></span>
                        </li>
                        <li>
                            <strong>Email : </strong><span id="txtEmail"></span>
                        </li>
                        <li>
                            <strong>Phone : </strong><span id="txtMobile"></span>
                        </li>
                        <li>
                            <strong>Address 1 : </strong><span id="txtAddressLine1"></span>
                        </li>
                        <li>
                            <strong>Address 2 : </strong><span id="txtAddressLine2"></span>
                        </li>
                        <li>
                            <strong>City : </strong><span id="txtCity"></span>
                        </li>
                        <li>
                            <strong>State : </strong><span id="txtState"></span>
                        </li>
                        <li>
                            <strong>PinCode : </strong><span id="txtZip"></span>
                        </li>
                    </ul>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div id="ShippingDetails" class="modal fade" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="myModalLabelShippingDetails">Shipping Details</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <ul class='shippingAddress_ul'>
                        <li>
                            <strong>Courier Name : </strong><span id="CourierName"></span>
                        </li>
                        <li>
                            <strong>Tracking Code : </strong><span id="TrackingCode"></span>
                        </li>
                        <li>
                            <strong>Tracking Link : </strong><span id="TrackingLink"></span>
                        </li>
                        <li>
                            <strong>Delivery Date : </strong><span id="deliveryDate"></span>
                        </li>
                    </ul>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal fade" id="DispatchModal" data-orderguid="" tabindex="-1" aria-labelledby="exampleModalgridLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Update Dispatch Info</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">

                    <div class="row g-3">
                        <!--end col-->
                        <div class="col-xxl-12">
                            <div>
                                <label for="txtCourierName" class="form-label">Courier Name <sup>*</sup></label>
                                <input type="text" maxlength="100" class="form-control" id="txtCourierName">
                                <span class="error"></span>
                            </div>
                        </div>
                        <!--end col-->

                        <div class="col-xxl-12">
                            <label for="txtTrackingCode" class="form-label">Tracking Code <sup>*</sup></label>
                            <input type="text" maxlength="20" class="form-control" id="txtTrackingCode">
                            <span class="error"></span>

                        </div>

                        <!--end col-->
                        <div class="col-xxl-12">
                            <label for="txtTrackingLink" class="form-label">Tracking Link <sup>*</sup></label>
                            <input type="text" maxlength="20" class="form-control" id="txtTrackingLink">
                            <span class="error"></span>
                        </div>
                        <div class="col-xxl-12">
                            <div>
                                <label for="txtDate" class="form-label">Delivery Date<sup>*</sup></label>
                                <input type="date" class="form-control today_datepicker_with_time" id="txtDate">
                                <span class="error"></span>
                            </div>
                        </div>
                        <!--end col-->

                        <div class="col-lg-12">

                            <div class="hstack gap-2 justify-content-end">
                                <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                                <button type="button" id="btnDispatch" class="btn btn-primary">Submit</button>
                            </div>
                        </div>
                    </div>
                    <!--end col-->
                </div>
                <!--end row-->

            </div>
        </div>
    </div>


    <div class="modal fade" id="PaymentModel" data-orderguid="" tabindex="-1" aria-labelledby="exampleModalgridLabel2" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Update Payment Status</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">

                    <div class="row g-3">
                        <!--end col-->
                        <div class="col-xxl-12">
                            <div>
                                <label for="txtPayId" class="form-label">Payment Id<sup>*</sup></label>
                                <input type="text" class="form-control" maxlength="100" id="txtPayId">
                                <span class="error"></span>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="hstack gap-2 justify-content-end">
                                <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                                <button type="button" id="btnPayStatus" class="btn btn-primary">Submit</button>
                            </div>
                        </div>
                    </div>
                    <!--end col-->
                </div>
                <!--end row-->

            </div>
        </div>
    </div>

    <div class="modal fade" id="CancelModal" data-orderguid="" tabindex="-1" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Cancellation Info</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">

                    <div class="row g-3">
                        <div class="col-xxl-12">
                            <div>
                                <label for="CancellationRemarks" class="form-label">Cancellation Remarks</label>
                                <textarea class="form-control" maxlength="1000" id="txtRemarks" rows="3" placeholder="Enter Cancellation Remarks"></textarea>
                            </div>
                        </div>
                        <!--end col-->
                        <div class="col-xxl-12">
                            <div>
                                <div class="form-check mb-3">
                                    <input class="form-check-input" type="checkbox" id="ShowRemarks">
                                    <label class="form-check-label" for="ShowRemarks">
                                        Show Remarks to User
                                    </label>
                                </div>
                            </div>
                        </div>

                        <!--end col-->
                        <div class="col-lg-12">
                            <div class="hstack gap-2 justify-content-end">
                                <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                                <button type="button" id="btnCancel" class="btn btn-primary">Submit</button>
                            </div>
                        </div>
                        <!--end col-->
                    </div>
                    <!--end row-->

                </div>
            </div>
        </div>
    </div>
    <div id="paymentModal" class="modal fade" tabindex="-1" aria-labelledby="myModalLabel" data-orderguid="" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Payments</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="OrderInfoBlock m-2 mb-3 alert alert-info">
                        <div class="row">
                            <div class="col-xxl-4 ">
                                <label>Total Price</label>
                                <div id="PaymentBlockTotalPrice"></div>
                            </div>
                            <div class="col-xxl-4 ">
                                <label>Order Status</label>
                                <div id="PaymentBlockOrderStatus"></div>
                            </div>
                            <div class="col-xxl-4 ">
                                <label>Payment Status</label>
                                <div id="PaymentBlockPaymentStatus"></div>
                            </div>
                        </div>
                    </div>

                    <div class="payWrap m-2 mb-3 ">
                        <div class="row">
                            <div class="col-xxl-3 paymentModeParent">
                                <label class="form-label">Payment Mode</label>
                                <select id="paymentMode" class="form-select fSelect">
                                    <option value="">Select Method</option>
                                    <option value="IMPS">IMPS</option>
                                    <option value="NEFT">NEFT</option>
                                    <option value="GPay">GPay</option>
                                    <option value="PhonePe">PhonePe</option>
                                    <option value="Paytm">Paytm</option>
                                    <option value="Credit Card">Credit Card</option>
                                    <option value="Waive Off">Waive Off</option>
                                    <option value="Other">Other</option>
                                </select>
                            </div>
                            <div class="col-xxl-3 mb-2">
                                <label class="form-label">Payment Id</label>
                                <input type="text" maxlength="100" class="form-control" id="paymentId">
                            </div>
                            <div class="col-xxl-3 mb-2">
                                <label class="form-label">Amount</label>
                                <input type="text" maxlength="10" class="form-control decimalInput" id="amount">
                            </div>
                            <div class="col-xxl-3 mb-2">
                                <label class="form-label">Remarks</label>
                                <textarea maxlength="1000" rows="1" class="form-control" id="remarks"></textarea>
                            </div>
                            <div class="col-xxl-3 mb-2">
                                <div class="hstack gap-2 justify-content-start m-t-25">
                                    <button type="button" id="addPayment" class="btn btn-dark">Add</button>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="payTableWrap">
                        <div class="table-responsive table-card m-2">
                            <table class="table table-nowrap align-middle table-striped table-bordered" style="width: 100%;">
                                <thead>
                                    <tr>
                                        <th class="nosort">#</th>
                                        <th class="nosort">Payment Mode</th>
                                        <th class="nosort">Payment Id</th>
                                        <th class="nosort">Amount</th>
                                        <th class="nosort">Remarks</th>
                                        <th class="nosort">Added On</th>
                                        <th class="nosort text-center">Action</th>
                                    </tr>
                                </thead>

                                <tbody id="tblPaymentBody">
                                </tbody>
                            </table>

                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <script src="assets/js/pages/order-report.js"></script>
</asp:Content>

