
using UnityEngine;
using UnityEngine.UI;

public class UINotification : MonoBehaviour
{
    [SerializeField] private Text _messageTxt;
    [SerializeField] private Image _colorImage;


    public void Initialize(Color color, string message)
    {
        _colorImage.color =color;
        _messageTxt.text = message;

        GetComponent<Animator>().Play("AnimatorNotificationEnable");
    }


    public void DestroyNotification()
    {
        GetComponent<Animator>().Play("AnimatorNotificationDisable");
        Destroy(gameObject,1);
    }
}
