﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoadTestResult
{
    public enum ControllerandAgents:byte
    {
        [Description("Available MBytes")]
        AvailableMBytes=0,
        [Description("%Processor Time")]
        ProcessorTime=48,
    }
}