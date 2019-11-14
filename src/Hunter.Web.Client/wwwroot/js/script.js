var wasmHelper = {};
var map;
wasmHelper.ACCESS_TOKEN_KEY = "AUTH_TOKEN";

wasmHelper.saveAccessToken = function (tokenStr) {
    localStorage.setItem(wasmHelper.ACCESS_TOKEN_KEY, tokenStr);
    Cookies.set(wasmHelper.ACCESS_TOKEN_KEY, tokenStr, { secure: true });
};

wasmHelper.getAccessToken = function () {
    return localStorage.getItem(wasmHelper.ACCESS_TOKEN_KEY);
};

wasmHelper.removeAccessToeken = function () {
    return localStorage.removeItem(wasmHelper.ACCESS_TOKEN_KEY);
};

function saveAsFile(filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}


function initMap() {
    if ($('#map').length > 0) {
        var location = { lat: 52.9115323, lng: -1.3230527 };
        map = new google.maps.Map(
            document.getElementById('map'), {
                zoom: 15,
                center: location,
                clickableIcons: false,
                disableDefaultUI: true
            });
    }
}

function addMarker(lat, lng, label, image) {
    var location = { lat: lat, lng: lng };
    marker = new google.maps.Marker({
        position: location,
        animation: google.maps.Animation.DROP,
        title: label,
        map: map,
        icon: image,
        size:new google.maps.Size(20, 32)
    });
}

function setCenter(lat, lng) {
    var location = { lat: lat, lng: lng };
    map.setCenter(location);
}

