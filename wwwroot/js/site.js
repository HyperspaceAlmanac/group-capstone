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
            handleCheckStatus();
            break;
        case 'TakePhotos':
            takePhotos();
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
    rows.push("<div class='btn btn-primary' onclick='confirmLocation()'>End Trip</div>");
    $("#MainArea").html(rows.join(""));
}

function handleConfirmLocation(values) {
    $("#MainArea").empty();
    let rows = [];
    rows.push("<div>Please enter the vehicle's location, or confirm that it is at the location shown:</div>");
    rows.push("<form id='location-form'>");
    rows.push("<label>Street Address</label>");
    rows.push(`<input type='text' name='street' value='${values.street}' required>`);
    rows.push("<label>City</label>");
    rows.push(`<input type='text' name='city' value='${values.city}' required>`);
    rows.push("<label>State</label>");
    rows.push(`<input type='text' name='state' value='${values.state}' required>`);
    rows.push("<label>Zipcode</label>");
    rows.push(`<input type='number' name='zipcode' value='${values.zipcode}' required>`);
    rows.push("<button type='submit' class='btn btn-info'>Confirm</button>");
    rows.push("</form>");
    $("#MainArea").html(rows.join(""));
    $("#location-form").submit(putConfirmLocation);
}

function handleCheckStatus(values) {
    $("#MainArea").empty();
    let rows = [];
    let odometer = $("#TripStatus").attr("data-odometer")
    rows.push("<div>Please fill out the status report</div>");
    rows.push("<form id='status-form'>");
    rows.push("<label>Odometer</label>");
    rows.push(`<input type='number' name='odometer' min=${odometer} value='${odometer}' required>`);
    rows.push("<br>");
    rows.push("<label>Fuel percentage</label>");
    rows.push(`<input type='number' name='fuel' value=50 min=0 max=100 required>`);
    rows.push("<br>");
    rows.push("<label>Please Check All that Applies</label>");
    rows.push("<br>");
    rows.push("<label>Tires Needs Repair</label><input type='checkbox' name='tire'>");
    rows.push("<br>");
    rows.push("<label>Body Damaged</label><input type='checkbox' name='body'>");
    rows.push("<br>");
    rows.push("<label>Interior Needs Cleaning</label><input type='checkbox' name='interior'>");
    rows.push("<br>");
    rows.push("<label>Windows Damaged</label><input type='checkbox' name='window'>");
    rows.push("<br>");
    rows.push("<label>Dashboard Lights On</label><input type='checkbox' name='dashboard'>");
    rows.push("<br>");
    rows.push("<label>Electronics Not Working</label><input type='checkbox' name='electronics'>");
    rows.push("<br>");
    rows.push("<label>Need to Top Off Supplies</label><input type='checkbox' name='supplies'>");
    rows.push("<br>");
    rows.push("<button type='submit' class='btn btn-info'>Confirm</button>");
    rows.push("</form>");
    $("#MainArea").html(rows.join(""));
    $("#status-form").submit(putCheckStatus);
}

function handleTakePictures(values) {
    $("#MainArea").empty();
    let rows = [];
    rows.push("<div>Please Upload Photos of the Vehicle</div>");
    $("#MainArea").html(rows.join(""));
}

function takePhotos() {
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
function putTakePhotos() {
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

function putCheckStatus(e) {
    e.preventDefault();
    let id = $('#TripStatus').attr("data-id");
    let dict = {
        Fuel: this["fuel"].value,
        Odometer: this["odometer"].value,
        Tire: this["tire"].checked,
        BodyRepair: this["body"].checked,
        InteriorCleaning: this["interior"].checked,
        WindowsRepair: this["window"].checked,
        DashboardLights: this["dashboard"].checked,
        ElectronicsRepair: this["electronics"].checked,
        Supplies: this["supplies"].checked
    };
    $.ajax({
        url: 'https://localhost:44303/api/Trip/CheckStatus/' + id,
        type: 'Put',
        contentType: 'application/json',
        data: JSON.stringify(dict),
        success: function (result, textStatus, jQxhr) {
            takePictures();
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
            handleConfirmLocation(result);
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}

function putConfirmLocation(e) {
    e.preventDefault();
    var id = $('#TripStatus').attr("data-id");
    var dict = {
        Street: this["street"].value,
        City: this["city"].value,
        State: this["state"].value,
        Zipcode: this["zipcode"].value
    };
    $.ajax({
        url: 'https://localhost:44303/api/Trip/ConfirmLocation/' + id,
        dataType: 'json',
        type: 'Put',
        contentType: 'application/json',
        data: JSON.stringify(dict),
        success: function (result, textStatus, jQxhr) {
            checkStatus();
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