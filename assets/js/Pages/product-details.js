$(document).ready(function () {
    $("#txtFullName, #txtPhone, #txtEmail").on("input", function () {
        $(this).next(".error-message").remove();
    });

    $(document.body).on("click", "#btnSendMessage", function (e1) {
        e1.preventDefault();
        var elem = $(this);
        elem.prop("disabled", true).text("Please wait...");

        $(".error-message").remove();

        var name = $("#txtFullName").val().trim();
        var phone = $("#txtPhone").val().trim();
        var email = $("#txtEmail").val().trim();
        var message = $("#txtMessage").val().trim();
        var city = $("#txtCity").val().trim();
        var products = $("input[id$='HiddenField1']").val();
        var isValid = true;

        if (name === "") {
            $("#txtFullName").after('<span class="error-message text-danger">Name is required.</span>');
            isValid = false;
        } if (city === "") {
            $("#txtCity").after('<span class="error-message text-danger">City is required.</span>');
            isValid = false;
        }

        if (phone === "") {
            $("#txtPhone").after('<span class="error-message text-danger">Phone number is required.</span>');
            isValid = false;
        } else if (!/^\d{10}$/.test(phone)) {
            $("#txtPhone").after('<span class="error-message text-danger">Enter a valid 10-digit phone number.</span>');
            isValid = false;
        }

        if (email === "") {
            $("#txtEmail").after('<span class="error-message text-danger">Email is required.</span>');
            isValid = false;
        } else if (!/^[\w.-]+@[\w.-]+\.\w{2,}$/.test(email)) {
            $("#txtEmail").after('<span class="error-message text-danger">Enter a valid email address.</span>');
            isValid = false;
        }

        if (!isValid) {
            elem.prop("disabled", false).text("Send Message");
            return;
        }

        $.ajax({
            type: 'POST',
            url: '/product-detail.aspx/SendProductEnquiry',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ name: name, phone: phone, email: email, message: message, products: products, city: city }),
            success: function (response) {
                if (response.d === "Success") {
                    Snackbar.show({ pos: 'top-right', text: 'We’ll get back to you shortly!', actionTextColor: '#fff', backgroundColor: '#008a3d' });
                    $("#txtFullName, #txtPhone, #txtEmail, #txtMessage,#txtCity").val("");
                    $(".error-message").remove();
                    $("#btn-closee").click();
                } else {
                    Snackbar.show({ pos: 'top-right', text: 'There is some problem now. Please try later.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                }
                elem.prop("disabled", false).text("Send Message");
            },
            error: function () {
                Snackbar.show({ pos: 'top-right', text: 'Something went wrong. Try again.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
            },
            elem.prop("disabled", false).text("Send Message");
        });
    });
});


//$(document).ready(function () {
//    $(document.body).on("click", ".submitdata", function (e) {
//        e.preventDefault();

//        if (
//            $(".textFname").val().trim() !== "" &&
//            $(".txtemailAdress").val().trim() !== "" &&
//            $(".txtProfession").val().trim() !== "" &&
//            $(".txtContact").val().trim() !== ""
//        ) {
//            // if ($(".textFname").val() || $(".txtemailAdress").val() || $(".txtContact").val() !== "") {
//            var name = $(".textFname").val().trim();
//            var email = $(".txtemailAdress").val().trim();
//            var contact = $(".txtContact").val().trim();
//            var prof = $(".txtProfession").val().trim();
//            $.ajax({
//                type: 'POST',
//                url: "product-details.aspx/SaveDownloadEnquiry",
//                contentType: 'application/json; charset=utf-8',
//                dataType: "json",
//                data: JSON.stringify({
//                    name: name, email: email, contact: contact, prof: prof
//                }),
//                success: function (data2) {
//                    var result = data2.d;
//                    if (result == "Success ") {
//                        Snackbar.show({ pos: 'top-right', text: 'Thank you for Submitting.', actionTextColor: '#fff', backgroundColor: '#008a3d' });
//                        $('.error-message').addClass("d-none");
//                        $(".textFname").val("");
//                        $(".txtemailAdress").val("");
//                        $(".txtContact").val("");
//                        $(".txtProfession").val("");
//                        $('.btn-close').trigger('click');
//                        var a = '/images_/others/venner-catelog.pdf';
//                        a.href = pdf;
//                        a.download = '';
//                        document.body.appendChild(a);
//                        a.click();
//                        document.body.removeChild(a);
//                    } else {
//                        Snackbar.show({
//                            pos: 'top-right', text: 'Oops...! There is some problem right now. Please try again later.', actionTextColor: '#fff', backgroundColor: '#ea1c1c'
//                        });
//                    }
//                }
//            });
//        }
//        else {
//            $('.error-message').removeClass("d-none");
//            $('.error-message').text('Please fill all the fields.');
//        }
//    });
//});


