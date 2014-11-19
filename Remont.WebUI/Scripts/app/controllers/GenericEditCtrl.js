(function() {
    angular.module('remontApp').controller('GenericEditCtrl', function ($http, $scope, item, baseEditCtrl, tableId) {

        $scope.values = [];

        baseEditCtrl.create($scope, item, 'generic', 'api/generic');

        $scope.save = function () {

            $http.post('/api/generic', {
                tableId: tableId,
                recordId: 0,
                values: $scope.values
            }).success(function (recordId) {

                console.log(recordId);

            });

        };

    });
})();