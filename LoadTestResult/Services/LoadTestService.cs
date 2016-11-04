using LoadTestResult.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadTestResult.Services
{
    public class LoadTestService
    {

        public static List<DetailedErrorModels> GetDetailedErrorMessage(int LoadTestRunId, string SubType)
        {
            List<DetailedErrorModels> Result = new List<DetailedErrorModels>();
            using (Entity.db_LoadTest2010Entities db = new Entity.db_LoadTest2010Entities())
            {
                var entityLoadTestMessages = db.LoadTestMessages.Where(x => x.LoadTestRunId == LoadTestRunId && x.SubType == SubType).OrderBy(x => x.MessageType).ToList();
                foreach (var entityLoadTestMessage in entityLoadTestMessages)
                {
                    Result.Add(new DetailedErrorModels(entityLoadTestMessage, db));
                }

            }
            return Result;
        }

        public static List<LoadTestResultList> GetLoadTestBasedOnName(string loadTestName, int take, int skip, out long Total)
        {
            List<LoadTestResultList> Results = new List<LoadTestResultList>();

            using (Entity.db_LoadTest2010Entities db = new Entity.db_LoadTest2010Entities())
            {
                var entityLoadTestRuns = db.LoadTestRuns.Where(e => e.LoadTestName == loadTestName);
                Total = entityLoadTestRuns.Count();
                entityLoadTestRuns = entityLoadTestRuns.OrderByDescending(x => x.EndTime);
                entityLoadTestRuns = entityLoadTestRuns.Skip(skip).Take(take);
                
                foreach (var loadTest in entityLoadTestRuns)
                {
                    Results.Add(new LoadTestResultList(loadTest));
                }

                return Results;

            }



        }
    }
}