(function () {
    'use strict';

    angular.module('clientApp', [
        // Angular modules
        'ngAnimate',
        'ngRoute'


        // Custom modules

        // 3rd Party Modules

    ])
    .config(function ($routeProvider) {

        $routeProvider
            .when('/', {
                controller: 'userController',
                controllerAs: 'vm',
                templateUrl: '/views/peopleView.html'
            })
            .when('/editor/', {
                controller: 'userEditorController',
                controllerAs: 'vm',
                templateUrl: '/views/addUserView.html'
            })
            .when('/editor/:alias', {
                controller: 'userEditorController',
                controllerAs: 'vm',
                templateUrl: '/views/addUserView.html'
            })
            .otherwise({
                redirectTo: '/'
            });

    });
})();
