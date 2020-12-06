
"use strict";
var vanilla;

jQuery(document).ready(function () {


    //$('.employee').selectpicker('val', ['1', '2']);

    var selectedEmployee = [];


    $('.kt-selectpicker').selectpicker(
        {
            noneSelectedText: "Seçiniz"
        }
    );

    $('.employee').change(function () {

        selectedEmployee = [];
        var brands = $('.employee option:selected');
        $(brands).each(function (index, brand) {
            if ($(this).val() != "") {
                selectedEmployee.push([$(this).val()]);
            }
        });

    });

    $("input[maxlength]").maxlength({
        alwaysShow: true,
        placement: 'right'
    });

    function scrollToAnchor(html) {
        $('html,body').animate({ scrollTop: html.offset().top - 75 }, 'slow');
    }
    $(".btnReturnList").click(function () {
        $.post("/Admin/Person/List", function (result) { $("#personContent").html(result); });
    });
    $("#customFile").on("change", function (event) {
        readFile(this);
        if (!$('#collapseOne5').hasClass("show")) {
            $('#collapseOne5').collapse('toggle');
        }
        scrollToAnchor($("#customFile"));
    });

    $(".btnRotateLeft").click(function (event) {
        vanilla.rotate(-90);
    });
    $(".btnRotateRight").click(function (event) {
        vanilla.rotate(90);
    });
    $(".btnCrop").click(function () {
        vanilla.result({
            type: 'blob'
        }).then(function (blob) {
            KTApp.blockPage({
                overlayColor: '#333',
                opacity: 0.6,
                type: 'v2',
                state: 'success',
                message: "İşleminiz gerçekleştiriliyor lütfen bekleyiniz..."
            });
            var FR = new FileReader();
            FR.addEventListener("load", function (e) {
                var string = e.target.result;
                var regex = /^data:.+\/(.+);base64,(.*)$/;

                var matches = string.match(regex);
                var data = matches[2];
                $("#Picture").val(data);
                scrollToAnchor($("#personImage"));
                $('#collapseOne5').collapse('toggle');
                document.getElementById("personImage").src = e.target.result;

            });
            FR.readAsDataURL(blob);
            KTApp.unblockPage();
        });
    });
    $(".btnSave").click(function (event) {
        event.preventDefault();

        var fileData = new FormData();
        var formSerializeArray = $("form#submit_form").serializeArray();
        formSerializeArray.forEach(function (item) {
            fileData.append(item.name, item.value);
        });

        fileData.append("Employees", selectedEmployee);

        $.validator.unobtrusive.parse($("form#submit_form"));

        $("form#submit_form").validate();
        if ($("form#submit_form").valid()) {
            $.ajax({
                type: "POST",
                url: "/Admin/Person/Save",
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
                        $.post("/Admin/Person/List", function (result) { $("#personContent").html(result); });
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
                    "/Admin/Person/Delete", //İstek Adresi
                    function (result) { // success function callback

                        if (result.status === 1) {
                            swal.fire({
                                title: "Başarılı!",
                                text: result.message,
                                type: "success"
                            }).then(function () {
                                $.post("/Admin/Person/List", function (result) { $("#personContent").html(result); });
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
                    { idPerson: $(".btnDel").data("id") }, //data
                    null,
                    this, // context erişimi için successcallback içerisinde thatLocal olarak kullanılabilir
                    'post' // istek yöntemi
                );
            }
        });
    });
    vanilla = new Croppie(document.getElementById('cropTool'), {
        enableExif: true,
        enableOrientation: true,
        viewport: {
            width: 380,
            height: 380
        },
        boundary: {
            width: 425,
            height: 425
        }
    });
    function readFile(input) {
        if (input.files && input.files[0]) {
            input.files[0].convertToBase64(function (base64) {
                vanilla.bind({
                    url: base64,
                    orientation: 1
                });
                $("#imageCropModal").modal();
            });
        }
        else {
            console("error image crop");
        }
    }
});
