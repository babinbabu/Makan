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
    
    public partial class LoadTestPerformanceCounterSample
    {
        public int LoadTestRunId { get; set; }
        public int TestRunIntervalId { get; set; }
        public int InstanceId { get; set; }
        public Nullable<float> ComputedValue { get; set; }
        public long RawValue { get; set; }
        public long BaseValue { get; set; }
        public long CounterFrequency { get; set; }
        public long SystemFrequency { get; set; }
        public long SampleTimeStamp { get; set; }
        public long SampleTimeStamp100nSec { get; set; }
        public int CounterType { get; set; }
        public byte ThresholdRuleResult { get; set; }
        public Nullable<int> ThresholdRuleMessageId { get; set; }
    
        public virtual LoadTestRun LoadTestRun { get; set; }
    }
}
