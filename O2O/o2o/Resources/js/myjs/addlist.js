$(function () {
    var getCategoryUrl = "../../Controller/supply.ashx?action=listcategory";
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
    /*$('#descImgGroup').on('change', '#descImg:last-child', function () {

        if ($(".form-control").length < 6) {

            $('#descImgGroup').append('<input class="form-control" type="file" id ="descImg" style="height:40px;" />');

        }
    });*/
    var supplyId;
    var addUrl = "../../Controller/supply.ashx?action=addsupply";
    var addImgUrl = "../../Controller/supply.ashx?action=addsupplyimg";
    var addDescImgUrl = "../../Controller/supply.ashx?action=addsupplydescimg"
    $("#submit").click(function () {
        /*if ($("#requirementOrSupply").find("option:selected").val() == 0) {
            addSupplyUrl = "../../Controller/";
        }*/
        $.ajax({
            type: "POST",
            url: addUrl,
            data: { Name: $("#Name").val(), Desc: $("#Desc").text(), CategoryId: $("#category").find("option:selected").val() },
            dataType: "json",
            success: function (data) {
                if (data.success == "true") {
                    supplyId = data.supplyId;
                    alert("添加成功");
                    /*多个ajax之间是并行的,所以这里用嵌套的结构*/


                    var formData = new FormData();
                    formData.append("supplyId", supplyId);
                    formData.append("smallImg", $('#smallImg')[0].files[0]);
                    $.ajax({
                        type: "POST",
                        url: addImgUrl,
                        data: formData,
                        dataType: "json",
                        contentType: false,
                        processData: false,
                        cache: false,
                        success: function (data) {
                            if (data.success == "true") {



                                var formData2 = new FormData();
                                for (var i = 0; i < $("#descImg")[0].files.length; i++) {
                                    formData2.append("descImg" + i, $("#descImg")[0].files[i]);
                                }
                                formData2.append("supplyId", supplyId);
                                $.ajax({
                                    type: "POST",
                                    url: addDescImgUrl,
                                    data: formData2,
                                    processData: false,
                                    contentType: false,
                                    async: false,
                                    success: function (data) {
                                        if (data.success == "true") {
                                            alert('添加图片成功');
                                        }
                                    }
                                });
                            }
                        }
                    });
                } else {
                    alert("添加失败");
                }
            }
        });
       
        

    });
});