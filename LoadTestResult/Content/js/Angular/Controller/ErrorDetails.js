app.controller("ErrorDetails", ['$scope', '$http', '$routeParams', function ($scope, $http, $routeParams) {
    getErrorDetails();
    function getErrorDetails() {
        $http.get("/LoadTestResult/DetailedErrorMessage?LoadTestRunId=" + $routeParams.Id + "&SubType=" + $routeParams.subType)
           .then(function (response) {
               $scope.LoadTestDetailedErrors = response.data;
           });
    }
}]);