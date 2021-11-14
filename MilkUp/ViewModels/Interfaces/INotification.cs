using MilkUp.ViewModels.Shared;
using System.Collections.Generic;

namespace MilkUp.ViewModels.Interfaces
{
    public interface INotification
    {
        List<NotificationViewModel> Notifications { get; set; }
    }
}
