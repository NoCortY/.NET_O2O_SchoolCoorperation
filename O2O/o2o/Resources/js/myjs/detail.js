$(function () {
    var receiveUserId;
    var submitEvaluate = "../../Controller/EvaluateController.ashx?action=createuserevaluate";
    /*获取地址栏参数*/
    (function ($) {
        $.getUrlParam = function (name) {
             var reg = new RegExp("(^|&)" +name + "=([^&]*)(&|$)");
             var r= window.location.search.substr(1).match(reg);
             if (r != null) return unescape(r[2]); return null;
         }})(jQuery);
    if ($.getUrlParam("classify") == 0) {
        var getSupplyDetailUrl = "../../Controller/supply.ashx?action=getsupplydetail&Id=" + $.getUrlParam('Id');
        var getSupplyDescImgUrl = "../../Controller/supply.ashx?action=getsupplydescimg&Id=" + $.getUrlParam('Id');
        
        
    } else {
        var getSupplyDetailUrl = "../../Controller/requirement.ashx?action=getrequirementdetail&Id=" + $.getUrlParam('Id');
        var getSupplyDescImgUrl = "../../Controller/requirement.ashx?action=getrequirementdescimg&Id=" + $.getUrlParam('Id');
        
    }
        $.ajax({
        type: "POST",
        url:getSupplyDetailUrl,
        dataType: "json",
        success: function (data) {
            var htmlStr = "";
            $("#contactMe").attr("value", data.userId);
            $("#categoryName").text(data.categoryName);
            $("#Name").text(data.Name);
            $("#Desc").text(data.Desc);
            $("#userNickName").text(data.userName);
            $("#teleNum").text(data.userTeleNum);
            $("#userEMail").text(data.userEMail);
            htmlStr+="<img src='../header/"+data.userHeader+"' alt='发布者头像'>";
            $("#publishUserHeader").html(htmlStr);
            receiveUserId = data.userId;
            htmlStr = "";
            $.ajax({
                type: "POST",
                url:getSupplyDescImgUrl,
                dataType: "json",
                success:function(data){
                    $.each(data, function (index, item) {
                        htmlStr += "<div class='col-lg-4 col-6 px-1 mb-2'><a href='../images/" + item.supplyDescImg + "'><img src='../images/" + item.DescImg + "' alt='详情图' class='img-fluid'></a></div>";
                    });
                    $("#descImg").html(htmlStr)
                }
            });
        }
    });

       
        
        $("#submitEvaluate").click(function () {
            var evaluateText = $("#evaluate").val();
            if (evaluateText != "") {
                $.ajax({
                    type: "POST",
                    url: submitEvaluate,
                    data: {"receiveUserId":receiveUserId,"evaluateContext":evaluateText},
                    dataType: "json",
                    success: function (data) {
                        if (data.success == "true") {
                            alert("评论成功");
                        } else {
                            alect("请先登录");
                        }
                    }
                });
            }
        });
     });