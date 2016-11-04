app.controller("DetailedResult",['$scope', '$http','$routeParams', function ($scope, $http,$routeParams) {
    getDetailedResult();
    function getDetailedResult() {
        $.blockUI({ message: null });
        $http.get("/LoadTestResult/GetLoadTestBasedOnId?LoadTestRunId=" + $routeParams.Id)
           .then(function (response) {
               $scope.loadTestResult = response.data;
               $.unblockUI();
           });
    }
}]);
