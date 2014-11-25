(function() {
    angular.module('remontApp').controller('GenericEditCtrl', function ($http, $scope, response, baseEditCtrl, extData, dataFeeder) {
        
        var item = response.Item;

        $scope.columns = response.Bag;

        $scope.dataSourcePerColumn = [];

        $scope.columns.forEach(function(column, cilumnIndex) {

            var dsItems = [];

            $scope.dataSourcePerColumn[cilumnIndex] = dsItems;

            if (column.DataSourceTableId && column.DataSourceColumnId) {

                dataFeeder.create('api/dataSource')
                    .get({ tableId: column.DataSourceTableId, columnId: column.DataSourceColumnId })
                    .then(function(data) {

                        data.Items.forEach(function(dsItem) {
                            dsItems.push({ Id: dsItem.Id.toString(), Value: dsItem.Value });
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

        $scope.beginPeopleSelect = function() {
            $('#PeoplePickerModal').modal('toggle');
        }

    });
})();