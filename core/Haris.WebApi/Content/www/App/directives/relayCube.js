harisApp.directive('relayCube', function () {

    return {
        scope: {
            cube: "="
        },
        restrict: 'E',
        templateUrl: '/Content/www/App/views/relayCube.html',
        link: function($scope, $element) {
            
        }
    }
});