// No special characters, only alphanumeric (spaces allowed)
$('.nospecial').keypress(function (e) {
    var charCode = (e.which) ? e.which : event.keyCode;
    var $input = $(this);
    if (charCode != 32 && !String.fromCharCode(charCode).match(/^[a-zA-Z0-9]*$/g)) {
        // Prevent special character input
        return false;
    } else {
        // Replace any non-alphanumeric characters
        var val = $input.val();
        val = val.replace(/[^a-zA-Z0-9 ]/g, "");  // Remove special characters
        $input.val(val);
    }
});
// Only numeric characters
$('.onlyNum').keypress(function (e) {
    var charCode = (e.which) ? e.which : event.keyCode;
    var $input = $(this);
    if (String.fromCharCode(charCode).match(/[^0-9]/g)) {
        // Prevent non-numeric input
        return false;
    } else {
        // Remove non-numeric characters already present
        var val = $input.val();
        val = val.replace(/[^0-9]/g, "");
        $input.val(val);
    }
});
// Numbers with periods (decimal values)
$('.numWPts').keypress(function (e) {
    var charCode = (e.which) ? e.which : event.keyCode;
    var $input = $(this);
    if (String.fromCharCode(charCode).match(/[^0-9\.]/g)) {
        // Prevent non-numeric and non-period input
        return false;
    } else {
        // Allow numeric and decimal point, but replace invalid characters
        var val = $input.val();
        val = val.replace(/[^0-9\.]/g, "");  // Remove any non-numeric, non-period characters
        $input.val(val);
    }
});
// Only alphabetic characters and spaces
$('.onlyAlpha').keypress(function (e) {
    var charCode = (e.which) ? e.which : event.keyCode;
    var $input = $(this);
    if (!String.fromCharCode(charCode).match(/^[a-zA-Z\s]*$/g)) {
        // Prevent non-alphabetic input
        return false;
    } else {
        // Replace any non-alphabetic characters
        var val = $input.val();
        val = val.replace(/[^a-zA-Z\s]/g, "");  // Remove any non-alphabetic characters
        $input.val(val);
    }
});