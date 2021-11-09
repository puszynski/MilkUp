using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace MilkUp.Services
{
    // source => https://youtu.be/BB4lK2kfKf0?t=681 
    public class ApplicationStateService
    {
        public List<NotificationViewModel> Notifications { get; private set; } = new List<NotificationViewModel>();

        public void AddNotification(ComponentBase componentBase, NotificationViewModel notification)
        {
            Notifications = new List<NotificationViewModel>(); //y sure to remove previous Notifications?

            Notifications.Add(notification);
            NotifyStateChanged(componentBase, "Notifications");
        }

        public void CloseNotification(ComponentBase componentBase, NotificationViewModel notification)
        {
            Notifications.Remove(notification);
            NotifyStateChanged(componentBase, "NotificationClosed");
        }


        public event Action<ComponentBase, string> StateChanged;
        private void NotifyStateChanged(ComponentBase componentBase, string property) => StateChanged?.Invoke(componentBase, property);
    }

    public class NotificationViewModel
    {
        public string Massage { get; set; }
        public EMessageType MessageType { get; set; }
    }

    public enum EMessageType
    {
        Success = 1,
        Info = 2,
        Error = 3
    }


}
