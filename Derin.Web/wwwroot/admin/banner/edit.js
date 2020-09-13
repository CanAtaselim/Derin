
"use strict";

jQuery(document).ready(function () {

    $("#bannerFile").on("change", function (event) {
        var image = document.querySelector("#bannerImage");
        image.src = URL.createObjectURL(event.target.files[0]);
        $(".imageDiv").removeClass("d-none")
    });


    function reInvokeComponent() {
        $.post("/Admin/Banner/ReInvokeEditComponent", function (result) { $("#bannerContent").html(result); });
    }

    $(".btnSave").click(function (event) {
        event.preventDefault();

        var fileUpload = $("#bannerFile").get(0);
        var files = fileUpload.files;
        var fileData = new FormData();

        fileData.append(files[0].name, files[0]);


        var formSerializeArray = $("form#submit_form").serializeArray();
        formSerializeArray.forEach(function (item) {
            fileData.append(item.name, item.value);
        });
        $.validator.unobtrusive.parse($("form#submit_form"));

        $("form#submit_form").validate();
        if ($("form#submit_form").valid()) {
            $.ajax({
                type: "POST",
                url: "/Admin/Banner/Save",
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
                        reInvokeComponent();

                    }
                },
                error: function (err) {
                    KTApp.unblockPage();
                    if (err) { swal.fire("Hata", err, "error"); }
                }
            });
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
                    "/Admin/Banner/Delete", //İstek Adresi
                    function (result) { // success function callback

                        if (result.status === 1) {
                            swal.fire({
                                title: "Başarılı!",
                                text: result.message,
                                type: "success"
                            }).then(function () {
                                reInvokeComponent();
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
                    { idBanner: $(".btnDel").data("id") }, //data
                    this, // context erişimi için successcallback içerisinde thatLocal olarak kullanılabilir
                    'post' // istek yöntemi
                );
            }
        });
    });
});

