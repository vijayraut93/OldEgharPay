(function () {
    'use strict';

    angular
        .module('Egharpay')
        .factory('ApartmentService', ApartmentService);

    ApartmentService.$inject = ['$http'];

    function ApartmentService($http) {
        var service = {
            retrieveApartment: retrieveApartment,
            searchApartment: searchApartment
        };

        return service;

        function retrieveApartment(Paging, OrderBy) {
            var url = "/Apartment/List",
                data = {
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }

        function searchApartment(SearchKeyword, Paging, OrderBy) {
            var url = "/Apartment/Search",
                data = {
                    searchKeyword: SearchKeyword,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }
    }
})();