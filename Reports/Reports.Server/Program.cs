using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Reports.DAL.Entities;
using Reports.Server.Services;


namespace Reports.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CheckCorrectStorageAtLocalMemory();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void CheckCorrectStorageAtLocalMemory()
        {
            IReportService _reportService = new ReportService();
            TaskService taskService = _reportService.GetTaskService();
            EmployeeService employeeService = _reportService.GetEmployeeService();
            TeamLead firstTeamLead = employeeService.CreateTeamLead("First Team Lead");
            Employee firstEmployee = employeeService.CreateEmployeeForTeamLead(firstTeamLead, "FirstEmployee");
            Employee secondEmployee = employeeService.CreateEmployeeForTeamLead(firstTeamLead, "SecondEmployee");
            DAL.Entities.Task firstTask = taskService.CreateTask(firstTeamLead);
            DAL.Entities.Task secondTask = taskService.CreateTask(firstEmployee);
            DAL.Entities.Task thirdTask = taskService.CreateTask(secondEmployee);
            firstEmployee.SaveNewReportForSomeDays(7);
            firstEmployee.CloseReport();
            secondEmployee.SaveNewReportForSomeDays(7);
            secondEmployee.CloseReport();
            firstEmployee.SaveNewReportForSomeDays(1);
            firstEmployee.CloseReport();
            secondEmployee.SaveNewReportForSomeDays(1);
            firstEmployee.SaveNewReportForSomeDays(7);
            firstEmployee.CloseReport();
            var repository = new Repository();
            repository.SerializeEmployeeService("C:/Users/Иван/Desktop/out18.json", employeeService);
            EmployeeService newEmployeeService =
                repository.DeserializeEmployeeService("C:/Users/Иван/Desktop/out18.json");
            Assert.AreEqual(employeeService.TeamLeads.Count, newEmployeeService.TeamLeads.Count);
            Assert.AreEqual(employeeService.FindByName("First Team Lead").Id,
                newEmployeeService.FindByName("First Team Lead").Id);
        }
    }
}