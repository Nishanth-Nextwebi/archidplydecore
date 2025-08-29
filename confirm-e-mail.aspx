<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="confirm-e-mail.aspx.cs" Inherits="email_verify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
      
        .statusImgBox img {
            width:auto;
            height: 120px;
            margin-bottom: 30px;
        }

        @media (min-width: 1025px) {
            #nt_footer {
                position: static;
            }
        }
    </style>
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="columns" class="columns-container section-padding-md">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 col-sm-12  col-md-12 text-center ">
                    <br />
                    <br />
                    <%=strStatus %>
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
