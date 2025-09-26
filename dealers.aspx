<%@ Page Title="Dealers of Plywood and Veneers" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="dealers.aspx.cs" Inherits="dealers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <meta name="keywords" content="plywood dealers in india, ArchidPly Decor dealers, laminated plywood, best plywood in india, door manufacturers, veneer plywood supplier, plywood company in india, flush door manufacturers, top plywood companies, bangalore plywood" />

<meta name="description" content="Locate verified ArchidPly Decor dealers near you. Buy high-quality plywood & doors across Bangalore and India. Call now for locations." />
    <link rel="stylesheet" href="https://unpkg.com/leaflet/dist/leaflet.css" />
    <script src="https://unpkg.com/leaflet/dist/leaflet.js"></script>
     <link rel="canonical" href='<%= Request.Url.AbsoluteUri %>' />
    <style>
        #map {
            height: 100vh;
            width: 100%;
            background: #f1f1f1
        }

        .custom-label {
            background: #007bff;
            color: white;
            padding: 5px 8px;
            border-radius: 5px;
            font-size: 12px;
            white-space: nowrap;
        }

        .form-select {
            background-color: #fff;
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }

        th, td {
            border: 1px solid black;
            padding: 8px;
            text-align: left;
        }

        th {
            background-color: #f2f2f2;
        }

        .dealer-list h2 {
            font-size: 24px !important;
            margin-bottom: 20px;
        }

        .new-sticky {
            position: sticky;
            top: 0px !important;
        }
                    .contact-card {
     padding: 40px 20px;
     background: #fff;
     display: flex;
     box-shadow: rgba(99, 99, 99, 0.2) 0px 2px 8px 0px;
     flex-direction: column;
     align-items: center;
 }

     .contact-card span {
         font-size: 18px;
         display: block;
         font-weight: 600;
     } .contact-card h3{
           font-size:32px ;
       }

     .contact-card p {
         font-size: 20px;
         margin-top: 5px;
     }

         .contact-card p a {
             color: #31ade3;
         }
          .bg-overlay::before{
     background:unset !important;
 }
    </style>
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
      "name": "Dealers",
      "item": "https://archidplydecor.com/dealers"
    }
  ]
}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--<section class="position-relative custom-overlay" id="about_introduction">

        <div class="lazy-bg bg-overlay position-absolute z-index-1 w-100 h-100   light-mode-img" data-bg-src="assets/imgs/investor-banner.jpg">
        </div>
        <div class="lazy-bg bg-overlay dark-mode-img position-absolute z-index-1 w-100 h-100" data-bg-src="assets/imgs/investor-banner.jpg">
        </div>

        <div class="position-relative z-index-2 container py-18 py-lg-20">

            <h2 class="fs-56px mb-7 text-white text-center">Investor </h2>
        </div>
    </section>-->

    <main id="content" class="wrapper layout-page investor-contact pt-5 pb-10" alt="Background image" style="background: #f1efec url('assets/imgs/bg-img2.png'); background-repeat: no-repeat; background-position: right; background-attachment: fixed">


        <div class="container">
            <nav class="py-2 lh-30px" aria-label="breadcrumb">
                <ol class="breadcrumb justify-content-start py-1">
                    <li class="breadcrumb-item"><a href="/">Home</a></li>

                    <li class="breadcrumb-item"><a href="/contact-us">Contact</a></li>
                    <li class="breadcrumb-item active" aria-current="page">Dealers</li>
                </ol>
            </nav>
        </div>
        <div class="container">
            <div class="row justify-content-center mt-5">
            </div>
        </div>
        <%--<div class="container">
            <div class="row justify-content-center">
                <div class="dashboard-page-content" data-animated-id="1">
                    <div class="row mb-9 align-items-center">
                        <div class="col-lg-8 mb-10 mb-sm-0 text-start">
                            <h2 class=" mb-0">Dealers</h2>
                        </div>
                        <div class="col-lg-4 col-ms-6 col-sm-12 col-12">
                            <div class="mx-auto">
                                <div class="input-group form-border-transparent d-flex">
                                    <asp:DropDownList runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" ID="ddlCity">
                                        <asp:ListItem Value="">Select City</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <%=strDealers %>
                    
                    </div>

                </div>

            </div>
        </div>--%>

        <%--for map start--%>
        

        <section class="section-padding mt-10">
            <div class="container">
                <div class="row gy-4">
                    <div class="col-lg-6 col-md-6">
                        <div class="contact-card">
                            <span>Contact Us</span>
                            <h3>Dealers – South India


                            </h3>
                            <p class="">
                                Ph :
                           <a href="tel:+919591849977">+91 9591849977 </a>/ <a href="tel: 7022012573
