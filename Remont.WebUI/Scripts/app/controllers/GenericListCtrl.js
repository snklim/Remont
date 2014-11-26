(function() {
    angular.module('remontApp').controller('GenericListCtrl', function ($location, $scope,
        baseListCtrl, response, extData, editEntityContext) {

        $scope.columns = response.Bag;
        
        baseListCtrl.create($scope, response, extData.pageUrl, extData.serviceUrl, extData.tableId);

        $scope.onRowClicked = function (row) {

            var action = editEntityContext.get('onEntitySelected');

            if (action) {
                action(row);
            }

        }

    });
})();