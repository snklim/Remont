(function() {
    angular.module('remontApp').controller('MainViewCtrl', function ($scope, pages) {

        $scope.pages = pages;

        var pageReg = /^#\/(\w+)\/(\w+)(\/\d+)?$/;

        $scope.isActive = function (path) {
            if (pageReg.test(location.hash))
                return path == pageReg.exec(location.hash)[1];
            return false;
        }

        $scope.isEditActivity = function() {
            if (pageReg.test(location.hash)) {
                var action = pageReg.exec(location.hash)[2];
                return action == 'edit' || action == 'create';
            }
            return false;
        }

        $scope.isListActivity = function () {
            if (pageReg.test(location.hash)) {
                var action = pageReg.exec(location.hash)[2];
                return action == 'list';
            }
            return false;
        }

    });
})();