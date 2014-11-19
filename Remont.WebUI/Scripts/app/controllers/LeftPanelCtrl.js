(function() {
    angular.module('remontApp').controller('LeftPanelCtrl', function ($scope, pages) {

        $scope.pages = pages;

        $scope.isActive = function(path) {
            return location.hash.indexOf(path) >= 0;
        }

    });
})();