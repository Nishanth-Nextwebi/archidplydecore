<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="investors-report-detail.aspx.cs" Inherits="investors_report_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
        <script type="application/ld+json">
{
  "@context": "https://schema.org",
  "@type": "BreadcrumbList",
  "itemListElement": [
    {
      "@type": "ListItem",
      "position": 1,
      "name": "Home",
      "item": "https://archidplydecor.com/"
    },{
      "@type": "ListItem",
      "position": 2,
      "name": "Investor report details",
      "item": "https://archidplydecor.com/investor-report-detail"
    }
  ]
}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <main id="content" class="wrapper layout-page">
    <section class="pb-14 pb-lg-12 pt-lg-12  bg-invest" style="background: url('assets/imgs/bg-img.png');background-attachment:fixed">
        <div class="container">
            <nav class="py-4 lh-30px" aria-label="breadcrumb">
                <ol class="breadcrumb justify-content-center py-1">
                    <li class="breadcrumb-item"><a href="/">Home</a></li>
                    <li class="breadcrumb-item active" aria-current="page"><%=strInvesterTitle %></li>
                </ol>
            </nav>
        </div>

        <div class="container">
            <h2 class="text-center mb-8"><%=strInvesterTitle %></h2>
            <div class="row justify-content-center">
                <div class="col-xl-10">
                    <table class="table table-bordered table-striped">
                        <thead class="table-primary">
                            <tr>
                                <th style="width: 80%" class="py-5 px-8 fs-5">Report Name</th>
                                <th class="py-5 fs-5 text-center">Download</th>
                            </tr>
                        </thead>
                        <tbody>
                            <%=strReports %>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>







</main>
</asp:Content>

