(function() {
    angular.module('remontApp').controller('OrderEditCtrl', function ($scope, item, baseEditCtrl, orderStatuses) {

        $scope.orderStatuses = orderStatuses;

        baseEditCtrl.create($scope, item, 'order', 'api/order');

    });
})();