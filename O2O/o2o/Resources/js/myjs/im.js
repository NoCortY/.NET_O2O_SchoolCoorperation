$(function () {
    var receiveMessageUrl = "../../Controller/IM.ashx?action=receivemessage";
    var sendMessageUrl = "../../Controller/IM.ashx?action=sendmessage";
    var messageCountUrl = "../../Controller/IM.ashx?action=messagecount";
    var htmlStr = "";
    var messageCount;
    setInterval(function () { receiveMessageCount() }, 5000);
    $("#chatBtn").click(function () {
        $(".chatBox").toggle(10);
        $(".chatBox-head-one").hide();
        $(".chatBox-head-two").show();
        $(".chatBox-list").hide();
        $(".chatBox-kuang").show();
        $("#chatMessageNum").text("");
        $.ajax({
            url:receiveMessageUrl,
            type: "POST",
            dataType: "json",
            success: function (data) {
                $.each(data, function (index, item) {
                    $(".ChatInfoName").text(item.sendUserName);
                    $(".ChatInfoName").attr("value", item.sendUserId);
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
                /*htmlStr +="<div class='chat-list-people'>"
                        +"<div><img src='' alt='头像' /></div>"
                        +"<div class='chat-name'>"
                        +    "<p>"+data.senUserName+"</p>"
                        +"</div>"
                        +"<div class='message-num'>"+messageCount+"</div></div>"*/
                    
            }
        });
        //  $("#chatBoxList").html(htmlStr);
    });

    $("#contactMe").click(function () {
        $(".chatBox").toggle(10);
        var n = $(this).index();
        $(".chatBox-head-one").hide();
        $(".chatBox-head-two").show();
        $(".chatBox-list").hide();
        $(".chatBox-kuang").show();

        $(".ChatInfoName").attr("value", $("#contactMe").attr("value"));
        //传名字
        $(".ChatInfoName").text($("#userNickName").text());

        //传头像
        $(".ChatInfoHead>img").attr("src", $(this).children().eq(0).children("img").attr("src"));

        //聊天框默认最底部
        $(document).ready(function () {
            $("#chatBox-content-demo").scrollTop($("#chatBox-content-demo")[0].scrollHeight);
        });
    });
    $("#chat-fasong").click(function () {
        var content = $(".div-textarea").html().replace(/[\n\r]/g, '');
        $("#sendBox").html("");
        var formData = new FormData();
        var receiveUserId = $(".ChatInfoName").attr("value");
        var sendUserName = $("#useritem").text();
        var receiveUserName = $(".ChatInfoName").html();
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