/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('loginController', loginController);
    loginController.$inject = ['$scope','$state','apiService'];
    function loginController($scope, $state, apiService) {
        $scope.loginSubmit = function(){
            $state.go('home');
        }
    }
})(angular.module('onlineshop'));