$(document).ready(function () {

    $(document).on('click', '.hidenId', function () {
        var dataId = $(this).attr('data-id');
        $('.BtnSubmit').attr('data-id', dataId);
    });
    $(document.body).on("click", ".submitdata", function (e) {
        e.preventDefault();
        if (
            $(".textFname").val().trim() !== "" &&
            $(".txtemailAdress").val().trim() !== "" &&
            $(".txtProfession").val().trim() !== "" &&
            $(".txtContact").val().trim() !== ""
        ) {
            var id = $('.BtnSubmit').attr('data-id');
            var name = $(".textFname").val().trim();
            var email = $(".txtemailAdress").val().trim();
            var contact = $(".txtContact").val().trim();
            var prof = $(".txtProfession").val().trim();
            $.ajax({
                type: 'POST',
                url: "Default.aspx/SaveDownloadEnquiry",
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                data: JSON.stringify({
                    name: name, email: email, contact: contact, Id: id, prof: prof
                }),
                success: function (data2) {
                    var result = data2.d;
                    if (result.split('|')[0] == "Success ") {
                        Snackbar.show({ pos: 'top-right', text: 'Thank you for Submitting.', actionTextColor: '#fff', backgroundColor: '#008a3d' });
                        $('.error-message').addClass("d-none");
                        $(".textFname").val("");
                        $(".txtemailAdress").val("");
                        $(".txtContact").val("");
                        $(".txtProfession").val("");
                        $('.btn-close').trigger('click');
                        var pdf = result.split('|')[1].trim();
                        if (pdf) {
                            var a = document.createElement('a');
                            a.href = pdf;
                            a.download = '';
                            document.body.appendChild(a);
                            a.click();
                            document.body.removeChild(a);
                        }
                    } else {
                        Snackbar.show({
                            pos: 'top-right', text: 'Oops...! There is some problem right now. Please try again later.', actionTextColor: '#fff', backgroundColor: '#ea1c1c'
                        });
                    }
                }
            });
        }
        else {
            $('.error-message').removeClass("d-none");
            $('.error-message').text('Please fill all the fields.');
        }
    });



    $("#txtName, #txtPhone, #txtEmail").on("input", function () {
        $(this).next(".error-message").remove();
    });

    $(document.body).on("click", "#btnSendMessage", function (e1) {
        e1.preventDefault();
        var elem = $(this);
        elem.prop("disabled", true).text("Please wait...");

        $(".error-message").remove();

        var name = $("#txtName").val().trim();
        var phone = $("#txtPhone").val().trim();
        var email = $("#txtEmail").val().trim();
        var message = $("#txtMessage").val().trim();
        var isValid = true;

        if (name === "") {
            $("#txtName").after('<span class="error-message text-danger">Name is required.</span>');
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
            url: 'Default.aspx/SaveEnquiry',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ name: name, phone: phone, email: email, message: message }),
            success: function (response) {
                if (response.d === "Success") {
                    Snackbar.show({ pos: 'top-right', text: 'We’ll get back to you shortly!', actionTextColor: '#fff', backgroundColor: '#008a3d' });
                    $("#txtName, #txtPhone, #txtEmail, #txtMessage").val("");
                    $(".error-message").remove();
                    $("#btn-closee").click();
                } else {
                    Snackbar.show({ pos: 'top-right', text: 'There is some problem now. Please try later.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                }
            },
            error: function () {
                Snackbar.show({ pos: 'top-right', text: 'Something went wrong. Try again.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
            },
           
        });
    });



    //$(document.body).on("click", "#btnSendMessage", function (e1) {
    //    e1.preventDefault();
    //    var name = $("#txtName").val().trim();
    //    var phone = $("#txtPhone").val().trim();
    //    var email = $("#txtEmail").val().trim();
    //    var message = $("#txtMessage").val().trim();

    //    if (name === "" || phone === "" || email === "") {
    //        Snackbar.show({ pos: 'top-right', text: 'Please fill all required fields.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
    //        return;
    //    }
    //    if (!/^\d{10}$/.test(phone)) {
    //        Snackbar.show({ pos: 'top-right', text: 'Enter a valid 10-digit phone number.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
    //        return;
    //    }
    //    if (!/^[\w.-]+@[\w.-]+\.\w{2,}$/.test(email)) {
    //        Snackbar.show({ pos: 'top-right', text: 'Enter a valid email address.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
    //        return;
    //    }
    //    $.ajax({
    //        type: 'POST',
    //        url: 'SubmitEnquiry.aspx/SaveEnquiry',
    //        contentType: 'application/json; charset=utf-8',
    //        dataType: 'json',
    //        data: JSON.stringify({ name: name, phone: phone, email: email, message: message }),
    //        success: function (response) {
    //            if (response.d === "Success") {
    //                Snackbar.show({ pos: 'top-right', text: 'We’ll get back to you shortly!', actionTextColor: '#fff', backgroundColor: '#008a3d' });
    //                $("#enquiryForm")[0].reset();
    //                $('#quickEnquiryModal').modal('hide');
    //            } else {
    //                Snackbar.show({ pos: 'top-right', text: 'There is some problem now. Please try later.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
    //            }
    //        },
    //        error: function () {
    //            Snackbar.show({ pos: 'top-right', text: 'Something went wrong. Try again.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
    //        }
    //    });
    //});
});


