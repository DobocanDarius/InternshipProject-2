using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InternshipProject_2.Helpers;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.History.Response;

namespace InternshipProject_2.BackgroundServices
{
    public class TicketChangeNotifier : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public TicketChangeNotifier(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var historyManager = scope.ServiceProvider.GetRequiredService<IHistoryManager>();
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                    var dbContext = scope.ServiceProvider.GetRequiredService<Project2Context>();

                    var startTime = DateTime.UtcNow.AddMinutes(-5);
                    var endTime = DateTime.UtcNow;

                    var historyRecords = await historyManager.GetHistoryInTimeRange(startTime, endTime);
                    var groupedHistory = historyRecords.GroupBy(hr => hr.TicketId);
                    foreach (var group in groupedHistory)
                    {
                        var ticketId = group.Key;
                        var historyRecordsForTicket = group.ToList();
                        var emailBody = GenerateEmailContent(historyRecords);
                        var watchers = await dbContext.Watchers
                             .Where(watcher => watcher.TicketId == ticketId).ToListAsync();

                        var watcherUserIds = watchers.Select(watcher => watcher.UserId).ToList();

                        var watcherEmails = await dbContext.Users
                        .Where(user => watcherUserIds.Contains(user.Id))
                        .Select(user => user.Email)
                        .ToListAsync();

                        await emailService.SendEmailAsync(watcherEmails, "Ticket Update Notification", emailBody);

                    }
                }
                
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
        private string GenerateEmailContent(List<AddHistoryRecordResponse> historyRecords)
        {
            var formattedHistory = historyRecords.Select(record => $"{record.CreatedAt}: {record.Body}");

            var emailContent = string.Join(Environment.NewLine, formattedHistory);

            return emailContent;
        }

    }
}