(function () {

    var apiService = function ($resource, $location) {

        var baseUrlDev = "http://localhost:14303/api";
        var baseUrlProd = "https://bynsapi.azurewebsites.net/api";
        var baseUrl = baseUrlProd;

        if ($location.host() === 'byns.azurewebsites.net') {
            baseUrl = baseUrlProd;
        } else {
            baseUrl = baseUrlDev;
        }
        
        return {
            app: $resource(baseUrl + "/app/version", null, {
                'getVersion': { method: 'GET' }
            }),
            item: $resource(baseUrl + "/item", null, {
                'update': { method: 'PUT' },
                'delete': {
                    method: 'DELETE',
                    params: {
                        id: "@id"
                    }
                },
                'search': {
                    method: 'GET',
                    params: {
                        pageNumber: "@pageNumber",
                        pageSize: "@pageSize",
                        searchText: "@searchText"
                    }
                }
            }),
            user: $resource(baseUrl + "/user", null, {
                'me': {
                    url: baseUrl + "/user/me",
                    method: 'GET'
                },
                'get': { method: 'GET' },
                'update': { method: 'PUT' }
            })
        }
    }

    apiService.$inject = ["$resource", "$location"];

    angular.module("app")
            .factory("apiService", apiService);

}());