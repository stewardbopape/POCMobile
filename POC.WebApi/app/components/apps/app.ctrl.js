(function() {

	'use strict';

	angular
		.module('stats-app')
		.controller('AppCtrl', ['$scope', '$http', '$localStorage', function AppCtrl($scope, $http, $localStorage) {

			$scope.mobileView = 767;

			$scope.app = {
				name: 'Stats SA',
				author: 'Business Modernization',
				version: '0.0.1',
				year: (new Date()).getFullYear(),
				layout: {
					isSmallSidebar: false,
					isChatOpen: false,
					isFixedHeader: false,
					isFixedFooter: false,
					isBoxed: false,
					isStaticSidebar: false,
					isRightSidebar: false,
					isOffscreenOpen: false,
					isConversationOpen: false,
					isQuickLaunch: false,
					sidebarTheme: '',
					headerTheme: ''
				},
				isMessageOpen: false,
				isConfigOpen: false
			};

			$scope.user = {
				fname: 'Risenga',
				lname: 'Maluleke',
				jobDesc: 'STATISTICIAN GENERAL',
				avatar: 'assets/images/profile.png',
			};

			if (angular.isDefined($localStorage.layout)) {
				$scope.app.layout = $localStorage.layout;
				// console.log('yes 1');
				localStorage.clear();
			} else {
				$localStorage.layout = $scope.app.layout;
				// console.log('yes 2');
				localStorage.clear();
			}

			$scope.$watch('app.layout', function () {
				$localStorage.layout = $scope.app.layout;
			}, true);

			$scope.getRandomArbitrary = function () {
				return Math.round(Math.random() * 100);
			};
		}
		]);
})();
