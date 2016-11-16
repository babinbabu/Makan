var app = angular.module("LoadTestResult", ["ngRoute", "checklist-model", 'ui.bootstrap']);
app.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.when("/", {
          templateUrl: "/Template/LoadTest/RunLoadTest.html",
          controller: "RunLoadTest",
          activetab: 'runLoadTest'
      })
     
      
      $routeProvider.when("/LoadTestResult", {
          templateUrl: "/Template/LoadTest/LoadTestResult.html",
          controller: "LoadTestList",
          activetab: 'loadlist'

      })

      $routeProvider.when("/RunLoadTest", {
          templateUrl: "/Template/LoadTest/RunLoadTest.html",
          controller: "RunLoadTest",
          activetab: 'runLoadTest'

      })

      $routeProvider.when("/LoadTest/DetailedResult/:Id", {
          templateUrl: "/Template/LoadTest/DetailedResult.html",
          controller: "DetailedResult",
          activetab: 'loadlist'

      })
      .when('/LoadTest/ErrorDetails/LoadTestRunId/:Id/SubType/:subType', {
          templateUrl: '/Template/LoadTest/ErrorDetails.html',
          controller: "ErrorDetails"
      })
      .when('/LoadTest/GraphRepesentation/:Id', {
          templateUrl: '/Template/LoadTest/GraphRepesentation.html',
          controller: "GraphRepesentation"
      })
      
      $routeProvider.when("/ChangePassword", {
          templateUrl: "/Template/LoadTest/ChangePassword.html",
          controller: 'ChangePassword'
      });
  }]).run(function ($rootScope, $route) {
      $rootScope.$route = $route;
  });
//app.filter("mydate", function () {
//    var re = /\/Date\(([0-9]*)\)\//;
//    return function (x) {
//        var m = x.match(re);
//        if (m) return new Date(parseInt(m[1]));
//        else return null;
//    };
//});
app.filter("mydate", function () {
    return function (item) {
        if (item != null) {
            return new Date(parseInt(item.substr(6)));
        }
        return "";
    };
});
app.filter('secondsToTime', function () {

    function padTime(t) {
        return t < 10 ? "0" + t : t;
    }

    return function (_seconds) {
        if (typeof _seconds !== "number" || _seconds < 0)
            return "00:00:00";

        var hours = Math.floor(_seconds / 3600),
            minutes = Math.floor((_seconds % 3600) / 60),
            seconds = Math.floor(_seconds % 60);

        return padTime(hours) + ":" + padTime(minutes) + ":" + padTime(seconds);
    };
});
app.filter('setDecimal', function ($filter) {
    return function (input, places) {
        if (isNaN(input)) return input;
        // If we want 1 decimal place, we want to mult/div by 10
        // If we want 2 decimal places, we want to mult/div by 100, etc
        // So use the following to create that factor
        var factor = "1" + Array(+(places > 0 && places + 1)).join("0");
        return Math.round(input * factor) / factor;
    };
});
app.constant('Constants', {
    MessageType: [
                { Id: 0, FullName: 'Test Error' },
                { Id: 1, FullName: 'Exception' },
                { Id: 2, FullName: 'Http Error' },
                { Id: 3, FullName: 'ValidationRule Error'},
                { Id: 4, FullName: 'ExtractionRule Error' },
                { Id: 5, FullName: 'Timeout' }
    ]
});

app.filter("MessageTypeFilter", function (Constants, $filter) {
    return function (item) {
        if (item != null) {
            return $filter('filter')(Constants.MessageType, { Id: item })[0].FullName;
        }
        return "";
    };
});
app.filter("ControllerFilter", function ($filter) {
    
    return function (item) {
        if (item == true) {
            return "Local Run";
        }
        return "Remote Run";
    };
});
app.filter('capitalize', function ($filter) {
    return function (input) {
        if (input != null) {
            input = input.toLowerCase();
            return input.substring(0, 1).toUpperCase() + input.substring(1);
        }
    }
});

