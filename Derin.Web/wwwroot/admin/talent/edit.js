﻿
"use strict";
var vanilla;

jQuery(document).ready(function () {
    $("input[maxlength]").maxlength({
        alwaysShow: true,
        placement: 'right'
    });
    $("textarea[maxlength]").maxlength({
        alwaysShow: true,
        placement: 'right'
    });


    $(".btnReturnList").click(function () {
        $.post("/Admin/Talent/List", function (result) { $("#talentContent").html(result); });
    });



    $(".btnSave").click(function (event) {
        event.preventDefault();

        var fileData = new FormData();
        var formSerializeArray = $("form#submit_form").serializeArray();
        formSerializeArray.forEach(function (item) {
            fileData.append(item.name, item.value);
        });
        $.validator.unobtrusive.parse($("form#submit_form"));

        $("form#submit_form").validate();
        if ($("form#submit_form").valid()) {
            $.ajax({
                type: "POST",
                url: "/Admin/Talent/Save",
                contentType: false,
                processData: false,
                beforeSend: KTApp.blockPage({
                    overlayColor: '#333',
                    opacity: 0.6,
                    type: 'v2',
                    state: 'success',
                    message: "İşleminiz gerçekleştiriliyor lütfen bekleyiniz..."
                }),
                data: fileData,
                success: function (result) {
                    KTApp.unblockPage();
                    if (result.status === 1) {
                        swal.fire("Başarılı", result.message, "success");
                        $.post("/Admin/Talent/List", function (result) { $("#talentContent").html(result); });
                    }
                },
                error: function (err) {
                    KTApp.unblockPage();
                    if (err) { swal.fire("Hata", err, "error"); }
                }
            });
        } else {
            toastr.warning("Lütfen Zorunlu alanları doldurunuz!");

        }
    });
    $(".btnDel").click(function (event) {
        event.preventDefault();
        swal.fire({
            title: 'Bu kaydı silmek istediğinizden emin misiniz?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet'
        }).then((result) => {
            if (result.value) {
                server.ajaxCall(
                    "/Admin/Talent/Delete", //İstek Adresi
                    function (result) { // success function callback

                        if (result.status === 1) {
                            swal.fire({
                                title: "Başarılı!",
                                text: result.message,
                                type: "success"
                            }).then(function () {
                                $.post("/Admin/Talent/List", function (result) { $("#talentContent").html(result); });
                            });
                        } else {
                            swal.fire({
                                title: "Hata!",
                                text: result.message,
                                type: "error"
                            });
                        }

                    },
                    function (err) { // error function callback
                        if (err) { swal.fire("Hata", err, "error"); }
                    },
                    { idTalent: $(".btnDel").data("id") }, //data
                    null,
                    this, // context erişimi için successcallback içerisinde thatLocal olarak kullanılabilir
                    'post' // istek yöntemi
                );
            }
        });
    });
});
