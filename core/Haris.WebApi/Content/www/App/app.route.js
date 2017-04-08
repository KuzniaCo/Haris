harisApp.config(function ($routeProvider) {
    $routeProvider

        .when('/', {
            templateUrl: 'Content/www/App/views/home.html',
            controller: 'mainController'
        })
        .when('/devices', {
            templateUrl: 'Content/www/App/views/devices.html',
            controller: 'devicesController'
        })

        .when('/logs', {
            templateUrl: 'Content/www/App/views/logs.html',
            controller: 'logsController'
        });
});