//$(document).ready(function () {

//    var app = angular.module("vendor", ['multipleSelect']);
//    app.controller("vendorCtrl", function ($scope, $http) {
//        $scope.message = $http.get('/api/VendorApi/' + 1).then(function (response) {
//            console.log(data);
//        });


//    });



//    //$.ajax({
//    //    url: '/api/VendorApi/' + 1,
//    //    type: 'GET',
//    //    contentType: "application/json;charset=utf-8",
//    //    dataType: "json",
//    //    success: function (data) {
//    //        console.log(data);
//    //        $.each(data.systemCategory, function (i) {
//    //            $('#CatogreyDropdown').append($('<option></option>').val(data.systemCategory[i].id).html(data.systemCategory[i].name));
//    //        });
//    //        $.each(data.type, function (i) {
//    //            $('#InventoryTypeDropdown').append($('<option></option>').val(data.type[i].id).html(data.type[i].type_Name));
//    //        });
//    //    },
//    //    error: function () {
//    //        alert('Dropdown Not Loaded');
//    //    }
//    //});
//});
//function Add() {
//    var v_name = document.getElementById('V_name').value.trim();
//    var v_address = document.getElementById('V_adress').value.trim();
//    var v_city = document.getElementById('V_city').value.trim();
//    var v_mobno = document.getElementById('V_mobileNo').value.trim();
//    var u_name = document.getElementById('user_name').value.trim();
//    var u_nic = document.getElementById('user_nic').value.trim();
//    var u_designation = document.getElementById('user_deg').value.trim();
//    var u_email = document.getElementById('user_email').value.trim();
//    var u_mobno = document.getElementById('user_mobno').value.trim();
//    var approve = document.getElementById('approve').checked;
//    var notapprove = document.getElementById('notapprove').checked;
//    var active = document.getElementById('active').checked;
//    var notactive = document.getElementById('inactive').checked;
//    var creditlimit = document.getElementById('v_creditlimit').value.trim();
//    var credittime = document.getElementById('credit_time').value.trim();
//    if (v_name == "") { alert("Please Insert Vendor Name"); }
//    else if (v_address == "") { alert("Please Insert Vendor Address"); }
//    else if (v_city == "") { alert("Please insert vendor City"); }
//    else if (v_mobno == "") { alert("Please enter valid number"); }
//    else if (u_name == "") { alert("Please enter Your Name"); }
//    else if (u_mobno == "") { alert("Please Insert Valid Number"); }
//    else if ((approve == false) && (notapprove == false)) { alert("Please select Any Evaluation Phase"); }
//    else if ((active == false) && (notactive == false)) { alert("Please select Any Vendor Status"); }
//    else if (creditlimit == "") { alert("Please enter Credit limit "); }
//    else if (credittime == "") { alert("Please enter Credit Time Per Day"); }
//    else {
//        var model = {
//            vendorName: v_name, vendortype: $("#InventoryTypeDropdown").val(), vendoraddress: v_address, vendorcity: v_city, vendormobNo: v_mobno,
//            vendorteleNo: $("#V_telephone").val(), vendorEmail: $("#V_email").val(), vendorwebsite: $("#V_web").val(), vendorfax: $("#V_fax").val(),
//            vendorNote: $("#V_Note").val(),
//            user_name: u_name, userregNo: $("#user_reg").val(), usernic: u_nic, usertitle: u_designation, useremail: u_email, usermobNo: u_mobno,
//            usertelephoneNo: $("#user_telephone").val(), userfax: $("#user_fax").val(),
//            vendorcreditLimit: creditlimit, vendordays: credittime, vendorpreferredCourier: $("#courier").val(),
//            vendorsyestemCategory: $("#CatogreyDropdown").val(), isApprove: approve, isActive: active
//        };
//        $.ajax({
//            url: '/api/VendorApi/',
//            type: 'POST',
//            data: JSON.stringify(model),
//            contentType: "application/json;charset=utf-8",
//            dataType: "json",
//            success: function (data) {
//                alert(data);
//                location.reload();

//            },
//            error: function () {
//                alert('Vendor Not Added Ajax Error');
//            }
//        });
//    }
//}