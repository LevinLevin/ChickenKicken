using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class EiNote : MonoBehaviour
{
    void Start()
    {
        //bisherige nachrichten löschen
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

        //setup für den android notification channel bei dem die nachrichten durch kommen
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        //erstellt die Nachricht
        var notification = new AndroidNotification();
        notification.Title = "Your chicken lays eggs!!";
        notification.Text = "or something like that..";
        notification.FireTime = System.DateTime.Now.AddHours(22);

        //Sendet die ausgewählte notifications an die angegebene channel id
        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        //wenn die nachricht abgesendet wird, werden nachfolgende nachrichten gelöscht
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllDisplayedNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