">7022012573</a>
                            </p>

                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="contact-card">
                            <span>Contact Us</span>
                            <h3>Dealers – Rest of India

                            </h3>
                            <p class="">
                                Ph :
                           <a href="tel:+919591849977">+91 9513325133  </a>/ <a href="tel:+917314202546
">7314202546</a>
                            </p>

                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section id="special_offer_save_on_sets_2" class="mt-10">
            <div class="container container-xxl">
                <h1 class="mb-10 mt-10 fs-2 text-center">Want to Become a <span class="text-primary">Dealer?</span></h1>
                <div class="row align-items-center">
                    <div class="container text-center col-lg-6 px-0  order-2">
                        <%--<p class="mw-lg-100 fs-17px text-justify">Are you looking to grow your business by partnering with a trusted brand? Becoming a dealer with us gives you access to:</p>--%>
                        <h4 class="mb-10 fs-4">Join Our Dealer Network Today!</h4>

                        <div class="contact-form" method="post" action="#">
                            <div class="row mb-8 mb-md-10">
                                <div class="col-md-6 col-12 mb-8 mb-md-0">
                                    <asp:TextBox runat="server" MaxLength="100" ID="txtName" CssClass="form-control acceptOnlyAlpha" placeholder="Name*" data-val-id="rfv1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtName" SetFocusOnError="true" ValidationGroup="ContactUs" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6 col-12">
                                    <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control onlyNum" MaxLength="10" placeholder="Phone*" data-val-id="rfv2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv4" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhone" SetFocusOnError="true" ValidationGroup="ContactUs" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6 mt-8 col-12">
                                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" MaxLength="200" placeholder="Email*" data-val-id="rfv2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmail" SetFocusOnError="true" ValidationGroup="ContactUs" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="ContactUs" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Invalid E-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-6 mt-8 col-12">
                                    <asp:TextBox runat="server" ID="txtCity" CssClass="form-control acceptOnlyAlpha" MaxLength="10" placeholder="City*" data-val-id="rfv2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCity" SetFocusOnError="true" ValidationGroup="ContactUs" ErrorMessage="Fields can't be blank"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox ID="txtMessage" TextMode="MultiLine" Rows="7" placeholder="Message" runat="server" CssClass="form-control mb-6"></asp:TextBox>
                            <div class="mt-1">
                            <asp:Button runat="server" ValidationGroup="ContactUs" OnClick="submit_Click" ID="submit" CssClass="btn btn-dark btn-hover-bg-primary btn-hover-border-primary px-11 mt-10 mb-10" Text="Submit" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>
                <section class="container">
            <h2 class=" mb-lg-15 mb-5 text-center">Find Our Dealers Near You</h2>
            <div class="row">
                <div class="col-lg-7">
                    <div class="new-sticky">
                        <div id="map"></div>
                    </div>
                </div>
                <div class="col-lg-5">
                    <div class="dealer-list">
                        <h2>Dealers List</h2>
