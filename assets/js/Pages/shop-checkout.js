
$(document).ready(function () {
    $("[id$='DeliveryDiv']").hide();

  /*  if ($("[id$='txtDelCity']").val() !== "") {
        CheckthePincode();
    }*/

/*
    if ($("[id*=customCheck5]").prop("checked")) {
        copyAddressFields();
    }
*/

    $("[id*=customCheck5]").change(function () {
        if ($(this).prop("checked")) {
            copyAddressFields();
            $("[id$='DeliveryDiv']").hide();
        } else {
            $("[id$='DeliveryDiv']").show();
        }
    });
    $("[id*=txtAddress1], [id*=txtAddress2], [id*=txtCity], [id*=txtState], [id*=txtZip], [id*=txtEmail], [id*=txtPhone]").on("input", function () {
        if ($("[id*=customCheck5]").prop("checked")) {
            copyAddressFields();
        }
    });
    $(document.body).on("change", "[id$='txtDelCity']", function (e) {
        CheckthePincode();
    });

    $(document.body).on("change", "[id$='txtZip']", function (e) {
        $("[id$='txtZip']").parent().find(".error").remove();
        if ($("[id$='txtZip']").val().length != 6) {
            $("[id$='txtZip']").parent().append("<span class='error text-danger'>Enter 6 digit valid pincode</span>");
        } else {
            var val = $(this).val();
            $.ajax({
                type: 'POST',
                url: 'shop-checkout.aspx/PincodeValidation',
                data: "{val:'" + val + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                async: false,
                success: function (res) {
                    var data = res.d.split('|');
                    if (data[0] === "S") {
                        $("[id$='txtCity']").val(data[1]);
                        $("[id$='txtState']").val(data[2]);
                        CheckthePincode();
                        copyAddressFields();
                    }
                }
            });
            $("[id$='txtZip']").parent().find(".error").remove();
            $("[id$='txtCity']").parent().find(".error").remove();
            $("[id$='txtState']").parent().find(".error").remove();
            CheckthePincode();
            copyAddressFields();
        }
    });

    $(document.body).on("change", "[id$='txtDelZip']", function (e) {
        $("[id$='txtDelZip']").parent().find(".error").remove();
        if ($("[id$='txtDelZip']").val().length != 6) {
            $("[id$='txtDelZip']").parent().append("<span class='error text-danger'>Enter 6 digit valid pincode</span>");
        } else {
            var val = $(this).val();
            $.ajax({
                type: 'POST',
                url: 'shop-checkout.aspx/PincodeValidation',
                data: "{val:'" + val + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                async: false,
                success: function (res) {
                    var data = res.d.split('|');
                    if (data[0] === "S") {
                        $("[id$='txtDelCity']").val(data[1]);
                        $("[id$='txtDelState']").val(data[2]);
                        CheckthePincode();
                    }
                }
            });
            $("[id$='txtDelZip']").parent().find(".error").remove();
            $("[id$='txtDelCity']").parent().find(".error").remove();
            $("[id$='txtDelState']").parent().find(".error").remove();
            CheckthePincode();
        }
    });
});
function copyAddressFields() {
    $("[id*=txtDelAddress1]").val($("[id*=txtAddress1]").val());
    $("[id*=txtDelAddress2]").val($("[id*=txtAddress2]").val());
    $("[id*=txtDelCity]").val($("[id*=txtCity]").val());
    $("[id*=txtDelState]").val($("[id*=txtState]").val());
    $("[id*=txtDelZip]").val($("[id*=txtZip]").val());
    $("[id*=txtDelEmailID]").val($("[id*=txtEmail]").val());
    $("[id*=txtDelPhone]").val($("[id*=txtPhone]").val());
}
function CheckthePincode() {
    var city = $('[id*=txtDelCity]').val().trim().toLowerCase();
    $.ajax({
        type: 'POST',
        url: "shop-checkout.aspx/GetOperationalCities",
        data: JSON.stringify({}),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        async: false,
        success: function (response) {
            var cities = response.d;
            var isCityAvailable = false;

            if (cities.length > 0) {
                for (var i = 0; i < cities.length; i++) {
                    if (cities[i].CityName.toLowerCase() === city) {
                        isCityAvailable = true;
                        break;
                    }
                }
            }
            if (isCityAvailable) {
                $('[id*=lblCityValidation]').html("").hide();
            } else {
                $('[id*=lblCityValidation]').html("Delivery is not available for the entered pincode or city.").show();
                $('[id*=lblCityValidation]').focus();
            }
        },
        error: function (xhr, status, error) {
            Snackbar.show({
                text: 'An error occurred. Please try again.',
                pos: 'top-right',
                textColor: '#ffffff',
                actionTextColor: '#ff0000',
                backgroundColor: '#000000'
            });
        }
    });
}


