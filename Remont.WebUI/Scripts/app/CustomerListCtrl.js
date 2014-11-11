﻿
angular.module('remontApp').controller('CustomerListCtrl', function ($scope, $http, $location, response) {

    console.log(response);

    $scope.prevPageIndex = 0;

    $scope.customers = response.Items;

    $scope.pages = [
        {
            pageIndex: 'prev',
            pageText: '«'
        },
        {
            pageIndex: 0,
            pageText: 1
        },
        {
            pageIndex: 1,
            pageText: 2
        },
        {
            pageIndex: 2,
            pageText: 3
        },
        {
            pageIndex: 3,
            pageText: 4
        },
        {
            pageIndex: 4,
            pageText: 5
        },
        {
            pageIndex: 'next',
            pageText: '»'
        }
    ];

    $scope.edit = function (id) {

        if (id > 0) {
            $location.path('customer/edit/' + id);
        } else {
            $location.path('customer/create');
        }

    };

    $scope.pageChange = function () {

        var pageIndex = $(event.target).attr('data-pageindex');

        if (pageIndex == 'prev') {
            pageIndex = $scope.prevPageIndex - 1;
        } else if (pageIndex == 'next') {
            pageIndex = $scope.prevPageIndex + 1;
        }

        $http.get('/api/customer/', {

            params: {
                pageIndex: pageIndex
            }

        }).success(function (data) {

            $scope.customers = data.Items;

            $scope.prevPageIndex = data.Request.PageIndex;

        });

        event.preventDefault();

    };

});