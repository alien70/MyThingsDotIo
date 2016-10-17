(function () {
    'use strict';

    angular
        .module('clientApp')
        .controller('userEditorController', userEditorController);

    userEditorController.$inject = ['$location', '$routeParams', '$http', '$scope'];

    function userEditorController($location, $routeParams, $http, $scope) {
        /* jshint validthis:true */
        var vm = this;
        vm.alias = $routeParams.alias;
        vm.title = vm.alias ? 'Edit User - ' + vm.alias : 'Add User';
        var url = '/api/person/';
        if (vm.alias) {
            vm.user = null;
            vm.busy = true;

            $http.get(url + vm.alias)
                .then(function (response) {
                    vm.user = {};
                    angular.copy(response.data, vm.user);
                }, function (error) {
                    vm.busy = false;
                })
                .finally(function () {
                    vm.busy = false;
                });

            $scope.submit = function () {
                vm.busy = true;
                $http.put(url, vm.user)
                    .then(function (response) {
                        vm.user = {};
                        $location.path("/");
                    }, function (error) {
                        vm.busy = false;
                        vm.erroMessage = 'Failed to update user ' + vm.alias + ': ' + error;
                    })
                    .finally(function () {
                        vm.busy = false;
                    });
            };
        } else {
            $scope.submit = function () {
                vm.busy = true;
                $http.post(url, vm.user)
                .then(function (response) {
                    vm.user = {};
                    $location.path("/");
                }, function (error) {
                    vm.erroMessage = 'Failed to create new user ' + error;
                    vm.busy = false;
                })
                .finally(function () {
                    vm.busy = false;
                });
            }
        }
    }
})();
