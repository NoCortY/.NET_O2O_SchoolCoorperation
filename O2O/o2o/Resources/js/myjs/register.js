$(function () {
    var url = "../../Controller/register.ashx?action=register";
    var userHeaderurl = "../../Controller/register.ashx?action=userheader";
    var flag = true;
    //邮箱验证正则表达式
    var reg_email = new RegExp("^[a-z0-9A-Z]+[- | a-z0-9A-Z . _]+@([a-z0-9A-Z]+(-[a-z0-9A-Z]+)?\\.)+[a-z]{2,}$");
    var reg_password = new RegExp("^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,20}$")
    var reg_telenum = new RegExp("^[1](([3][0-9])|([4][5-9])|([5][0-3,5-9])|([6][5,6])|([7][0-8])|([8][0-9])|([9][1,8,9]))[0-9]{8}$");
    $("#submit").click(function () {
        /*重置提醒格式变量*/
        $("#alerttelenum").html("");
        $("#alertemail").html("");
        $("#alertpassword").html("");
        $("#alertnickname").html("");
        $("#alertconfirmpassword").html("");
        $("#alertregisterresult").text("");
        if ($("#InputEmail").val() == "" || !(reg_email.test($("#InputEmail").val()))) {
            $("#alertemail").html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请输入正确的邮箱");
            flag = false;
        }
        if ($("#InputPassword").val() == "" || !(reg_password.test($("#InputPassword").val()))) {
            $("#alertpassword").html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请按正确格式输入密码(6-21位字母和数字组成)");
            flag = false;
        }
        if ($("#InputNickname").val() == "") {
            $("#alertnickname").html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;昵称不能为空");
            flag = false;
        }
        if ($("#InputPassword").val() != $("#ConfirmPassword").val()) {
            $("#alertconfirmpassword").html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;两次密码不一致");
            flag = false;
        }
        if ($("#InputTeleNumber").val() != "" && !(reg_telenum.test($("#InputTeleNumber").val()))) {
            $("#alerttelenum").html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请输入正确格式的手机号");
            flag = false;
        }
        var form = $("#registerform")[0];
        var formData = new FormData(form);
        if (flag) {
            $.ajax({
                type:'POST',
                url: url,
                data: formData,
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                dataType: 'json',

                success: function (data) {
                    if (data.success == "true") {
                        alert("注册成功");
                        window.location("index.html");
                    } else {
                        $("#alertregisterresult").text("用户名已存在");
                    }
                }
            });
        } else {
            /*最终flag要还原
            因为这个函数是页面加载的时候运行的,如果不还原
            只能等到页面第二次加载才能还原了
            */
            flag = true;
        }
    });
    $("#back").click(function () {
        window.location("index.html");
    });
});