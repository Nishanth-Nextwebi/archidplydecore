<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="view-products-stories.aspx.cs" Inherits="Admin_view_products_stories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        /*     .ImagesLoaded {
        margin: 20px;
        border: 1px solid #ddd;
        border-radius: 10px;
        padding: 20px;
    }*/

        #left-defaults {
            padding-left: 0px;
        }

            #left-defaults .maindiv {
                border: 1px solid #ddd;
                border-radius: 5px;
                margin: 15px;
                padding: 15px;
            }
        /*  .deleteGalleryItem{
            display: inline-block;
background: red;
color: #fff;
border-radius: 5px;
padding: 5px 20px;
border:1px solid red;

    }
    .deleteGalleryItem:hover{
background: #fff;
color: red;
    }*/

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
    </style>

    <!-- dropzone css -->
    <link rel="stylesheet" href="assets/libs/dropzone/dropzone.css" type="text/css" />
    <link href="assets/libs/dragula/dragula.min.css" rel="stylesheet" />
    <link href="assets/libs/dragula/example.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">View Product Stories</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="dashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">View Product Stories</li>
                            </ol>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex">
                            <h4 class="card-title mb-0 flex-grow-1">Manage Product Stories</h4>
                        </div>
                        <div class="card-body">
                            <table id="alternative-pagination" class="table nowrap dt-responsive align-middle table-striped table-bordered myTable" style="width: 100%;">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Thumb Image</th>
                                        <th>Title</th>
                                        <th>Link</th>
                                        <th>AddedOn On</th>
                                        <th>Featured ?</th>
                                        <th class="text-center">Action</th>
                                    </tr>
                                </thead>
                                <tbody id="tbdy">
                                    <%=strstoriess %>
                                </tbody>

                            </table>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
    <!-- Full screen modal content -->
    <div class="modal fade exampleModalFullscreen" tabindex="-1" id="GalleryModal" aria-labelledby="exampleModalFullscreenLabel" aria-hidden="true">
        <div class="modal-dialog modal-fullscreen">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">

                                <div class="card">
                                    <div class="row">

                                        <div class="col-lg-8 ">
                                            <div class="card-header stickyGalleryOrder">
                                                <h4 class="card-title mb-0">Gallery Images</h4>
                                                <input type="button" style="margin-top: 10px;" value="Update Image Order" id="UpdateImgOrder" class="btn btn-primary">
                                            </div>
                                            <ul id='left-defaults' class='row dragula sortablev ImagesLoaded'>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4">
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

                                                            <h4>Drop files here or click to upload.</h4>
                                                        </div>
                                                    </div>
                                                    <small class="d-block link-danger mt-2">.png, .jpeg, .jpg, .webp formats are required, Image Size Should be 500*800 px</small>

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
                                                    <!-- end dropzon-preview -->
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end col -->

                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <a href="javascript:void(0);" class="btn btn-link shadow-none link-success fw-medium" data-bs-dismiss="modal"><i class="ri-close-line me-1 align-middle"></i>Close</a>
                    <button type="button" id="AddToGallery" data-pid="1" class="btn btn-primary">Upload</button>

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="assets/js/pages/product-stories.js"></script>
    <!-- dropzone min -->
    <script src="assets/libs/dropzone/dropzone-min.js"></script>
    <script src="assets/js/form-file-upload.init.js"></script>
    <script src="assets/libs/drag-and-drop/dragula/dragula.min.js"></script>
    <script src="assets/libs/drag-and-drop/dragula/custom-dragula.js"></script>
</asp:Content>


