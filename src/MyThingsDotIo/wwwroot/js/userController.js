(function () {
    'use strict';

    angular
        .module('clientApp')
        .controller('userController', userController);

    userController.$inject = ['$location', '$http', '$scope'];

    function userController($location, $http, $scope) {
        var vm = this;
        vm.busy = true;
        vm.erroMessage = '';
        vm.people = [];
        var url = "/api/person/";
        $http.get(url)
            .then(function (response) {
                angular.copy(response.data, vm.people);
            }, function (error) {
                //failure
                vm.errorMessage = "Failed to load data " + error;
                vm.busy = false;
            })
        .finally(function () {
            vm.busy = false;
        });

        $scope.remove = function (alias) {
            vm.busy = true;
            $http.delete(url + alias)
                .then(function (response) {
                    vm.user = {};
                    var people = eval(vm.people);
                    var index = -1;
                    for (var i = 0; i < people.length; i++) {
                        if (people[i].alias === alias) {
                            index = i;
                            break;
                        }
                    }

                    if (index < 0) {
                        vm.busy = false;
                        vm.erroMessage = 'Failed to delete user ' + alias + ': ' + error;
                    }
                    else {
                        people.splice(index, 1);
                    }

                    $location.path("/");
                }, function (error) {
                    vm.busy = false;
                    vm.erroMessage = 'Failed to delete user ' + alias + ': ' + error;
                })
                .finally(function () {
                    vm.busy = false;
                });
        };

    }
})();
