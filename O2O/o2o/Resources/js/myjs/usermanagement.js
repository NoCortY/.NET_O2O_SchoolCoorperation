$(function () {
    var getSupplyListUrl = "../../Controller/ordinaryUserManagement.ashx?action=supplymanage";
    $("#mySupplyManagement").click(function () {
        var htmlStr = "<thead><tr><th>Id</th><th>名称</th><th>优先级</th><th>状态</th><th>创建时间</th><th>修改时间</th><th>操作</th></tr></thead><tbody>";
        $("#mySupplyManagementLi").attr("class", "active");
        $("#myRequirementManagementLi").attr("class", "");
        $("#myAccountManagementLi").attr("class", "");
        $("#myEvaluateManagementLi").attr("class", "");
        $.ajax({
            type: "POST",
            url: getSupplyListUrl,
            dataType: "json",
            success: function (data) {
                $.each(data, function (index, item) {
                    htmlStr += "<tr><td id='Id'>"+item.Id+"</td>"
                            +  "<td id='Name'>"+item.supplyName+"</td>"
                            +  "<td>"+item.priority+"</td>"
                            +  "<td>"+item.supplyStatus+"</td>"
                            +  "<td>"+item.createTime+"</td>"
                            +  "<td>"+item.modifyTime+"</td>"
                            +  "<td><input type='button' class='btn btn-success' name='updateSupply' id='updateSupply' value='修改' />&nbsp;"
                            +  "<input type='button' class='btn btn-danger' name='deleteSupply' id='deleteSupply' value='删除' /></td></tr>"
                });
                htmlStr += "</tbody>";
                $("#managelist").html(htmlStr);
            }
        });
    });
    var deleteMySupplyUrl = "../../Controller/ordinaryUserManagement.ashx?action=deletemysupply";
    $("#managelist").on("click", "#deleteSupply", function () {
        var id = $(this).parent().parent().children("#Id").text();
        $.ajax({
            type: "POST",
            url: deleteMySupplyUrl,
            data: { Id: id },
            dataType: "json",
            success: function (data) {
                if (data.success == "true") {
                    var htmlStr = "<thead><tr><th>Id</th><th>名称</th><th>优先级</th><th>状态</th><th>创建时间</th><th>修改时间</th><th>操作</th></tr></thead><tbody>";
                    $.ajax({
                        type: "POST",
                        url: getSupplyListUrl,
                        dataType: "json",
                        success: function (data) {
                            $.each(data, function (index, item) {
                                htmlStr += "<tr><td id='Id'>" + item.Id + "</td>"
                                        + "<td id='Name'>" + item.supplyName + "</td>"
                                        + "<td>" + item.priority + "</td>"
                                        + "<td>" + item.supplyStatus + "</td>"
                                        + "<td>" + item.createTime + "</td>"
                                        + "<td>" + item.modifyTime + "</td>"
                                        + "<td><input type='button' class='btn btn-success' id='updateSupply' value='修改' />&nbsp;"
                                        + "<input type='button' class='btn btn-danger' id='deleteSupply' value='删除' /></td></tr>"
                            });
                            htmlStr += "</tbody>";
                            $("#managelist").html(htmlStr);
                        }
                    });
                }
            }
        });
    })
    $("#managelist").on("click", "#updateSupply", function () {

    });



    /*需求管理*/
    var getRequirementListUrl = "../../Controller/ordinaryUserManagement.ashx?action=requirementmanage";
    $("#myRequirementManagement").click(function () {
        var htmlStr = "<thead><tr><th>Id</th><th>名称</th><th>优先级</th><th>状态</th><th>创建时间</th><th>修改时间</th><th>操作</th></tr></thead><tbody>";
        $("#mySupplyManagementLi").attr("class", "");
        $("#myRequirementManagementLi").attr("class", "active");
        $("#myAccountManagementLi").attr("class", "");
        $("#myEvaluateManagementLi").attr("class", "");
        $.ajax({
            type: "POST",
            url: getRequirementListUrl,
            dataType: "json",
            success: function (data) {
                $.each(data, function (index, item) {
                    htmlStr += "<tr><td id='Id'>" + item.Id + "</td>"
                            + "<td id='Name'>" + item.requirementName + "</td>"
                            + "<td>" + item.priority + "</td>"
                            + "<td>" + item.requirementStatus + "</td>"
                            + "<td>" + item.createTime + "</td>"
                            + "<td>" + item.modifyTime + "</td>"
                            + "<td><input type='button' class='btn btn-success' name='updateRequirement' id='updateRequirement' value='修改' />&nbsp;"
                            + "<input type='button' class='btn btn-danger' name='deleteRequirement' id='deleteRequirement' value='删除' /></td></tr>"
                });
                htmlStr += "</tbody>";
                $("#managelist").html(htmlStr);
            }
        });
    });
    var deleteMyRequirementUrl = "../../Controller/ordinaryUserManagement.ashx?action=deletemyrequirement";
    $("#managelist").on("click", "#deleteRequirement", function () {
        var id = $(this).parent().parent().children("#Id").text();
        $.ajax({
            type: "POST",
            url: deleteMyRequirementUrl,
            data: { Id: id },
            dataType: "json",
            success: function (data) {
                if (data.success == "true") {
                    var htmlStr = "<thead><tr><th>Id</th><th>名称</th><th>优先级</th><th>状态</th><th>创建时间</th><th>修改时间</th><th>操作</th></tr></thead><tbody>";
                    $.ajax({
                        type: "POST",
                        url: getRequirementListUrl,
                        dataType: "json",
                        success: function (data) {
                            $.each(data, function (index, item) {
                                htmlStr += "<tr><td id='Id'>" + item.Id + "</td>"
                                        + "<td id='Name'>" + item.requirementName + "</td>"
                                        + "<td>" + item.priority + "</td>"
                                        + "<td>" + item.requirementStatus + "</td>"
                                        + "<td>" + item.createTime + "</td>"
                                        + "<td>" + item.modifyTime + "</td>"
                                        + "<td><input type='button' class='btn btn-success' id='updateRequirement' value='修改' />&nbsp;"
                                        + "<input type='button' class='btn btn-danger' id='deleteRequirement' value='删除' /></td></tr>"
                            });
                            htmlStr += "</tbody>";
                            $("#managelist").html(htmlStr);
                        }
                    });
                }
            }
        });
    })
    $("#managelist").on("click", "#updateRequirement", function () {

    });
});