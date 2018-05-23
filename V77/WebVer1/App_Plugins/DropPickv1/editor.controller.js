angular.module("umbraco").controller("My.EditorController", function ($scope) {
    $scope.glasses = [
        {
            name: "cocktail",
            image: "/images/icon 1.png"
        },
        {
            name: "shot",
            image: "/images/icon 2.png"
        },
        {
            name: "highball",
            image: "/images/icon 3.png"
        },
    ];

    $scope.choose = function (glass) {
        $scope.model.value = glass.image;
    };


});