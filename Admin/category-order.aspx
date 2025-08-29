<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="category-order.aspx.cs" Inherits="Admin_category_order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link rel="stylesheet" href="assets/libs/dropzone/dropzone.css" type="text/css" />
<link href="assets/libs/dragula/dragula.min.css" rel="stylesheet" />
<link href="assets/libs/dragula/example.css" rel="stylesheet" />
    <style>
        .sortablev {
            list-style-type: none;
            margin: 0;
            float: left;
            margin-right: 10px;
            background: #fff none repeat scroll 0 0;
            border: 1px solid #e7e7e7;
            padding: 5px;
            width: 430px;
            min-height: 300px;
            width: 100%;
            min-height: auto;
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
        }

            .sortablev li {
                margin: 5px;
                padding: 5px;
                font-size: 1.2em;
                width: 400px;
                width: 32%;
                cursor: pointer;
                background: #f7f7f7;
                padding: 10px;
                align-items: center;
            }

        .DisplayOrderNumber {
            background: #000000;
            color: #fff;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 30px;
            width: 30px;
            border-radius: 50px;
            margin-right: 10px;
        }

        [data-layout-mode="dark"] .sortablev {
            background: #383d42 none repeat scroll 0 0;
            border: 1px solid #383d42;
        }
      [data-layout-mode="dark"]    .sortablev li .CategoryName{
          color:#000 !important;
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
                        <h4 class="mb-sm-0">Manage Category Display Order</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/">Dashboard</a></li>
                                <li class="breadcrumb-item active">Manage Category Display Order</li>
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
                            <h4 class="card-title mb-0 flex-grow-1">Manage Category Display Order</h4>
                        </div>
                        <div class="card-body">
                            <div class="row ">
                                <div class="col-lg-12">
                                    <ul id='left-defaults' class='dragula sortablev'>
                                    </ul>
                                </div>
                                <div class="col-lg-12">
                                    <button id="Update" class="btn btn-primary btn-dark waves-effect waves-light m-t-25">Update</button>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
       <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="assets/libs/dropzone/dropzone-min.js"></script>
<script src="assets/js/form-file-upload.init.js"></script>
<script src="assets/libs/dragula/dragula.min.js"></script>
<script src="assets/libs/dragula/custom-dragula.js"></script>
    <script src="assets/js/pages/category-order.js"></script>
</asp:Content>

