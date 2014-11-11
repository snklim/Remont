
angular.module('remontApp').controller('CustomerEditCtrl', function ($scope, $location, $http, customer) {

    $scope.customer = customer;

    $scope.save = function() {

        $http.post('/api/customer', customer).success(function(id) {
            $location.path('customer/edit/' + id);
        });

    };

    $scope.cancel = function () {
        $location.path('customer/list/');
    };

});