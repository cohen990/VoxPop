    $scope.scrollPos = {}; // scroll position of each view

$(window).on('scroll', function() {
    if ($scope.okSaveScroll) { // false between $routeChangeStart and $routeChangeSuccess
        $scope.scrollPos[$location.path()] = $(window).scrollTop();
        //console.log($scope.scrollPos);
    }
});

$scope.scrollClear = function(path) {
    $scope.scrollPos[path] = 0;
}

$scope.$on('$routeChangeStart', function() {
    $scope.okSaveScroll = false;
});

$scope.$on('$routeChangeSuccess', function() {
    $timeout(function() { // wait for DOM, then restore scroll position
        $(window).scrollTop($scope.scrollPos[$location.path()] ? $scope.scrollPos[$location.path()] : 0);
        $scope.okSaveScroll = true;
    }, 0);
});

