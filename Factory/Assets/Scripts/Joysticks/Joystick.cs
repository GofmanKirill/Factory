
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float Horizontal { get { return _input.x; } }
    public float Vertical { get { return _input.y; } }
    public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }

    public float HandleRange
    {
        get { return handleRange; }
        set { handleRange = Mathf.Abs(value); }
    }

    public float DeadZone
    {
        get { return deadZone; }
        set { deadZone = Mathf.Abs(value); }
    }


    [SerializeField] private float handleRange = 1;
    [SerializeField] private float deadZone = 0;

    [SerializeField] protected RectTransform background = null;
    [SerializeField] private RectTransform handle = null;


    private RectTransform _baseRect = null;

    private Canvas _canvas;
    private Camera _camera;

    private Vector2 _input = Vector2.zero;

    protected virtual void Start()
    {
        HandleRange = handleRange;
        DeadZone = deadZone;
        _baseRect = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        if (_canvas == null)
            Debug.LogError("The Joystick is not placed inside a canvas");

        Vector2 center = new Vector2(0.5f, 0.5f);
        background.pivot = center;
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }


    public void OnDrag(PointerEventData eventData)
    {
        _camera = null;
        if (_canvas.renderMode == RenderMode.ScreenSpaceCamera)
            _camera = _canvas.worldCamera;

        Vector2 position = RectTransformUtility.WorldToScreenPoint(_camera, background.position);
        Vector2 radius = background.sizeDelta / 2;
        _input = (eventData.position - position) / (radius * _canvas.scaleFactor);
        HandleInput(_input.magnitude, _input.normalized, radius, _camera);
        handle.anchoredPosition = _input * radius * handleRange;
    }


    protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
                _input = normalised;
        }
        else
            _input = Vector2.zero;
    }



    public virtual void OnPointerUp(PointerEventData eventData)
    {
        _input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }


    protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    {
        Vector2 localPoint = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_baseRect, screenPosition, _camera, out localPoint))
        {
            Vector2 pivotOffset = _baseRect.pivot * _baseRect.sizeDelta;
            return localPoint - (background.anchorMax * _baseRect.sizeDelta) + pivotOffset;
        }
        return Vector2.zero;
    }
}
