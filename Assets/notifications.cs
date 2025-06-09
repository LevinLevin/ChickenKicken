using Unity.Notifications.Android;
using UnityEngine;

public class notifications : MonoBehaviour
{
    // Start is called before the first frame update
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
        notification.Title = PlayerPrefs.GetString("NameDesHuhn", "Chicken")+ " IS HUNGRY";
        notification.Text = "Feed it now";
        notification.FireTime = System.DateTime.Now.AddHours(20);

        //Sendet die ausgewählte notifications an die angegebene channel id
        var id =AndroidNotificationCenter.SendNotification(notification, "channel_id");

        //wenn die nachricht abgesendet wird, werden nachfolgende nachrichten gelöscht
        if(AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
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
