using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.Android;


public class AndroidNotifications : MonoBehaviour
{
    public void RequestAuthorization()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }

    public void RegisterNotificationChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "default_channel",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Nieuwe opdrachten"
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendNotification(string title, string text, int fireTimeinHours)
    {
        // Create a custom notification content
        var notification = new AndroidNotification();
        
            notification.Title = title;
            notification.Text = text;
            notification.SmallIcon = "icon_o";
            notification.FireTime = System.DateTime.Now.AddHours(fireTimeinHours);
        

        
        AndroidNotificationCenter.SendNotification(notification, "default_channel");
    }
}
