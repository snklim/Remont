(function() {
    angular.module('remontApp').controller('GenericEditCtrl', function ($http, $location, $scope,
        response, baseEditCtrl, extData, dataFeeder, editEntityContext) {

        var editingItem = editEntityContext.get('editingItem');
        if (editingItem) {
            editEntityContext.remove('editingItem');
            response.Item = editingItem;
        }
        
        var item = response.Item;

        $scope.dataSource = {};

        $scope.columns = response.Bag;

        $scope.dataSourcePerColumn = [];

        $scope.columns.forEach(function(column, cilumnIndex) {

            var dsItems = [];

            $scope.dataSourcePerColumn[cilumnIndex] = dsItems;

            if (column.DataSourceTableId) {

                dataFeeder.create('api/dataSource')
                    .get({ tableId: column.DataSourceTableId })
                    .then(function(data) {

                        data.Items.forEach(function (dsItem) {
                            dsItems.push(dsItem);
                        });

                    });

            }
        });

        baseEditCtrl.create($scope, item, extData.pageUrl, extData.serviceUrl);

        $scope.onSave = function (newItem) {
            $.each(newItem.Cells, function (i, cell) {
                item.Cells[i].Id = cell.Id;
            });
        };

        $scope.beginEntitySelect = function (cell, columnIndex) {

            var backHash = $location.path();
            var currentEditingItem = $scope.item;

            editEntityContext.put('onEntitySelected', function (row) {
                editEntityContext.remove('onEntitySelected');

                currentEditingItem.Cells[columnIndex].DataSourceRowId = row.Id;
                currentEditingItem.Cells[columnIndex].DataSourceRow = row;

                editEntityContext.put('editingItem', currentEditingItem);

                $location.path(backHash);
            });
            
            $location.path($scope.columns[columnIndex].DataSourceTable.TableName.toLowerCase() + '/list');
        }

    });
})();