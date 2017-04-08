var harisApp = angular.module('harisApp', ['ngRoute']);

harisApp.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.hashPrefix('');
}]);