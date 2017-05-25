(function () {

    var categoryService = function () {
        var initialSortOrder = "name";

        var getCategories = function () {
            var data = [];

            data[0] = {
                name: "Book",
                itemCount: 544
            }
            data[1] = {
                name: "Bag",
                itemCount: 33
            }
            data[2] = {
                name: "Shoes",
                itemCount: 9987
            }
            data[3] = {
                name: "Shirt",
                itemCount: 5
            }
            data[4] = {
                name: "Car",
                itemCount: 23
            }
            data[5] = {
                name: "Bike",
                itemCount: 9
            }
            data[6] = {
                name: "Appliances",
                itemCount: 489571
            }

            // Replace above with $http.get call from API
            return data;
        };

        var getSortOrder = function () {
            return initialSortOrder;
        }

        return {
            getCategories: getCategories,
            getSortOrder: getSortOrder
        };
    };

    var module = angular.module("app");
    module.factory("categoryService", categoryService);

}());