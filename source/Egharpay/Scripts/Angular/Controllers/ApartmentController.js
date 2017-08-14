(function () {
    'use strict';

    angular
        .module('Egharpay')
        .controller('ApartmentController', ApartmentController);

    ApartmentController.$inject = ['$window', 'ApartmentService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function ApartmentController($window, ApartmentService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.apartments = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.viewApartment = viewApartment;
        vm.searchApartment = searchApartment;
        vm.searchKeyword = "";
        vm.searchMessage = "";
        initialise();

        function initialise() {
            order("Name");
        }

        function retrieveApartment() {
            return ApartmentService.retrieveApartment(vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.apartments = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.apartments.length === 0 ? "No Records Found" : "";
                    return vm.apartments;
                });
        }

        function searchApartment(searchKeyword) {
            vm.searchKeyword = searchKeyword;
            return ApartmentService.searchApartment(vm.searchKeyword, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.apartments = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.apartments.length === 0 ? "No Records Found" : "";
                    return vm.apartments;
                });
        }

        function pageChanged() {
            if (vm.searchKeyword) {
                return searchApartment(vm.searchKeyword)();
            }
            return retrieveApartment();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            if (vm.searchKeyword) {
                return searchApartment(vm.searchKeyword)();
            }
            return retrieveApartment();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

        function viewApartment(apartmentId) {
            $window.location.href = "/Apartment/Edit/" + apartmentId;
        }

    }
})();
