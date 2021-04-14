﻿"use strict";

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
            break;
        case 'CheckStatus':
            break;
        case 'Take Pictures':
            break;
        default:
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
    rows.push("<div>Enjoy your trip</div>");
    rows.push(`<div>Destination: ${values.destination}</div>`);
    rows.push(`<div>Estimated Time to arrival: ${values.estimatedTime} minutes</div>`);
    rows.push("<div>Estimated Cost: $" + `${values.estimatedTime} dollars </div>`);
    rows.push(`<div>Map coordinates: Lng: ${values.lng}, lat: ${values.lat} </div>`);
    rows.push("<div class='btn btn-primary' onclick='endTripButtion()'>End Trip</div>");
    rows.push("<div class='btn btn-primary' onclick='completeTripButton()'>Skip to End</div>");
    $("#MainArea").html(rows.join(""));
}
function endTripButton() {
    alert("Next Step!");
}

function completeTripButton() {
    var id = $('#TripStatus').attr("data-id");
    var dict = {
        Something: "String"  
    }
    $.ajax({
        url: 'https://localhost:44303/api/Trip/CompleteTrip/' + id,
        dataType: 'json',
        type: 'Put',
        contentType: 'application/json',
        data: JSON.stringify(dict),
        success: function (result, textStatus, jQxhr) {
            alert("Trip ended");
            window.location.assign("Index");
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}