<table>
    <tr>
        <th>City</th>
    </tr>
    <tr><td>Ahmedabad</td></tr>
    <tr><td>Anantpur</td></tr>
    <tr><td>Bangalore</td></tr>
    <tr><td>Belgaum</td></tr>
    <tr><td>Bellary</td></tr>
    <tr><td>Bhilai</td></tr>
    <tr><td>Bhopal</td></tr>
    <tr><td>Bhopal</td></tr>
    <tr><td>Bilaspur</td></tr>
    <tr><td>Bhubaneswar</td></tr>
    <tr><td>Brahmapur</td></tr>
    <tr><td>Chennai</td></tr>
    <tr><td>Chikmagalur</td></tr>
    <tr><td>Coimbatore</td></tr>
    <tr><td>Davangere</td></tr>
    <tr><td>Dehradun</td></tr>
    <tr><td>Delhi</td></tr>
    <tr><td>Dharmapuri</td></tr>
    <tr><td>Guntur</td></tr>
    <tr><td>Guwahati</td></tr>
    <tr><td>Gwalior</td></tr>
    <tr><td>Hubli</td></tr>
    <tr><td>Hyderabad</td></tr>
    <tr><td>Indore</td></tr>
    <tr><td>Jabalpur</td></tr>
    <tr><td>Jammu</td></tr>
    <tr><td>Kanpur</td></tr>
    <tr><td>Kakinada</td></tr>
    <tr><td>Kolkata</td></tr>
    <tr><td>Krishnagiri</td></tr>
    <tr><td>Lucknow</td></tr>
    <tr><td>Mangalore</td></tr>
    <tr><td>Mohali, Ludhiana</td></tr>
    <tr><td>Mumbai</td></tr>
    <tr><td>Mysore</td></tr>
    <tr><td>Namakkal</td></tr>
    <tr><td>Nellore</td></tr>
    <tr><td>Panchkula, Faridabad</td></tr>
    <tr><td>Pondicherry</td></tr>
    <tr><td>Prayagraj</td></tr>
    <tr><td>Pune</td></tr>
    <tr><td>Raipur</td></tr>
    <tr><td>Rajahmundry</td></tr>
    <tr><td>Ratlam</td></tr>
    <tr><td>Salem</td></tr>
    <tr><td>Sonepur</td></tr>
    <tr><td>Surat</td></tr>
    <tr><td>Tiruppur</td></tr>
    <tr><td>Udaipur, Jaipur</td></tr>
    <tr><td>Vadodara</td></tr>
    <tr><td>Varanasi</td></tr>
    <tr><td>Vijayawada</td></tr>
