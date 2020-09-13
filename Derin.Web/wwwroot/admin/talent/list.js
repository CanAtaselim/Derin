
"use strict";
jQuery(document).ready(function ()
{
    $(".btnEdit").click(function ()
    {
        $.post("/Admin/Talent/Edit", { idTalent: $(this).data("id") }, function (result) { $("#talentContent").html(result); });
    });
    $(".btnNew").click(function ()
    {
        $.post("/Admin/Talent/Edit", function (result) { $("#talentContent").html(result); });
    });

    $(".btnDel").click(function (event)
    {
        event.preventDefault();
        var that = this;
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
                    "/Admin/Talent/Delete", //İstek Adresi
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
                                $.post("/Admin/Talent/List", function (result) { $("#talentContent").html(result); });
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
                    { idTalent: $(that).data("id") }, //data
                    null,
                    this, // context erişimi için successcallback içerisinde thatLocal olarak kullanılabilir
                    'post' // istek yöntemi
                );
            }
        });
    });
});
