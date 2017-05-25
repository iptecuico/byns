(function () {
     
    var routeConfig = function ($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('/');

        $stateProvider
            .state("home", {
                url: "/",
                controller: "homeController",
                templateUrl: "/app/modules/home/home.html"
            })
            .state("cat", {
                url: "/cat/:name",
                controller: "categoryController",
                templateUrl: "/app/modules/category/category.html"
            })
            .state("user", {
                abstract: true,
                url: "/user",
                controller: "userController",
                templateUrl: "/app/modules/user/user.html",
                data: {
                    requiresLogin: true
                }
            })
            .state("user.profile", {
                url: "/profile",
                controller: "userProfileController",
                controllerAs: "vm",
                templateUrl: "/app/modules/user/userProfile.html",
                data: {
                    requiresLogin: true
                }
            })
            .state("user.edit", {
                url: "/edit",
                controller: "userProfileEditController",
                controllerAs: "vm",
                templateUrl: "/app/modules/user/userProfileEdit.html",
                data: {
                    requiresLogin: true
                }
            })
            .state("user.settings", {
                url: "/settings",
                controller: "userProfileEditController",
                controllerAs: "vm",
                template: "settings",
                data: {
                    requiresLogin: true
                }
            })
            .state("user.help", {
                url: "/help",
                controller: "userProfileEditController",
                controllerAs: "vm",
                template: "help",
                data: {
                    requiresLogin: true
                }
            })
            .state("item", {
                abstract: true,
                url: "/item",
                controller: "itemController",
                templateUrl: "/app/modules/item/item.html",
                data: {
                    requiresLogin: true
                }
            })
            .state("item.list", {
                url: "/list",
                controller: "listController",
                controllerAs: "vm",
                templateUrl: "/app/modules/item/list.html",
                data: {
                    requiresLogin: true
                }
            })
            .state("item.post", {
                url: "/post",
                controller: "postItemController",
                controllerAs: "vm",
                templateUrl: "/app/modules/item/postItem.html",
                data: {
                    requiresLogin: true
                }
            })
            .state("item.edit", {
                url: "/edit",
                controller: "editItemController",
                controllerAs: "vm",
                templateUrl: "/app/modules/item/editItem.html",
                data: {
                    requiresLogin: true
                },
                params: {
                    itemId: 0
                }
            })
            .state("item.settings", {
                url: "/settings",
                controller: "itemController",
                controllerAs: "vm",
                template: "item settings",
                data: {
                    requiresLogin: true
                }
            })
            .state("item.help", {
                url: "/help",
                controller: "itemController",
                controllerAs: "vm",
                templateUrl: "/app/modules/item/itemHelp.html",
                data: {
                    requiresLogin: true
                }
            });

        console.info("app.config route executed");
    }

    routeConfig.$inject = ["$stateProvider", "$urlRouterProvider"];

    angular.module("app")
            .config(routeConfig);

}());