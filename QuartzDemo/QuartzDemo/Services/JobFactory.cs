﻿using Quartz;
using Quartz.Spi;

namespace QuartzDemo.Services
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;


        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }


        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobType = bundle.JobDetail.JobType;

            // 從 DI 容器取出指定 Job Type 實體
            return _serviceProvider.GetRequiredService(jobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}
