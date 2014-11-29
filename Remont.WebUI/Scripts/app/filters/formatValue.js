(function() {
    angular.module('remontApp').filter('formatValue', function() {
        return function (cell, column, indexInGroup) {

            if (!column)
                column = cell.Column;

            var dataSourceRow = cell.DataSourceRow;

            if (!isNaN(indexInGroup))
                dataSourceRow = cell.DataSourceRows[indexInGroup];

            if (dataSourceRow) {

                if (column && column.DataSourceValueFormat) {
                    var values = [];
                    dataSourceRow.Cells.forEach(function (c) {
                        values.push(c.Value);
                    });
                    var ret = String.format(column.DataSourceValueFormat, values);

                    return ret;
                }

                return dataSourceRow.Cells && dataSourceRow.Cells[0] && dataSourceRow.Cells[0].Value;
            }

            return cell.Value;
        }
    });
})();