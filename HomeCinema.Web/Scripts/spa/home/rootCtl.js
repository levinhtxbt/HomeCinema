(function (app) {
    'use strict';

    app.controller('rootCtl', rootCtl);

    function rootCtl($scope) {
        $scope.userData = {};
        $scope.userData.displayUserInfo = displayUserInfo;
        $scope.logout = logout;

        function displayUserInfo() {

        }

        function logout() {

        }
    }


})(angular.module('homeCinema'));