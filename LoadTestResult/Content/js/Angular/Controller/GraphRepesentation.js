app.controller("GraphRepesentation",['$scope', '$http','$routeParams', function ($scope, $http,$routeParams) {
    GetControllerandAgents();
    GetKeyIndicators();
    GetPageResponseTime();
    GetGraph();
    function GetControllerandAgents() {
        $.blockUI({ message: null });
        $http.get("/LoadTestGraph/GetControllerandAgents?loadTestRunId=" + $routeParams.Id)
           .then(function (response) {
               $scope.ControllerandAgents = response.data;
               $.unblockUI();
           });
    }
    function GetKeyIndicators() {
        $.blockUI({ message: null });
        $http.get("/LoadTestGraph/GetKeyIndicators?loadTestRunId=" + $routeParams.Id)
           .then(function (response) {
               $scope.KeyIndicators = response.data;
               $.unblockUI();
           });
    }
    function GetPageResponseTime() {
        $.blockUI({ message: null });
        $http.get("/LoadTestGraph/GetPageResponseTime?loadTestRunId=" + $routeParams.Id)
           .then(function (response) {
               $scope.PageResponseTimes = response.data;
               $.unblockUI();
           });
    }
    function GetGraph() {
        $.blockUI({ message: null });
        $http.get("/LoadTestResult/GetLoadTestPageGraphValues?loadTestRunId=" + $routeParams.Id)
           .then(function (response) {
               var GraphData = [];               var XandYPoints = [];               for (var i = 0; i < response.data.length; ++i) {
                   
                   for (var j = 0; j < response.data[i].Axis.length; ++j) {
                       XandYPoints.push({ y: response.data[i].Axis[j].YAxis })
                   }
                   GraphData.push({ type: "line", name: response.data[i].PageName, showInLegend: true, dataPoints: XandYPoints});
                   XandYPoints = [];
               }
               DrawGraph(GraphData);
           });
    }
    function DrawGraph(GraphData) {
  
        var chart = new CanvasJS.Chart("chartContainer",
        {
            title: {
                text: "Page Response Time Graph"
            },
            axisX: {
                title: "Time",
                interval: 5
            },
            axisY: {
                title: "Response Time (Sec)"
            },
            legend: {
                cursor: "pointer",
                itemclick: function (e) {
                    //console.log("legend click: " + e.dataPointIndex);
                    //console.log(e);
                    if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                        e.dataSeries.visible = false;
                    } else {
                        e.dataSeries.visible = true;
                    }

                    e.chart.render();
                }
            },
            toolTip: {
                content: "{name} : {y}"
            },
            data: GraphData
        });

        chart.render();
    }
}]);