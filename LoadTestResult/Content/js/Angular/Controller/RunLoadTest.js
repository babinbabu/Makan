app.controller("RunLoadTest", function ($scope, $http) {
    GetLoadTestNameList();
    function GetLoadTestNameList() {
        $.blockUI({ message: null });
        $http.get("/RunLoadTest/GetLoadTestNames", {cache: false})
           .then(function (response) {
               $scope.LoadTestNames = response.data;
               $.unblockUI();
           });
    }

    $scope.submit = function () {
        ExecuteLoadTest();
    }
    function ExecuteLoadTest() {
        $.blockUI({ message: $('#domMessage') });
        $http.post("/RunLoadTest/ExecuteLoadTest", { "loadTestPath": $scope.CurrentLoadTestName, cache: false })
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

window.onbeforeunload = function (e) {
    e = e || window.event;

    // For IE and Firefox prior to version 4
    if (e) {
        e.returnValue = 'Any string';
    }

    // For Safari
    return 'Any string';
};