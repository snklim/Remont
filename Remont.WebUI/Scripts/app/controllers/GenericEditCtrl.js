(function() {
    angular.module('remontApp').controller('GenericEditCtrl', function ($http, $location, $scope, response, baseEditCtrl, extData, dataFeeder) {
        
        var item = response.Item;

        $scope.dataSource = {};

        $scope.columns = response.Bag;

        $scope.dataSourcePerColumn = [];

        $scope.columns.forEach(function(column, cilumnIndex) {

            var dsItems = [];

            $scope.dataSourcePerColumn[cilumnIndex] = dsItems;

            if (column.DataSourceTableId && column.DataSourceColumnId) {

                dataFeeder.create('api/dataSource')
                    .get({ tableId: column.DataSourceTableId, columnId: column.DataSourceColumnId })
                    .then(function(data) {

                        data.Items.forEach(function (dsItem) {
                            dsItems.push(dsItem);
                        });

                    });

            }
        });

        baseEditCtrl.create($scope, item, extData.pageUrl, extData.serviceUrl);

        $scope.onSave = function (newItem) {
            item.Cells.splice(0, item.Cells.length);
            $.each(newItem.Cells, function (i, cell) {
                item.Cells.push(cell);
            });
        };

        $scope.beginEntitySelect = function (cell, cilumnIndex) {
            //console.log(cilumnIndex, cell, $scope.columns[cilumnIndex]);


            $scope.dataSource.tableId = $scope.columns[cilumnIndex].DataSourceTableId;

            //$('#EntityPickerModal').modal('toggle');

            $location.path('/#/customer/list');
        }

    });
})();