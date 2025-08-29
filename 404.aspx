<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="404.aspx.cs" Inherits="_404" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .error-img svg {
    width: 100%;
    height: 100%;
    color: var(--tg-theme-secondary);
}
    </style>
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="error-area">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-lg-8">
                    <div class="error-wrap text-center">
                        <div class="error-img">
                            <img src="/images_/404erropage.png" alt="img" class="injectable">
                        </div>
                        <div class="error-content">
                            <h2 class="title fw-semibold">This Page is Not Available!</h2>
                            <div class="tg-button-wrap mb-15 mt-10">
                                <a href="/" class="mt-3 btn green-btn shadow-sm mb-6">Go To Home Page<i class="fa-solid fa-arrow-circle-right"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>


