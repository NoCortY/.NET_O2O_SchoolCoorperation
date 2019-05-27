$(function () {
    var flag = false;
    loginstatusurl = "../../Controller/loginstatus.ashx";
    $.ajax({
        type: 'POST',
        url: loginstatusurl,
        dataType: 'json',
        success: function (data) {
            if (data.success == "true") {
                flag = true;
                append(data.nickname);
            }
        }

    });
    function append(nickname) {
        $("#useritem").text(nickname);
        $("#dropdownhaslogin").html("<li><a href='#'>账户管理</a></li>" +
                            "<li><a href='#'>我的发布</a></li>" +
                            "<li><a href='#'>我的评价</a></li>" +
                            "<li role='separator' class='divider'></li>" +
                            "<li><a href='#'>退出登录</a></li>")
    }
});