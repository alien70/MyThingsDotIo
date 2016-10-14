(function () {
    'use strict';

    angular
        .module('clientApp')
        .controller('userController', userController);

    userController.$inject = ['$location', '$http'];

    function userController($location, $http) {
        var vm = this;
        vm.busy = true;
        vm.erroMessage = '';
        vm.people = [];
        $http.get("/api/person")
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

        activate();

        function activate() { }
    }
})();
