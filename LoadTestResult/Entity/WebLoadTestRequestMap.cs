//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoadTestResult.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class WebLoadTestRequestMap
    {
        public int LoadTestRunId { get; set; }
        public int RequestId { get; set; }
        public int TestCaseId { get; set; }
        public string RequestUri { get; set; }
        public Nullable<double> ResponseTimeGoal { get; set; }
    
        public virtual LoadTestRun LoadTestRun { get; set; }
    }
}
