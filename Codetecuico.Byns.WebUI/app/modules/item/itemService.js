(function () {
    "use strict";

    var itemService = function (apiService) {
        var sortOrder = {};
        var items = [];
        var itemFilters = [];

        var getSortOrderOptions = function () {
            var data = [
                {
                    name: "Most Recent",
                    value: "-datePosted"
                },
                {
                    name: "Oldest",
                    value: "datePosted"
                },
                {
                    name: "Highest Starred",
                    value: "-starCount"
                },
                {
                    name: "Lowest to Highest Price",
                    value: "price"
                },
                {
                    name: "Highest to Lowest Price",
                    value: "-price"
                }
            ];

            return data;
        };

        var getActiveSort = function () {
            if ($.isEmptyObject(sortOrder) === true) {
                sortOrder = getSortOrderOptions()[0];
            }

            return sortOrder;
        };

        var getItemCount = function () {
            return items.length;
        };

        var getFilters = function () {
            return itemFilters;
        };

        var setActiveSort = function (sort) {
            sortOrder = sort;
        };

        var removeArrayObjectByAttr = function (arr, attr, value) {
            var i = arr.length;
            while (i--) {
                if (arr[i]
                    && arr[i].hasOwnProperty(attr)
                    && (arguments.length > 2 && arr[i][attr] === value)) {
                    arr.splice(i, 1);
                }
            }
            return arr;
        };

        var setCategoryFilter = function (value) {
            removeArrayObjectByAttr(itemFilters, "name", "Category");

            itemFilters.push({
                name: "Category",
                value: value
            });
        };

        var removeFilter = function (filter) {
            removeArrayObjectByAttr(itemFilters, "name", filter.name);
        };

        /* Active functions below
         *
         */

        // Items specific

        var getCurrencies = function () {
            return [{
                id: "Php",
                name: "Php"
            }, {
                id: "$",
                name: "$"
            }];
        }

        var getItem = function (id) {
            return apiService.item.query({
                $filter: "Id eq " + id
            }).$promise;
        };

        var getItems = function () {
            return apiService.item.query().$promise;
        };

        var save = function (item) {
            return apiService.item.save(item).$promise;
        };

        var update = function (id, item) {
            return apiService.item.update({ id: id }, item).$promise;
        };

        var deleteItem = function (item) {
            return apiService.item.delete({ id: item.id }).$promise;
        };

        var setItems = function (data) {
            items = angular.copy(data);
        };

        var search = function (pageNumber, pageSize, searchText) {
            return apiService.item.search({
                pageNumber: pageNumber,
                pageSize: pageSize,
                searchText: searchText
            }).$promise;
        };

        // User specific
        var getUserItems = function (userId) {
            return apiService.item.query({
                $filter: "UserId eq " + userId
            }).$promise;
        };

        return {
            getSortOrderOptions: getSortOrderOptions,
            getActiveSort: getActiveSort,
            getItemCount: getItemCount,
            getFilters: getFilters,
            setActiveSort: setActiveSort,
            setCategoryFilter: setCategoryFilter,
            removeFilter: removeFilter,

            getCurrencies: getCurrencies,
            getItem: getItem,
            getItems: getItems,
            save: save,
            update: update,
            _delete: deleteItem,
            setItems: setItems,
            search: search,

            getUserItems: getUserItems
        };
    };

    var module = angular.module("app");

    itemService.$inject = ["apiService"];
    module.factory("itemService", itemService);
}());