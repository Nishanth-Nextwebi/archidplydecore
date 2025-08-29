
$(document).ready(function () {
    var products = [];
    var MonthSale = [];
    var DayMonthNYear = [];
    var StateCounts = [];
    var StateNames = [];
    var url = 'affiliate-panel.aspx/AffiliateSaleChart';
    var key = getApiKey("aKey");


    $.ajax({
        type: 'POST',
        url: url,
        data: "{siteKey:'" + key + "'}",
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        async: false,
        success: function (data) {
            products = data.d;
            MonthSale = data.d.MonthSale;
            DayMonthNYear = data.d.DayMonthNYear;
            StateCounts = data.d.StateCounts;
            StateNames = data.d.StateNames;
        }
    });

    //State Wise Sales
    var options = {
        chart: {
            width: "100%",
            height: 380,
            type: "bar"
        },
        //plotOptions: {
        //  bar: {
        //    horizontal: true
        //  }
        //},
        dataLabels: {
            enabled: false
        },
        stroke: {
            width: 1,
            colors: ["#fff"]
        },
        series: [
            {
                name: "State", type: "bar", data: StateCounts /*[44, 55, 41, 64, 22, 43, 21]*/
            }
        ],
        xaxis: {
            categories: StateNames
            //[
            //    "Andhra Pradesh",
            //    "Arunachal Pradesh",
            //    "Chhattisgarh",
            //    "Gujarat",
            //    "Jharkhand",
            //    "Maharashtra",
            //    "Karnataka"
            //]
        },
        legend: {
            position: "right",
            verticalAlign: "top",
            containerMargin: {
                left: 35,
                right: 60
            }
        },
        colors: ['#9747FF'],
        responsive: [
            {
                breakpoint: 1000,
                options: {
                    plotOptions: {
                        bar: {
                            horizontal: false
                        }
                    },
                    legend: {
                        position: "bottom"
                    }
                }
            }
        ]
    };

    var chart = new ApexCharts(
        document.querySelector("#state-wise-sales "),
        options
    );

    chart.render();



    var options,
        options2,
        chart,
        worldemapmarkers,
        overlay,
        chartDonutBasicColors =
            (((options = {
                series: [
                    //{ name: "Orders", type: "area", data: [34, 65, 46, 68, 49, 61, 42, 44, 78, 52, 63, 67] },
                    { name: "Sales", type: "area", data: MonthSale/*[89.25, 98.58, 68.74, 108.87, 77.54, 84.03, 51.24, 28.57, 92.57, 42.36, 88.51, 36.57]*/ },
                    //{ name: "Initiated Sale", type: "area", data: MonthInitiated /*[8, 12, 7, 17, 21, 11, 5, 9, 7, 29, 12, 35]*/ },
                ],
                chart: {
                    height: 370,
                    type: "line",
                    toolbar: { show: !1 }
                },
                stroke: { curve: "straight", dashArray: [0, 0, 8], width: [2, 0, 2.2] },
                fill: { opacity: [0.1, /*0.2, 1*/] },
                markers: { size: [0, 0, 0], strokeWidth: 2, hover: { size: 4 } },
                xaxis: {
                    categories: DayMonthNYear/*["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]*/,
                    axisTicks: { show: !1 },
                    axisBorder: { show: !1 }
                },
                grid: {
                    show: !0, xaxis:
                    {
                        lines: { show: !0 }
                    },
                    yaxis:
                    {
                        lines:
                        {
                            show: !1
                        }
                    },
                    padding:
                    {
                        top: 0,
                        right: -2,
                        bottom: 15,
                        left: 10
                    }
                },
                legend: {
                    show: !0,
                    horizontalAlign: "center",
                    offsetX: 0,
                    offsetY: -5,
                    markers: { width: 9, height: 9, radius: 6 },
                    itemMargin: { horizontal: 10, vertical: 0 }
                },
                plotOptions: {
                    bar:
                        { columnWidth: "30%", barHeight: "70%" }
                },
                colors: ['#9747FF'],
                tooltip: {
                    shared: !0,
                    y: [
                        {
                            formatter: function (e) {
                                return void 0 !== e ? e.toFixed(0) : e;
                            },
                        },
                        {
                            formatter: function (e) {
                                return void 0 !== e ? "₹" + e.toFixed(2) + "k" : e;
                            },
                        },
                        {
                            formatter: function (e) {
                                return void 0 !== e ? e.toFixed(0) + " Sales" : e;
                            },
                        },
                    ],
                },
            }),
                (chart = new ApexCharts(document.querySelector("#customer_sales_charts"), options)).render())
            )


    $(".filterRev").click(function () {
        $("#customer_sales_charts").empty();
        $(".filterRev").removeClass("selected");
        $(this).addClass("selected");

        var fValue = $(this).attr("data-val");
        $.ajax({
            type: 'POST',
            url: "affiliate-panel.aspx/FilterAffiliateSalesChart",
            data: "{fValue:'" + fValue + "',siteKey:'" + key + "'}",
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            async: false,
            success: function (data) {
                products = data.d;
                MonthSale = data.d.MonthSale;
                DayMonthNYear = data.d.DayMonthNYear;

                var options1,
                    chart1
                chartDonutBasicColors1 =
                    (((options1 = {
                        series: [
                            //{ name: "Orders", type: "area", data: [34, 65, 46, 68, 49, 61, 42, 44, 78, 52, 63, 67] },
                            { name: "Sales", type: "area", data: MonthSale /*[89.25, 98.58, 68.74, 108.87, 77.54, 84.03, 51.24, 28.57, 92.57, 42.36, 88.51, 36.57]*/ },
                            //{ name: "Initiated Sale", type: "area", data: MonthInitiated /*[8, 12, 7, 17, 21, 11, 5, 9, 7, 29, 12, 35]*/ },
                        ],
                        chart: {
                            height: 370,
                            type: "line",
                            toolbar: { show: !1 }
                        },
                        stroke: { curve: "straight", dashArray: [0, 0, 8], width: [2, 0, 2.2] },
                        fill: { opacity: [0.1, /*0.2, 1*/] },
                        markers: { size: [0, 0, 0], strokeWidth: 2, hover: { size: 4 } },
                        xaxis: {
                            categories: DayMonthNYear /*["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]*/,
                            axisTicks: { show: !1 },
                            axisBorder: { show: !1 }
                        },
                        grid: {
                            show: !0, xaxis:
                            {
                                lines: { show: !0 }
                            },
                            yaxis:
                            {
                                lines:
                                {
                                    show: !1
                                }
                            },
                            padding:
                            {
                                top: 0,
                                right: -2,
                                bottom: 15,
                                left: 10
                            }
                        },
                        legend: {
                            show: !0,
                            horizontalAlign: "center",
                            offsetX: 0,
                            offsetY: -5,
                            markers: { width: 9, height: 9, radius: 6 },
                            itemMargin: { horizontal: 10, vertical: 0 }
                        },
                        plotOptions: {
                            bar:
                                { columnWidth: "30%", barHeight: "70%" }
                        },
                        colors: ['#9747FF'],
                        tooltip: {
                            shared: !0,
                            y: [],
                        },
                    }),
                        (chart1 = new ApexCharts(document.querySelector("#customer_sales_charts"), options1)).render())
                    )
            }
        });
    });

});
