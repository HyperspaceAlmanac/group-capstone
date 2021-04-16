"use strict";

(function ($) {
    //alert("Hello");
    if ($("#TripStatus").length) {
        handleStatus();
    }
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
    rows.push(`<input type='string' name='zipcode' value='${values.zipcode}' required>`);
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

function handleTakePhotos(values) {
    $("#MainArea").empty();
    let rows = [];
    let temp;
    let hideFinishTripButton = values.afterTripFrontImage == "" || values.afterTripBackImage == "" || values.afterTripLeftImage == ""
        || values.afterTripRightImage == "" || values.afterTripInteriorFront == "" || values.afterTripInteriorBack == "";
    rows.push("<div>Please Upload Photos of the Vehicle</div>");
    if (hideFinishTripButton) {
        rows.push("<div>Finish Trip option will appear when all photos are uploaded</div>");
    } else {
        rows.push("<div class='btn btn-primary' onclick='completeTrip()'>Finish Trip</div>");
    }
    rows.push("<div class='row'>");
    rows.push("<div class='col-6'>");
    rows.push("<div>Before Trip Photo of Front</div>");
    temp = values.beforeTripFrontImage !== "" ? values.beforeTripFrontImage : "FrontDefault.png";
    rows.push(`<img src='../Images/Before/${temp}' alt='Default gray image' class='img-fluid'>`);
    rows.push("<div>Before Trip Photo of Back</div>");
    temp = values.beforeTripBackImage !== "" ? values.beforeTripBackImage : "BackDefault.png";
    rows.push(`<img src='../Images/Before/${temp}' alt='Default gray image' class='img-fluid'>`);
    rows.push("<div>Before Trip Photo of Left</div>");
    temp = values.beforeTripLeftImage !== "" ? values.beforeTripLeftImage : "LeftDefault.png";
    rows.push(`<img src='../Images/Before/${temp}' alt='Default gray image' class='img-fluid'>`);
    rows.push("<div>Before Trip Photo of Right</div>");
    temp = values.beforeTripRightImage !== "" ? values.beforeTripRightImage : "RightDefault.png";
    rows.push(`<img src='../Images/Before/${temp}' alt='Default gray image' class='img-fluid'>`);
    rows.push("<div>Before Trip Photo of Front Interior</div>");
    temp = values.beforeTripInteriorFront !== "" ? values.beforeTripInteriorFront : "InteriorFrontDefault.png";
    rows.push(`<img src='../Images/Before/${temp}' alt='Default gray image' class='img-fluid'>`);
    rows.push("<div>Before Trip Photo of Back Interior</div>");
    temp = values.beforeTripInteriorBack !== "" ? values.beforeTripInteriorBack : "InteriorBackDefault.png";
    rows.push(`<img src='../Images/Before/${temp}' alt='Default gray image' class='img-fluid'>`);
    rows.push("</div>");
    rows.push(`<div class='col-6'>`);

    let photoType;
    let photoTypes = [];
    rows.push("<div>Photo of Front of Vehicle</div>");
    temp = values.afterTripFrontImage !== "" ? values.afterTripFrontImage : "FrontDefault.png";
    photoType = "front";
    photoTypes.push(photoType);
    rows.push(`<form id="photo-${photoType}">`);
    rows.push(`<input type="file" id="myFile-${photoType}" accept="image/x-png" name="file" />`);
    rows.push("<button type='submit' class='btn btn-info'>Upload Image</button>");
    rows.push("</form>");
    rows.push(`<img src='../Images/After/${temp}' alt='Default gray image' class='img-fluid'>`);
    rows.push("<div>Photo of Back of Vehicle</div>");
    temp = values.afterTripBackImage !== "" ? values.afterTripBackImage : "BackDefault.png";
    photoType = "back";
    photoTypes.push(photoType);
    rows.push(`<form id="photo-${photoType}">`);
    rows.push(`<input type="file" id="myFile-${photoType}" accept="image/x-png" name="file" />`);
    rows.push("<button type='submit' class='btn btn-info'>Upload Image</button>");
    rows.push("</form>");
    rows.push(`<img src='../Images/After/${temp}' alt='Default gray image' class='img-fluid'>`);
    rows.push("<div>Photo of Left Side of Vehicle</div>");
    temp = values.afterTripLeftImage !== "" ? values.afterTripLeftImage : "LeftDefault.png";
    photoType = "left";
    photoTypes.push(photoType);
    rows.push(`<form id="photo-${photoType}">`);
    rows.push(`<input type="file" id="myFile-${photoType}" accept="image/x-png" name="file" />`);
    rows.push("<button type='submit' class='btn btn-info'>Upload Image</button>");
    rows.push("</form>");
    rows.push(`<img src='../Images/After/${temp}' alt='Default gray image' class='img-fluid'>`);
    rows.push("<div>Photo of Right Side of Vehicle</div>");
    temp = values.afterTripRightImage !== "" ? values.afterTripRightImage : "RightDefault.png";
    photoType = "right";
    photoTypes.push(photoType);
    rows.push(`<form id="photo-${photoType}">`);
    rows.push(`<input type="file" id="myFile-${photoType}" accept="image/x-png" name="file" />`);
    rows.push("<button type='submit' class='btn btn-info'>Upload Image</button>");
    rows.push("</form>");
    rows.push(`<img src='../Images/After/${temp}' alt='Default gray image' class='img-fluid'>`);
    rows.push("<div>Photo of Front Interior of Vehicle</div>");
    temp = values.afterTripInteriorFront !== "" ? values.afterTripInteriorFront : "InteriorFrontDefault.png";
    photoType = "interiorFront";
    photoTypes.push(photoType);
    rows.push(`<form id="photo-${photoType}">`);
    rows.push(`<input type="file" id="myFile-${photoType}" accept="image/x-png" name="file" />`);
    rows.push("<button type='submit' class='btn btn-info'>Upload Image</button>");
    rows.push("</form>");
    rows.push(`<img src='../Images/After/${temp}' alt='Default gray image' class='img-fluid'>`);
    rows.push("<div>Photo of Back Interior of Vehicle</div>");
    temp = values.afterTripInteriorBack !== "" ? values.afterTripInteriorBack : "InteriorBackDefault.png";
    photoType = "interiorBack";
    photoTypes.push(photoType);
    rows.push(`<form id="photo-${photoType}">`);
    rows.push(`<input type="file" id="myFile-${photoType}" accept="image/x-png" name="file" />`);
    rows.push("<button type='submit' class='btn btn-info'>Upload Image</button>");
    rows.push("</form>");
    rows.push(`<img src='../Images/After/${temp}' alt='Default gray image' class='img-fluid'>`);
    rows.push("</div>");
    rows.push("</div>");

    $("#MainArea").html(rows.join(""));
    photoTypes.forEach(strVal => $("#photo-" + strVal).submit(generatePhotoUpload(strVal)));
}

function takePhotos() {
    var id = $('#TripStatus').attr("data-id");
    $.ajax({
        url: 'https://localhost:44303/api/Trip/TakePhotos/' + id,
        dataType: 'json',
        type: 'Get',
        contentType: 'application/json',
        success: function (result, textStatus, jQxhr) {
            handleTakePhotos(result);
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}

function generatePhotoUpload(photoType) {
    function innerFunction(e) {
        var id = $('#TripStatus').attr("data-id");
        e.preventDefault();
        let formData = new FormData();
        let file_data = $("#myFile-" + photoType).prop("files")[0];
        if (typeof file_data === 'undefined') {
            alert("No file selected. Please Try again");
            return;
        }
        if (file_data.type !== "image/png") {
            alert("Please only upload images in .png format");
            return;
        }
        formData.append("file", file_data);
        $.ajax({
            url: 'https://localhost:44303/api/Trip/TakePhotos/' + photoType + "/" + id,
            type: "Put",
            data: formData,
            cache: false,
            processData: false,
            contentType: false,
            success: function (result, textStatus, jQxhr) {
                alert("Photo upload successful");
                location.reload();
            },
            error: function (jqXhr, textStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    }
    return innerFunction
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
            takePhotos();
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
        type: 'Put',
        contentType: 'application/json',
        data: JSON.stringify(dict),
        success: function (result, textStatus, jQxhr) {
            handleCheckStatus();
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
        type: 'Get',
        contentType: 'application/json',
        success: function (result, textStatus, jQxhr) {
            alert("Thank you for using the Car Rental Service!");
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
