harisApp.directive('tempCube', function () {

    return {
        scope: {
            cube: "="
        },
        restrict: 'E',
        templateUrl: '/Content/www/App/views/tempCube.html',
        link: function($scope, $element) {
        }
    }
});