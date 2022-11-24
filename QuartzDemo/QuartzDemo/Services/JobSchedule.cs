using System.ComponentModel;

namespace QuartzDemo.Services
{
    public class JobSchedule
    {
        public JobSchedule(Type jobType, string cronExpression, string jobName)
        {
            JobType = jobType ?? throw new ArgumentNullException(nameof(jobType));
            CronExpression = cronExpression ?? throw new ArgumentNullException(nameof(cronExpression));
            JobName = jobName ?? throw new ArgumentNullException(nameof(jobName));
        }

        /// <summary>
        /// Job識別名稱
        /// </summary>
        public string JobName { get; private set; }

        /// <summary>
        /// Job型別
        /// </summary>
        public Type JobType { get; private set; }

        /// <summary>
        /// Cron表示式
        /// </summary>
        public string CronExpression { get; private set; }

        /// <summary>
        /// Job狀態
        /// </summary>
        public JobStatus JobStatus { get; set; } = JobStatus.Init;
    }


    public enum JobStatus : byte
    {
        [Description("初始化")]
        Init = 0,
        [Description("已排程")]
        Scheduled = 1,
        [Description("執行中")]
        Running = 2,
        [Description("已停止")]
        Stopped = 3,
    }
}
