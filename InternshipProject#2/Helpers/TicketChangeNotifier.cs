using Microsoft.EntityFrameworkCore;

using RequestResponseModels.History.Response;

using InternshipProject_2.Helpers;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;

namespace InternshipProject_2.BackgroundServices;

public class TicketChangeNotifier : BackgroundService
{
    readonly IServiceProvider _ServiceProvider;
    const short minutes = -5;

    public TicketChangeNotifier(IServiceProvider serviceProvider)
    {
        _ServiceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _ServiceProvider.CreateScope())
            {
                IHistoryManager historyManager = scope.ServiceProvider.GetRequiredService<IHistoryManager>();
                IEmailService emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                Project2Context dbContext = scope.ServiceProvider.GetRequiredService<Project2Context>();

                DateTime startTime = DateTime.UtcNow.AddMinutes(minutes);
                DateTime endTime = DateTime.UtcNow;

                List<AddHistoryRecordResponse> historyRecords = await historyManager.GetHistoryInTimeRange(startTime, endTime);
                IEnumerable<IGrouping<int, AddHistoryRecordResponse>> groupedHistory = historyRecords.GroupBy(hr => hr.TicketId);
                foreach (var group in groupedHistory)
                {
                    int ticketId = group.Key;
                    List<AddHistoryRecordResponse> historyRecordsForTicket = group.ToList();
                    string emailBody = GenerateEmailContent(historyRecords);
                    List<Watcher> watchers = await dbContext.Watchers
                         .Where(watcher => watcher.TicketId == ticketId).ToListAsync();

                    List<int?> watcherUserIds = watchers.Select(watcher => watcher.UserId).ToList();

                    List<string> watcherEmails = await dbContext.Users
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
        IEnumerable<string> formattedHistory = historyRecords.Select(record => $"{record.CreatedAt}: {record.Body}");

        string emailContent = string.Join(Environment.NewLine, formattedHistory);

        return emailContent;
    }

}