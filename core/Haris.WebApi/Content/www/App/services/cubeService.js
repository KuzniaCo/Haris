harisApp.service('cubeService', function ($http) {

    this.getCubes = function () {
        return $http.get("/api/cubes");
    }

    this.setDisplay = function (address, data) {
        return $http.post("/api/cube/display/"+address, data);
    }
});