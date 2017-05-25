(function () {

    var app = angular.module("app");

    app.directive("commentDisqus", function () {
        return {
            scope:{
                url: "=",
                id: "="
            },
            templateUrl: "/app/shared/comment.html",
            controller: function () {

            }
        }

    });

}());
