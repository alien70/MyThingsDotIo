(function () {
    'use strict';

    angular
        .module('clientApp')
        .directive('contactCard', contactCard);

    contactCard.$inject = ['$window'];

    function contactCard($window) {
        return {
            restrict: 'EA',
            scope: {
                contacts: '=',
                title: '=',
                alias: '='
            },
            controller: ['$scope', function ($scope) {

            }],
            templateUrl: '/views/contactCard.html'
        };
    }

})();