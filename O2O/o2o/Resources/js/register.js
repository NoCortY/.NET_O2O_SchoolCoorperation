$(function () {
    var url = "../../Controller/register.ashx";
    $("#submit").click(function () {
        if ($("#InputEmail").val() == "") {
            $("#alertemail").html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;邮箱不能为空");
        }
        if ($("#InputPassword").val() == "") {
            $("#alertpassword").html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;密码不能为空");
        }
        if ($("#InputNickname").val() == "") {
            $("#alertnickname").html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;昵称不能为空");
        }
        if (($("#InputEmail").val() != "") && ($("#InputPassword").val() == "") && ($("#InputNickname").val() == "")) {
            $.ajax({
                type:'POST',
                url: url,
                data: $('#registerform').serialize(),
                dataType:'json',
                success: function (data) {
                    alert(data.username);
                }
            });
        }
    });
});