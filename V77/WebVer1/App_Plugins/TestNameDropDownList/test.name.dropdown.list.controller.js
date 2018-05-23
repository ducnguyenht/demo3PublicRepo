angular.module('umbraco').controller('TestNameDropDownList.TestNameDropDownListController',
function ($scope, $http, $filter) {
    $scope.onLoad = function () {
        $http({
            method: 'GET',
            url: '/umbraco/api/branchesApi/Get'
        }).then(function successCallback(response) {
            $scope.testNames = response.data.data;
            $scope.selectedTestName = $filter('getByValue')($scope.testNames, $scope.model.value);
        }, function errorCallback(response) {

        });
    };

    $scope.testNameChange = function () {
        $scope.model.value = $scope.selectedTestName.id;
    };

    $scope.onLoad();
});