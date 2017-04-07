var harisApp = angular.module('harisApp', ['ngRoute']);

harisApp.config(function ($routeProvider) {
    $routeProvider

        .when('/', {
            templateUrl: 'Content/App/views/home.html',
            controller: 'mainController'
        })
        .when('/devices', {
            templateUrl: 'Content/App/views/devices.html',
            controller: 'devicesController'
        })

        .when('/logs', {
            templateUrl: 'Content/App/views/logs.html',
            controller: 'logsController'
        });
});