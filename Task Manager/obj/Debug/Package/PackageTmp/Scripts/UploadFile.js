////$(document).ready(function () {
////    $('ul.tabs li').click(function () {
////        var tab_id = $(this).attr('data-tab');

////        $('ul.tabs li').removeClass('current');
////        $('.tab-content').removeClass('current');

////        $(this).addClass('current');
////        $("#" + tab_id).addClass('current');
////    })
////    $.ajax({
////        async: false,
////        url: '/api/TaskApi/',
////        type: 'GET',
////        contentType: "application/json;charset=utf-8",
////        dataType: "json",
////        success: function (data) {
////            console.log(data);
////            for (var i = 0; i < data.length; i++) {
////                $("#dynamic-Grid").append("<tr id=" + i + "><td>" + i + "</td><td>" + data[i].task_name + "</td><td>" + data[i].startDate + "</td><td>" + data[i].endDate + "</td></tr>");
////            }
////        },
////        error: function () { alert('Task Not Load'); }
////    });
////})
////function Add() {
////    var dropdowntype = document.getElementById("FileCatogreyDropdown").value.trim();
////    var file = document.getElementById("myfile").value;
////    if (dropdowntype == "00") {
////        alert("Please Select Any File Name");
////    }
////    else if (file == null) {
////        alert("Please Select Any File")
////    }
////    else {
////        var model = { fileName: file, fileCode: dropdowntype };
////        console.log(file + "" + dropdowntype);
////        $.ajax({
////            async: false,
////            url: '/api/ProjectFilesUploadController/',
////            type: 'POST',
////            data: JSON.stringify(model),
////            contentType: "application/json;charset=utf-8",
////            dataType: "json",
////            success: function (data) {
////                alert(data);
////                window.location.href = '/Project/ProjectFileUpload';
////            },
////            error: function () {
////                alert(data);
////            }
////        });
////    }
////}