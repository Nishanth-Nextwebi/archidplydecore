<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="blog-detail.aspx.cs" Inherits="blog_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main id="content" class="wrapper layout-page">
        <section class="z-index-2 position-relative pb-2 mb-12" data-animated-id="1">

            <div class="bg-body-secondary mb-3">
                <div class="container">
                    <nav class="py-4 lh-30px" aria-label="breadcrumb">
                        <ol class="breadcrumb justify-content-center py-1 mb-0">
                            <li class="breadcrumb-item"><a title="Home" href="/">Home</a></li>
                            <li class="breadcrumb-item active" aria-current="page">Blog Details</li>
                        </ol>
                    </nav>
                </div>
            </div>
        </section>
        <section class="blogDetail mb-5">
            <div class="container mb-5">
                <div class="row justify-content-center">
                    <div class="col-lg-10">
                        <div class=" text-center mb-3">
                            <h2 class=" px-6 text-body-emphasis border-0 fw-500 mb-5 fs-3 "><%=StrBlogTitle %></h2>


                            <ul class="list-inline fs-15px fw-semibold letter-spacing-01 d-flex justify-content-center align-items-center">
                                <li>
                                <li class="border-end px-6 text-body-emphasis border-0 text-body">By
		
            <a href="javascript:void(0)"><%=StrPostedBy %></a>
                                </li>
                                <li class="list-inline-item px-6"><%=StrPostedOn %></li>
                            </ul>
                        </div>
                            <img alt="" class="w-100" src="/<%=StrImgUrl %>" loading="lazy" data-ll-status="loaded">

                        <p class=" fs-18px text-body-emphasis mt-5 mb-6"><%=strShortDesc %></p>

                        <div class="">
                            <%=StrDesc %>
                        </div>

                    </div>
                </div>
            </div>
        </section>


        
    </main>
</asp:Content>

