angular.module('antix.cellLayout', [])
    .controller('cellLayoutController',
    [
        '$log', '$scope', '$window', '$element',
        function ($log, $scope, $window, $element) {
            var columns,
                cellElements = [];

            var positionElement = function (cellElement) {
                $log.debug('cellLayoutContainer.positionElement()');

                cellElement.css({ marginTop: 0, marginBottom: 0 });

                var left = cellElement[0].offsetLeft,
                    top = cellElement[0].offsetTop,
                    height = cellElement[0].offsetHeight;

                var column = '_' + left;
                if (!columns[column]) columns[column] = 0;

                cellElement.css({ marginTop: (columns[column] - top) + 'px' });

                columns[column] += height;
            }

            this.addElement = function (cellElement) {
                $log.debug('cellLayoutContainer.addElement()');

                cellElements.push(cellElement);
            };

            this.removeElement = function (cellElement) {
                $log.debug('cellLayoutContainer.removeElement()');

                var index = cellElements.indexOf(cellElement);
                cellElements.splice(index, 1);
            };

            var resize = this.resize = function () {
                $log.debug('cellLayoutContainer.resize(' + cellElements.length + ')');

                columns = {};
                $scope.$evalAsync(function () {
                    angular.forEach(cellElements, positionElement);
                });
            },
                getSize = function () {
                    var size = $element[0].offsetWidth;
                    angular.forEach(cellElements, function (cellElement) {
                        size += cellElement[0].offsetHeight;
                    });
                    return size;
                };

            $scope.$watch(getSize, resize);
            angular.element($window).on('resize', function () { $scope.$apply(); });
        }
    ])
    .directive('cellLayoutContainer', [
        '$log',
        function ($log) {

            $log.debug('cellLayoutContainer.init()');

            return {
                restrict: 'AE',
                link: function ($scope, element) {
                    $log.debug('cellLayoutContainer.link()');
                    element.addClass('cell-layout-container');
                },
                controller: 'cellLayoutController'
            };
        }
    ])
    .directive('cellLayoutCell', [
        function () {

            return {
                restrict: 'AE',
                require: '^cellLayoutContainer',
                link: function (
                    $scope,
                    element,
                    attributes,
                    cellsContainer) {
                    element.addClass('cell-layout-cell');

                    cellsContainer.addElement(element);

                    $scope.$on('$destroy', function () {
                        cellsContainer.removeElement(element);
                    });
                }
            };
        }
    ]);