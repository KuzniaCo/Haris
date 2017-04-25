harisApp.directive('relayCube', function (cubeService) {

    return {
        scope: {
            cube: "="
        },
        restrict: 'E',
        templateUrl: '/Content/www/App/views/relayCube.html',
        controller: function($scope) {
            $scope.turnOn = function () {
                cubeService.turnOnRelay($scope.cube.CubeAddress);
            }
            $scope.turnOff = function () {
                cubeService.turnOffRelay($scope.cube.CubeAddress);
            }
        },
        link: function($scope, $element) {

        }
    }
});