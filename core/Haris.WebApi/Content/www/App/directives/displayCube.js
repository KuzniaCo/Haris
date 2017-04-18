harisApp.directive('displayCube', function (cubeService) {

    return {
        scope: {
            cube: "="
        },
        restrict: 'E',
        templateUrl: '/Content/www/App/views/displayCube.html',
        link: function($scope, $element) {
            
        },
        controller: function ($scope) {
            $scope.row = 0;
            $scope.content = "";

            $scope.setDisplay = function () {
                cubeService.setDisplay($scope.cube.CubeAddress, { Row: $scope.row, Content: $scope.content });
            }
        }
    }
});