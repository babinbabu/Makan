using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoadTestResult
{
    public enum ControllerandAgents : byte
    {
        [Description("Available MBytes")]
        AvailableMBytes = 0,
        [Description("%Processor Time")]
        ProcessorTime = 48,
    }
    public enum OverallThresholdRuleResult : byte
    {
        critical = 2,
        warnings = 1,
        ok = 0
    }
    public enum LoadTestRunOutcome : byte
    {
        InProgress=1,
        Completed=2,
        Aborted=3,
        Error=4
    }
}