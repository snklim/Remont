(function() {
    angular.module('remontApp').filter('formatValue', function() {
        return function (cell, column) {

            if (!column)
                column = cell.Column;

            if (cell.DataSourceRow) {

                if (column && column.DataSourceValueFormat) {
                    var values = [];
                    cell.DataSourceRow.Cells.forEach(function (c) {
                        values.push(c.Value);
                    });
                    var ret = String.format(column.DataSourceValueFormat, values);

                    return ret;
                }

                return cell.DataSourceRow.Cells[0].Value;
            }

            return cell.Value;
        }
    });
})();