using Grpc.Core;
using SmartWatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SmartWatch.WatchService;

namespace server
{
    public class WatchServiceImpl : WatchServiceBase
    {
        public override Task<CalorieResponse> Calories(CalorieRequest request, ServerCallContext context)
        {
            int sum_result = request.FirstNumber +
                             request.SecondNumber +
                             request.ThirdNumber +
                             request.FourthNumber +
                             request.FifthNumber;
            return Task.FromResult(new CalorieResponse() { SumResult = sum_result });
        }

        public override async Task<SleepAverageResponse> SleepAverage(IAsyncStreamReader<SleepAverageRequest> requestStream, ServerCallContext context)
        {
            int total = 0;
            double average = 0.0;

            while (await requestStream.MoveNext())
            {
                average += requestStream.Current.Number;
                total++;
            }

            return new SleepAverageResponse() { Average = average / total };
        }

        public override async Task MaxHeartRate(IAsyncStreamReader<MaxHeartRateRequest> requestStream, IServerStreamWriter<MaxHeartRateResponse> responseStream, ServerCallContext context)
        {
            int? max = null;

            while (await requestStream.MoveNext())
            {
                if (max == null || max < requestStream.Current.Number)
                {
                    max = requestStream.Current.Number;
                    await responseStream.WriteAsync(new MaxHeartRateResponse() { Maximum = max.Value });
                }
            }
        }


    }
}
