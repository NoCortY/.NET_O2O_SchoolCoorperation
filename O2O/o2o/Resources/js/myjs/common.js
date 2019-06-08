$(function () {
    var flag = false;
    loginstatusurl = "../../Controller/loginstatus.ashx";
    var userStatus;
    $.ajax({
        type: 'POST',
        url: loginstatusurl,
        dataType: 'json',
        success: function (data) {
            if (data.success == "true") {
                flag = true;
                userStatus = data.userStatus;
                append(data.nickname);
            }
        }

    });
    function append(nickname) {
        $("#useritem").text(nickname);
        $("#dropdownhaslogin").html("<li><a href='../../Views/usermanagement.html'>我的供应</a></li>" +
                            "<li><a href='../../Views/usermanagement.html'>我的需求</a></li>" +
                            "<li role='separator' class='divider'></li>" +
                            "<li><a href='' id='quitLogin'>退出登录</a></li>");
        if (userStatus == "1") {
            $("#dropdownhaslogin").html("<li><a href='../../Views/usermanagement.html'>我的供应</a></li>" +
                            "<li><a href='../../Views/usermanagement.html'>我的需求</a></li>" +
                            "<li role='separator' class='divider'></li>" +
                            "<li><a href='../../Views/management.html'>管理员模式</a></li>" +
                            "<li><a href='' id='quitLogin'>退出登录</a></li>");
        }
    }
    $("#dropdownhaslogin").on("click", "#quitLogin", function () {
        $.ajax({
            type: "POST",
            url: "../../Controller/quitLogin.ashx",
            dataType: "json",
            success: function (data) {
                if (data.success == "true") {
                }
            }
        });
    });
});