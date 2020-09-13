
"use strict";

jQuery(document).ready(function ()
{

    $('#kt_modal_6').on('show.bs.modal', function (e)
    {

        $.getJSON("/lib/denticons.json", function (data)
        {
            var item = "";
            $.each(data, function (key, val)
            {
                item = '<div class="col-md-2">' +
                    '<div class="kt-demo-icon d-flex justify-content-center btnIcon" data-iconname="' + val.icon + '" style="cursor:pointer;border:1px solid #ececec">' +
                    '<div class="kt-demo-icon__preview" style="padding-right:0px!important; flex:0!important">' +
                    '<i class="' + val.icon + '" style="font-size:2.8rem !important"></i>' +
                    '</div></div></div>';

                $("#iconContent").append(item);
            });
        });
    });
    $(document).on("click", '.btnIcon', function ()
    {
        var iconName = $(this).data("iconname");
        $("#Icon").val(iconName);
        $('[name="icon"]').attr("class", iconName);
        $('#kt_modal_6').modal('toggle');

    });
    $(".btnChooseIcon").click(function ()
    {
        $('#kt_modal_6').modal('toggle');

    });
    $("input[maxlength]").maxlength({
        alwaysShow: true,
        placement: 'right'
    });
    $("textarea").maxlength({
        alwaysShow: true,
        placement: 'right'
    });
    $(".btnReturnList").click(function ()
    {
        $.post("/Admin/Services/List", function (result) { $("#servicesContent").html(result); });
    });
    $(".btnSave").click(function (event)
    {
        event.preventDefault();
        var fileData = new FormData();
        var formSerializeArray = $("form#submit_form").serializeArray();
        formSerializeArray.forEach(function (item)
        {
            fileData.append(item.name, item.value);
        });

        $.validator.unobtrusive.parse($("form#submit_form"));

        $("form#submit_form").validate();
        if ($("form#submit_form").valid())
        {
            $.ajax({
                type: "POST",
                url: "/Admin/Services/Save",
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
                success: function (result)
                {
                    KTApp.unblockPage();
                    if (result.status === 1)
                    {
                        swal.fire("Başarılı", result.message, "success");
                        $.post("/Admin/Services/List", function (result) { $("#servicesContent").html(result); });
                    }
                },
                error: function (err)
                {
                    KTApp.unblockPage();
                    if (err) { swal.fire("Hata", err, "error"); }
                }
            });
        } else
        {
            toastr.warning("Lütfen Zorunlu alanları doldurunuz!");

        }
    });
    $(".btnDel").click(function (event)
    {
        event.preventDefault();
        swal.fire({
            title: 'Bu kaydı silmek istediğinizden emin misiniz?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet'
        }).then((result) =>
        {
            if (result.value)
            {
                server.ajaxCall(
                    "/Admin/Services/Delete", //İstek Adresi
                    function (result)
                    { // success function callback

                        if (result.status === 1)
                        {
                            swal.fire({
                                title: "Başarılı!",
                                text: result.message,
                                type: "success"
                            }).then(function ()
                            {
                                $.post("/Admin/Services/List", function (result) { $("#servicesContent").html(result); });
                            });
                        } else
                        {
                            swal.fire({
                                title: "Hata!",
                                text: result.message,
                                type: "error"
                            });
                        }

                    },
                    function (err)
                    { // error function callback
                        if (err) { swal.fire("Hata", err, "error"); }
                    },
                    { idServices: $(".btnDel").data("id") }, //data
                    null,
                    this, // context erişimi için successcallback içerisinde thatLocal olarak kullanılabilir
                    'post' // istek yöntemi
                );
            }
        });
    });
});
