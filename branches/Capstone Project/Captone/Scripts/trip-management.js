$(function loadTable() {
    var oTable = $('#TableTrips').dataTable();

    $.extend($.fn.dataTable.defaults, {
        "bSort": true,
        "bPaginate": false,
        "bLengthChange": false,
        "sPaginationType": "full_numbers"
    });

});

function newTrip(stationID) {
    $.ajax({
        url: "/Trip/NewTrip",
        data: { stationID: stationID },
        type: "Post",
        success: function (result) {
            $("#newTrip").html($(result));
        }
    });
}
function otherStart(stationId) {
    $.ajax({
        url: "/Trip/OtherStart",
        data: { stationId: stationId },
        type: "Post",
        success: function (result) {
            $("#otherStart").html(result);
        }
    });
}
function otherTrip(stationID) {
    $.ajax({
        url: "/Trip/OtherTrip",
        data: { stationID: stationID },
        type: "Post",
        success: function (result) {
            $("#otherTrip").html(result);
        }
    });
}

function arriveTrip(stationID) {
    $.ajax({
        url: "/Trip/ArrivedTrip",
        data: {stationID: stationID},
        type: "Post",
        success: function (result) {
            $("#arrivalTrip").html(result);
        }
    });
}

function departureTrip(stationID) {
    $.ajax({
        url: "/Trip/DepartedTrip",
        data: { stationID: stationID },
        type: "Post",
        success: function (result) {
            $("#departureTrip").html(result);
        }
    });
}

$(function () {
    $('#selectall').click(function (event) {  //on click 
        if (this.checked) { // check select status
            $('.case').each(function () { //loop through each checkbox
                this.checked = true;  //select all checkboxes with class "checkbox1"  
            });
        } else {
            $('.case').each(function () { //loop through each checkbox
                this.checked = false; //deselect all checkboxes with class "checkbox1"                 
            });
        }
    });
});
//change request status when checkbox 'arrived' checked
function getRequests() {
    var listRq = [];
    $('[name="chk"]:checked').each(function () {
        var row = $(this).closest('tr');
        var rq = document.getElementById("rqID").innerText;
        // Push request object to collection
        listRq.push(rq);
    });
    return listRq;
}
// Sent request collection to controller
function SentObject() {
    var requests = getRequests();
    $.ajax({
        url: "/Request/UpdateStatus",
        type: "POST",
        data: JSON.stringify({ requestIDs: requests }),
        contentType: "application/json",
        dataType: "json",
        success: function (result) {
            window.location = "/Request/Index";
            window.addActive(5);
        }
    });
}
// Click on button 'Đã đến trạm'
$('#sttSet').click(function () {
    SentObject();
});
function CreateTrip(stationId) {
    $.get('/Trip/Create?stationID=' + stationId, function(ret) {
        $('#createTripModal').modal('show');
        $("#tripmodal-create").empty().html(ret);
        $("#tripmodal-create input").addClass("form-control");
        $("#tripmodal-create select").addClass("form-control");
        $("#RouteID").val(null);
        $("#schedule").hide();
        
        $("#startDate").datepicker({ dateFormat: "yy/mm/dd" });
        $("#createTripModal #RouteID").on('change', function (e) {
            listOnRoute($('#RouteID').val());
        });
    });
}

function listTrip(value) {
    $.ajax({
        url: "/Trip/ListTrip",
        type: "POST",
        data: { tripDate: value },
        success: function (result) {
            $("#samedateTrip").html(result);
        }
    });
}

function listOnRoute(route) {
    $.ajax({
        url: "/Schedule/SchedulePartialView",
        type: "Post",
        data: { routeID: route },
        success: function (result) {
            $("#schedule").html(result).show();
        }
    });
}

// Get schedule objects
function getScheduleObject() {
    var schedules = [];
    $('[name="1"]:checked').each(function () {
        var row = $(this).closest('tr');
        var sch = {
            ScheduleID: row.find('td:nth-child(2)').text(),
            RouteID: $('#RouteID').val(),
            CoachID: row.find('td:nth-child(4)').text(),
            EstimateDepartureTime: row.find('td:nth-child(7)').text(),
            EstimateVolume: row.find('td:nth-child(9)').text() * row.find('td:nth-child(10)').text()
        };
        // Push schedule object to collection
        schedules.push(sch);
    });
    return schedules;
}

// Sent schedule objects to controller
function SentObject() {
    var schedules = getScheduleObject();
    var date = $('#startDate').val();
    $.ajax({
        url: "/Trip/Create",
        type: "POST",
        data: JSON.stringify({ schedules: schedules, date: date }),
        contentType: "application/json",
        dataType: "json",
        success: function (result) {
        }
    });
}

function Detail(id) {
    $.ajax({
        url: "/Trip/Details/" + id,
        type: "Get",
        success: function (result) {
            $("#tripDetails").html(result);
        }
    });
}