(function () {
    'use strict';

    angular
        .module('clientApp')
        .controller('contactController', contactController);

    contactController.$inject = ['$location', '$routeParams', '$scope'];

    function contactController($location, $routeParams, $scope) {
        /* jshint validthis:true */
        var vm = this;
        vm.alias = $routeParams.alias;
        vm.contact = {
            type: '10',
            value: null
        };
        vm.title = 'New Contact for user ';

        $scope.selectChanged = function () {
            
        };
    }
})();
