(function () {
    'use strict';

    angular
        .module('clientApp')
        .controller('userEditorController', userEditorController);

    userEditorController.$inject = ['$location', '$routeParams', '$http'];

    function userEditorController($location, $routeParams, $http) {
        /* jshint validthis:true */
        var vm = this;
        vm.alias = $routeParams.alias;
        vm.title = vm.alias ? 'Edit User - ' + vm.alias : 'Add User';
        vm.user = null;
        if (vm.alias) {
            vm.busy = true;
            var url = '/api/person/' + vm.alias;

            $http.get(url)
                .then(function (response) {
                    vm.user = {};
                    angular.copy(response.data, vm.user);
                }, function (error) {
                    vm.busy = false;
                })
                .finally(function () {
                    vm.busy = false;
                });
        }
    }
})();
