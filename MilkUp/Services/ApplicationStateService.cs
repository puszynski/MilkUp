using Microsoft.AspNetCore.Components;
using MilkUp.ViewModels.Shared;
using System;
using System.Collections.Generic;

namespace MilkUp.Services
{
    // source => https://docs.microsoft.com/pl-pl/aspnet/core/blazor/state-management?view=aspnetcore-6.0&pivots=server (koniec artykułu) "Usługa kontenera stanu w pamięci"
    public class ApplicationStateService
    {
        public List<NotificationViewModel> Notifications { get; private set; } = new List<NotificationViewModel>();

        public void AddNotification(NotificationViewModel notification)
        {
            //Notifications = new List<NotificationViewModel>(); //y sure to remove previous Notifications?
            Notifications.Add(notification);
            NotifyStateChanged();
        }

        public void CloseNotification(ComponentBase componentBase, NotificationViewModel notification)
        {
            Notifications.Remove(notification);
            NotifyStateChanged();
        }

        public event Action StateChanged;
        private void NotifyStateChanged() => StateChanged?.Invoke();
    }
}
