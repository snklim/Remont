(function() {
    angular.module('remontApp').controller('GenericEditCtrl', function ($http, $scope, item, baseEditCtrl, tableId) {

        $scope.row = item.Rows[0];

        baseEditCtrl.create($scope, item, 'generic', 'api/generic');

        $scope.save = function () {

            $http.post('/api/generic', $scope.row).success(function (recordId) {

                console.log(recordId);

            });

        };

    });
})();