using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.Android;



public class NotificationControler : MonoBehaviour
{
    [SerializeField] AndroidNotifications androidNotifications;
    

    private void Start()
    {
        androidNotifications.RequestAuthorization();
        androidNotifications.RegisterNotificationChannel();
    }

    // Update is called once per frame
    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            androidNotifications.SendNotification("Nieuwe opdrachten", "Er zijn nieuwe opdrachten", 2);
        }
    }
}
