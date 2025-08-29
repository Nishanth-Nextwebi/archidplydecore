<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="payment-status.aspx.cs" Inherits="payment_status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="flex-center-content course-content">
    <div class="container">
        <div class="row">
            <div class="col-lg-8 offset-lg-2">
                <div class="section-title">
                    <div class="text-center">
                        <p><%=payStatus %>.</p> 
                    </div>
                </div>
                <div class="mt-4">
                    <hr />
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>