</table>


                    </div>
                </div>
            </div>
        </section>

        <script>
            var map = L.map('map', {
                center: [22.3511148, 78.6677428], // Center of India
                zoom: 5,
                minZoom: 4,
                maxZoom: 10,
                zoomControl: true,
                scrollWheelZoom: true,
                doubleClickZoom: true,
                touchZoom: true,
                dragging: true
            });

            // Restrict users to India's bounds
            var indiaBounds = [
                [6.746, 68.162],  // Southwest corner
                [35.674, 97.395]  // Northeast corner
            ];
            map.setMaxBounds(indiaBounds);
            map.on('drag', function () { map.panInsideBounds(indiaBounds, { animate: false }); });

            // Load India GeoJSON for state boundaries
            fetch("https://raw.githubusercontent.com/geohacker/india/master/state/india_telengana.geojson")
                .then(response => response.json())
                .then(data => {
                    L.geoJson(data, {
                        style: function (feature) {
                            return {
                                color: "#242020",
                                weight: 1,
                                fillColor: "#fff",
                                fillOpacity: 0.7
                            };
                        }
                    }).addTo(map);
                });

            // Define category colors
           /* var categories = {
                "Chintamani unit": "blue",
                "Registered Office": "red",
                "Marketing Office": "yellow"
            };
*/


            // Locations
            var locations = [
                // Special category locations
               // { state: "Karnataka", lat: 13.416243, lon: 77.288418, cities: ["Chintamani, Karnataka, India"], category: "Chintamani unit" },
               // { state: "Karnataka", lat: 15.3173, lon: 75.7139, cities: ["Bangalore, India"], category: "Marketing Office" },
                //{ state: "Uttarakhand", lat: 30.0668, lon: 79.0193, cities: ["Uttarakhand, India"], category: "Registered Office" },

                // Normal locations (no categories)

                { state: "Jammu & Kashmir", lat: 32.7266, lon: 74.8570, cities: ["Jammu"] },
                { state: "Uttarakhand", lat: 30.3165, lon: 78.0322, cities: ["Dehradun"] },
                { state: "Punjab", lat: 30.9010, lon: 75.8573, cities: ["Mohali", "Ludhiana"] },
                { state: "Haryana", lat: 30.6942, lon: 76.8606, cities: ["Panchkula", "Faridabad"] },
                { state: "Delhi", lat: 28.7041, lon: 77.1025, cities: ["Delhi"] },
                { state: "Rajasthan", lat: 26.9124, lon: 75.7873, cities: ["Udaipur", "Jaipur"] },
                { state: "Assam", lat: 26.1445, lon: 91.7362, cities: ["Guwahati"] },
                { state: "Uttar Pradesh", lat: 26.8467, lon: 80.9462, cities: ["Lucknow", "Varanasi", "Kanpur", "Prayagraj"] },
                { state: "West Bengal", lat: 22.5726, lon: 88.3639, cities: ["Kolkata"] },
                { state: "Gujarat", lat: 23.0225, lon: 72.5714, cities: ["Ahmedabad", "Vadodara", "Surat"] },
                { state: "Madhya Pradesh", lat: 22.9734, lon: 75.8577, cities: ["Jabalpur", "Indore", "Ratlam", "Bhopal", "Gwalior"] },
                { state: "Maharashtra", lat: 19.0760, lon: 72.8777, cities: ["Pune", "Mumbai"] },
                { state: "Odisha", lat: 20.2961, lon: 85.8245, cities: ["Bhubaneswar", "Brahmapur", "Sonepur"] },
                { state: "Chhattisgarh", lat: 21.2514, lon: 81.6296, cities: ["Raipur", "Bilaspur", "Bhilai"] },
                { state: "Karnataka", lat: 14.520447, lon: 75.643303, cities: ["Bangalore", "Belgaum", "Mangalore", "Davangere", "Hubli", "Chikmagalur", "Mysore", "Bellary"] },
                { state: "Andhra Pradesh & Telangana", lat: 16.5062, lon: 80.6480, cities: ["Vijayawada", "Anantapur", "Guntur", "Rajahmundry", "Hyderabad", "Kakinada", "Nellore"] },
                { state: "Tamil Nadu & Puducherry", lat: 13.0827, lon: 80.2707, cities: ["Chennai", "Coimbatore", "Salem", "Pondicherry", "Tiruppur", "Namakkal", "Dharmapuri", "Krishnagiri"] }
            ];

            locations.forEach(location => {
                // var iconUrl = "https://unpkg.com/leaflet@1.9.4/dist/images/marker-icon.png"; // Default icon
                var iconUrl = "/images_/pin.png"; // Default icon

/*                if (location.category && categories[location.category]) {
                    if (categories[location.category] == "yellow") {
                        iconUrl = `/images_/yello.png`;

                    }
                    else if (categories[location.category] == "blue") {
                        iconUrl = `/images_/blue.png`;

                    }
                    else {
                        iconUrl = `/images_/red.png`;

                    }
                    //  iconUrl = `https://maps.google.com/mapfiles/ms/icons/${categories[location.category]}-dot.png`;
                }*/

                var icon = L.icon({
                    iconUrl: iconUrl,
                    iconSize: [32, 32]
                });

                L.marker([location.lat, location.lon], { icon: icon })
                    .addTo(map)
                    .bindPopup(`<b>${location.state}</b><br>${location.cities.join(", ")}`);
            });

            // Legend for categories
            /*var legend = L.control({ position: 'bottomleft' });
            legend.onAdd = function (map) {
                var div = L.DomUtil.create('div', 'info legend');
                var categoriesList = ['Registered Office', 'Marketing Office', 'Chintamani unit'];

                categoriesList.forEach(category => {
                    div.innerHTML += `<i style="background:${categories[category]}; width:15px; height:15px; display:inline-block; margin-right:5px;"></i> ${category}<br>`;
                });

                return div;
            };
            legend.addTo(map);*/
        </script>

        <%--<script>
            var map = L.map('map', {
                center: [22.3511148, 78.6677428], // Center India
                zoom: 5,
                minZoom: 4,
                maxZoom: 10,
                zoomControl: true, // Enable zoom buttons
                scrollWheelZoom: true, // Enable scroll zoom
                doubleClickZoom: true, // Enable double-click zoom
                touchZoom: true, // Enable touch zoom
                dragging: true // Allow panning within India
            });

            // Restrict users to India's bounds
            var indiaBounds = [
                [6.746, 68.162],  // Southwest corner
                [35.674, 97.395]  // Northeast corner
            ];
            map.setMaxBounds(indiaBounds);
            map.on('drag', function () { map.panInsideBounds(indiaBounds, { animate: false }); });

            // Load India GeoJSON for state boundaries
            fetch("https://raw.githubusercontent.com/geohacker/india/master/state/india_telengana.geojson")
                .then(response => response.json())
                .then(data => {
                    L.geoJson(data, {
                        style: function (feature) {
                            return {
                                color: "#242020",  // Black outline
                                weight: 1,
                                fillColor: "#fff", // Orange fill for India
                                fillOpacity: 0.7
                            };
                        }
                    }).addTo(map);
                });

            // Define locations with markers
            var locations = [
                { state: "Jammu & Kashmir", lat: 32.7266, lon: 74.8570, cities: ["Jammu"] },
                { state: "Uttarakhand", lat: 30.3165, lon: 78.0322, cities: ["Dehradun"] },
                { state: "Punjab", lat: 30.9010, lon: 75.8573, cities: ["Mohali", "Ludhiana"] },
                { state: "Haryana", lat: 30.6942, lon: 76.8606, cities: ["Panchkula", "Faridabad"] },
                { state: "Delhi", lat: 28.7041, lon: 77.1025, cities: ["Delhi"] },
                { state: "Rajasthan", lat: 26.9124, lon: 75.7873, cities: ["Udaipur", "Jaipur"] },
                { state: "Assam", lat: 26.1445, lon: 91.7362, cities: ["Guwahati"] },
                { state: "Uttar Pradesh", lat: 26.8467, lon: 80.9462, cities: ["Lucknow", "Varanasi", "Kanpur", "Prayagraj"] },
                { state: "West Bengal", lat: 22.5726, lon: 88.3639, cities: ["Kolkata"] },
                { state: "Gujarat", lat: 23.0225, lon: 72.5714, cities: ["Ahmedabad", "Vadodara", "Surat"] },
                { state: "Madhya Pradesh", lat: 22.7196, lon: 75.8577, cities: ["Jabalpur", "Indore", "Ratlam", "Bhopal", "Gwalior"] },
                { state: "Maharashtra", lat: 19.0760, lon: 72.8777, cities: ["Pune", "Mumbai"] },
                { state: "Odisha", lat: 20.2961, lon: 85.8245, cities: ["Bhubaneswar", "Brahmapur", "Sonepur"] },
                { state: "Chhattisgarh", lat: 21.2514, lon: 81.6296, cities: ["Raipur", "Bilaspur", "Bhilai"] },
                { state: "Karnataka", lat: 12.9716, lon: 77.5946, cities: ["Bangalore", "Belgaum", "Mangalore", "Davangere", "Hubli", "Chikmagalur", "Mysore", "Bellary"] },
                { state: "Andhra Pradesh & Telangana", lat: 16.5062, lon: 80.6480, cities: ["Vijayawada", "Anantapur", "Guntur", "Rajahmundry", "Hyderabad", "Kakinada", "Nellore"] },
                { state: "Tamil Nadu & Puducherry", lat: 13.0827, lon: 80.2707, cities: ["Chennai", "Coimbatore", "Salem", "Pondicherry", "Tiruppur", "Namakkal", "Dharmapuri", "Krishnagiri"] }
            ];

            // Add markers to the map
            locations.forEach(location => {
                L.marker([location.lat, location.lon])
                    .addTo(map)
                    .bindPopup(`<b>${location.state}</b><br>${location.cities.join(", ")}`);
            });

    </script>--%>
    </main>
</asp:Content>

