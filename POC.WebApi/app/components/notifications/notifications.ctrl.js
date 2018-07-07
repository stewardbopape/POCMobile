(function () {
    'use strict';

    function NotificationsCtrl($scope) {


        $scope.options = {
            theme: 'urban-noty',
            text: 'Your request has succeded!',
            type: 'success',
            timeout: 3000,
            layout: 'topRight',
            closeWith: ['button', 'click'],
            animation: {
                open: 'in',
                close: 'out',
                easing: 'swing'
            },
        };


        $scope.selectLocation = function (pos, $event) {

            $scope.options.layout = pos;

            angular.element('.location-selector').find('li').removeClass('active');

            angular.element($event.currentTarget).addClass('active');
        };

        $scope.showNoty = function () {
            noty($scope.options);
        };
    }

    angular.module('stats-app').controller('NotificationsCtrl', ['$scope', NotificationsCtrl]);
})();

