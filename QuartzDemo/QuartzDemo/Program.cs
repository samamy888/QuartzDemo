using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using QuartzDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IJobFactory, JobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddSingleton<ReportJob>();

//向DI容器註冊JobSchedule
builder.Services.AddSingleton(new JobSchedule(jobName: "111", jobType: typeof(ReportJob), cronExpression: "0/30 * * * * ?"));
builder.Services.AddSingleton(new JobSchedule(jobName: "222", jobType: typeof(ReportJob), cronExpression: "0/52 * * * * ?"));

//向DI容器註冊Host服務
builder.Services.AddSingleton<QuartzHostedService>();
builder.Services.AddHostedService(provider => provider.GetService<QuartzHostedService>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
