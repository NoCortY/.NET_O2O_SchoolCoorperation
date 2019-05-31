$(function () {
    var supplyListManageUrl = "../../Controller/SupplyManagementController.ashx?action=supplyListManagement";
    var updateSupplyUrl = "../../Controller/SupplyManagementController.ashx?action=updateSupply";
    var listUserManageUrl = "../../Controller/UserManagementController.ashx?action=listUsers";
    $("#supplyManagement").click(function () { 
        var htmlStr = "<thead><tr><th>Id</th><th>名称</th><th>优先级</th><th>Status</th><th>操作</th></tr></thead>";
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
                        htmlStr+=      "<tbody><tr>"
                                +      "<td id = 'Id'>" + item.Id + "</td>"
                                +      "<td id = 'name'>" +item.supplyName+"</td>"
                                +      "<td id = 'priority' value='"+item.priority+"'>" + priority + "</td>"
                                +      "<td id = 'status' value='"+ item.supplyStatus + "' >" + status + "</td>"
                                +      "<td id = 'manage'>"+manage+"</td></tr></tbody>"
                        
                    });
                    $("#managelist").html(htmlStr);
                    htmlStr = "";
                }
            });
    });

    $("#managelist").on("click", "a", function () {
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
            url: updateSupplyUrl,
            dataType: 'json',
            data:{id:id,name:name,priority:priority,status:status},
            success: function (data) {
                if (data.success == "true") {
                }
            }
        });
    });
    $("#userManagement").click(function () {
        var htmlStr = "<thead><tr><th>Id</th><th>用户名</th><th>性别</th><th>昵称</th><th>真实姓名</th><th>用户状态</th><th>注册时间</th><th>操作</th></tr></thead>";
        $.ajax({
            type: "POST",
            url: listUserManageUrl,
            dataType: 'json',
            success: function (data) {
                $.each(data, function (index, item) {
                    var status = "正常";
                    var manage = "<a href='#' class='btn btn-danger' role='button' name='btmanage' id='btmanage'>禁用";
                    if (item.userStatus == "-1") {
                        status = "禁用";
                        manage = "<a href='#' class='btn btn-success' role='button' name='btmanage' id='btmanage'>解除禁用";
                    }
                    if (item.userStatus == "1") {
                        status = "超级管理员"
                        manage = "";
                    }
                    htmlStr += "<tbody><tr>"
                            + "<td id = 'Id'>" + item.Id + "</td>"
                            + "<td id = 'name'>" + item.username + "</td>"
                            + "<td id = 'gender'>" + item.gender + "</td>"
                            + "<td id = 'nickName'>" + item.nickName + "</td>"
                            + "<td id = 'realName'>" + item.realName + "</td>"
                            + "<td id = 'status' value='" + item.userStatus + "' >" + status + "</td>"
                            + "<td id = 'registerTime'>" + item.registerTime + "</td>"
                            + "<td id = 'manage'>" + manage + "</td></tr></tbody>"

                });
                $("#managelist").html(htmlStr);
                htmlStr = "";
            }
        });
    });
});