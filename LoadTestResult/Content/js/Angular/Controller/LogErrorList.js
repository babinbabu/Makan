app.controller("LogErrorList", function ($scope, $http) {
    $scope.pagingInfo = {
        page: 1,
        itemsPerPage: 20,
        totalItems: 0
    };
    $scope.submit = function () {

        if ($scope.logdate == null || $scope.logdate == undefined || $scope.logdate == '') {

            $.growl.error({ message: "Please select a date " });
        }
        else {

            GetLogErrorDetails();
        }
    }
    $scope.selectPage = function (page) {
        $scope.pagingInfo.page = page;
        GetLogErrorDetails();
    };
    function GetLogErrorDetails() {
        $.blockUI({ message: null });
        $http.post("/LogError/LogErrorDateWise", { "logErrorDate": $scope.logdate, "fromTime": $("#fromtime").val(), "toTime": $("#totime").val(), }, { params: $scope.pagingInfo })
           .then(function success(response) {
               if (response.data) {
                   $(".test_result").show();
                   $scope.LogErrorList = response.data;
                   $scope.pagingInfo.totalItems = response.data.TotalRecord;
               }
               else {
                   $.growl.warning({ message: "No error log on " + $scope.logdate });
               }
               $.unblockUI();
           }, function error(response) {
               // this function will be called when the request returned error status
               $.unblockUI();
               $.growl.error({ message: "An Error occurred" });
           });
    }
    $scope.DetailedError = function (LogError) {
        $.blockUI({ message: null });
        $http.post("/LogError/GetDetailedLogError", { "logErrorDate": $scope.logdate, "rowKey": LogError.RowKey })
           .then(function success(response) {
               // this function will be called when the request is success
               $scope.DetailedLogError = response.data;
               $.unblockUI();
           }, function error(response) {
               // this function will be called when the request returned error status
               $.unblockUI();
               $.growl.error({ message: "An Error occurred" });
           });
    }
});
