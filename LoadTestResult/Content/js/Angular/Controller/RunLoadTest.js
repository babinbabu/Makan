app.controller("RunLoadTest", function ($scope, $http) {
    GetLoadTestNameList();
    function GetLoadTestNameList() {
        $.blockUI({ message: null });
        $http.get("/RunLoadTest/GetLoadTestNames")
           .then(function (response) {
               $scope.LoadTestNames = response.data;
               $.unblockUI();
           });
    }

    $scope.submit = function () {
        console.log($scope.CurrentLoadTestName);
        ExecuteLoadTest();
    }
    function ExecuteLoadTest() {
        $.blockUI({ message: $('#domMessage') });
        $http.post("/RunLoadTest/ExecuteLoadTest", { "loadTestPath": $scope.CurrentLoadTestName })
           .then(function (response) {
               $.unblockUI();
               if (response.data == 'True') {
                   $.growl.notice({ message: "Successfully Executed the Load Test!" });
               }
               else {
                   $.growl.error({ message: "An Error occurred during Load Test!" });
               }
           });
    }
});