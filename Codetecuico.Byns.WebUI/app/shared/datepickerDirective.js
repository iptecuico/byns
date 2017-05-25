// Requires jquery-ui.*.js, datepicker.css, theme.css

(function () {

    var app = angular.module("app");

    app.directive("datePicker", function () {
        return {
            restrict: "A",
            link: function (scope, element) {
                element.datepicker({ dateFormat: 'dd MM yy' });
            }
        }

    });

}());