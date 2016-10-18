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
                templateUrl: '/views/userView.html'
            })
            .when('/editor/', {
                controller: 'userEditorController',
                controllerAs: 'vm',
                templateUrl: '/views/userEditorView.html'
            })
            .when('/editor/:alias', {
                controller: 'userEditorController',
                controllerAs: 'vm',
                templateUrl: '/views/userEditorView.html'
            })
            .when('/editor/:alias/contact/', {
                controller: 'contactController',
                controllerAs: 'vm',
                templateUrl: '/views/contactEditorView.html'
            })
            .otherwise({
                redirectTo: '/'
            });

    });
})();
