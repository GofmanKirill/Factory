
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UIBuildIdentifier
{
    [SerializeField] private Color _colorBuild;
    [SerializeField] private UINotifications _uINotifications;
    private List<UINotification> _notifications=new List<UINotification>();



    public void WarningNoResources()
    {
        _uINotifications.CreateNotification(_colorBuild, "No Resources", _notifications);
    }



    public void WarningWarehouseFull()
    {
        _uINotifications.CreateNotification(_colorBuild, "Warehouse Full", _notifications);
    }



    public void ClearNotifications()
    {
        foreach (UINotification not in _notifications)
        {
            not.DestroyNotification();
        }

        _notifications.Clear();
    }
}
