$(function () {
    var addUrl = "../../Controller/supply.ashx?action=addsupply";
    var addImgUrl = "../../Controller/supply.ashx?action=addsupplyimg";
    var addDescImgUrl = "../../Controller/supply.ashx?action=addsupplydescimg";
    var getCategoryUrl = "../../Controller/supply.ashx?action=listcategory";
    (function ($) {
        $.getUrlParam = function (name) {
            var reg = new RegExp("(^|&)" +name + "=([^&]*)(&|$)");
            var r= window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }})(jQuery);
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
    if ($.getUrlParam('Id') != ""&&$.getUrlParam('Id')!=null) {
        var getInfomationUrl;
        if($.getUrlParam('classify')==0){
            getInfomationUrl = "../../Controller/supply.ashx?action=getsupplydetail&Id=" + $.getUrlParam('Id');
            $("#requirementOrSupply").find("option").eq(1).attr("selected", "selected");
            $("#requirementOrSupply").attr("disabled", "disabled").css("background-color", "#EEEEEE;");
        }else{
            getInfomationUrl = "../../Controller/requirement.ashx?action=getrequirementdetail&Id=" + $.getUrlParam('Id');
            $("#requirementOrSupply").find("option").eq(0).attr("selected", "selected");
            $("#requirementOrSupply").attr("disabled", "disabled").css("background-color", "#EEEEEE;");
        }
        $.ajax({
            type: "POST",
            url: getInfomationUrl,
            dataType: "json",
            success: function (data) {
                $("#Name").val(data.Name);
                $("#Desc").val(data.Desc);
            }
        });
    }
    var Id;
    $("#submit").click(function () {
        /*if ($("#requirementOrSupply").find("option:selected").val() == 0) {
            addSupplyUrl = "../../Controller/";
        }*/
        if ($.getUrlParam('Id') != "" && $.getUrlParam('Id') != null) {
            var updateUrl;
            if ($("#requirementOrSupply option:selected").val() == 1) {
                addUrl = "../../Controller/supply.ashx?action=addsupply";
                addImgUrl = "../../Controller/supply.ashx?action=addsupplyimg";
                addDescImgUrl = "../../Controller/supply.ashx?action=addsupplydescimg";
                updateUrl = "../../Controller/ordinaryUserManagement.ashx?action=updatemysupply";
            }
            else {
                addUrl = "../../Controller/requirement.ashx?action=addrequirement";
                addImgUrl = "../../Controller/requirement.ashx?action=addrequirementimg";
                addDescImgUrl = "../../Controller/requirement.ashx?action=addrequirementdescimg";
                updateUrl = "../../Controller/ordinaryUserManagement.ashx?action=updatemyrequirement";
            }
            var desc = $("#Desc").val().replace(/[\n\r]/g, ".");
            var formData = new FormData();
            formData.append("Id", $.getUrlParam('Id'));
            formData.append("Name",$("#Name").val());
            formData.append("Desc",desc);
            formData.append("categoryId",$("#category").find("option:selected").val());
            formData.append("descImg0", $('#smallImg')[0].files[0]);
            formData.append("status0","0");
            for (var i = 0; i < $("#descImg")[0].files.length; i++) {
                alert("status" + (i + 1));
                formData.append("descImg" + (i+1), $("#descImg")[0].files[i]);
                formData.append("status"+(i+1),1);
            }
            $.ajax({
                type: "POST",
                url: updateUrl,
                data: formData,
                processData: false,
                contentType: false,
                async: false,
                dataType: "json",
                success: function (data) {
                    if (data.success = "true") {
                        alert("修改成功");
                        window.location("../../Views/usermanagement.html");
                        return;
                    }
                    else {
                        alert("修改失败");
                        return;
                    }
                }
            });
        }else{
        if ($("#requirementOrSupply option:selected").val() == 1) {
            addUrl = "../../Controller/supply.ashx?action=addsupply";
            addImgUrl = "../../Controller/supply.ashx?action=addsupplyimg";
            addDescImgUrl = "../../Controller/supply.ashx?action=addsupplydescimg";
        }
        else {
            addUrl = "../../Controller/requirement.ashx?action=addrequirement";
            addImgUrl = "../../Controller/requirement.ashx?action=addrequirementimg";
            addDescImgUrl = "../../Controller/requirement.ashx?action=addrequirementdescimg";
        }
        var desc = $("#Desc").val().replace(/[\n\r]/g, ".");
        $.ajax({
            type: "POST",
            url: addUrl,
            data: { Name: $("#Name").val(), Desc: desc, CategoryId: $("#category").find("option:selected").val() },
            dataType: "json",
            success: function (data) {
                if (data.success == "true") {
                    Id = data.Id;
                    alert("添加成功");
                    /*多个ajax之间是并行的,所以这里用嵌套的结构*/


                    var formData = new FormData();
                    formData.append("Id", Id);
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
                                formData2.append("Id", Id);
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
        }
    });
});