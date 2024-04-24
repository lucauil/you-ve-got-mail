using System;
using UnityEngine;
using Unity.Notifications.Android;

public class NotificationHandler : MonoBehaviour
{
    private const string LAST_NOTIFICATION_TIME_KEY = "last_notification_time";
    
    private void Start()
    {
        Application.targetFrameRate = 120;

        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Notifications Channel",
            Importance = Importance.Default,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        
        if (PlayerPrefs.HasKey(LAST_NOTIFICATION_TIME_KEY))
        {
            DateTime lastNotificationTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString(LAST_NOTIFICATION_TIME_KEY)));
            DateTime now = DateTime.Now;

            if (now - lastNotificationTime < TimeSpan.FromMinutes(420))
            {
                return;
            }

            else
            {
                AndroidNotificationCenter.CancelAllNotifications();
            }
        }

        var notification = new AndroidNotification();
        notification.Title = "Check je mail!";
        notification.Text = "Het is alweer 7 uur geleden dat jij je mail gecheckt hebt!";
        notification.FireTime = System.DateTime.Now.AddMinutes(420);
        AndroidNotificationCenter.SendNotification(notification, "channel_id");

        PlayerPrefs.SetString(LAST_NOTIFICATION_TIME_KEY, DateTime.Now.ToBinary().ToString());
    }
}
