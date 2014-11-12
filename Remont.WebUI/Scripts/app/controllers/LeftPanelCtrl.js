(function() {
    angular.module('remontApp').controller('LeftPanelCtrl', function($scope) {

        $scope.isActive = function(path) {
            return location.hash.indexOf(path) >= 0;
        }

    });
})();