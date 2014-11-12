(function() {
    angular.module('remontApp').controller('OrderListCtrl', function($scope, baseListCtrl, response) {

        baseListCtrl.create($scope, response, 'order', 'api/order');

    });
})();