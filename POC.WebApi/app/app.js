(function() {
	'use strict';

	angular
		.module('stats-app', [
			'ui.router',
			'ngAnimate',
			'ui.bootstrap',
			'oc.lazyLoad',
			'ngStorage',
			'ngSanitize',
			'ui.utils',
			'ngTouch'
		])
		.constant('COLORS', {
			brand1: '#0057a7',
			brand2: '#e0e0e0',
			brand3: '#d2d3d5',
			brand4: '#949599',
			brand5: '#231f20',
			'default': '#e2e2e2',
			primary: '#09c',
			success: '#2ECC71',
			warning: '#ffc65d',
			danger: '#d96557',
			info: '#4cc3d9',
			white: 'white',
			dark: '#4C5064',
			border: '#e4e4e4',
			bodyBg: '#e0e8f2',
			textColor: '#6B6B6B',
		});
})();
