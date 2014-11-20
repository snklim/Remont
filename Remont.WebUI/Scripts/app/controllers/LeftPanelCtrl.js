(function() {
    angular.module('remontApp').controller('LeftPanelCtrl', function ($scope, pages) {

        $scope.pages = pages;

        $scope.isActive = function (path) {
            return path == /^#\/(\w+)\/(\w+)$/.exec(location.hash)[1];
        }

    });
})();