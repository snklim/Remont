(function() {
    angular.module('remontApp').controller('LeftPanelCtrl', function ($scope, pages) {

        $scope.pages = pages;

        var pageReg = /^#\/(\w+)\/(\w+)(\/\d+)?$/;

        $scope.isActive = function (path) {
            if (pageReg.test(location.hash))
                return path == pageReg.exec(location.hash)[1];
            return false;
        }

    });
})();