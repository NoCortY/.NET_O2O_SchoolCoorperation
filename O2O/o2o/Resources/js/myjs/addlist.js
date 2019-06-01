$(function () {
    getCategoryUrl = "../../Controller/supply.ashx?action=listcategory";
    $.ajax({
        type: "POST",
        url: getCategoryUrl,
        dataType: "json",
        success: function (data) {
            var htmlStr = "";
            $.each(data, function (index, item) {
                htmlStr += "<option value='"+item.categoryId+"'>"+item.categoryName+"</option>";
            });
            $("#category").html(htmlStr);
        }
    });
    addSupplyUrl = "../../Controller/supply.ashx?action=addSupply";
    $("#submitSupply").click(function () {

    });
});