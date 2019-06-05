$(function () {
    var receiveMessageUrl = "../../Controller/IM.ashx?action=receivemessage";
    var sendMessageUrl = "../../Controller/IM.ashx?action=sendmessage";
    var messageCountUrl = "../../Controller/IM.ashx?action=messagecount";
    var htmlStr = "";
    var messageCount;
    setInterval(function () { receiveMessageCount() }, 5000);
    $("#chatBtn").click(function () {
        $("#chatMessageNum").text("");
        $.ajax({
            url:receiveMessageUrl,
            type: "POST",
            dataType: "json",
            success: function (data) {
                $.each(data, function (index, item) {
                    htmlStr+="<div class='clearfloat'>"
                              +  "<div class='author-name'>"
                              +   "  <small class='chat-date'>"+item.sendTime+"</small></div>"
                              +  "<div class='left'>"
                              +  "  <div class='chat-avatars'><img src='' alt='头像' /></div>"
                              +   " <div class='chat-message'>"
                              +item.content
                              +"</div></div></div>"
                });
                $("#chatBox-content-demo").html(htmlStr);
                htmlStr = "";
                htmlStr +="<div class='chat-list-people'>"
                        +"<div><img src='' alt='头像' /></div>"
                        +"<div class='chat-name'>"
                        +    "<p>"+data.senUserName+"</p>"
                        +"</div>"
                        +"<div class='message-num'>"+messageCount+"</div></div>"
                    
            }
        });
        $("#chatBoxList").html(htmlStr);
    });
    $("#chat-fasong").click(function () {
        var content = $("#sendBox").html();
        var formData = new FormData();
        var receiveUserId = $("#contactMe").attr("value");
        var sendUserName = $("#useritem").text();
        var receiveUserName = $("#userNickName").text();
        formData.append("sendUserName", sendUserName);
        formData.append("receiveUserId", receiveUserId);
        formData.append("receiveUserName", receiveUserName);
        formData.append("content", content);
        $.ajax({
            url: sendMessageUrl,
            type: "POST",
            data:formData,
            dataType:"json",
            contentType: false,
            processData: false,
            cache: false,
            success: function (data) {
                alert("成功");
            }
        });
    });
    function receiveMessageCount() {
        $.ajax({
            url: messageCountUrl,
            type: "POST",
            dataType: "json",
            success: function (data) {
                if (data.count != "0") {
                    messageCount = data.count;
                    $("#chatMessageNum").text(data.count);
                }
            }
        });
    }
});