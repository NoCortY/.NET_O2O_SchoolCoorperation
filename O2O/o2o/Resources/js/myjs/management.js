$(function () {
    var supplyListManageUrl = "../../Controller/SupplyManagementController.ashx?action=supplyListManagement";
    var updateSupplyUrl = "../../Controller/SupplyManagementController.ashx?action=updateSupply";
    var listUserManageUrl = "../../Controller/UserManagementController.ashx?action=listUsers";
    var requirementListManageUrl = "../../Controller/RequirementManagementController.ashx?action=requirementListManagement";
    var updateRequirementUrl = "../../Controller/RequirementManagementController.ashx?action=updateRequirement";
    var commonUpdateUrl;
    $("#supplyManagement").click(function () {
        $("#supplyManagementLi").attr("class", "active");
        $("#requirementManagementLi").attr("class", "");
        $("#categoryManagementLi").attr("class", "");
        $("#userManagementLi").attr("class", "");
        var htmlStr = "<thead><tr><th>Id</th><th>名称</th><th>优先级</th><th>Status</th><th>发布人</th><th>操作</th></tr></thead><tbody>";
            $.ajax({
                type: "POST",
                url: supplyListManageUrl,
                dataType: 'json',
                success: function (data) {
                    $.each(data, function (index, item) {
                        var priority = "禁用";
                        var status = "正常";
                        var manage = "<a href='#' class='btn btn-danger' role='button' name='btmanage' id='btmanage'>禁用";
                        if(item.priority=="1"){
                            priority = "正常";
                        }
                        if(item.supplyStatus=="0"){
                            status ="禁用";
                            manage = "<a href='#' class='btn btn-success' role='button' name='btmanage' id='btmanage'>解除禁用";
                        }
                        htmlStr+=      "<tr>"
                                +      "<td id = 'Id'>" + item.Id + "</td>"
                                +      "<td id = 'name'>" +item.supplyName+"</td>"
                                +      "<td id = 'priority' value='"+item.priority+"'>" + priority + "</td>"
                                +      "<td id = 'status' value='"+ item.supplyStatus + "' >" + status + "</td>"
                                +      "<td id = 'nickName value='" + item.nickName+"'>"+item.nickName+"</td>"
                                +      "<td id = 'manage'>"+manage+"</td></tr>"
                        
                    });
                    htmlStr += "</tbody>";
                    $("#managelist").html(htmlStr);
                    $("#addButton").html("");
                    htmlStr = "";
                }
            });
    });
    $("#requirementManagement").click(function () {
        $("#categoryManagementLi").attr("class", "");
        $("#requirementManagementLi").attr("class", "active");
        $("#supplyManagementLi").attr("class", "");
        $("#userManagementLi").attr("class", "");
        var htmlStr = "<thead><tr><th>Id</th><th>名称</th><th>优先级</th><th>Status</th><th>发布人</th><th>操作</th></tr></thead><tbody>";
        $.ajax({
            type: "POST",
            url: requirementListManageUrl,
            dataType: 'json',
            success: function (data) {
                $.each(data, function (index, item) {
                    var priority = "禁用";
                    var status = "正常";
                    var manage = "<a href='#' class='btn btn-danger' role='button' name='btmanage' id='btmanage'>禁用";
                    if (item.priority == "1") {
                        priority = "正常";
                    }
                    if (item.requirementStatus == "0") {
                        status = "禁用";
                        manage = "<a href='#' class='btn btn-success' role='button' name='btmanage' id='btmanage'>解除禁用";
                    }
                    htmlStr += "<tr>"
                            + "<td id = 'Id'>" + item.Id + "</td>"
                            + "<td id = 'name'>" + item.requirementName + "</td>"
                            + "<td id = 'priority' value='" + item.priority + "'>" + priority + "</td>"
                            + "<td id = 'status' value='" + item.requirementStatus + "' >" + status + "</td>"
                            + "<td id = 'nickName value='" + item.nickName + "'>" + item.nickName + "</td>"
                            + "<td id = 'manage'>" + manage + "</td></tr>"

                });
                htmlStr += "</tbody>";
                $("#managelist").html(htmlStr);
                $("#addButton").html("");
                htmlStr = "";
            }
        });
    });


    /*类别管理*/
    var categoryListManageUrl = "../../Controller/CategoryManagementController.ashx?action=getAllcategory"
    var deleteCategoryManageUrl = "../../Controller/CategoryManagementController.ashx?action=deletecategory"
    var addCategoryManageUrl = "../../Controller/CategoryManagementController.ashx?action=addcategory";
    $("#categoryManagement").click(function () {
        $("#categoryManagementLi").attr("class", "active");
        $("#requirementManagementLi").attr("class", "");
        $("#supplyManagementLi").attr("class", "");
        $("#userManagementLi").attr("class", "");
        var htmlStr = "<thead><tr><th>Id</th><th>名称</th><th>创建时间</th><th>修改时间</th><th>操作</th></tr></thead><tbody>";
        $.ajax({
            type: "POST",
            url: categoryListManageUrl,
            dataType: 'json',
            success: function (data) {
                $.each(data, function (index, item) {
                    var manage = "<button  class='btn btn-danger' role='button' name='categorymanage' id='categorymanage'>删除";
                    
                    htmlStr += "<tr>"
                            + "<td id = 'Id'>" + item.categoryId + "</td>"
                            + "<td id = 'name'>" + item.categoryName + "</td>"
                            + "<td id = 'createTime' value='" + item.createTime + "'>" + item.createTime + "</td>"
                            + "<td id = 'modifyTime' value='" + item.modifyTime + "' >" + item.modifyTime + "</td>"
                            + "<td id = 'manage'>" + manage + "</td></tr>"
                    
                });
                htmlStr += "</tbody>";
                $("#addButton").html("<input type='button' class='btn btn-success' value='新增分类' />");
                $("#managelist").html(htmlStr);
                htmlStr = "";
            }
        });
    });
    $("#addButton").on("click", "input", function () {
        var categoryName = prompt("请输入要添加的分类名:");
        $.ajax({
            type: "POST",
            url: addCategoryManageUrl,
            data:{"categoryName":categoryName},
            dataType:"json",
            success: function (data) {
                if (data.success == "true") {
                    var htmlStr = "<thead><tr><th>Id</th><th>名称</th><th>创建时间</th><th>修改时间</th><th>操作</th></tr></thead><tbody>";
                    $.ajax({
                        type: "POST",
                        url: categoryListManageUrl,
                        dataType: 'json',
                        success: function (data) {
                            $.each(data, function (index, item) {
                                var manage = "<button  class='btn btn-danger' role='button' name='categorymanage' id='categorymanage'>删除";

                                htmlStr += "<tr>"
                                        + "<td id = 'Id'>" + item.categoryId + "</td>"
                                        + "<td id = 'name'>" + item.categoryName + "</td>"
                                        + "<td id = 'createTime' value='" + item.createTime + "'>" + item.createTime + "</td>"
                                        + "<td id = 'modifyTime' value='" + item.modifyTime + "' >" + item.modifyTime + "</td>"
                                        + "<td id = 'manage'>" + manage + "</td></tr>"

                            });
                            htmlStr += "</tbody>";
                            $("#managelist").html(htmlStr);
                            htmlStr = "";
                        }
                    });
                } else {
                    alert("添加失败,服务器异常");
                }
            }
        });
    });
    $("#managelist").on("click", "#categorymanage", function () {
        var id = $(this).parent().parent().children("#Id").text();
        $.ajax({
            type: "POST",
            url:deleteCategoryManageUrl,
            data: { categoryId: id },
            dataType:"json",
            success: function (data) {
                if (data.success = "true") {
                    var htmlStr = "<thead><tr><th>Id</th><th>名称</th><th>创建时间</th><th>修改时间</th><th>操作</th></tr></thead><tbody>";
                    $.ajax({
                        type: "POST",
                        url: categoryListManageUrl,
                        dataType: 'json',
                        success: function (data) {
                            $.each(data, function (index, item) {
                                var manage = "<button  class='btn btn-danger' role='button' name='categorymanage' id='categorymanage'>删除";

                                htmlStr += "<tr>"
                                        + "<td id = 'Id'>" + item.categoryId + "</td>"
                                        + "<td id = 'name'>" + item.categoryName + "</td>"
                                        + "<td id = 'createTime' value='" + item.createTime + "'>" + item.createTime + "</td>"
                                        + "<td id = 'modifyTime' value='" + item.modifyTime + "' >" + item.modifyTime + "</td>"
                                        + "<td id = 'manage'>" + manage + "</td></tr>"

                            });
                            htmlStr += "</tbody>";
                            $("#managelist").html(htmlStr);
                            htmlStr = "";
                        }
                    });
                }
                else {
                    alert("删除失败,服务器异常");
                }
            }
        });
    });
    $("#managelist").on("click", "a", function () {
        if ($("#requirementManagementLi").attr("class") == "active") {
            commonUpdateUrl = updateRequirementUrl;
        } else {
            commonUpdateUrl = updateSupplyUrl;
        }
        var id = $(this).parent().parent().children("#Id").text();
        var name = $(this).parent().parent().children("#name").text();
        var priority = $(this).parent().parent().children("#priority").attr("value");
        var status = $(this).parent().parent().children("#status").attr("value");
        if (priority == "1") {
            priority = "0";
            $(this).parent().parent().children("#priority").text("禁用");
           
        }
        else {
            $(this).parent().parent().children("#priority").text("正常");
            priority = "1";
        }
        if (status == "1") {
            status = "0";
            $(this).parent().parent().children("#status").text("禁用");
            $(this).parent().parent().children("#manage").html("<a href='#' class='btn btn-success' role='button' name='btmanage' id='btmanage'>解除禁用");
        }
        else {
            status = "1";
            $(this).parent().parent().children("#status").text("正常");
             $(this).parent().parent().children("#manage").html("<a href='#' class='btn btn-danger' role='button' name='btmanage' id='btmanage'>禁用");
        }
        $.ajax({
            type: "POST",
            url: commonUpdateUrl,
            dataType: 'json',
            data:{id:id,name:name,priority:priority,status:status},
            success: function (data) {
                if (data.success == "true") {
                }
            }
        });
    });
    $("#userManagement").click(function () {
        $("#categoryManagementLi").attr("class", "");
        $("#requirementManagementLi").attr("class", "");
        $("#supplyManagementLi").attr("class", "");
        $("#userManagementLi").attr("class", "active");
        var htmlStr = "<thead><tr><th>Id</th><th>用户名</th><th>性别</th><th>昵称</th><th>真实姓名</th><th>用户状态</th><th>注册时间</th><th>操作</th></tr></thead><tbody>";
        $.ajax({
            type: "POST",
            url: listUserManageUrl,
            dataType: 'json',
            success: function (data) {
                $.each(data, function (index, item) {
                    var status = "正常";
                    var manage = "<button href='#' class='btn btn-danger' role='button' name='userManage' id='userManage'>禁用";
                    if (item.userStatus == "-1") {
                        status = "禁用";
                        manage = "<button href='#' class='btn btn-success' role='button' name='userManage' id='userManage'>解除禁用";
                    }
                    if (item.userStatus == "1") {
                        status = "超级管理员"
                        manage = "";
                    }
                    htmlStr += "<tr>"
                            + "<td id = 'Id'>" + item.Id + "</td>"
                            + "<td id = 'name'>" + item.username + "</td>"
                            + "<td id = 'gender'>" + item.gender + "</td>"
                            + "<td id = 'nickName'>" + item.nickName + "</td>"
                            + "<td id = 'realName'>" + item.realName + "</td>"
                            + "<td id = 'status' value='" + item.userStatus + "' >" + status + "</td>"
                            + "<td id = 'registerTime'>" + item.registerTime + "</td>"
                            + "<td id = 'manage'>" + manage + "</td></tr>"

                });
                htmlStr += "</tbody>";
                $("#managelist").html(htmlStr);
                $("#addButton").html("");
                htmlStr = "";
            }
        });
    });
    /*用户管理*/
    var userManagementUrl = "../../Controller/UserManagementController.ashx?action=updateUserStatus";
    $("#managelist").on("click", "#userManage", function () {
        var id = $(this).parent().parent().children("#Id").text();
        var name = $(this).parent().parent().children("#name").text();
        var status = $(this).parent().parent().children("#status").attr("value");
        if (status == "0") {
            status = "-1";
            $(this).parent().parent().children("#status").text("禁用");
            $(this).parent().parent().children("#manage").html("<a href='#' class='btn btn-success' role='button' name='btmanage' id='btmanage'>解除禁用");
        }
        else {
            status = "0";
            $(this).parent().parent().children("#status").text("正常");
            $(this).parent().parent().children("#manage").html("<a href='#' class='btn btn-danger' role='button' name='btmanage' id='btmanage'>禁用");
        }
        $.ajax({
            type: "POST",
            url: userManagementUrl,
            dataType: 'json',
            data: { id: id, status: status },
            success: function (data) {
                if (data.success == "true") {
                    alert("操作成功");
                }
            }
        });
    });
});