
using UnityEngine;
using UnityEngine.EventSystems;

public class VariableJoystick : Joystick
{
    private Vector2 _fixedPosition = Vector2.zero;


    public void SetMode()
    {
        background.gameObject.SetActive(false);
    }


    protected override void Start()
    {
        base.Start();
        _fixedPosition = background.anchoredPosition;
        SetMode();
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);

        base.OnPointerDown(eventData);
    }


    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);

        base.OnPointerUp(eventData);
    }


    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        base.HandleInput(magnitude, normalised, radius, cam);
    }
}
