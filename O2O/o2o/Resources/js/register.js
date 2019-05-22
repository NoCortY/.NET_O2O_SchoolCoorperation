$(function () {
    var url = "../../Controller/register.ashx";
    $("#submit").click(function () {
        $.ajax({
            type:'POST',
            url: url,
            data: $('#registerform').serialize(),
            dataType:'json',
            success: function (data) {
                alert(data.username);
            }
        });
    });
});