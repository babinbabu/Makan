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
    
    public partial class LoadTestScenario
    {
        public int LoadTestRunId { get; set; }
        public int ScenarioId { get; set; }
        public string ScenarioName { get; set; }
    
        public virtual LoadTestRun LoadTestRun { get; set; }
    }
}
