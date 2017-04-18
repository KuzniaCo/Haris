harisApp.controller('devicesController', function ($scope, cubeService) {
    $scope.cubes = [];

    $scope.getCubes = function() {
        cubeService.getCubes().then(function(res) {
            $scope.cubes = res.data;
        });
    }

    $scope.init = function() {
        $scope.getCubes();
    }

    $scope.init();
});