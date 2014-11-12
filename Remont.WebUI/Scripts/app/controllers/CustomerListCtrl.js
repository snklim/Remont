(function() {
    angular.module('remontApp').controller('CustomerListCtrl', function ($scope, baseListCtrl, response) {

        baseListCtrl.create($scope, response, 'customer', 'api/customer');

    });
})();