(function() {
    angular.module('remontApp').controller('OrderEditCtrl', function($scope, $location, $http, orderStatuses, order) {

        $scope.orderStatuses = orderStatuses;

        $scope.order = order;

        $scope.save = function() {

            $http.post('/api/order', $scope.order).success(function(id) {
                $location.path('order/edit/' + id);
            });

        };

        $scope.cancel = function() {
            $location.path('order/list/');
        };

    });
})();