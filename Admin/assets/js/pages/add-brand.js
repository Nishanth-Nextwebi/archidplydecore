$(document).ready(function () {
    $(".txtProdName").change(function () {
        $(".txtURL").val($(".txtProdName").val().toLowerCase().replace(/\./g, '').replace(/\//g, '').replace(/\\/g, '').replace(/\*/g, '').replace(/\?/g, '').replace(/\~/g, '').replace(/\ /g, '-'));
    });
    $(".textcount1").on('keyup', function (event) {
        var elem = $(this);
        var tps = elem.attr("data-id");
        var len = elem.val().length;
        elem.siblings('span').text("Character count : " + len);
        if (tps === "Title") {
            if (len > 60) {
                elem.siblings('span').css("color", "red");
            }
            else {
                elem.siblings('span').css("color", "green");
            }
        }
        else if (tps === "MetaDesc") {
            if (len > 160) {
                elem.siblings('span').css("color", "red");
            }
            else {
                elem.siblings('span').css("color", "green");
            }
        }
    });
});