
angular.module('remontApp').controller('LeftPanel', function ($scope) {

    $scope.isActive = function(path) {
        return location.hash.indexOf(path) >= 0;
    }

})