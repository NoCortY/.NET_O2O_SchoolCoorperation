$(function () {
    var flag = false;
    loginstatusurl = "../../Controller/loginstatus.ashx";
    var userStatus;
    $.ajax({
        type: 'POST',
        url: loginstatusurl,
        dataType:'json',
        success: function (data) {
            if (data.success=="true") {
                flag = true;
                userStatus = data.userStatus;
                append(data.nickname);
            }
        }

    });
    $("#loginsubmit").click(function () {
        var url = "../../Controller/login.ashx";
        $("#alertloginfail").html("");
        $.ajax({
            type:'POST',
            url:url,
            data: $("#loginform").serialize(),
            dataType:'json',
            success: function (data) {
                if (data.success == "banned") {
                    $("#alertloginfail").html("&nbsp;&nbsp&nbsp;账号被封禁,如有疑问请联系我们");
                }else if (data.success == "true"&&data.success!="banned") {
                    flag = true;
                    alert("登录成功");
                    append();
                    window.location("index.html");
                } else {
                    $("#alertloginfail").html("&nbsp;&nbsp&nbsp;用户名或密码错误");
                }
            }
        });
    });
    $("#publishInformation").click(function () {
        loginstatusurl = "../../Controller/loginstatus.ashx";
        $.ajax({
            type: 'POST',
            url: loginstatusurl,
            dataType: 'json',
            success: function (data) {
                if (data.success == "false") {
                    alert("请先登录");
                    window.location("../../Views/index.html");
                }
            }

        });
    });
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
    function append(nickname) {
        $("#useritem").text(nickname);
        if (userStatus == "1") {
            $("#dropdownhaslogin").html("<li><a href='#'>账户管理</a></li>" +
                            "<li><a href='../../Views/usermanagement.html'>我的发布</a></li>" +
                            "<li><a href='../../Views/usermanagement.html'>我的评价</a></li>" +
                            "<li role='separator' class='divider'></li>" +
                            "<li><a href='../../Views/management.html'>管理员模式</a></li>" +
                            "<li><a href='' id='quitLogin'>退出登录</a></li>");
        }
        else {
            $("#dropdownhaslogin").html("<li><a href='../../Views/usermanagement.html'>账户管理</a></li>" +
                           "<li><a href='../../Views/usermanagement.html'>我的发布</a></li>" +
                           "<li><a href='../../Views/usermanagement.html'>我的评价</a></li>" +
                           "<li role='separator' class='divider'></li>" +
                           "<li><a href='' id='quitLogin'>退出登录</a></li>")
        }
    }
});