<%@ Page Title="ArchidPly Blogs | Plywood Tips & Design Ideas in India" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="blogs.aspx.cs" Inherits="blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="keywords" content="plywood blog, ArchidPly Decor blog, plywood buying tips, plywood in bangalore, best plywood in india, home interior plywood, top plywood company, furniture plywood, laminated plywood, flush doors" />

    <meta name="description" content="Read expert blogs from ArchidPly Decor – plywood trends, home interiors, and buying guides." />
    <style>
        .pagination {
            display: flex;
            list-style: none;
            padding: 0;
        }

        .page-item {
            margin: 0 5px;
        }

            .page-item.active a {
                background-color: #4e7661;
                color: white;
                border-color: #4e7661;
            }

            .page-item.disabled a {
                pointer-events: none;
                opacity: 0.6;
            }

        #BlogListBindingSec div {
            display: block !important;
            visibility: visible !important;
            opacity: 1 !important;
        }
    </style>
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
      "name": "Blogs",
      "item": "https://archidplydecor.com/blogs"
    }
  ]
}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main id="content" class="wrapper layout-page">
        <section class="page-title z-index-2 position-relative" data-animated-id="1">

            <div class="bg-body-secondary">
                <div class="container">
                    <nav class="py-4 lh-30px" aria-label="breadcrumb">
                        <ol class="breadcrumb justify-content-center py-1">
                            <li class="breadcrumb-item"><a href="../index.html">Home</a></li>
                            <li class="breadcrumb-item active" aria-current="page">Our Latest Blogs</li>
                        </ol>
                    </nav>
                </div>
            </div>
            <div class="text-center py-13">
                <div class="container">
                    <h1 class="mb-12">Our Latest Blogs</h1>
                </div>
            </div>
        </section>
        <div class="container mb-lg-18 mb-15 pb-3 mainBlogPage" data-animated-id="2">
            <div class="row" id="BlogListBindingSec">
                <%-- <div class="col-md-6 col-lg-4">
                    <article class="card card-post-grid-1 bg-transparent border-0 animate__fadeInUp animate__animated" data-animate="fadeInUp">
                        <figure class="card-img-top position-relative mb-10">
                            <a href="#" class="hover-shine hover-zoom-in d-block" title="How to Plop Hair for Bouncy, Beautiful Curls">
                                <img data-src="../assets/images/blog/post-01-370x450.jpg" class="img-fluid w-100 loaded" alt="How to Plop Hair for Bouncy, Beautiful Curls" width="370" height="450" src="../assets/images/blog/post-01-370x450.jpg" loading="lazy" data-ll-status="loaded">
                            </a><a href="#" class="post-item-cate btn btn-light btn-text-light-body-emphasis btn-hover-bg-dark btn-hover-text-light fw-500 post-cat position-absolute top-100 start-50 translate-middle py-2 px-7 border-0" title="Life style">Life style</a>
                        </figure>
                        <div class="card-body text-center px-md-9 py-0">
                            <h4 class="card-title lh-base mb-9"><a class="text-decoration-none" href="../blog/details-01.html" title="How to Plop Hair for Bouncy, Beautiful Curls">How to Plop Hair for Bouncy, Beautiful Curls</a></h4>
                            <ul class="post-meta list-inline lh-1 d-flex flex-wrap justify-content-center m-0">
                                <li class="list-inline-item border-end pe-5 me-5">By <a href="#" title="Admin">Admin</a></li>
                                <li class="list-inline-item">Jan 19th, 2021</li>
                            </ul>
                        </div>
                    </article>
                </div>
                <div class="col-md-6 col-lg-4">
                    <article class="card card-post-grid-1 bg-transparent border-0 animate__fadeInUp animate__animated" data-animate="fadeInUp">
                        <figure class="card-img-top position-relative mb-10">
                            <a href="#" class="hover-shine hover-zoom-in d-block" title="Which foundation formula is right for your skin?">
                                <img data-src="../assets/images/blog/post-02-370x450.jpg" class="img-fluid w-100 loaded" alt="Which foundation formula is right for your skin?" width="370" height="450" src="../assets/images/blog/post-02-370x450.jpg" loading="lazy" data-ll-status="loaded">
                            </a><a href="#" class="post-item-cate btn btn-light btn-text-light-body-emphasis btn-hover-bg-dark btn-hover-text-light fw-500 post-cat position-absolute top-100 start-50 translate-middle py-2 px-7 border-0" title="Life style">Life style</a>
                        </figure>
                        <div class="card-body text-center px-md-9 py-0">
                            <h4 class="card-title lh-base mb-9"><a class="text-decoration-none" href="../blog/details-01.html" title="Which foundation formula is right for your skin?">Which foundation formula is right for your skin?</a></h4>
                            <ul class="post-meta list-inline lh-1 d-flex flex-wrap justify-content-center m-0">
                                <li class="list-inline-item border-end pe-5 me-5">By <a href="#" title="Admin">Admin</a></li>
                                <li class="list-inline-item">Jan 19th, 2021</li>
                            </ul>
                        </div>
                    </article>
                </div>
                <div class="col-md-6 col-lg-4">
                    <article class="card card-post-grid-1 bg-transparent border-0 animate__fadeInUp animate__animated" data-animate="fadeInUp">
                        <figure class="card-img-top position-relative mb-10">
                            <a href="#" class="hover-shine hover-zoom-in d-block" title="How To Choose The Right Sofa for your home">
                                <img data-src="../assets/images/blog/post-03-370x450.jpg" class="img-fluid w-100 loaded" alt="How To Choose The Right Sofa for your home" width="370" height="450" src="../assets/images/blog/post-03-370x450.jpg" loading="lazy" data-ll-status="loaded">
                            </a><a href="#" class="post-item-cate btn btn-light btn-text-light-body-emphasis btn-hover-bg-dark btn-hover-text-light fw-500 post-cat position-absolute top-100 start-50 translate-middle py-2 px-7 border-0" title="Life style">Life style</a>
                        </figure>
                        <div class="card-body text-center px-md-9 py-0">
                            <h4 class="card-title lh-base mb-9"><a class="text-decoration-none" href="../blog/details-01.html" title="How To Choose The Right Sofa for your home">How To Choose The Right Sofa for your home</a></h4>
                            <ul class="post-meta list-inline lh-1 d-flex flex-wrap justify-content-center m-0">
                                <li class="list-inline-item border-end pe-5 me-5">By <a href="#" title="Admin">Admin</a></li>
                                <li class="list-inline-item">Jan 19th, 2021</li>
                            </ul>
                        </div>
                    </article>
                </div>
                <div class="col-md-6 col-lg-4">
                    <article class="card card-post-grid-1 bg-transparent border-0 animate__fadeInUp animate__animated" data-animate="fadeInUp">
                        <figure class="card-img-top position-relative mb-10">
                            <a href="#" class="hover-shine hover-zoom-in d-block" title="A User-Friendly Guide to Natural Cleansers">
                                <img data-src="../assets/images/blog/post-04-370x450.jpg" class="img-fluid w-100 loaded" alt="A User-Friendly Guide to Natural Cleansers" width="370" height="450" src="../assets/images/blog/post-04-370x450.jpg" loading="lazy" data-ll-status="loaded">
                            </a><a href="#" class="post-item-cate btn btn-light btn-text-light-body-emphasis btn-hover-bg-dark btn-hover-text-light fw-500 post-cat position-absolute top-100 start-50 translate-middle py-2 px-7 border-0" title="Life style">Life style</a>
                        </figure>
                        <div class="card-body text-center px-md-9 py-0">
                            <h4 class="card-title lh-base mb-9"><a class="text-decoration-none" href="../blog/details-01.html" title="A User-Friendly Guide to Natural Cleansers">A User-Friendly Guide to Natural Cleansers</a></h4>
                            <ul class="post-meta list-inline lh-1 d-flex flex-wrap justify-content-center m-0">
                                <li class="list-inline-item border-end pe-5 me-5">By <a href="#" title="Admin">Admin</a></li>
                                <li class="list-inline-item">Jan 19th, 2021</li>
                            </ul>
                        </div>
                    </article>
                </div>
                <div class="col-md-6 col-lg-4">
                    <article class="card card-post-grid-1 bg-transparent border-0 animate__fadeInUp animate__animated" data-animate="fadeInUp">
                        <figure class="card-img-top position-relative mb-10">
                            <a href="#" class="hover-shine hover-zoom-in d-block" title="Our Favorite Multitasking Products">
                                <img data-src="../assets/images/blog/post-05-370x450.jpg" class="img-fluid w-100 loaded" alt="Our Favorite Multitasking Products" width="370" height="450" src="../assets/images/blog/post-05-370x450.jpg" loading="lazy" data-ll-status="loaded">
                            </a><a href="#" class="post-item-cate btn btn-light btn-text-light-body-emphasis btn-hover-bg-dark btn-hover-text-light fw-500 post-cat position-absolute top-100 start-50 translate-middle py-2 px-7 border-0" title="Life style">Life style</a>
                        </figure>
                        <div class="card-body text-center px-md-9 py-0">
                            <h4 class="card-title lh-base mb-9"><a class="text-decoration-none" href="../blog/details-01.html" title="Our Favorite Multitasking Products">Our Favorite Multitasking Products</a></h4>
                            <ul class="post-meta list-inline lh-1 d-flex flex-wrap justify-content-center m-0">
                                <li class="list-inline-item border-end pe-5 me-5">By <a href="#" title="Admin">Admin</a></li>
                                <li class="list-inline-item">Jan 19th, 2021</li>
                            </ul>
                        </div>
                    </article>
                </div>
                <div class="col-md-6 col-lg-4">
                    <article class="card card-post-grid-1 bg-transparent border-0 animate__fadeInUp animate__animated" data-animate="fadeInUp">
                        <figure class="card-img-top position-relative mb-10">
                            <a href="#" class="hover-shine hover-zoom-in d-block" title="How To Choose The Right Sofa for your home">
                                <img data-src="../assets/images/blog/post-06-370x450.jpg" class="img-fluid w-100 loaded" alt="How To Choose The Right Sofa for your home" width="370" height="450" src="../assets/images/blog/post-06-370x450.jpg" loading="lazy" data-ll-status="loaded">
                            </a><a href="#" class="post-item-cate btn btn-light btn-text-light-body-emphasis btn-hover-bg-dark btn-hover-text-light fw-500 post-cat position-absolute top-100 start-50 translate-middle py-2 px-7 border-0" title="Life style">Life style</a>
                        </figure>
                        <div class="card-body text-center px-md-9 py-0">
                            <h4 class="card-title lh-base mb-9"><a class="text-decoration-none" href="../blog/details-01.html" title="How To Choose The Right Sofa for your home">How To Choose The Right Sofa for your home</a></h4>
                            <ul class="post-meta list-inline lh-1 d-flex flex-wrap justify-content-center m-0">
                                <li class="list-inline-item border-end pe-5 me-5">By <a href="#" title="Admin">Admin</a></li>
                                <li class="list-inline-item">Jan 19th, 2021</li>
                            </ul>
                        </div>
                    </article>
                </div>
                <div class="col-md-6 col-lg-4">
                    <article class="card card-post-grid-1 bg-transparent border-0 animate__fadeInUp animate__animated" data-animate="fadeInUp">
                        <figure class="card-img-top position-relative mb-10">
                            <a href="#" class="hover-shine hover-zoom-in d-block" title="Why You Should Try an Overnight Balm">
                                <img data-src="../assets/images/blog/post-07-370x450.jpg" class="img-fluid w-100 loaded" alt="Why You Should Try an Overnight Balm" width="370" height="450" src="../assets/images/blog/post-07-370x450.jpg" loading="lazy" data-ll-status="loaded">
                            </a><a href="#" class="post-item-cate btn btn-light btn-text-light-body-emphasis btn-hover-bg-dark btn-hover-text-light fw-500 post-cat position-absolute top-100 start-50 translate-middle py-2 px-7 border-0" title="Life style">Life style</a>
                        </figure>
                        <div class="card-body text-center px-md-9 py-0">
                            <h4 class="card-title lh-base mb-9"><a class="text-decoration-none" href="../blog/details-01.html" title="Why You Should Try an Overnight Balm">Why You Should Try an Overnight Balm</a></h4>
                            <ul class="post-meta list-inline lh-1 d-flex flex-wrap justify-content-center m-0">
                                <li class="list-inline-item border-end pe-5 me-5">By <a href="#" title="Admin">Admin</a></li>
                                <li class="list-inline-item">Jan 19th, 2021</li>
                            </ul>
                        </div>
                    </article>
                </div>
                <div class="col-md-6 col-lg-4">
                    <article class="card card-post-grid-1 bg-transparent border-0 animate__fadeInUp animate__animated" data-animate="fadeInUp">
                        <figure class="card-img-top position-relative mb-10">
                            <a href="#" class="hover-shine hover-zoom-in d-block" title="Our Favorite Multitasking Products">
                                <img data-src="../assets/images/blog/post-08-370x450.jpg" class="img-fluid w-100 loaded" alt="Our Favorite Multitasking Products" width="370" height="450" src="../assets/images/blog/post-08-370x450.jpg" loading="lazy" data-ll-status="loaded">
                            </a><a href="#" class="post-item-cate btn btn-light btn-text-light-body-emphasis btn-hover-bg-dark btn-hover-text-light fw-500 post-cat position-absolute top-100 start-50 translate-middle py-2 px-7 border-0" title="Life style">Life style</a>
                        </figure>
                        <div class="card-body text-center px-md-9 py-0">
                            <h4 class="card-title lh-base mb-9"><a class="text-decoration-none" href="../blog/details-01.html" title="Our Favorite Multitasking Products">Our Favorite Multitasking Products</a></h4>
                            <ul class="post-meta list-inline lh-1 d-flex flex-wrap justify-content-center m-0">
                                <li class="list-inline-item border-end pe-5 me-5">By <a href="#" title="Admin">Admin</a></li>
                                <li class="list-inline-item">Jan 19th, 2021</li>
                            </ul>
                        </div>
                    </article>
                </div>
                <div class="col-md-6 col-lg-4">
                    <article class="card card-post-grid-1 bg-transparent border-0 animate__fadeInUp animate__animated" data-animate="fadeInUp">
                        <figure class="card-img-top position-relative mb-10">
                            <a href="#" class="hover-shine hover-zoom-in d-block" title="Which foundation formula is right for your skin?">
                                <img data-src="../assets/images/blog/post-09-370x450.jpg" class="img-fluid w-100 loaded" alt="Which foundation formula is right for your skin?" width="370" height="450" src="../assets/images/blog/post-09-370x450.jpg" loading="lazy" data-ll-status="loaded">
                            </a><a href="#" class="post-item-cate btn btn-light btn-text-light-body-emphasis btn-hover-bg-dark btn-hover-text-light fw-500 post-cat position-absolute top-100 start-50 translate-middle py-2 px-7 border-0" title="Life style">Life style</a>
                        </figure>
                        <div class="card-body text-center px-md-9 py-0">
                            <h4 class="card-title lh-base mb-9"><a class="text-decoration-none" href="../blog/details-01.html" title="Which foundation formula is right for your skin?">Which foundation formula is right for your skin?</a></h4>
                            <ul class="post-meta list-inline lh-1 d-flex flex-wrap justify-content-center m-0">
                                <li class="list-inline-item border-end pe-5 me-5">By <a href="#" title="Admin">Admin</a></li>
                                <li class="list-inline-item">Jan 19th, 2021</li>
                            </ul>
                        </div>
                    </article>
                </div>
                <div class="col-md-6 col-lg-4">
                    <article class="card card-post-grid-1 bg-transparent border-0" data-animate="fadeInUp">
                        <figure class="card-img-top position-relative mb-10">
                            <a href="#" class="hover-shine hover-zoom-in d-block" title="A User-Friendly Guide to Natural Cleansers">
                                <img data-src="../assets/images/blog/post-10-370x450.jpg" class="img-fluid w-100 loaded" alt="A User-Friendly Guide to Natural Cleansers" width="370" height="450" src="../assets/images/blog/post-10-370x450.jpg" loading="lazy" data-ll-status="loaded">
                            </a><a href="#" class="post-item-cate btn btn-light btn-text-light-body-emphasis btn-hover-bg-dark btn-hover-text-light fw-500 post-cat position-absolute top-100 start-50 translate-middle py-2 px-7 border-0" title="Life style">Life style</a>
                        </figure>
                        <div class="card-body text-center px-md-9 py-0">
                            <h4 class="card-title lh-base mb-9"><a class="text-decoration-none" href="../blog/details-01.html" title="A User-Friendly Guide to Natural Cleansers">A User-Friendly Guide to Natural Cleansers</a></h4>
                            <ul class="post-meta list-inline lh-1 d-flex flex-wrap justify-content-center m-0">
                                <li class="list-inline-item border-end pe-5 me-5">By <a href="#" title="Admin">Admin</a></li>
                                <li class="list-inline-item">Jan 19th, 2021</li>
                            </ul>
                        </div>
                    </article>
                </div>
                <div class="col-md-6 col-lg-4">
                    <article class="card card-post-grid-1 bg-transparent border-0" data-animate="fadeInUp">
                        <figure class="card-img-top position-relative mb-10">
                            <a href="#" class="hover-shine hover-zoom-in d-block" title="Why You Should Try an Overnight Balm">
                                <img data-src="../assets/images/blog/post-11-370x450.jpg" class="img-fluid w-100 loaded" alt="Why You Should Try an Overnight Balm" width="370" height="450" src="../assets/images/blog/post-11-370x450.jpg" loading="lazy" data-ll-status="loaded">
                            </a><a href="#" class="post-item-cate btn btn-light btn-text-light-body-emphasis btn-hover-bg-dark btn-hover-text-light fw-500 post-cat position-absolute top-100 start-50 translate-middle py-2 px-7 border-0" title="Life style">Life style</a>
                        </figure>
                        <div class="card-body text-center px-md-9 py-0">
                            <h4 class="card-title lh-base mb-9"><a class="text-decoration-none" href="../blog/details-01.html" title="Why You Should Try an Overnight Balm">Why You Should Try an Overnight Balm</a></h4>
                            <ul class="post-meta list-inline lh-1 d-flex flex-wrap justify-content-center m-0">
                                <li class="list-inline-item border-end pe-5 me-5">By <a href="#" title="Admin">Admin</a></li>
                                <li class="list-inline-item">Jan 19th, 2021</li>
                            </ul>
                        </div>
                    </article>
                </div>
                <div class='col-md-6 col-lg-4'>
                    <article class='card card-post-grid-1 bg-transparent border-0' data-animate='fadeInUp'>
                        <figure class='card-img-top position-relative mb-10'>
                            <a href='#' class='hover-shine hover-zoom-in d-block' title='How To Choose The Right Sofa for your home'>
                                <img data-src='../assets/images/blog/post-12-370x450.jpg' class='img-fluid w-100 loaded' alt='How To Choose The Right Sofa for your home' width='370' height='450' src='../assets/images/blog/post-12-370x450.jpg' loading='lazy' data-ll-status='loaded'>
                            </a>
                   <a href='#' class='post-item-cate btn btn-light btn-text-light-body-emphasis btn-hover-bg-dark btn-hover-text-light fw-500 post-cat position-absolute top-100 start-50 translate-middle py-2 px-7 border-0' title='Life style'>Life style</a>
                        </figure>
                        <div class='card-body text-center px-md-9 py-0'>
                            <h4 class='card-title lh-base mb-9'><a class='text-decoration-none' href='../blog/details-01.html' title='How To Choose The Right Sofa for your home'>How To Choose The Right Sofa for your home</a></h4>
                            <ul class='post-meta list-inline lh-1 d-flex flex-wrap justify-content-center m-0'>
                                <li class='list-inline-item border-end pe-5 me-5'>By <a href='#' title='Admin'>Admin</a></li>
                                <li class='list-inline-item'>Jan 19th, 2021</li>
                            </ul>
                        </div>
                    </article>
                </div>--%>
            </div>
            <div class="row mt-12">
                <div class="col-12 text-center">
                    <ul class="pagination pPagination justify-content-center">
                    </ul>
                </div>
            </div>
        </div>
    </main>
    <script src="assets/js/Pages/blogs.js"></script>
</asp:Content>

