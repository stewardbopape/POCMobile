'use strict';

function tableCtrl($scope) {
  $scope.dataTableOpt = {
    'ajax': 'assets/data/datatables-arrays.json'
  };
}

angular
  .module('stats-app')
  .controller('tableCtrl', ['$scope', tableCtrl]);
