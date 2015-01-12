angular.module('antix.angularjs.cell-layout', [
        'antix.cellLayout'
    ])
    .controller('cellLayoutDemo', [
        '$scope',
        function($scope) {

            var cells = [],
                random = function(min, max) {
                    return Math.round((Math.random() * (max - min)) + min);
                };

            for (var i = 0; i < 20; i++) {

                cells.push({
                    title: HolderIpsum.words(random(2, 3), true),
                    content: HolderIpsum.paragraphs(random(1, 3), true)
                });
            }

            $scope.cells = cells;
        }
    ]);