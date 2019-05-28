$(function () {
    var allListUrl = "../../Controller/supply.ashx?action=test";
    var htmlStr = "";
    $.ajax({
        type: 'POST',
        url: allListUrl,
        dataType: 'json',
        success: function (data) {
            jQuery.each(data, function (index, item) {
                htmlStr+="<div class='col-md-12 col-sm-12 col-xs-12'>"
                                            + "<div class='row ListriBox'>"
                                                + "<div class='col-md-5 col-sm-6 col-xs-12 Nopadding'>"
                                                    + "<figure>"
                                                        + "<a href='listings-single-page-3.html' class='wishlist_bt'></a> <a href='listings-single-page-3.html'>"
                                                            + "<img src='images/hotel1.jpg' class='img-fluid' alt=''>"
                                                            + "<div class='read_more'><span>更多信息</span></div></a></figure></div>"
                                                + "<div class='col-md-7 col-sm-6 col-xs-12 Nopadding'>"
                                                    + "<div class='ListriBoxmain'>"
                                                        + "<h3><a href='listings-single-page-3.html'>"+item.supplyName+"</a></h3>"
                                                        + "<p>"+item.supplyDesc+"</p>"
                                                        + ""
                                                    + "<ul><li><a class='address' href=''>发布人:" + item.nickName + "&nbsp;&nbsp;Tel:" + item.teleNumber + "</a></li></ul></div></div></div>";

                $("#RequirementAndSupplyList").html(htmlStr);
            });
        }
    });
    $("#search").click(function () {
        
    });
});