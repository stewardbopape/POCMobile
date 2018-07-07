(function() {
	'use strict';

	angular
		.module('stats-app')
		.run(['$rootScope', '$state', '$stateParams', function ($rootScope, $state, $stateParams) {
			$rootScope.$state = $state;
			$rootScope.$stateParams = $stateParams;
			$rootScope.$on('$stateChangeSuccess', function () {
				window.scrollTo(0, 0);
			});
			FastClick.attach(document.body);
		}])
		.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

			// For unmatched routes
			$urlRouterProvider.otherwise('/');

			// Application routes
			$stateProvider
				.state('app', {
					abstract: true,
					templateUrl: 'app/shared/common/layout.html',
				})

				.state('app.dashboard', {
					url: '/',
					templateUrl: 'app/components/dashboard/dashboard.html',
					resolve: {
					deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([ {
									insertBefore: '#load_styles_before',
									files: [
											'assets/css/climacons-font.css',
											'assets/libs/rickshaw/rickshaw.min.css',
											//'custom.min.css'
									] },  {
									serie: true,
									files: [
											'assets/libs/d3/d3.min.js',
											'assets/libs/rickshaw/rickshaw.min.js',
											'assets/libs/flot/jquery.flot.js',
											'assets/libs/flot/jquery.flot.resize.js',
											'assets/libs/flot/jquery.flot.pie.js',
											'assets/libs/flot/jquery.flot.categories.js',
											
									] }, {
										name: 'angular-flot',
										files: [
													'assets/libs/angular-flot/angular-flot.js',
											]
									}]).then(function () {
									return $ocLazyLoad.load('./app/components/dashboard/dashboard.ctrl.js');
							});
					}]
					},
					data: {
						title: 'Dashboard',
					}
				})

			.state('app.widgets', {
			    url: '/widgets',
			    templateUrl: 'app/components/widgets/widgets.html',
			    resolve: {
			        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
			            return $ocLazyLoad.load([
							{
							    insertBefore: '#load_styles_before',
							    files: [
																'assets/css/climacons-font.css',
																'assets/libs/checkbo/src/0.1.4/css/checkBo.min.css'
							    ]
							},
							{
							    files: [
																'assets/libs/checkbo/src/0.1.4/js/checkBo.min.js'
							    ]
							}]);
			        }]
			    },
			    data: {
			        title: 'Widgets',
			    }
			})

