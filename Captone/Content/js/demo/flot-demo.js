//Flot Pie Chart
$(function () {
    var ca = 0;
    var onl = 0;
    $.ajax({
        url: "/Admin/PaymentChart",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                ca = result[i].Cash;
                onl = result[i].Online;
            }

            data = [{
                label: "Tiền mặt",
                data: ca
            }, {
                label: "Chuyển khoản",
                data: onl
            }];

            var plotObj = $.plot($("#flot-pie-chart"), data, {
                series: {
                    pie: {
                        show: true
                    }
                },
                grid: {
                    hoverable: true
                },
                tooltip: true,
                tooltipOpts: {
                    content: "%s: %p.0%, ", // show percentages, rounding to 2 decimal places
                    shifts: {
                        x: 20,
                        y: 0
                    },
                    defaultTheme: false
                }
            });
        }
    });
});

//Flot Bar Chart

$(function () {

    var barOptions = {
        series: {
            bars: {
                show: true,
                barWidth: 10
            }
        },
        grid: {
            hoverable: true
        },
        legend: {
            show: false
        },
        tooltip: true,
        tooltipOpts: {
            content: "x: %x, y: %y"
        }
    };
    var barData = {
        label: "bar",
        data: [
            [10, 50],
            [20, 70],
            [30, 100],
            [50, 150],
            [75, 185],
            [100, 200]
        ]
    };
    $.plot($("#flot-bar-chart"), [barData], barOptions);

});
