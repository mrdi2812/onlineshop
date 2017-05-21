﻿(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function productAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.ckeditorOptions = {
            languague: 'vi',
            height : '200px'
        }
        $scope.AddProduct = AddProduct;
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }
        function AddProduct() {
            apiService.post('api/productcategory/create', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + 'đã được thêm mới.');
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công');
            });
        }
        function loadListProductCategory() {
            apiService.get('api/productcategory/getallparents', null, function (result) {
                $scope.productCategories = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadListProductCategory();
    }

})(angular.module('onlineshop.products'));