"use strict";

(function ($) {
    //alert("Hello");
    handleStatus();
})(jQuery);

// Calls this first to get status
function handleStatus() {
    let status = $('#TripStatus');
    let currentStatus = status.attr("data-status");
    switch (currentStatus) {
        case 'DuringTrip':
            getDuringTrip();
            break;
        case 'ConfirmLocation':
            confirmLocation();
            break;
        case 'CheckStatus':
            checkStatus();
            break;
        case 'TakePictures':
            takePictures();
            break;
        default:
            getDuringTrip();
            break;
    }
}

function getDuringTrip() {
    var id = $('#TripStatus').attr("data-id");
    $.ajax({
        url: 'https://localhost:44303/api/Trip/DuringTrip/' + id,
        dataType: 'json',
        type: 'Get',
        contentType: 'application/json',
        success: function (result, textStatus, jQxhr) {
            handleDuringTrip(result);
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}

function handleDuringTrip(values) {
    $("#MainArea").empty();
    let rows = [];
    rows.push("<div>Enjoy your trip!</div>");
    rows.push("<div class='btn btn-primary' onclick='twilioRequest()'>Get Your Access Code</div>");
    rows.push(`<div>Destination: ${values.destination}</div>`);
    rows.push(`<div>Estimated Time to arrival: ${values.estimatedTime} minutes</div>`);
    rows.push("<div>Estimated Cost: $" + `${values.estimatedTime} dollars </div>`);
    rows.push(`<div>Map coordinates: Lng: ${values.lng}, lat: ${values.lat} </div>`);
    rows.push("<div class='btn btn-primary' onclick='endTripButton()'>End Trip</div>");
    $("#MainArea").html(rows.join(""));
}

function handleConfirmLocation(values) {
    $("#MainArea").empty();
    let rows = [];
    rows.push("<div>Enjoy your trip</div>");
    rows.push(`<div>Destination: ${values.destination}</div>`);
    rows.push(`<div>Estimated Time to arrival: ${values.estimatedTime} minutes</div>`);
    rows.push("<div>Estimated Cost: $" + `${values.estimatedTime} dollars </div>`);
    rows.push(`<div>Map coordinates: Lng: ${values.lng}, lat: ${values.lat} </div>`);
    rows.push("<div class='btn btn-primary' onclick='endTripButton()'>End Trip</div>");
    rows.push("<div class='btn btn-primary' onclick='completeTripButton()'>Skip to End</div>");
    $("#MainArea").html(rows.join(""));
}

function handleCheckStatus(values) {
    $("#MainArea").empty();
    let rows = [];
    rows.push("<div>Enjoy your trip</div>");
    rows.push(`<div>Destination: ${values.destination}</div>`);
    rows.push(`<div>Estimated Time to arrival: ${values.estimatedTime} minutes</div>`);
    rows.push("<div>Estimated Cost: $" + `${values.estimatedTime} dollars </div>`);
    rows.push(`<div>Map coordinates: Lng: ${values.lng}, lat: ${values.lat} </div>`);
    rows.push("<div class='btn btn-primary' onclick='endTrip()'>End Trip</div>");
    rows.push("<div class='btn btn-primary' onclick='completeTrip()'>Skip to End</div>");
    $("#MainArea").html(rows.join(""));
}

function handleTakePictures(values) {
    $("#MainArea").empty();
    let rows = [];
    rows.push("<div>Enjoy your trip</div>");
    rows.push(`<div>Destination: ${values.destination}</div>`);
    rows.push(`<div>Estimated Time to arrival: ${values.estimatedTime} minutes</div>`);
    rows.push("<div>Estimated Cost: $" + `${values.estimatedTime} dollars </div>`);
    rows.push(`<div>Map coordinates: Lng: ${values.lng}, lat: ${values.lat} </div>`);
    rows.push("<div class='btn btn-primary' onclick='endTrip()'>End Trip</div>");
    rows.push("<div class='btn btn-primary' onclick='completeTrip()'>Skip to End</div>");
    $("#MainArea").html(rows.join(""));
}

function takePictures() {
    var id = $('#TripStatus').attr("data-id");
    $.ajax({
        url: 'https://localhost:44303/api/Trip/TakePhotos/' + id,
        dataType: 'json',
        type: 'Get',
        contentType: 'application/json',
        success: function (result, textStatus, jQxhr) {
            handleTakePictures();
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}
function putTakePictures() {
    var id = $('#TripStatus').attr("data-id");
    $.ajax({
        url: 'https://localhost:44303/api/Trip/TakePhotos/' + id,
        dataType: 'json',
        type: 'Put',
        contentType: 'application/json',
        success: function (result, textStatus, jQxhr) {
            handleTakePictures();
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}


function checkStatus() {
    var id = $('#TripStatus').attr("data-id");
    $.ajax({
        url: 'https://localhost:44303/api/Trip/CheckStatus/' + id,
        dataType: 'json',
        type: 'Get',
        contentType: 'application/json',
        success: function (result, textStatus, jQxhr) {
            handleCheckStatus();
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}

function putCheckStatus() {
    var id = $('#TripStatus').attr("data-id");
    $.ajax({
        url: 'https://localhost:44303/api/Trip/CheckStatus/' + id,
        dataType: 'json',
        type: 'Put',
        contentType: 'application/json',
        success: function (result, textStatus, jQxhr) {
            handleCheckStatus();
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}

function confirmLocation() {
    var id = $('#TripStatus').attr("data-id");
    $.ajax({
        url: 'https://localhost:44303/api/Trip/ConfirmLocation/' + id,
        dataType: 'json',
        type: 'Get',
        contentType: 'application/json',
        success: function (result, textStatus, jQxhr) {
            handleConfirmLocation();
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}

function putConfirmLocation() {
    var id = $('#TripStatus').attr("data-id");
    $.ajax({
        url: 'https://localhost:44303/api/Trip/ConfirmLocation/' + id,
        dataType: 'json',
        type: 'Put',
        contentType: 'application/json',
        success: function (result, textStatus, jQxhr) {
            handleConfirmLocation();
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}

function completeTrip() {
    var id = $('#TripStatus').attr("data-id");
    $.ajax({
        url: 'https://localhost:44303/api/Trip/CompleteTrip/' + id,
        dataType: 'json',
        type: 'Get',
        contentType: 'application/json',
        success: function (result, textStatus, jQxhr) {
            alert("Trip ended");
            window.location.assign("Index");
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}

function twilioRequest() {
    var id = $('#TripStatus').attr("data-id");
    $.ajax({
        url: 'https://localhost:44303/api/Trip/Twilio/' + id,
        type: 'Get',
        success: function (result, textStatus, jQxhr) {
            alert("Door Key Access Code Sent to Phone");
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}