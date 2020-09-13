var server = {
    ajaxCall: function (url, successCB, errCB, data, bSendMsg, that, type) {
        var thatLocal = that;
        $.ajax({
            cache: false,
            beforeSend: KTApp.blockPage({
                    overlayColor: '#333',
                    opacity: 0.6,
                    type: 'v2',
                    state: 'success',
                    message: bSendMsg === null ? "İşleminiz gerçekleştiriliyor lütfen bekleyiniz..." : bSendMsg
            }),
            url: url,
            data: data,
            success: function (result) {
                KTApp.unblockPage();
                successCB(result, thatLocal);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                KTApp.unblockPage();
                if (xhr.responseJSON.status === 4) {
                    swal.fire({
                        title: "Beklenmedik Bir Hata Oluştu!",
                        text: "Dilerseniz Hatayla İlgili Geri Bildirimde Bulunun:",
                        type: "input",
                        showCancelButton: true,
                        closeOnConfirm: false,
                        confirmButtonText: "Gönder",
                        cancelButtonText: "Vazgeç",
                        animation: "slide-from-top",
                        inputPlaceholder: "Hata Nasıl Oluştu?",
                        showLoaderOnConfirm: true
                    },
                        function (inputValue) {
                            if (inputValue === false) return false;

                            if (inputValue === "") {
                                swal.fire.showInputError("Geribildirim için birşeyler yazın!");
                                return false;
                            }
                            $.ajax({
                                cache: false,
                                url: '/administration/ExceptionFeedBack/Feedback',
                                data: { feedback: inputValue, exCode: xhr.responseJSON.data.errCode },
                                success: function (result) {
                                    swal.fire("Güzel!", "Geribildirim başarı ile kaydedildi. Teşekkürler.");
                                },
                                error: function (xhr, ajaxOptions, thrownError) {
                                    swal.fire("Güzel!", "Geribildirim başarı ile kaydedildi. Teşekkürler.");
                                },
                                type: 'post'
                            });

                        });
                }
                else if (xhr.responseJSON.status === 2) {
                    swal.fire({
                        title: "Oturum Sona Erdi!",
                        text: "Şifrenizi Girerek Oturuma Devam Edebilirsiniz",
                        type: "input",
                        inputType: "password",
                        showCancelButton: true,
                        closeOnConfirm: false,
                        confirmButtonText: "Gönder",
                        cancelButtonText: "Vazgeç",
                        animation: "slide-from-top",
                        inputPlaceholder: "Şifrenizi Giriniz",
                        showLoaderOnConfirm: true
                    },
                        function (inputValue) {
                            if (inputValue === false) window.location = "/auth/login/signout";
                            if (inputValue === "") {
                                swal.fire.showInputError("Şifre alanı boş olamaz!");
                                return false;
                            }
                            $.ajax({
                                cache: false,
                                url: '/auth/login/UnauthorizedAjax',
                                data: { UserName: uIdNo, Password: inputValue },
                                success: function (result) {
                                    if (result.status === 1)
                                        swal.fire("Giriş Başarılı!", "Oturuma Devam Edebilirsiniz.", "success");
                                    else {
                                        swal.fire({
                                            title: "Giriş Başarısız!",
                                            text: "Giriş Sayfasına Yönlendirileceksiniz!",
                                            type: "error",
                                            closeOnConfirm: false,
                                            showLoaderOnConfirm: true,
                                        },
                                            function () {
                                                window.location = "/auth/login/signout";
                                            });

                                    }
                                },
                                error: function (xhr, ajaxOptions, thrownError) {
                                    swal.fire({
                                        title: "Hata Oluştu!",
                                        text: "Giriş Sayfasına Yönlendirileceksiniz!",
                                        type: "error",
                                        closeOnConfirm: false,
                                        showLoaderOnConfirm: true,
                                    },
                                        function () {
                                            window.location = "/auth/login/signout";
                                        });
                                },
                                type: 'post'
                            });

                        });
                }
                else if (xhr.responseJSON.status === 3) {
                    swal.fire("Yetkisiz İşlem", xhr.responseJSON.message, "warning");
                }
                else if (xhr.responseJSON.status === 0) {
                    errCB(xhr.responseJSON.message);
                }
            },
            type: type === null ? "post" : type
        });
    },
    ajaxCallBackProcess: function (url, successCB, errCB, data, bSendMsg, that, type) {
        var thatLocal = that;
        $.ajax({
            cache: false,
            beforeSend: KTApp.blockPage({
                overlayColor: '#333',
                opacity: 0.6,
                type: 'v2',
                state: 'success',
                message: bSendMsg === null ? "İşleminiz gerçekleştiriliyor lütfen bekleyiniz..." : bSendMsg
            }),
            url: url,
            data: data,
            success: function (result) {
                KTApp.unblockPage();
                successCB(result, thatLocal);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                KTApp.unblockPage();
                if (xhr.responseJSON.status === 4) {
                    swal.fire({
                        title: "Beklenmedik Bir Hata Oluştu!",
                        text: "Dilerseniz Hatayla İlgili Geri Bildirimde Bulunun:",
                        type: "input",
                        showCancelButton: true,
                        closeOnConfirm: false,
                        confirmButtonText: "Gönder",
                        cancelButtonText: "Vazgeç",
                        animation: "slide-from-top",
                        inputPlaceholder: "Hata Nasıl Oluştu?",
                        showLoaderOnConfirm: true
                    },
                        function (inputValue) {
                            if (inputValue === false) return false;

                            if (inputValue === "") {
                                swal.fire.showInputError("Geribildirim için birşeyler yazın!");
                                return false;
                            }
                            $.ajax({
                                cache: false,
                                url: '/administration/ExceptionFeedBack/Feedback',
                                data: { feedback: inputValue, exCode: xhr.responseJSON.data.errCode },
                                success: function (result) {
                                    swal.fire("Güzel!", "Geribildirim başarı ile kaydedildi. Teşekkürler.");
                                },
                                error: function (xhr, ajaxOptions, thrownError) {
                                    swal.fire("Güzel!", "Geribildirim başarı ile kaydedildi. Teşekkürler.");
                                },
                                type: 'post'
                            });

                        });
                }

                else if (xhr.responseJSON.status === 2) {
                    swal.fire({
                        title: "Oturum Sona Erdi!",
                        text: "Şifrenizi Girerek Oturuma Devam Edebilirsiniz",
                        type: "input",
                        inputType: "password",
                        showCancelButton: true,
                        closeOnConfirm: false,
                        confirmButtonText: "Gönder",
                        cancelButtonText: "Vazgeç",
                        animation: "slide-from-top",
                        inputPlaceholder: "Şifrenizi Giriniz",
                        showLoaderOnConfirm: true
                    },
                        function (inputValue) {
                            if (inputValue === false) window.location = "/auth/login/signout";
                            if (inputValue === "") {
                                swal.fire.showInputError("Şifre alanı boş olamaz!");
                                return false
                            }
                            $.ajax({
                                cache: false,
                                url: '/auth/login/UnauthorizedAjax',
                                data: { UserName: uIdNo, Password: inputValue },
                                success: function (result) {
                                    if (result.status === 1)
                                        swal.fire("Giriş Başarılı!", "Oturuma Devam Edebilirsiniz.", "success");
                                    else {
                                        swal.fire({
                                            title: "Giriş Başarısız!",
                                            text: "Giriş Sayfasına Yönlendirileceksiniz!",
                                            type: "error",
                                            closeOnConfirm: false,
                                            showLoaderOnConfirm: true,
                                        },
                                            function () {
                                                window.location = "/auth/login/signout";
                                            });

                                    }
                                },
                                error: function (xhr, ajaxOptions, thrownError) {
                                    swal.fire({
                                        title: "Hata Oluştu!",
                                        text: "Giriş Sayfasına Yönlendirileceksiniz!",
                                        type: "error",
                                        closeOnConfirm: false,
                                        showLoaderOnConfirm: true,
                                    },
                                        function () {
                                            window.location = "/auth/login/signout";
                                        });
                                },
                                type: 'post'
                            });

                        });
                }

                else if (xhr.responseJSON.status === 3) {
                    swal.fire("Yetkisiz İşlem", xhr.responseJSON.message, "warning");
                }
                else if (xhr.responseJSON.status === 0) {
                    errCB(xhr.responseJSON.message);
                }
            },
            type: type === null ? "post" : type
        });
    }
    ,
    ajaxMenuStyle: function (url) {
        $(".fa-bookmark").addClass("fa fa-bookmark-o").removeClass("fa-bookmark");
        $(".nav-item").removeClass("active");
        $(".page-sidebar-menu").find("[href='" + url + "']").parent().addClass("active");
        $(".page-sidebar-menu").find("[href='" + url + "']").children().first().removeClass("fa-bookmark-o").addClass("fa fa-bookmark");
        $(".page-sidebar-menu").find("[href='" + url + "']").parents("li").addClass("active");
        $(".page-sidebar-menu").find("[href='" + url + "']").parents("li").find("span.arrow").addClass("open");
    },
    excelDownloadClickFlag: false
}
function ajaxCall(url, successCB, errCB, data, bSendMsg, that, type) {
    var thatLocal = that;
    $.ajax({
        cache: false,
        url: url,
        data: data,
        success: function (result) {
            successCB(result, thatLocal);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            if (xhr.responseJSON.status === 4) {
                swal.fire({
                    title: "Beklenmedik Bir Hata Oluştu!",
                    text: "Dilerseniz Hatayla İlgili Geri Bildirimde Bulunun:",
                    type: "input",
                    showCancelButton: true,
                    closeOnConfirm: false,
                    confirmButtonText: "Gönder",
                    cancelButtonText: "Vazgeç",
                    animation: "slide-from-top",
                    inputPlaceholder: "Hata Nasıl Oluştu?",
                    showLoaderOnConfirm: true
                },
                    function (inputValue) {
                        if (inputValue === false) return false;

                        if (inputValue === "") {
                            swal.fire.showInputError("Geribildirim için birşeyler yazın!");
                            return false
                        }
                        $.ajax({
                            cache: false,
                            url: '/administration/ExceptionFeedBack/Feedback',
                            data: { feedback: inputValue, exCode: xhr.responseJSON.data.errCode },
                            success: function (result) {
                                swal.fire("Güzel!", "Geribildirim başarı ile kaydedildi. Teşekkürler.");
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                swal.fire("Güzel!", "Geribildirim başarı ile kaydedildi. Teşekkürler.");
                            },
                            type: 'post'
                        });

                    });
            }
            else if (xhr.responseJSON.status === 2) {
                swal.fire({
                    title: "Oturum Sona Erdi!",
                    text: "Şifrenizi Girerek Oturuma Devam Edebilirsiniz",
                    type: "input",
                    inputType: "password",
                    showCancelButton: true,
                    closeOnConfirm: false,
                    confirmButtonText: "Gönder",
                    cancelButtonText: "Vazgeç",
                    animation: "slide-from-top",
                    inputPlaceholder: "Şifrenizi Giriniz",
                    showLoaderOnConfirm: true
                },
                    function (inputValue) {
                        if (inputValue === false) window.location = "/auth/login/signout";
                        if (inputValue === "") {
                            swal.fire.showInputError("Şifre alanı boş olamaz!");
                            return false
                        }
                        $.ajax({
                            cache: false,
                            url: '/auth/login/UnauthorizedAjax',
                            data: { UserName: uIdNo, Password: inputValue },
                            success: function (result) {
                                if (result.status === 1)
                                    swal.fire("Giriş Başarılı!", "Oturuma Devam Edebilirsiniz.", "success");
                                else {
                                    swal.fire({
                                        title: "Giriş Başarısız!",
                                        text: "Giriş Sayfasına Yönlendirileceksiniz!",
                                        type: "error",
                                        closeOnConfirm: false,
                                        showLoaderOnConfirm: true,
                                    },
                                        function () {
                                            window.location = "/auth/login/signout";
                                        });

                                }
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                swal.fire({
                                    title: "Hata Oluştu!",
                                    text: "Giriş Sayfasına Yönlendirileceksiniz!",
                                    type: "error",
                                    closeOnConfirm: false,
                                    showLoaderOnConfirm: true,
                                },
                                    function () {
                                        window.location = "/auth/login/signout";
                                    });
                            },
                            type: 'post'
                        });

                    });
            }
            else if (xhr.responseJSON.status === 3) {
                swal.fire("Yetkisiz İşlem", xhr.responseJSON.message, "warning");
            }
            else if (xhr.responseJSON.status === 0) {
                errCB(xhr.responseJSON.message);
            }
        },
        type: type === null ? "post" : type
    });
}



