angular.module('umbraco').filter('getByValue',
function () {
    return function (testNames, value) {
        var i = 0, len = testNames.length;
        for (; i < len; i++) {
            if (testNames[i].id == value) {
                return testNames[i];
            }
        }
        return null;
    }
});