// UI Routes
			.state('app.ui', {
					template: '<div ui-view></div>',
					abstract: true,
					url: '/ui',
				})
				.state('app.ui.buttons', {
					url: '/buttons',
					templateUrl: 'app/components/ui/ui-buttons.html',
					controller: 'buttonsCtrl',
					controllerAs: 'vm',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									insertBefore: '#load_styles_before',
									files: [
												'custom.min.css'
											]
								},
								{
									files: [
											]
								}]).then(function () {													
									return $ocLazyLoad.load('./app/components/ui/buttons.ctrl.js');
								});
							}]
					},
					data: {
						title: 'Buttons',
					}
				})
				.state('app.ui.directives', {
					url: '/directives',
					templateUrl: './app/components/ui/ui-general.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									insertBefore: '#load_styles_before',
									files: [
																'assets/libs/checkbo/src/0.1.4/css/checkBo.min.css',
																'assets/libs/chosen_v1.4.0/chosen.min.css'
																//'custom.min.css'
														]
												},
								{
									files: [
																'assets/libs/checkbo/src/0.1.4/js/checkBo.min.js',
																'assets/libs/chosen_v1.4.0/chosen.jquery.min.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/bootstrap.ui/bootstrap.ui.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Bootstrap Directives',
					}
				})
				.state('app.ui.tabs_accordion', {
					url: '/tabs_accordions',
					templateUrl: './app/components/ui/ui-tabs-accordion.html',
					data: {
						title: 'Nav Tabs',
					}
				})
				.state('app.ui.portlets', {
					url: '/portlets',
					templateUrl: './app/components/ui/ui-portlets.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									serie: true,
									files: [
																'assets/libs/perfect-scrollbar/js/perfect-scrollbar.jquery.js',
																'assets/libs/jquery.ui/ui/core.js',
																'assets/libs/jquery.ui/ui/widget.js',
																'assets/libs/jquery.ui/ui/mouse.js',
																'assets/libs/jquery.ui/ui/sortable.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/draggable/draggable.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Portlets',
					}
				})
				.state('app.ui.fontawesome', {
					url: '/fontawesome',
					templateUrl: './app/components/ui/ui-fontawesome.html',
					data: {
						title: 'Fontawesome Icons',
					}
				})
				.state('app.ui.statsicons', {
					url: '/statsicons',
					templateUrl: './app/components/ui/icons.html',
					data: {
						title: 'Stats Icons',
					}
				})
				.state('app.ui.feather', {
					url: '/feather',
					templateUrl: './app/components/ui/ui-feather.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load('assets/css/feather.css');
										}]
					},
					data: {
						title: 'Feather Icons',
					}
				})
				.state('app.ui.climacon', {
					url: '/climacon',
					templateUrl: './app/components/ui/ui-climacon.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load('assets/css/climacons-font.css');
										}]
					},
					data: {
						title: 'Climacon Icons',
					}
				})
				.state('app.ui.progressbars', {
					url: '/progressbars',
					templateUrl: './app/components/ui/ui-progressbars.html',
					data: {
						title: 'Progress Bars',
					}
				})
				.state('app.ui.sliders', {
					url: '/sliders',
					templateUrl: './app/components/ui/ui-sliders.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									serie: true,
									files: [
																'assets/libs/jquery.ui/ui/core.js',
																'assets/libs/jquery.ui/ui/widget.js',
																'assets/libs/jquery.ui/ui/mouse.js',
																'assets/libs/jquery.ui/ui/slider.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/slider/slider.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Sliders',
					}
				})
				.state('app.ui.pagination', {
					url: '/pagination',
					templateUrl: './app/components/ui/ui-pagination.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load('./app/components/bootstrap.ui/bootstrap.ui.ctrl.js');
										}]
					},
					data: {
						title: 'Pagination',
					}
				})
				.state('app.ui.notifications', {
					url: '/notifications',
					templateUrl: './app/components/ui/ui-notifications.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									insertBefore: '#load_styles_before',
									files: ['assets/libs/chosen_v1.4.0/chosen.min.css']
												},
								{
									serie: true,
									files: [
																'assets/libs/chosen_v1.4.0/chosen.jquery.min.js',
																'assets/libs/noty/js/noty/packaged/jquery.noty.packaged.min.js',
																'assets/js/extentions/noty-defaults.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/notifications/notifications.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Notifications',
					}
				})
				.state('app.ui.alert', {
					url: '/alert',
					templateUrl: './app/components/ui/ui-alert.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									insertBefore: '#load_styles_before',
									files: ['assets/libs/sweetalert/dist/sweetalert.css']
												},
								{
									name: 'oitozero.ngSweetAlert',
									files: [
																'assets/libs/sweetalert/dist/sweetalert.min.js',
																'assets/libs/angular-sweetalert/SweetAlert.min.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/alert/alert.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Alerts',
					}
				})


			// Forms routes
			.state('app.forms', {
					template: '<div ui-view></div>',
					abstract: true,
					url: '/forms',
				})
				.state('app.forms.native_forms', {
					url: '/native_forms',
					templateUrl: './app/components/form/form-basic.html',
					data: {
						title: 'Native Form Elements',
					}
				})
				.state('app.forms.advanced_forms', {
					url: '/advanced_forms',
					templateUrl: './app/components/form/form-advanced.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									insertBefore: '#load_styles_before',
									files: [
																'assets/libs/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.css',
																'assets/libs/chosen_v1.4.0/chosen.min.css',
																'assets/libs/jquery.tagsinput/src/jquery.tagsinput.css',
																'assets/libs/checkbo/src/0.1.4/css/checkBo.min.css',
																'assets/libs/intl-tel-input/build/css/intlTelInput.css',
																'assets/libs/bootstrap-daterangepicker/daterangepicker-bs3.css',
																'assets/libs/bootstrap-datepicker/dist/css/bootstrap-datepicker3.css',
																'assets/libs/bootstrap-timepicker/css/bootstrap-timepicker.min.css',
																'assets/libs/clockpicker/dist/bootstrap-clockpicker.min.css',
																'assets/libs/mjolnic-bootstrap-colorpicker/dist/css/bootstrap-colorpicker.min.css'
														]
												},
								{
									serie: true,
									files: [
																'assets/libs/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.js',
																'assets/libs/chosen_v1.4.0/chosen.jquery.min.js',
																'assets/libs/jquery.tagsinput/src/jquery.tagsinput.js',
																'assets/libs/checkbo/src/0.1.4/js/checkBo.min.js',
																'assets/libs/intl-tel-input//build/js/intlTelInput.min.js',
																'assets/libs/moment/min/moment.min.js',
																'assets/libs/bootstrap-daterangepicker/daterangepicker.js',
																'assets/libs/bootstrap-datepicker/dist/js/bootstrap-datepicker.js',
																'assets/libs/bootstrap-timepicker/js/bootstrap-timepicker.min.js',
																'assets/libs/clockpicker/dist/jquery-clockpicker.min.js',
																'assets/libs/mjolnic-bootstrap-colorpicker/dist/js/bootstrap-colorpicker.min.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/form/form.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Advanced Form Plugins',
					}
				})
				.state('app.forms.validation', {
					url: '/validation',
					templateUrl: './app/components/validation/form-validation.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load('assets/libs/jquery-validation/dist/jquery.validate.min.js').then(function () {
								return $ocLazyLoad.load('./app/components/validation/validation.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Form Validation',
					}
				})
				.state('app.forms.wizard', {
					url: '/wizard',
					templateUrl: './app/components/wizard/form-wizard.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									insertBefore: '#load_styles_before',
									files: [
																'assets/libs/checkbo/src/0.1.4/css/checkBo.min.css',
																'assets/libs/chosen_v1.4.0/chosen.min.css'
														]
												},
								{
									files: [
																'assets/libs/checkbo/src/0.1.4/js/checkBo.min.js',
																'assets/libs/chosen_v1.4.0/chosen.jquery.min.js',
																'assets/libs/card/lib/js/jquery.card.js',
																'assets/libs/bootstrap/js/tab.js',
																'assets/libs/jquery-validation/dist/jquery.validate.min.js',
																'assets/libs/twitter-bootstrap-wizard/jquery.bootstrap.wizard.min.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/wizard/wizard.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Form Wizards',
					}
				})
				.state('app.forms.editors', {
					url: '/editors',
					templateUrl: './app/components/editor/form-editors.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									insertBefore: '#load_styles_before',
									files: [
																'assets/libs/summernote/dist/summernote.css',
																'assets/libs/bootstrap3-wysihtml5-bower/dist/bootstrap3-wysihtml5.min.css'
														]
												},
								{
									files: [
																'assets/libs/bootstrap/js/tooltip.js',
																'assets/libs/bootstrap/js/dropdown.js',
																'assets/libs/bootstrap/js/modal.js',
																'assets/libs/bootstrap3-wysihtml5-bower/dist/bootstrap3-wysihtml5.all.js',
																'assets/libs/summernote/dist/summernote.min.js'
														]
												},
								{
									name: 'summernote',
									files: [
																'assets/libs/angular-summernote/dist/angular-summernote.min.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/editor/editor.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Form Editors',
					}
				})
				.state('app.forms.masks', {
					url: '/masks',
					templateUrl: './app/components/mask/form-masks.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load('assets/libs/jquery.maskedinput/dist/jquery.maskedinput.min.js').then(function () {
								return $ocLazyLoad.load('./app/components/mask/mask.ctrl.js');
							});
						}]
					},
					data: {
						title: 'Input Masks',
					}
				})
				.state('app.forms.upload', {
					url: '/upload',
					templateUrl: './app/components/upload/form-upload.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									name: 'angularFileUpload',
									files: [
																'assets/libs/angular-file-upload/angular-file-upload.min.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/upload/upload.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Form Upload',
					}
				})


			// Tables routes
			.state('app.tables', {
					template: '<div ui-view></div>',
					abstract: true,
					url: '/tables',
				})
				.state('app.tables.table_basic', {
					url: '/table_basic',
					templateUrl: './app/components/table/table-basic.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									insertBefore: '#load_styles_before',
									files: [
																'assets/libs/sortable/css/sortable-theme-bootstrap.css',
																'assets/libs/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.css',
								'assets/libs/chosen_v1.4.0/chosen.min.css',
								'assets/libs/jquery.tagsinput/src/jquery.tagsinput.css',
								'assets/libs/checkbo/src/0.1.4/css/checkBo.min.css',
								'assets/libs/intl-tel-input/build/css/intlTelInput.css',
								'assets/libs/bootstrap-daterangepicker/daterangepicker-bs3.css',
								'assets/libs/bootstrap-datepicker/dist/css/bootstrap-datepicker3.css',
								'assets/libs/bootstrap-timepicker/css/bootstrap-timepicker.min.css',
								'assets/libs/clockpicker/dist/bootstrap-clockpicker.min.css',
								'assets/libs/mjolnic-bootstrap-colorpicker/dist/css/bootstrap-colorpicker.min.css',
								'assets/css/climacons-font.css',
								'assets/libs/rickshaw/rickshaw.min.css',
								'assets/libs/datatables/media/css/jquery.dataTables.css'
														]
												},
								{
									files: [
																'assets/libs/sortable/js/sortable.min.js',
																'assets/libs/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.js',
								'assets/libs/chosen_v1.4.0/chosen.jquery.min.js',
								'assets/libs/jquery.tagsinput/src/jquery.tagsinput.js',
								'assets/libs/checkbo/src/0.1.4/js/checkBo.min.js',
								'assets/libs/intl-tel-input//build/js/intlTelInput.min.js',
								'assets/libs/moment/min/moment.min.js',
								'assets/libs/bootstrap-daterangepicker/daterangepicker.js',
								'assets/libs/bootstrap-datepicker/dist/js/bootstrap-datepicker.js',
								'assets/libs/bootstrap-timepicker/js/bootstrap-timepicker.min.js',
								'assets/libs/clockpicker/dist/jquery-clockpicker.min.js',
								'assets/libs/mjolnic-bootstrap-colorpicker/dist/js/bootstrap-colorpicker.min.js',
								'assets/libs/chosen_v1.4.0/chosen.jquery.min.js',
																'assets/libs/datatables/media/js/jquery.dataTables.js',
																// 'assets/js/extentions/bootstrap-datatables.js',
														]
												}]).then(function () {
								Sortable.init();
							});
										}]
					},
					data: {
						title: 'Basic Table',
					}
				})
				.state('app.tables.table_responsive', {
					url: '/table_responsive',
					templateUrl: './app/components/table/table-responsive.html',
					data: {
						title: 'Responsive Table',
					}
				})
				.state('app.tables.datatable', {
					url: '/datatable',
					templateUrl: './app/components/table/table-datatable.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									insertBefore: '#load_styles_before',
									files: [
																'assets/libs/chosen_v1.4.0/chosen.min.css',
																'assets/libs/datatables/media/css/jquery.dataTables.css'
														]
												},
								{
									serie: true,
									files: [
																'assets/libs/chosen_v1.4.0/chosen.jquery.min.js',
																'assets/libs/datatables/media/js/jquery.dataTables.js',
																'assets/js/extentions/bootstrap-datatables.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/table/table.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Datatable',
					}
				})
				.state('app.tables.table_editable', {
					url: '/table_editable',
					templateUrl: './app/components/editable/table-editable.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									insertBefore: '#load_styles_before',
									files: [
																'assets/libs/angular-xeditable/dist/css/xeditable.css'
														]
												},
								{
									name: 'xeditable',
									files: [
																'assets/libs/angular-xeditable/dist/js/xeditable.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/editable/editable.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Editable Table',
					}
				})


			// Chart routes
			.state('app.charts', {
					template: '<div ui-view></div>',
					abstract: true,
					url: '/charts',
				})
				.state('app.charts.flot', {
					url: '/flot',
					templateUrl: './app/components/flot/charts-flot.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									serie: true,
									files: [
																'assets/libs/flot/jquery.flot.js',
																'assets/libs/flot/jquery.flot.resize.js',
																'assets/libs/flot/jquery.flot.categories.js',
																'assets/libs/flot/jquery.flot.stack.js',
																'assets/libs/flot/jquery.flot.time.js',
																'assets/libs/flot/jquery.flot.pie.js',
																'assets/libs/flot-spline/js/jquery.flot.spline.js',
																'assets/libs/flot.orderbars/js/jquery.flot.orderBars.js'
														]
												},
								{
									name: 'angular-flot',
									files: [
																'assets/libs/angular-flot/angular-flot.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/flot/flot.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Flot Charts',
					}
				})
				.state('app.charts.easypie', {
					url: '/easypie',
					templateUrl: './app/components/easychart/charts-easypie.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									name: 'easypiechart',
									files: [
																'assets/libs/jquery.easy-pie-chart/dist/angular.easypiechart.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/easychart/easychart.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Easypie Charts',
					}
				})
				.state('app.charts.chartjs', {
					url: '/chartjs',
					templateUrl: './app/components/chartjs/charts-chartjs.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								 {
									files: [
																'assets/libs/chartjs/Chart.js',
														]
												},
								{
									name: 'angles',
									serie: true,
									files: [
																'assets/libs/angles/angles.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/chartjs/chartjs.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Chartjs',
					}
				})
				.state('app.charts.rickshaw', {
					url: '/rickshaw',
					templateUrl: './app/components/rickshaw/charts-rickshaw.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									insertBefore: '#load_styles_before',
									files: [
																'assets/libs/rickshaw/rickshaw.min.css'
														]
												},
								{
									serie: true,
									files: [
																'assets/libs/d3/d3.min.js',
																'assets/libs/rickshaw/rickshaw.min.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/rickshaw/rickshaw.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Rickshaw Charts',
					}
				})
				.state('app.charts.nvd3', {
					url: '/nvd3',
					templateUrl: './app/components/nvd3/charts-nvd3.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									insertBefore: '#load_styles_before',
									files: [
																'assets/libs/nvd3/nv.d3.min.css'
														]
												},
								{
									name: 'nvd3',
									serie: true,
									files: [
																'assets/libs/d3/d3.min.js',
																'assets/libs/nvd3/nv.d3.min.js',
																'assets/libs/angular-nvd3/dist/angular-nvd3.min.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/nvd3/nvd3.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Nvd3 Charts',
					}
				})
				.state('app.charts.c3', {
					url: '/c3',
					templateUrl: './app/components/c3/charts-c3.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									insertBefore: '#load_styles_before',
									files: [
																'assets/libs/c3/c3.min.css'
														]
												},
								{
									serie: true,
									files: [
																'assets/libs/d3/d3.min.js',
																'assets/libs/c3/c3.min.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/c3/c3.ctrl.js');
							});
										}]
					},
					data: {
						title: 'C3',
					}
				})


			// Maps routes
			.state('app.maps', {
					template: '<div ui-view></div>',
					abstract: true,
					url: '/maps',
				})
				.state('app.maps.google', {
					url: '/google',
					templateUrl: './app/components/google/map-google.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									name: 'ui.map',
									files: [
																'assets/libs/angular-ui-map/ui-map.min.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/google/google.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Google Maps',
						contentClasses: 'no-padding'
					}
				})
				.state('app.maps.vector', {
					url: '/vector',
					templateUrl: './app/components/vector/map-vector.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									insertBefore: '#load_styles_before',
									files: [
																'assets/libs/bower-jvectormap/jquery-jvectormap-1.2.2.css'
														]
												},
								{
									serie: true,
									files: [
																'assets/libs/bower-jvectormap/jquery-jvectormap-1.2.2.min.js',
																'data/maps/jquery-jvectormap-world-mill-en.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/vector/vector.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Vector Maps',
						contentClasses: 'no-padding'
					}
				})


			// Apps routes
			.state('app.apps', {
					template: '<div ui-view></div>',
					abstract: true,
					url: '/apps',
				})
				.state('app.apps.calendar', {
					url: '/calendar',
					templateUrl: './app/components/calendar/app-calendar.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									insertBefore: '#load_styles_before',
									files: [
																'assets/libs/fullcalendar/dist/fullcalendar.min.css'
														]
												},
								{
									serie: true,
									files: [
																'assets/libs/jquery.ui/ui/core.js',
																'assets/libs/jquery.ui/ui/widget.js',
																'assets/libs/jquery.ui/ui/mouse.js',
																'assets/libs/jquery.ui/ui/draggable.js',
																'assets/libs/moment/moment.js',
																'assets/libs/fullcalendar/dist/fullcalendar.min.js',
																'assets/libs/fullcalendar/dist/gcal.js'
														]
												},
								{
									name: 'ui.calendar',
									files: [
																'assets/libs/angular-ui-calendar/src/calendar.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/calendar/calendar.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Calendar',
					}
				})
				.state('app.apps.gallery', {
					url: '/gallery',
					templateUrl: './app/components/gallery/app-gallery.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									serie: true,
									insertBefore: '#load_styles_before',
									files: [
																'assets/libs/blueimp-gallery/css/blueimp-gallery.min.css',
																'assets/libs/blueimp-bootstrap-image-gallery/css/bootstrap-image-gallery.min.css'
														]
												},
								{
									serie: true,
									files: [
																'assets/libs/blueimp-gallery/js/jquery.blueimp-gallery.min.js',
																'assets/libs/blueimp-bootstrap-image-gallery/js/bootstrap-image-gallery.min.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/gallery/gallery.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Gallery',
					}
				})
				.state('app.apps.messages', {
					url: '/messages',
					templateUrl: './app/components/messages/app-messages.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load('./app/components/messages/messages.ctrl.js').then(function () {
								return $ocLazyLoad.load('./app/components/messages/messages.services.js');
							});
										}]
					},
					data: {
						title: 'Messages',
						appClasses: 'layout-small-menu',
						contentClasses: 'no-padding'
					}
				})
				.state('app.apps.social', {
					url: '/social',
					templateUrl: './app/components/social/app-social.html',
					data: {
						title: 'Social Profile',
					}
				})


			// Apps routes
			.state('app.extras', {
					template: '<div ui-view></div>',
					abstract: true,
					url: '/extras',
				})
				.state('app.extras.popup', {
					url: '/popup',
					templateUrl: './app/components/extra/extras-popup.html',
					data: {
						title: 'Popup',
					}
				})
				.state('app.extras.invoice', {
					url: '/invoice',
					templateUrl: './app/components/extra/extras-invoice.html',
					data: {
						title: 'Invoice',
					}
				})
				.state('app.extras.timeline', {
					url: '/timeline',
					templateUrl: './app/components/extra/extras-timeline.html',
					data: {
						title: 'Timeline',
					}
				})
				.state('app.extras.sortable', {
					url: '/sortable',
					templateUrl: './app/components/extra/extras-sortable.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load([
								{
									serie: true,
									files: [
																'assets/libs/jquery.ui/ui/core.js',
																'assets/libs/jquery.ui/ui/widget.js',
																'assets/libs/jquery.ui/ui/mouse.js',
																'assets/libs/jquery.ui/ui/sortable.js'
														]
												}]).then(function () {
								return $ocLazyLoad.load('./app/components/sortable/sortable.ctrl.js');
							});
										}]
					},
					data: {
						title: 'Sortable',
					}
				})
				.state('app.extras.nestable', {
					url: '/nestable',
					templateUrl: './app/components/extra/extras-nestable.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load('assets/libs/nestable/jquery.nestable.js');
										}]
					},
					data: {
						title: 'Nestable',
					}
				})
				.state('app.extras.search', {
					url: '/search',
					templateUrl: './app/components/extra/extras-search.html',
					data: {
						title: 'Search',
					}
				})
				.state('app.extras.changelog', {
					url: '/changelog',
					templateUrl: './app/components/extra/extras-changelog.html',
					data: {
						title: 'Changelog',
					}
				})
				.state('app.extras.blank', {
					url: '/blank',
					templateUrl: './app/components/extra/extras-blank.html',
					data: {
						title: 'Blank Pages',
					}
				})




			.state('user', {
					templateUrl: './app/shared/common/session.html',
				})
				.state('user.signin', {
					url: '/signin',
					templateUrl: './app/components/extra/extras-signin.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load('./app/components/session/session.ctrl.js');
										}]
					},
					data: {
						appClasses: 'bg-white usersession',
						contentClasses: 'full-height'
					}
				})
				.state('user.signup', {
					url: '/signup',
					templateUrl: './app/components/extra/extras-signup.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load('./app/components/session/session.ctrl.js');
										}]
					},
					data: {
						appClasses: 'bg-white usersession',
						contentClasses: 'full-height'
					}
				})
				.state('user.forgot', {
					url: '/forgot',
					templateUrl: './app/components/extra/extras-forgot.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load('./app/components/session/session.ctrl.js');
										}]
					},
					data: {
						appClasses: 'bg-white usersession',
						contentClasses: 'full-height'
					}
				})

			.state('app.404', {
					url: '/404',
					templateUrl: './app/components/extra/extras-404.html',
					data: {
						title: 'Page Not Found',
						contentClasses: 'no-padding',
					}
				})
				.state('user.500', {
					url: '/500',
					templateUrl: './app/components/extra/extras-500.html',
					data: {
						appClasses: 'usersession',
						contentClasses: 'full-height'
					}
				})
				.state('user.lockscreen', {
					url: '/lockscreen',
					templateUrl: './app/components/extra/extras-lockscreen.html',
					resolve: {
						deps: ['$ocLazyLoad', function ($ocLazyLoad) {
							return $ocLazyLoad.load('./app/components/session.js');
										}]
					},
					data: {
						appClasses: 'usersession',
						contentClasses: 'full-height'
					}
				})

			.state('app.kit', {
				template: '<div ui-view></div>',
				abstract: true,
				url: '/kit',
			})
			.state('app.kit.started', {
				url: '/getting-started',
				templateUrl: './app/components/design-kit/getting-started.html',
				data: {
					title: 'Getting started',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.install', {
				url: '/install',
				templateUrl: './app/components/design-kit/install.html',
				data: {
					title: 'Install',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.guidelines', {
				url: '/guidelines',
				templateUrl: './app/components/design-kit/guidelines.html',
				data: {
					title: 'Guidelines',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.content', {
				url: '/content',
				templateUrl: './app/components/design-kit/content.html',
				data: {
					title: 'Content',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.principles', {
				url: '/principles',
				templateUrl: './app/components/design-kit/principles.html',
				data: {
					title: 'Principles',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.style', {
				url: '/style',
				templateUrl: './app/components/design-kit/style.html',
				data: {
					title: 'Style',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.color', {
				url: '/color',
				templateUrl: './app/components/design-kit/color.html',
				data: {
					title: 'Color',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.icons', {
				url: '/icons',
				templateUrl: './app/components/design-kit/icons.html',
				data: {
					title: 'Icons',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.typography', {
				url: '/typography',
				templateUrl: './app/components/design-kit/typography.html',
				data: {
					title: 'Typography',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.pages', {
				url: '/pages',
				templateUrl: './app/components/design-kit/pages.html',
				data: {
					title: 'Pages',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.login-page', {
				url: '/login-page',
				templateUrl: './app/components/design-kit/login-page.html',
				data: {
					title: 'Login page',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.landing-page', {
				url: '/landing-page',
				templateUrl: './app/components/design-kit/landing-page.html',
				data: {
					title: 'Landing page',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.dashboards', {
				url: '/dashboards',
				templateUrl: './app/components/design-kit/dashboards.html',
				data: {
					title: 'Dashboards',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.list-view', {
				url: '/list-view',
				templateUrl: './app/components/design-kit/list-view.html',
				data: {
					title: 'List view',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.components', {
				url: '/components',
				templateUrl: './app/components/design-kit/components.html',
				data: {
					title: 'Components',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.grid-view', {
				url: '/grid-view',
				templateUrl: './app/components/design-kit/grid-view.html',
				data: {
					title: 'Grid view',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.forms-view', {
				url: '/forms-view',
				templateUrl: './app/components/design-kit/forms-view.html',
				data: {
					title: 'Forms view',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.buttons-view', {
				url: '/buttons-view',
				templateUrl: './app/components/design-kit/buttons-view.html',
				data: {
					title: 'Buttons view',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.radio-buttons', {
				url: '/radio-buttons',
				templateUrl: './app/components/design-kit/radio-buttons.html',
				data: {
					title: 'Radio buttons',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.tabs', {
				url: '/tabs',
				templateUrl: './app/components/design-kit/tabs.html',
				data: {
					title: 'Tabs',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.loading-icons', {
				url: '/loading-icons',
				templateUrl: './app/components/design-kit/loading-icons.html',
				data: {
					title: 'Loading icons',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.breadcrumbs', {
				url: '/breadcrumbs',
				templateUrl: './app/components/design-kit/breadcrumbs.html',
				data: {
					title: 'Breadcrumbs',
					contentClasses: 'no-padding'
				}
			})
			.state('app.kit.date-picker', {
				url: '/date-picker',
				templateUrl: './app/components/design-kit/date-picker.html',
				data: {
					title: 'Date picker',
					contentClasses: 'no-padding'
				}
			});


		}])
		.config(['$ocLazyLoadProvider', function ($ocLazyLoadProvider) {
			$ocLazyLoadProvider.config({
				debug: false,
				events: false
			});
		}]);
})();
