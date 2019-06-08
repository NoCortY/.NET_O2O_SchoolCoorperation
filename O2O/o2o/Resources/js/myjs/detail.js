$(function () {
    var receiveUserId;
    var submitEvaluate = "../../Controller/EvaluateController.ashx?action=createuserevaluate";
    /*获取地址栏参数*/
    (function ($) {
        $.getUrlParam = function (name) {
             var reg = new RegExp("(^|&)" +name + "=([^&]*)(&|$)");
             var r= window.location.search.substr(1).match(reg);
             if (r != null) return unescape(r[2]); return null;
        }
    })(jQuery);

    var getAllUserEvaluateUrl = "../../Controller/EvaluateController.ashx?action=getalluserevaluate";
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
            $("title").html(data.Name + "-详情");
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
                success: function (data) {
                    $.ajax({
                        type: "POST",
                        url: getAllUserEvaluateUrl,
                        data: { "receiveUserId": receiveUserId },
                        dataType: "json",
                        success: function (data) {
                            var htmlStr = "";
                            $.each(data, function (index, item) {
                                htmlStr += "<div class='media-body'>"
                                        + "<h5 class='mt-2 mb-1'>" + item.sendUserName + "</h5>"
                                        + "<div class='mb-2'><i class='fa fa-xs fa-star text-primary'></i><i class='fa fa-xs fa-star text-primary'></i><i class='fa fa-xs fa-star text-primary'></i><i class='fa fa-xs fa-star text-primary'></i><i class='fa fa-xs fa-star text-primary'></i> </div>"
                                        + "<p class='text-muted text-sm'>" + item.content + "</p></div><br/><hr class='featurette-divider'>";
                            });
                            $("#userEvaluate").html(htmlStr);
                            htmlStr = "";
                        }
                    });
                    $.each(data, function (index, item) {
                        htmlStr += "<div class='col-lg-4 col-6 px-1 mb-2'><a href='../images/" + item.DescImg + "'><img src='../images/" + item.DescImg + "' alt='详情图' class='img-fluid'></a></div>";
                    });
                    $("#descImg").html(htmlStr)
                    htmlStr = "";
                }
            });
        }
    });

        
        
        $("#submitEvaluate").click(function () {
            var evaluateText = $("#evaluate").text();
            if (evaluateText != "") {
                $.ajax({
                    type: "POST",
                    url: submitEvaluate,
                    data: {"receiveUserId":receiveUserId,"evaluateContext":evaluateText},
                    dataType: "json",
                    success: function (data) {
                        if (data.success == "true") {
                            var htmlStr = "";
                            $.ajax({
                                type: "POST",
                                url: getAllUserEvaluateUrl,
                                data:{"receiveUserId":receiveUserId},
                                dataType: "json",
                                success: function (data) {
                                    $.each(data, function (index, item) {
                                        htmlStr += "<div class='media-body'>"
                                                +"<h6 class='mt-2 mb-1'>"+item.sendUserName+"</h6>"
                                                +"<div class='mb-2'><i class='fa fa-xs fa-star text-primary'></i><i class='fa fa-xs fa-star text-primary'></i><i class='fa fa-xs fa-star text-primary'></i><i class='fa fa-xs fa-star text-primary'></i><i class='fa fa-xs fa-star text-primary'></i> </div>"
                                                +"<p class='text-muted text-sm'>"+item.content+"</p></div><br/><hr class='featurette-divider'>";
                                    });
                                    $("#userEvaluate").html(htmlStr);
                                    $(".div-textarea").html("");
                                    htmlStr = "";
                                }
                            });
                            alert("评论成功");
                        } else {
                            alect("请先登录");
                        }
                    }
                });
            }
        });
     });