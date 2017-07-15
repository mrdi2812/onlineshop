/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    'use strict';
    app.controller('applicationUserAddController', applicationUserAddController);
    applicationUserAddController.$inject = ['$scope', 'apiService', 'notificationService', '$location', 'commonService'];
    function applicationUserAddController($scope, apiService, notificationService, $location, commonService) {
        $scope.account = {
            Groups: []
        }
        $scope.addAccount = addAccount;
        function addAccount() {
            apiService.post('/api/applicationUser/add', $scope.account, addSuccessed, addFailed);
        }
        function addSuccessed() {
            notificationService.displaySuccess($scope.account.Name + 'đã được thêm mới');
            $location.url('application_users');
        }
        function addFailed(response) {
            notificationService.displayError(response.data.Message);
            notificationService.displayErrorValidation(response);
        }
        function loadGroup() {
            apiService.get('/api/applicationgroup/getlistall', null, function (response) {
                $scope.groups = response.data;
            }, function (response) {
                notificationService.displayError('Không tải được danh sách nhóm người dùng');
            });
        }
        loadGroups();
    }
})(angular.module('onlineshop.application_users'));