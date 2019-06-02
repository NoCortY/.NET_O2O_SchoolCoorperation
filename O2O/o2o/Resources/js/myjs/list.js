$(function () {
    var allListUrl = "../../Controller/supply.ashx?action=all";
    var categoryListUrl = "../../Controller/supply.ashx?action=category";
    var nameListUrl = "../../Controller/supply.ashx?action=name";
    var listCategoryUrl = "../../Controller/supply.ashx?action=listcategory";
    var htmlStr = "";
    $.ajax({
        type: 'POST',
        url: listCategoryUrl,
        dataType: 'json',
        success: function (data) {
            $.each(data, function (index, item) {
                htmlStr+="<option class='bs-title-option' value='"+item.categoryId+"'>"+item.categoryName+"</option>"
            });
            $("#categoryselect").html(htmlStr);
            htmlStr = "";
        }
    });

    $.ajax({
        type: 'POST',
        url: allListUrl,
        dataType: 'json',
        success: function (data) {
            var count = 0;
            $.each(data, function (index, item) {
                count++;
                htmlStr += "<div class='col-md-12 col-sm-12 col-xs-12'>"
                                            + "<div class='row ListriBox'>"
                                                + "<div class='col-md-5 col-sm-6 col-xs-12 Nopadding'>"
                                                    + "<figure>"
                                                        + " <a href='listings-single-page-3.html'>"
                                                            + "<img src='../images/" + item.supplyFirstImgPath + "' class='img-fluid' alt='图片'>"
                                                            + "<div class='read_more'><span>更多信息</span></div></a></figure></div>"
                                                + "<div class='col-md-7 col-sm-6 col-xs-12 Nopadding'>"
                                                    + "<div class='ListriBoxmain'>"
                                                        + "<h3><a href='listings-single-page-3.html'>" + item.supplyName + "</a></h3>"
                                                        + "<p>" + item.supplyDesc + "</p>"
                                                        + ""
                                                    + "<ul><li><a class='address' href=''>发布人:" + item.nickName + "&nbsp;&nbsp;Tel:" + item.teleNumber + "</a></li></ul></div></div></div></div>";

                $("#RequirementAndSupplyList").html(htmlStr);
            });
            $("#count").text(count);
        }
    });
    $("#categoryselect").change(function () {
        var count = 0;
        htmlStr = "";
        var categoryId = $("#categoryselect option:selected").val();
        $.ajax({
            type: 'POST',
            url: categoryListUrl,
            data: {"categoryId":categoryId},
            dataType: 'json',
            success: function (data) {
                if (data.success != "false") {
                    $.each(data, function (index, item) {
                        count++;
                        htmlStr += "<div class='col-md-12 col-sm-12 col-xs-12'>"
                                                    + "<div class='row ListriBox'>"
                                                        + "<div class='col-md-5 col-sm-6 col-xs-12 Nopadding'>"
                                                            + "<figure>"
                                                                + "<a href='listings-single-page-3.html'>"
                                                                    + "<img src='../images/" + item.supplyFirstImgPath + "' class='img-fluid' alt='图片'>"
                                                                    + "<div class='read_more'><span>更多信息</span></div></a></figure></div>"
                                                        + "<div class='col-md-7 col-sm-6 col-xs-12 Nopadding'>"
                                                            + "<div class='ListriBoxmain'>"
                                                                + "<h3><a href='listings-single-page-3.html'>" + item.supplyName + "</a></h3>"
                                                                + "<p>" + item.supplyDesc + "</p>"
                                                                + ""
                                                            + "<ul><li><a class='address' href=''>发布人:" + item.nickName + "&nbsp;&nbsp;Tel:" + item.teleNumber + "</a></li></ul></div></div></div></div>";

                        $("#RequirementAndSupplyList").html(htmlStr);
                    });
                } else {
                    $("#RequirementAndSupplyList").html(htmlStr);
                }
                $("#count").text(count);
            }
        });
    });
    $("#search").click(function () {
        var count = 0;
        htmlStr = "";
        $.ajax({
            type: 'POST',
            url: nameListUrl,
            data: $('#searchsubmit').serialize(),
            dataType: 'json',
            success: function (data) {
                if (data.success != "false") {
                    $.each(data, function (index, item) {
                        count++;
                        htmlStr += "<div class='col-md-12 col-sm-12 col-xs-12'>"
                                                    + "<div class='row ListriBox'>"
                                                        + "<div class='col-md-5 col-sm-6 col-xs-12 Nopadding'>"
                                                            + "<figure>"
                                                                + "<a href='listings-single-page-3.html'>"
                                                                    + "<img src='../images/" + item.supplyFirstImgPath + "' class='img-fluid' alt='图片'>"
                                                                    + "<div class='read_more'><span>更多信息</span></div></a></figure></div>"
                                                        + "<div class='col-md-7 col-sm-6 col-xs-12 Nopadding'>"
                                                            + "<div class='ListriBoxmain'>"
                                                                + "<h3><a href='listings-single-page-3.html'>" + item.supplyName + "</a></h3>"
                                                                + "<p>" + item.supplyDesc + "</p>"
                                                                + ""
                                                            + "<ul><li><a class='address' href=''>发布人:" + item.nickName + "&nbsp;&nbsp;Tel:" + item.teleNumber + "</a></li></ul></div></div></div></div>";

                        $("#RequirementAndSupplyList").html(htmlStr);
                    });
                } else {
                    $("#RequirementAndSupplyList").html(htmlStr);
                }
                $("#count").text(count);
            }
        });
    });
});