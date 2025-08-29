<%@ Page Title="Payment Error | ARCHIDPLY DECOR LTD" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="Pay-Error.aspx.cs" Inherits="Pay_Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="paySuccessPage">

        <section class="breadscrumb-section  pt-0">
            <div class="container-fluid-lg">
                <div class="row">
                    <div class="col-12">
                        <div class="breadscrumb-contain breadscrumb-order">
                            <div class="order-box">
                                <div class="order-image">
                                    <div class="checkmark text-center">
                                        <img src="/images_/error-img.gif" class="img-fluid" />
                                    </div>
                                </div>

                                <div class="order-contain text-center">
                                    <h3 class="theme-color">Error</h3>
                                    <h5 class="text-content">Something Went Wrong Please Try After Sometime</h5>
                                </div>
                                <div class="text-center mt-6">
                                    <a href="/" class="btn btn-primary mb-3">
                                        <span>Go To Home</span>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </section>
</asp:Content>

