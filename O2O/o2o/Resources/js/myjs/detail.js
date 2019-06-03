$(function () {
    
    /*获取地址栏参数*/
    (function ($) {

        $.getUrlParam

         = function (name) {

             var reg

              = new RegExp("(^|&)" +

              name + "=([^&]*)(&|$)");

             var r

              = window.location.search.substr(1).match(reg);

             if (r != null) return unescape(r[2]); return null;

         }})(jQuery);
        var getSupplyDetailUrl = "../../Controller/supply.ashx?action=getsupplydetail&supplyId="+$.getUrlParam('supplyId');
        var getSupplyDescImgUrl = "../../Controller/supply.ashx?action=getsupplydescimg&supplyId="+$.getUrlParam('supplyId');
        $.ajax({
        type: "POST",
        url:getSupplyDetailUrl,
        dataType: "json",
        success: function (data) {
            var htmlStr = "";
            $("#categoryName").text(data.categoryName);
            $("#Name").text(data.supplyName);
            $("#supplyDesc").text(data.supplyDesc);
            $("#userNickName").text(data.userName);
            $("#teleNum").text(data.userTeleNum);
            $("#userEMail").text(data.userEMail);
            htmlStr+="<img src=../header/'"+data.userHeader+"' alt='头像' class='avatar avatar-xl p-2 mb-2'>";
            $("#userHeader").html(htmlStr);
            $("#descImg").html(htmlStr);
            htmlStr = "";
            $.ajax({
                type: "POST",
                url:getSupplyDescImgUrl,
                dataType: "json",
                success:function(data){
                    $.each(data, function (index, item) {
                        htmlStr += "<div class='col-lg-4 col-6 px-1 mb-2'><a href='../images/" + item.supplyDescImg + "'><img src='../images/" + item.supplyDescImg + "' alt='详情图' class='img-fluid'></a></div>";
                    });
                    $("#descImg").html(htmlStr)
                }
            });
        }
    });
});