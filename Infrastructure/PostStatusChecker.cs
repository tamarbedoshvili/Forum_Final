using Final.database;
using Final.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class PostStatusChecker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly TimeSpan _checkInterval = TimeSpan.FromHours(1);

    public PostStatusChecker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CheckPostStatus();
            await Task.Delay(_checkInterval, stoppingToken);
        }
    }

    private async Task CheckPostStatus()
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var xDays = 30;
            var now = DateTime.Now;

            var postsToUpdate = await context.Posts
                .Where(p => p.Status != EStatus.Inactive)
                .Include(p => p.Comments)
                .ToListAsync();

            postsToUpdate = postsToUpdate
                .Where(x => x.Comments.Count > 0)
                .Where(p => p.Comments.All(x => (now - x.CreateDate).TotalDays > xDays))
                .ToList();

            foreach (var post in postsToUpdate)
            {
                post.Status = EStatus.Inactive;
            }

            await context.SaveChangesAsync();
        }
    }
}