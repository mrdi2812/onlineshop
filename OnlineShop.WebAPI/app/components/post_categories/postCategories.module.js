/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('onlineshop.post_categories', ['onlineshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('post_categories', {
            url: "/post_categories",
            parent: 'base',
            templateUrl: "/app/components/post_categories/postCategoryListView.html",
            controller: "postCategoryListController"
        }).state('post_add_category', {
            url: "/post_add_category",
            parent: 'base',
            templateUrl: "/app/components/post_categories/postCategoryAddView.html",
            controller: "postCategoryAddController"
        }).state('post_edit_category', {
            url: "/post_edit_category/:id",
            parent: 'base',
            templateUrl: "/app/components/post_categories/postCategoryEditView.html",
            controller: "postCategoryEditController"
        });
    }
})();