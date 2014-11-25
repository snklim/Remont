(function() {
    angular.module('remontApp').controller('TableEditCtrl', function ($scope, response, baseEditCtrl, controls, dataSourceTables, dataFeeder) {

        var item = response.Item;

        $scope.controls = controls;
        $scope.dataSourceTables = dataSourceTables;
        
        if (!item.Columns) {
            item.Columns = [];
        }

        if (item.Columns.length == 0) {
            item.Columns.push({ IsDeleted: false });
        }

        baseEditCtrl.create($scope, item, 'table', 'api/table');
        
        $scope.addColumn = function() {
            item.Columns.push({ IsDeleted: false });
        };

        $scope.addVisible = function (index) {
            return _.filter(item.Columns, function(c) {
                return c.IsDeleted == false;
            }).length - 1 == index;
        };

        $scope.removeVisible = function (index) {
            return index > 0 && _.filter(item.Columns, function (c) {
                return c.IsDeleted == false;
            }).length - 1 == index;
        };

        $scope.removeColumn = function (column) {
            column.IsDeleted = true;
        };

        $scope.onSave = function (newItem) {
            item.Columns.splice(0, item.Columns.length);
            $.each(newItem.Columns, function(i, column) {
                item.Columns.push(column);
            });
        };

        $scope.dataSourceTableColumnsPerColumn = [];

        var tableFeeder = dataFeeder.create('api/table');
        $scope.onDataSourceTableChanged = function (columnIndex, tableId) {

            if (!$scope.dataSourceTableColumnsPerColumn[columnIndex]) {
                $scope.dataSourceTableColumnsPerColumn[columnIndex] = [];
            }

            tableFeeder.get({ id: tableId, action: 'item' }).then(function (data) {
                
                var columns = $scope.dataSourceTableColumnsPerColumn[columnIndex];
                columns.splice(0, columns.length);
                data.Item.Columns.forEach(function(column) {
                    columns.push(column);
                });

            });
        };

        item.Columns.forEach(function (column, columnIndex) {
            if (column.DataSourceTableId) {
                $scope.onDataSourceTableChanged(columnIndex, column.DataSourceTableId);
            }
        });

    });
})();