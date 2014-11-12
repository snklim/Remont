(function() {
    angular.module('remontApp').controller('CustomerEditCtrl', function ($scope, item, baseEditCtrl) {

        baseEditCtrl.create($scope, item, 'customer', 'api/customer');

    });
})();