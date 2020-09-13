
"use strict";
var galleryItems = [];
var map = {};
jQuery(document).ready(function ()
{
    $('#lightgallery').lightGallery({
        pager: true,
        autoplay: true,
        share: false
    });

    var totalFileSizeLimit = 25 * 1024 * 1024;
    var fileSizeLimit = 4 * 1024 * 1024;
    function addArrayFile(files, callback)
    {
        KTApp.blockPage({
            overlayColor: '#333',
            opacity: 0.6,
            type: 'v2',
            state: 'success',
            message: "İşleminiz gerçekleştiriliyor lütfen bekleyiniz..."
        });
        setTimeout(function ()
        {
            Array.from(files).forEach(file =>
            {
                var count = 0;
                if (file.size < fileSizeLimit)
                {
                    if (galleryItems.sum("size") > totalFileSizeLimit)
                    {
                        swal.fire("Dikkat", "25 MB limitiniz aştınız", "error");
                        return;
                    }
                    var hasItem = galleryItems.filter(item => item.name === file.name);
                    if (hasItem.length === 0)
                    {
                        var item = map[file.name] = {
                            name: file.name,
                            file: file,
                            size: file.size
                        };
                        galleryItems.push(item);

                        var avatarDiv = $('<div/>').attr({
                            'class': 'kt-avatar kt-avatar--outline'
                        }).appendTo('#galleryContents');

                        var label = $('<label/>').attr({
                            'class': 'kt-avatar__upload btnRemoveImage',
                            'data-toggle': 'kt-tooltip',
                            'data-original-title': '(' + formatBytes(file.size) + ') Fotoğrafı Sil ',
                            'title': '',
                            'style': 'right:0px; top:0px; width:22px; height:22px',
                            'data-filename': file.name
                        }).appendTo(avatarDiv);

                        $('<i/>').attr({
                            'class': 'la la-close'
                        }).appendTo(label);


                        $('<img />').attr({
                            'src': URL.createObjectURL(file),
                            'data-toggle': 'kt-tooltip',
                            'data-original-title': formatBytes(file.size),
                            'class': "img-thumbnail imgDblClick",
                            'data-filename': file.name,
                            'style': 'max-height:100px; ' + (count === 0 ? 'margin:10px 10px 10px 0' : 'margin:10px')
                        }).appendTo(avatarDiv);
                    }
                    count++;
                }
            });
            callback();
        }, 300);
    }
    function unblock()
    {
        setTimeout(function ()
        {
            $('[data-toggle="kt-tooltip"]').tooltip();
            $("#txtFileSize").html("");
            fileSizeText();

            if (galleryItems.length > 0)
            {
                $("#emptyDiv").hide();
            }
            $("#submit_form")[0].reset();
            KTApp.unblockPage();
            if (galleryItems.sum("size") > totalFileSizeLimit)
            {
                swal.fire("Dikkat", "25 MB limitiniz aştınız", "error");
            }
        }, 300);
    }
    $("#customFile").change(function (event)
    {
        addArrayFile(event.target.files, unblock);
    });

    function fileSizeText()
    {
        $("#txtFileSize").show();
        $("#txtFileSize").html("Toplam Boyut " + formatBytes(galleryItems.sum("size")));
        if (galleryItems.sum("size") > totalFileSizeLimit)
        {
            $("#txtFileSize").removeClass("kt-badge--success").addClass("kt-badge--danger");
        } else
        {
            $("#txtFileSize").addClass("kt-badge--success").removeClass("kt-badge--danger");
        }
    }

    function removeImg(that)
    {
        $("[data-toggle='kt-tooltip']").tooltip('hide');
        $(that).parent("div").remove();
        galleryItems = galleryItems.filter(function (item)
        {
            return item.name !== $(that).data("filename");
        });
        fileSizeText();
        if (galleryItems.length === 0)
        {
            $("#txtFileSize").hide();
            $("#emptyDiv").show();
        }
    }
    $(document).on("click", '.btnRemoveImage', function ()
    {
        removeImg(this);
    });
    $(document).on("dblclick", '.imgDblClick', function ()
    {
        removeImg(this);
    });
    $("#cbDepartment").change(function ()
    {
        $.post("/Admin/Gallery/List", { Department: $(this).val() }, function (result) { $("#galleryContent").html(result); });
    });
    $(".delete-image").click(function ()
    {
        event.preventDefault();
        var that = this;
        swal.fire({
            title: 'Bu fotoğrafı silmek istediğinizden emin misiniz?',
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
                    "/Admin/Gallery/Delete", //İstek Adresi
                    function (result)
                    { // success function callback

                        if (result.status === 1)
                        {
                            debugger;
                            $(that).closest("li").remove();

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
                    { imageName: $(that).data("name"), department: $("#cbDepartment").val() }, //data
                    null,
                    this, // context erişimi için successcallback içerisinde thatLocal olarak kullanılabilir
                    'post' // istek yöntemi
                );
            }
        });


    });

    $(".btnSave").click(function (event)
    {
        event.preventDefault();
        if (galleryItems.sum("size") > totalFileSizeLimit)
        {
            swal.fire("Dikkat", "25 MB limitiniz aştınız", "error");
        }
        else
        {
            var fileData = new FormData();
            galleryItems.forEach(item =>
            {
                fileData.append(item.name, item.file);
            });
            var formSerializeArray = $("form#submit_form").serializeArray();
            formSerializeArray.forEach(function (item)
            {
                fileData.append(item.name, item.value);
            });

            $.ajax({
                type: "POST",
                url: "/Admin/Gallery/Save",
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
                        swal.fire({
                            title: "Başarılı!",
                            text: result.message,
                            type: "success"
                        }).then(function ()
                        {
                            $.post("/Admin/Gallery/List", { Department: $("#cbDepartment").val() }, function (result) { $("#galleryContent").html(result); });
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
                error: function (err)
                {
                    KTApp.unblockPage();
                    if (err) { swal.fire("Hata", err, "error"); }
                }
            });
        }
    });
});

