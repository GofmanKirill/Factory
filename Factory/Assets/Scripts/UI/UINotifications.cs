
using System.Collections.Generic;
using UnityEngine;

public class UINotifications : MonoBehaviour
{
    [SerializeField] private GameObject _notification;



    public void CreateNotification(Color color, string message,List<UINotification> notifications)
    {
        GameObject not = Instantiate(_notification, transform);

        not.GetComponent<UINotification>().Initialize(color, message);

        notifications.Add(not.GetComponent<UINotification>());
    }
}
