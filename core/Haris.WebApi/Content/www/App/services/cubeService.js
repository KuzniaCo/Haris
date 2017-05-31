harisApp.service('cubeService', function ($http) {

    this.getCubes = function () {
        return $http.get("/api/cubes");
    }

    this.setDisplay = function (address, data) {
        return $http.post("/api/cube/display/"+address, data);
    }

    this.turnOnBacklight = function(address) {
        return $http.get("/api/cube/display/" + address + "/TurnOnBacklight/");
    }
	
    this.turnOffBacklight = function(address) {
        return $http.get("/api/cube/display/" + address + "/TurnOffBacklight/");
    }	

    this.turnOnRelay = function(address) {
        return $http.get("/api/cube/relay/" + address + "/TurnOn/");
    }

    this.turnOffRelay = function (address) {
        return $http.get("/api/cube/relay/" + address + "/TurnOff/");
    }
});