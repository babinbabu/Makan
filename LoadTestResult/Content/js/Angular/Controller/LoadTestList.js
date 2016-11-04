app.controller("LoadTestList", function ($scope, $http) {
    $scope.LoadTestList = {};
    $scope.pagingInfo = {
        page: 1,
        itemsPerPage: 20,
        totalItems: 0
    };
    $scope.selectPage = function (page) {
        $scope.pagingInfo.page = page;
        GetLoadTests();
    };
    GetLoadTestNameList();
    function GetLoadTestNameList() {
        $scope.users = null;
        $.blockUI({ message: null });
        $http.get("/LoadTestResult/List")
           .then(function (response) {
               $scope.LoadTestNames = response.data;
               $.unblockUI();
           });
    }
    $scope.change = function () {
        GetLoadTests();
    }

    $scope.Compare = function () {
        $scope.compareLoadTestIds = [];
        angular.forEach($scope.LoadTestList.data, function (LoadTest) {
            if (LoadTest.Compare) $scope.compareLoadTestIds.push(LoadTest.LoadTestRunId);
        });
        if ($scope.compareLoadTestIds.length > 3 || $scope.compareLoadTestIds.length <= 1) {
            $.growl.warning({ title: "Warning", message: "Please select minimum 2 or maximum 3 loadtests !" });
        }
        else {
            CompareLoadTests();
        }

    }
    function CompareLoadTests() {
        $.blockUI({ message: null });
        $http.get("/LoadTestResult/GetResultCompare", { params: { "LoadTestRunIds": $scope.compareLoadTestIds } })
           .then(function (response) {
               $(".test_result").hide();
               $(".compare_result").show();
               
               $scope.CompareLoadTestLists = response.data;
               $.unblockUI();
           });
    }
    function GetLoadTests() {
        $.blockUI({ message: null });
        $http.get("/LoadTestResult/GetLoadTestBasedOnName?loadTestName=" + $scope.CurrentLoadTestName, { params: $scope.pagingInfo })
        .then(function (response) {
            $(".compare_result").hide();
            $(".test_result").show();
            $scope.LoadTestList = response.data;
            $scope.pagingInfo.totalItems = response.data.TotalRecord;
            $.unblockUI();
        });
    }

});
