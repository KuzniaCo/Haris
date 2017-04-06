var harisApp = angular.module('harisApp', ['ngRoute']);

harisApp.config(function ($routeProvider) {
    $routeProvider

        .when('/', {
            templateUrl: 'Content/App/views/home.html',
            controller: 'mainController'
        })
        .when('/about', {
            templateUrl: 'Content/App/views/about.html',
            controller: 'aboutController'
        })

        .when('/contact', {
            templateUrl: 'Content/App/views/contact.html',
            controller: 'contactController'
        });
});

harisApp.controller('mainController', function ($scope) {
    $scope.message = 'Everyone come and see how good I look!';
});

harisApp.controller('aboutController', function ($scope) {
    $scope.message = 'Look! I am an about page.';
});

harisApp.controller('contactController', function ($scope) {
    $scope.message = 'Contact us! JK. This is just a demo.';
});