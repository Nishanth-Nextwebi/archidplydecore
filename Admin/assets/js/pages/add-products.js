
$(document).ready(function () {

    $(document.body).on('click', '#AddToGallery', function () {
        var elem = $(this);
        elem.empty();
        elem.append("Please wait...");
        var type = "Image";
        var pid = $(this).attr("data-pid");
        var files = dropzone.getAcceptedFiles();
        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }
        data.append("pid", pid);
        data.append("tp", type);
        if (files.length == 0) {
            elem.empty();
            elem.append("Upload");

            Snackbar.show({ pos: 'top-right', text: 'Please select atleast one file to upload', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });

            return false;
        }
        $.ajax({
            url: "product-gallery.ashx",
            type: "POST",
            data: data,
            contentType: false,
            processData: false,
            success: function (result) {
                if (result.split('|')[0] == "Success") {
                    elem.empty();
                    elem.append("Upload");
                    Snackbar.show({ pos: 'top-right', text: 'Images added successfully.', actionTextColor: '#fff', backgroundColor: '#008a3d' });
                    dropzone.removeAllFiles();
                    BindGalleryImages(pid);
                }
                else if (result.split('|')[0] == "Permission") {
                    elem.empty();
                    elem.append("Upload");
                    Snackbar.show({ pos: 'top-right', text: 'Oops! Access denied. Contact to your administrator', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                }
                else {
                    elem.empty();
                    elem.append("Upload");
                    Snackbar.show({ pos: 'top-right', text: 'Oops! Something went wrong. Please try after some time.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                }
            },
            error: function (err) {
                elem.empty();
                elem.append("Upload");
                Snackbar.show({ pos: 'top-right', text: 'Oops! Something went wrong. Please try after some time.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
            }
        });
    });
    $(document.body).on('click', "#UpdateImgOrder", function (e) {
        var product = "";
        var elem = $(this);
        elem.empty();
        elem.val("Please wait...")
        e.preventDefault();
        $.each($('#left-defaults').find('li'), function () {
            if (product == "") {
                product = product + $(this).attr("data-id");
            }
            else {
                product = product + "|" + $(this).attr("data-id");
            }
        });
        if (product != "") {
            ArrengeCategory(product);
        }
        else {
            elem.empty();
            elem.val("Update Image Order")
        }
    });
    $(document.body).on('click', '.deleteGalleryItem', function () {
        var elem = $(this);
        var id = $(this).attr('data-id');
        var pid = $(this).attr('data-pid');
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: !0,
            confirmButtonClass: "btn btn-primary w-xs me-2 mt-2",
            cancelButtonClass: "btn btn-danger w-xs mt-2",
            confirmButtonText: "Yes, delete it!",
            buttonsStyling: !1,
            showCloseButton: !0,
        }).then(function (result) {
            if (result.value) {
                $.ajax({
                    type: 'POST',
                    url: "add-products.aspx/DeleteGallery",
                    data: "{id: '" + id + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: false,
                    success: function (data2) {
                        if (data2.d.toString() == "Success") {
                            Swal.fire({ title: "Deleted!", text: "Image has been deleted.", icon: "success", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: false })
                            BindGalleryImages(pid);
                        }
                        else if (data2.d.toString() == "Permission") {
                            Swal.fire({ title: "Oops...", text: "Permission denied! Please contact to your administrator.", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                        }
                        else {
                            Swal.fire({ title: "Oops...", text: "Something went wrong!", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                        }
                    },
                    error: function (err) {
                        Swal.fire({ title: "Oops...", text: "Something went wrong!", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                    }
                });
            }
        })
    });


    $(document.body).on("click", ".idTabGallery", function () {
        var id = $("[id*=idPid]").val();
        if (id != "") {
            $("#AddToGallery").attr("data-pid", id);
            dropzone.removeAllFiles();
            BindGalleryImages(id);
        }
    });
    $(document.body).on("click", ".idTabPrices", function () {
        BindAllPrices($('[id*=idPid]').val());

        var category = $(this).attr("data-id").toLowerCase();
        if (category === 'plywood') {
            $('.plywood').removeClass('d-none');
        } else {
            $('.plywood').addClass('d-none');
        }
    });

    $('.lvTest').fSelect();

    $('.bs-tooltip').tooltip();

    $('.fs-label').text("Select Options");

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

    $('.DecimalAcceptor').keypress(function (event) {
        var $this = $(this);
        if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
            ((event.which < 48 || event.which > 57) &&
                (event.which != 0 && event.which != 8))) {
            event.preventDefault();
        }

        var text = $(this).val();
        if ((event.which == 46) && (text.indexOf('.') == -1)) {
            setTimeout(function () {
                if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                    $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
                }
            }, 1);
        }

        if ((text.indexOf('.') != -1) &&
            (text.substring(text.indexOf('.')).length > 2) &&
            (event.which != 0 && event.which != 8) &&
            ($(this)[0].selectionStart >= text.length - 2)) {
            event.preventDefault();
        }
    });

    $('.DecimalAcceptor').bind("paste", function (e) {
        var text = e.originalEvent.clipboardData.getData('Text');
        if ($.isNumeric(text)) {
            if ((text.substring(text.indexOf('.')).length > 3) && (text.indexOf('.') > -1)) {
                e.preventDefault();
                $(this).val(text.substring(0, text.indexOf('.') + 3));
            }
        }
        else {
            e.preventDefault();
        }
    });

    $(document.body).on('click', '.EditPrice', function () {
        var elem = $(this);
        var id = $(this).attr('data-id');
        var size = $(this).attr('data-size');
        var act = $(this).attr('data-act');
        var disc = $(this).attr('data-disc');
        var desc = $(this).attr('data-desc');
        var clr = $(this).attr('data-clr');
        var ProductThickness = $(this).attr('data-thick');
        //$('#<%=hdPrId.ClientID %>').val(id);
        $('[id*=hdPrId]').val(id);
        $('[id*=txtSize]').val(size);
        $('[id*=txtactual]').val(act);
        $('[id*=txtdiscount]').val(disc);
        $('[id*=txt_thickness]').val(ProductThickness);
        //$('#<%=txtSize.ClientID %>').val(size);
        // $('#<%=txtactual.ClientID %>').val(act);
        // $('#<%=txtdiscount.ClientID %>').val(disc);
        //$('#<%=txt_thickness.ClientID %>').val(ProductThickness);
        $('#idBtnProdPrices').val("Update");
        $('#idBtnProdPrices').text("Update");
        $('#cancel').removeAttr('style');
    });

    $(document.body).on('click', "#cancel", function () {
        $('[id*=hdPrId]').val('');
        $('[id*=txtSize]').val('');
        $('[id*=txtactual]').val('');
        $('[id*=txtdiscount]').val('');
        $('[id*=txt_thickness]').val('');
        $('#idBtnProdPrices').val("Save");
        $('#idBtnProdPrices').text("Save");
        $('#cancel').attr('style', 'display:none;');
    });

    $(document.body).on('click', '.deleteppItem', function () {
        var elem = $(this);
        var id = $(this).attr('data-id');
        swal.fire({
            title: 'Are you sure to delete?',
            text: "You won't be able to revert this!",
            type: 'question',
            showCancelButton: true,
            confirmButtonText: 'Delete',
            padding: '2em'
        }).then(function (result) {
            if (result.value) {
                $.ajax({
                    type: 'POST',
                    url: "add-products.aspx/DeleteProductPrices",
                    data: "{id: '" + id + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: false,
                    success: function (data2) {
                        if (data2.d.toString() == "Success") {
                            swal.fire(
                                'Deleted!',
                                'Your file has been deleted.',
                                'success'
                            );
                            elem.parent().parent().remove();
                            if ($("#tbdy").children().length == 0) {
                                $("#tbdy").append("<tr class=''><td valign='top' colspan='7' class='dataTables_empty'>No data available in table</td></tr>");
                            }
                        }
                        else if (data2.d.toString() == "Permission") {
                            swal.fire(
                                'Error !',
                                'Permission denied. Please contact to your administrator',
                                'error'
                            );
                        }
                        else {
                            swal.fire(
                                'Error !',
                                'There is some problem now. Please try after sometime.',
                                'error'
                            );
                        }
                    },
                    error: function (err) {
                        swal.fire(
                            'Error !',
                            'There is some problem now. Please try after sometime.',
                            'error'
                        );
                    }
                });
            }
        })
    });
    /*
        $(document.body).on('click', "#UpdateImgOrder", function (e) {
            var product = "";
            var elem = $(this);
            elem.empty();
            elem.val("Please wait...")
            e.preventDefault();
            $.each($('#left-defaults').find('li'), function () {
                if (product == "") {
                    product = product + $(this).attr("data-id");
                }
                else {
                    product = product + "|" + $(this).attr("data-id");
                }
            });
            if (product != "") {
                ArrengeCategory(product);
            }
            else {
                elem.empty();
                elem.val("Update Image Order")
            }
        });
    
        $(document.body).on('change', '.galleryType', function () {
            var txt = $(this).val();
            if (txt.toLowerCase() === "video") {
                $("#divVideo").removeAttr("style");
                $("#divImage").attr("style", "display:none !important;");
            }
            else {
                $("#divVideo").attr("style", "display:none !important;");
                $("#divImage").removeAttr("style");
            }
        });
    
        $(document.body).on('click', '#btnSave1', function () {
            var pid = $('[id*=idPid]').val();
            var type = $('[id*=ddlType]').val();
            var lnk = $('[id*=txtLink]').val().trim();
            const toast = swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000,
                padding: '2em'
            });
    
            var elem = $(this);
            elem.empty();
            elem.append("Please wait...");
            if (type.toLowerCase() === "video") {
                if (lnk === "") {
                    elem.empty();
                    elem.append("Upload");
                    toast({
                        type: 'error',
                        title: 'Please enter embedded link to upload',
                        padding: '2em',
                    });
                    return false;
                }
                $.ajax({
                    url: "add-products.aspx/InsertGallery",
                    type: "POST",
                    data: "{pid:'" + pid + "',lnk:'" + lnk + "',tp:'" + type + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: false,
                    success: function (result) {
                        if (result.d.toString() == "Success") {
                            elem.empty();
                            elem.append("Upload");
                            $('[id*=txtLink]').val("");
                            BindGalleryImages($('[id*=idPid]').val());
                            toast({
                                type: 'success',
                                title: 'Uploaded successfully',
                                padding: '2em',
                            })
                        }
                        else if (result.d.toString() == "Permission") {
                            elem.empty();
                            elem.append("Upload");
                            toast({
                                type: 'error',
                                title: 'Permission denied. Please contact to your administrator',
                                padding: '2em',
                            })
                        }
                        else {
                            elem.empty();
                            elem.append("Upload");
                            toast({
                                type: 'error',
                                title: 'There is some problem now. Please try after sometime.',
                                padding: '2em',
                            })
                        }
                    },
                    error: function (err) {
                        elem.empty();
                        elem.append("Upload");
                        toast({
                            type: 'error',
                            title: 'There is some problem now. Please try after some time',
                            padding: '2em',
                        });
                    }
                });
            }
            else {
                var fileUpload = $("#fileUp").get(0);
                var files = fileUpload.files;
                var data = new FormData();
                for (var i = 0; i < files.length; i++) {
                    data.append(files[i].name, files[i]);
                }
                data.append("pid", pid);
                data.append("tp", type);
                if (files.length == 0) {
                    elem.empty();
                    elem.append("Upload");
                    toast({
                        type: 'error',
                        title: 'Please select atleast one file to upload',
                        padding: '2em',
                    });
                    return false;
                }
                $.ajax({
                    url: "product-images.ashx",
                    type: "POST",
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (result) {
                        if (result.split('|')[0] == "Success") {
                            elem.empty();
                            elem.append("Upload");
                            BindGalleryImages($('[id*=idPid]').val());
                            toast({
                                type: 'success',
                                title: 'Uploaded successfully',
                                padding: '2em',
                            })
                        }
                        else if (result.split('|')[0] == "Permission") {
                            elem.empty();
                            elem.append("Upload");
                            toast({
                                type: 'error',
                                title: 'Permission denied. Please contact to your administrator',
                                padding: '2em',
                            })
                        }
                        else {
                            elem.empty();
                            elem.append("Upload");
                            toast({
                                type: 'error',
                                title: 'There is some problem now. Please try after sometime.',
                                padding: '2em',
                            })
                        }
                    },
                    error: function (err) {
                        elem.empty();
                        elem.append("Upload");
                        toast({
                            type: 'error',
                            title: 'There is some problem now. Please try after some time',
                            padding: '2em',
                        });
                    }
                });
            }
        });
    
        $(document.body).on('click', '.deleteGalleryItem', function () {
            var elem = $(this);
            var id = $(this).attr('data-id');
            swal({
                title: 'Are you sure to delete?',
                text: "You won't be able to revert this!",
                type: 'question',
                showCancelButton: true,
                confirmButtonText: 'Delete',
                padding: '2em'
            }).then(function (result) {
                if (result.value) {
                    $.ajax({
                        type: 'POST',
                        url: "add-products.aspx/Delete",
                        data: "{id: '" + id + "'}",
                        contentType: 'application/json; charset=utf-8',
                        dataType: "json",
                        async: false,
                        success: function (data2) {
                            if (data2.d.toString() == "Success") {
                                swal(
                                    'Deleted!',
                                    'Your file has been deleted.',
                                    'success'
                                );
                                elem.parent().parent().parent().parent().remove();
                            }
                            else if (data2.d.toString() == "Permission") {
                                swal(
                                    'Error !',
                                    'Permission denied. Please contact to your administrator',
                                    'error'
                                );
                            }
                            else {
                                swal(
                                    'Error !',
                                    'There is some problem now. Please try after sometime.',
                                    'error'
                                );
                            }
                        },
                        error: function (err) {
                            swal(
                                'Error !',
                                'There is some problem now. Please try after sometime.',
                                'error'
                            );
                        }
                    });
                }
            })
        });
    */
    $(document.body).on('click', '#idBtnProdPrices', function () {
        var elem = $(this);
        var err = "";
        var size = $('[id*=txtSize]').val();
        var legth = $('[id*=txt_thickness]').val();
        var act = parseFloat($('[id*=txtactual]').val());
        var dis = parseFloat($('[id*=txtdiscount]').val());
        if (!legth) {
            err = "E";
            $('[id*=rfvSize]').removeAttr("style");
            $('[id*=rfvSize]').attr('style', 'color:Red;');
        } else {
            $('[id*=rfvSize]').removeAttr("style");
            $('[id*=rfvSize]').attr('style', 'color:Red;visibility:hidden;');
        }
        if (!size) {
            err = "E";
            $('[id*=rfvthickness]').removeAttr("style");
            $('[id*=rfvthickness]').attr('style', 'color:Red;');
        } else {
            $('[id*=rfvthickness]').removeAttr("style");
            $('[id*=rfvthickness]').attr('style', 'color:Red;visibility:hidden;');
        }
        if (isNaN(act)) {
            err = "E";
            $('[id*=reqtxtactual]').removeAttr("style");
            $('[id*=reqtxtactual]').attr('style', 'color:Red;');
        } else {
            $('[id*=reqtxtactual]').removeAttr("style");
            $('[id*=reqtxtactual]').attr('style', 'color:Red;visibility:hidden;');
        }
        if (isNaN(dis)) {
            err = "E";
            $('[id*=reqtxtdiscount]').removeAttr("style");
            $('[id*=reqtxtdiscount]').attr('style', 'color:Red;');
        } else {
            $('[id*=reqtxtdiscount]').removeAttr("style");
            $('[id*=reqtxtdiscount]').attr('style', 'color:Red;visibility:hidden;');
        }
        if (parseFloat(act) < parseFloat(dis)) {
            err = "E";
            $("#lblProdPrices").removeAttr('class');
            Snackbar.show({ pos: 'top-right', text: 'Actual price should not be less than Discount price', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
          //  $("#lblProdPrices").attr('class', 'alert alert-danger d-block');
           // $("#lblProdPrices").text('Actual price should not be less than Discount price');
        }
        else {
            $("#lblProdPrices").removeAttr('class');
            $("#lblProdPrices").attr('class', 'd-none');
            $("#lblProdPrices").text('');
        }
        if (err != "") {
            return false;
        }

        const toast = swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 3000,
            padding: '2em'
        });

        var txt = elem.text();
        elem.empty();
        elem.append("Please Wait...");
        var pdid = $("[id*=idPid]").val();
        var prid = $("[id*=hdPrId]").val();
        $.ajax({
            type: 'POST',
            url: "add-products.aspx/AddPrices",
            data: "{pdid:'" + pdid + "',prid:'" + prid + "',sz: '" + size + "',act:'" + act + "',dis:'" + dis + "',legth:'" + legth + "'}",
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            async: false,
            success: function (data2) {
                if (data2.d.toString() == "Inserted") {
                    $('[id*=txtSize]').val('');
                    $('[id*=txtactual]').val('');
                    $('[id*=txtdiscount]').val('');
                    $('[id*=txt_thickness]').val('');
                    elem.empty();
                    elem.append("Save");
                    Snackbar.show({
                        pos: 'top-right',
                        text: 'Price added successfully',
                        actionTextColor: '#fff',
                        backgroundColor: '#008a3d'
                    });
                }
                else if (data2.d.toString() == "Updated") {
                    elem.empty();
                    elem.append("Update");
                    Snackbar.show({
                        pos: 'top-right',
                        text: 'Price updated successfully',
                        actionTextColor: '#fff',
                        backgroundColor: '#008a3d'
                    });

                }
                else if (data2.d.toString() == "Permission") {
                    if (txt === "Update") {
                        elem.empty();
                        elem.append("Update");
                    } else {
                        elem.empty();
                        elem.append("Save");
                    }
                    Snackbar.show({
                        pos: 'top-right',
                        text: 'Permission denied. Please contact your administrator',
                        actionTextColor: '#fff',
                        backgroundColor: '#ff4c4c'
                    });

                }
                else if (data2.d.toString() == "exist") {
                    if (txt === "Update") {
                        elem.empty();
                        elem.append("Update");
                    } else {
                        elem.empty();
                        elem.append("Save");
                    }
                    Snackbar.show({
                        pos: 'top-right',
                        text: 'The entered data already exists. Please check and try again',
                        actionTextColor: '#fff',
                        backgroundColor: '#ff4c4c'
                    });

                }
                else {
                    if (txt === "Update") {
                        elem.empty();
                        elem.append("Update");
                    } else {
                        elem.empty();
                        elem.append("Save");
                    }
                    Snackbar.show({
                        pos: 'top-right',
                        text: 'There is some problem now. Please try after sometime!',
                        actionTextColor: '#fff',
                        backgroundColor: '#ff4c4c'
                    });

                }
            },
            error: function (err) {
                if (txt === "Update") {
                    elem.empty();
                    elem.append("Update");
                } else {
                    elem.empty();
                    elem.append("Save");
                }
                Snackbar.show({
                    pos: 'top-right',
                    text: 'There is some problem now. Please try after sometime!',
                    actionTextColor: '#fff',
                    backgroundColor: '#ff4c4c'
                });
            }
        });
        BindAllPrices($('[id*=idPid]').val());
    });

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

    function EndRequestHandler(sender, args) {

        BindGalleryImages($('[id*=idPid]').val());

        BindAllPrices($('[id*=idPid]').val());

        $('.lvTest').fSelect();

        $('.fs-label').text("Select Options");

        $('.bs-tooltip').tooltip();



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

        $('.DecimalAcceptor').keypress(function (event) {
            var $this = $(this);
            if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
                ((event.which < 48 || event.which > 57) &&
                    (event.which != 0 && event.which != 8))) {
                event.preventDefault();
            }

            var text = $(this).val();
            if ((event.which == 46) && (text.indexOf('.') == -1)) {
                setTimeout(function () {
                    if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                        $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
                    }
                }, 1);
            }

            if ((text.indexOf('.') != -1) &&
                (text.substring(text.indexOf('.')).length > 2) &&
                (event.which != 0 && event.which != 8) &&
                ($(this)[0].selectionStart >= text.length - 2)) {
                event.preventDefault();
            }
        });

        $('.DecimalAcceptor').bind("paste", function (e) {
            var text = e.originalEvent.clipboardData.getData('Text');
            if ($.isNumeric(text)) {
                if ((text.substring(text.indexOf('.')).length > 3) && (text.indexOf('.') > -1)) {
                    e.preventDefault();
                    $(this).val(text.substring(0, text.indexOf('.') + 3));
                }
            }
            else {
                e.preventDefault();
            }
        });
    };
});

/*function BindGalleryImages(id) {
    $.ajax({
        type: 'POST',
        url: "add-products.aspx/GetGalleryImage",
        data: "{id: '" + id + "'}",
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        async: false,
        success: function (data2) {
            var lnt = data2.d;
            var strL = "";
            var str = "";
            for (var i = 0; i < lnt.length; i++) {
                var del = "";
                var strtype = "";
                if (lnt[i].GType === "Video") {
                    strtype = "<embed src='" + lnt[i].Images + "' class='img-responsive' style='max-width:100%;'>";
                }
                else {
                    strtype = "<img src='/" + lnt[i].Images + "' class='img-responsive' style='max-width:100%;' />";
                }
                del += "<a href='javascript:void(0);' class='bs-tooltip deleteGalleryItem' data-id='" + lnt[i].Id + "' data-toggle='tooltip' data-placement='top' title='' data-original-title='Delete'>";
                del += "<svg xmlns='http://www.w3.org/2000/svg' width='20' height='24' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round' class='feather feather-trash-2'><polyline points='3 6 5 6 21 6'></polyline><path d='M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2'></path><line x1='10' y1='11' x2='10' y2='17'></line><line x1='14' y1='11' x2='14' y2='17'></line></svg></a>";
                str = str + "<li data-id='" + lnt[i].Id + "' class='ui-state-default media  d-md-flex d-block text-sm-left text-center col-md-3'><div class='maindiv'>" + strtype + "<div><span>" + del + "</span></div></div></li>";
            }
            $("#left-defaults").html(str);
        }
    });
}*/

/*function ArrengeCategory(product) {
    var parameter = { "id": product };
    const toast = swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        padding: '2em'
    });
    $.ajax({
        type: 'POST',
        url: "add-products.aspx/ImageOrderUpdate",
        data: JSON.stringify(parameter),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (data) {
            if (data.d == 0) {
                $("#UpdateImgOrder").val("Update Image Order");
                toast({
                    type: 'success',
                    title: 'Image arraged successfully',
                    padding: '2em',
                });
                BindGalleryImages($('[id*=idPid]').val());
            }
            else {
                $("#UpdateImgOrder").val("Update Image Order");
                toast({
                    type: 'error',
                    title: 'There is some problem now. Please try after some time',
                    padding: '2em',
                });
            }
        },
        error: function (error) {
            $("#UpdateImgOrder").val("Update Image Order");
            toast({
                type: 'error',
                title: 'There is some problem now. Please try after some time',
                padding: '2em',
            });
        }
    });
}*/
function BindGalleryImages(id) {
    $.ajax({
        type: 'POST',
        url: "add-products.aspx/GetGalleryImage",
        data: "{id: '" + id + "'}",
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        async: false,
        success: function (data2) {
            var lnt = data2.d;
            var strL = "";
            var str = "";
            for (var i = 0; i < lnt.length; i++) {
                var del = "";
                var strtype = "";
                strtype = "<img src='/" + lnt[i].Images + "' class='img-responsive' style='max-width:100%;' />";
                del += "<a href='javascript:void(0);' class='bs-tooltip btn btn-danger  deleteGalleryItem mt-3' data-id='" + lnt[i].Id + "' data-pid='" + lnt[i].ProductId + "' data-toggle='tooltip' data-placement='top' title='' data-original-title='Delete'>";
                del += "<svg xmlns='http://www.w3.org/2000/svg' width='20' height='24' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round' class='feather feather-trash-2'><polyline points='3 6 5 6 21 6'></polyline><path d='M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2'></path><line x1='10' y1='11' x2='10' y2='17'></line><line x1='14' y1='11' x2='14' y2='17'></line></svg></a>";
                str = str + "<li data-id='" + lnt[i].Id + "' class='ui-state-default media  d-md-flex d-block text-sm-left text-end col-md-3'><div class='maindiv'>" + strtype + "<div><span>" + del + "</span></div></div></li>";
            }
            $("#left-defaults").html(str);
        }
    });
}
function ArrengeCategory(product) {
    var parameter = { "id": product };

    $.ajax({
        type: 'POST',
        url: "add-products.aspx/ImageOrderUpdate",
        data: JSON.stringify(parameter),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (data2) {
            if (data2.d.toString() == "Success") {
                $("#UpdateImgOrder").val("Update Image Order");
                Snackbar.show({ pos: 'top-right', text: 'Images arranged successfully.', actionTextColor: '#fff', backgroundColor: '#008a3d' });
                BindGalleryImages($("#AddToGallery").attr("data-pid"));
            }
            else if (data2.d.toString() == "Permission") {
                Swal.fire({ title: "Oops...", text: "Permission denied! Please contact to your administrator.", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
            }
            else {
                Swal.fire({ title: "Oops...", text: "Something went wrong!", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
            }
        },
        error: function (err) {
            Swal.fire({ title: "Oops...", text: "Something went wrong!", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
        }
    });
}
function BindAllPrices(id) {
    $.ajax({
        type: 'POST',
        url: "add-products.aspx/GetEditedProductPrices",
        data: "{idPid: '" + id + "'}",
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        async: false,
        success: function (data2) {
            var lnt = data2.d;
            var loop = "";
            var x = 1;
            for (var i = 0; i < lnt.length; i++) {
                var re = /-?\d+/;
                var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                var pOn = re.exec(lnt[i].AddedOn);
                var addedOn = new Date(parseInt(pOn[0]));
                //var addedOn2 = addedOn.getDate() + "/" + months[addedOn.getMonth()] + "/" + addedOn.getFullYear() + ", " + addedOn.toLocaleTimeString();
                var pOn1 = re.exec(lnt[i].UpdatedOn);
                var updatedOn = new Date(parseInt(pOn1[0]));
                var thickns = lnt[i].ProductThickness == "" ? "Not Applicable" : lnt[i].ProductThickness;
                var updatedOn2 = updatedOn.getDate() + "/" + months[updatedOn.getMonth()] + "/" + updatedOn.getFullYear() + ", " + updatedOn.toLocaleTimeString();
                loop += "<tr><td>" + x + "</td><td>" + lnt[i].ProductSize + "</td><td>" + thickns + "</td><td>" + lnt[i].ActualPrice + "</td><td>" + lnt[i].DiscountPrice + "</td><td><span class='bs-tooltip' data-toggle='tooltip' data-placement='top' title='' data-original-title='Updated By:" + lnt[i].UpdatedBy + "'>" + updatedOn2 + "</span></td><td class='text-center'><a href ='javascript:void(0);' class='bs-tooltip  fs-16 mr-5px EditPrice' data-id='" + lnt[i].Id + "' data-size='" + lnt[i].ProductSize + "' data-thick='" + lnt[i].ProductThickness + "' data-act='" + lnt[i].ActualPrice + "' data-disc='" + lnt[i].DiscountPrice + "' data-bs-toggle='tooltip'data-placement='top' title='Edit Price'><i class='mdi mdi-square-edit-outline'></i></a><a href='javascript:void(0);' class='bs-tooltip text-danger fs-16 mr-5px deleteppItem' data-id='" + lnt[i].Id + "' data-bs-toggle='tooltip' data-placement='top' title='Delete Price'><i class='mdi mdi-trash-can-outline'></i></a>";
                x++;
            }
            var tableBind = "";

            tableBind += "<table class='table table-nowrap align-middle table-striped table-bordered' style ='width: 100%;'>";
            tableBind += "    <thead class='table-light'>                                                   ";
            tableBind += "        <tr>                                                  ";
            tableBind += "            <th>#</th>                                        ";
            tableBind += "            <th>Product Size </th>                            ";
            tableBind += "            <th>Product Thickness </th>                            ";
            tableBind += "            <th>Actual Price</th>                             ";
            tableBind += "            <th>Discount Price</th>                           ";
            tableBind += "            <th>Updated On</th>                                  ";
            tableBind += "            <th class='text-center'>Action</th>               ";
            tableBind += "        </tr>                                                 ";
            tableBind += "    </thead>                                                  ";
            tableBind += "    <tbody id='tbdy'>                                         ";
            tableBind += loop;
            tableBind += "    </tbody>                                                  ";
            tableBind += "    <tfoot>                                                   ";
            tableBind += "        <tr>                                                  ";
            tableBind += "            <th>#</th>                                        ";
            tableBind += "            <th>Product Size </th>                            ";
            tableBind += "            <th>Product Thickness </th>                            ";
            tableBind += "            <th>Actual Price</th>                             ";
            tableBind += "            <th>Discount Price</th>                           ";
            tableBind += "            <th>Updated On</th>                                  ";
            tableBind += "            <th class='text-center'>Action</th>               ";
            tableBind += "        </tr>                                                 ";
            tableBind += "    </tfoot>                                                  ";
            tableBind += "</table>   ";
            $("#PriceTable").empty();
            $("#PriceTable").html(tableBind);

            $('.myTable').DataTable({
                "pagingType": "full_numbers",
                "oLanguage": {
                    "oPaginate": {
                        "sFirst": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-chevron-left"><polyline points="15 18 9 12 15 6"></polyline></svg>',
                        "sPrevious": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-left"><line x1="19" y1="12" x2="5" y2="12"></line><polyline points="12 19 5 12 12 5"></polyline></svg>',
                        "sNext": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-right"><line x1="5" y1="12" x2="19" y2="12"></line><polyline points="12 5 19 12 12 19"></polyline></svg>',
                        "sLast": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-chevron-right"><polyline points="9 18 15 12 9 6"></polyline></svg>'
                    },
                    "sInfo": "Showing page _PAGE_ of _PAGES_",
                    "sSearch": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-search"><circle cx="11" cy="11" r="8"></circle><line x1="21" y1="21" x2="16.65" y2="16.65"></line></svg>',
                    "sSearchPlaceholder": "Search...",
                    "sLengthMenu": "Results :  _MENU_",
                },
                "stripeClasses": [],
                "lengthMenu": [30, 50, 100, 200],
                "pageLength": 30
            });
        }
    });
}
