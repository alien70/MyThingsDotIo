(function () {
    'use strict';

    angular
        .module('clientApp')
        .directive('contactCard', contactCard);

    contactCard.$inject = ['$window', '$http'];

    function contactCard($window, $http) {
        return {
            restrict: 'EA',
            scope: {
                contacts: '=',
                title: '=',
                url: '='
            },
            controller: ['$scope', function ($scope) {
                $scope.contact = {
                    type: '10',
                    value: null,
                    default: false,
                };

                $scope.erroMessage = '';

                $scope.inputType = 'tel';
                $scope.inputPlaceholde = 'phone number...';

                var phoneNumberPattern = '^(0039)?([+]39)?(\.|\_|\-)?([0-9]*)(\.|\_|\-|\/)?([0-9]*)$';
                var mobileNumberPattern = '^(0039)?([+]39)?(\.|\_|\-)?((38[{8,9}|0])|(34[{7-9}|0])|(36[6|8|0])|(33[{3-9}|0])|(32[{8,9}]))(\.|\_|\-)?([\\d]{7})$';
                var emailPattern = '^([a-zA-Z0-9]*)(\.|\_|\-|\/)?([a-zA-Z0-9]*)(@)([a-zA-Z0-9]*)[\.]([a-zA-Z0-9]{1,4})$';
                var urlPattern = '^((http[s]?):[\/]{2})?([\\w\-\.]+[^#?\s]+)(.*)?(#[\\w\-]+)?$';

                $scope.inputPattern = phoneNumberPattern;
                $scope.onSelectionChanged = function () {
                    switch ($scope.contact.type) {
                        case '10':
                            {
                                $scope.inputType = 'tel';
                                $scope.inputPlaceholde = 'phone number...';
                                $scope.inputPattern = phoneNumberPattern;
                            } break;

                        case '20':
                            {
                                $scope.inputType = 'tel';
                                $scope.inputPlaceholde = 'mobile number...';
                                $scope.inputPattern = mobileNumberPattern;
                            } break;

                        case '30':
                            {
                                $scope.inputType = 'tel';
                                $scope.inputPlaceholde = 'fax number...';
                                $scope.inputPattern = mobileNumberPattern;
                            } break;

                        case '40':
                            {
                                $scope.inputType = 'email';
                                $scope.inputPlaceholde = 'email ...';
                                $scope.inputPattern = emailPattern;
                            } break;

                        case '50':
                            {
                                $scope.inputType = 'url';
                                $scope.inputPlaceholde = 'web site...';
                                $scope.inputPattern = urlPattern;
                            } break;
                    }
                };

                $scope.busy = false;
                $scope.submit = function () {
                    $scope.busy = true;
                    $http.post($scope.url, $scope.contact)
                        .then(function (response) {
                            $scope.contact.value = null;
                            $scope.contact.default = false;
                            $scope.contacts.push(response.data);
                        }, function (error) {
                            $scope.erroMessage = 'Failed to add contact ' + error;
                            $scope.busy = true;
                        })
                        .finally(function () {
                            $scope.busy = true;
                        })
                };


            }],

            templateUrl: '/views/contactCard.html'
        };
    }

})();