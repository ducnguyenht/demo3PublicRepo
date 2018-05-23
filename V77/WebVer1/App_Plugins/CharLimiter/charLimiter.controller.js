angular.module("umbraco").controller("CharLimiter.CharLimiterController", function ($scope) {
    $scope.limitchars = function () {
        var limit = 100;
        //var charLeft = null;
        if ($scope.model.value.length > limit) {
            $scope.model.value = $scope.model.value.substr(0, limit);
            $scope.info = "You cannot write more than " + limit + " characters!";
        }
        else {
            //		charLeft = limit - $scope.model.value.length;
            $scope.info = "You have " + (limit - $scope.model.value.length) + " characters remaining";
        }
    }
});