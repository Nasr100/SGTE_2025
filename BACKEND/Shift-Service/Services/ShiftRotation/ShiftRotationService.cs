

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shift_Service.Data;
using Shift_Service.Enums;

namespace Shift_Service.Services.ShiftRotation
{
    public class ShiftRotationService : BackgroundService
    {
        private readonly ILogger<ShiftRotationService> _logger;

        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval = TimeSpan.FromHours(1);

        public ShiftRotationService(ILogger<ShiftRotationService> logger,
       IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (DateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday &&
                   DateTime.UtcNow.Hour == 0)
                    {
                        using var scope = _serviceProvider.CreateScope();

                       var context = scope.ServiceProvider.GetRequiredService<ShiftServiceContext>();

                        await RotateAllGroups(context);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred during shift rotation");

                }

                await Task.Delay(_checkInterval, stoppingToken);
            }
        }

        private async Task RotateAllGroups(ShiftServiceContext context) 
        {
            var allGroups = await context.Groups
        .Include(g => g.Shift).Where(g => g.Shift.Role == Enums.Role.worker || g.Shift.Role == Enums.Role.driver)
        .ToListAsync();

            var allShifts = await context.Shifts.ToListAsync();

            foreach (var group in allGroups)
            {
                rotate(group, allShifts);
            }

            await context.SaveChangesAsync();
        }

        private void rotate(Models.Group grp, List<Models.Shift> allShifts)
        {
            var currentShift = allShifts.First(s => s.Id == grp.ShiftId);
            var nextShiftType = Next(currentShift.shift);

            var nextShift = allShifts.FirstOrDefault(s =>
                s.Role == currentShift.Role &&
                s.shift == nextShiftType);

            if (nextShift != null)
            {
                grp.ShiftId = nextShift.Id;
            }
        }

        private ShiftTypes Next(ShiftTypes current) => current switch
        {
            ShiftTypes.Matin => ShiftTypes.Soir,
            ShiftTypes.Soir => ShiftTypes.Nuit,
            ShiftTypes.Nuit => ShiftTypes.Matin,
            _ => throw new ArgumentOutOfRangeException(nameof(current))
        };
    }
}
