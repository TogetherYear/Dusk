using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private RectTransform jt;

    [SerializeField]
    private RectTransform mt;

    [SerializeField]
    private float maxOffset = 120.0f;

    private Vector2 originMPosition;

    private Vector2 originJSize;

    private Vector2 centerPosition;

    private void Start()
    {
        this.originMPosition = jt.anchoredPosition;
        this.originJSize = jt.rect.size;
        this.centerPosition = this.originMPosition + this.originJSize * 0.5f;
    }

    public void OnBeginDrag(PointerEventData e)
    {
        this.SetMPosition(e.position);
    }

    public void OnDrag(PointerEventData e)
    {
        this.SetMPosition(e.position);
    }

    public void OnEndDrag(PointerEventData e)
    {
        this.ResetMPosition();
    }

    public void OnPointerDown(PointerEventData e)
    {
        this.SetMPosition(e.position);
    }

    public void OnPointerUp(PointerEventData e)
    {
        this.ResetMPosition();
    }

    private void SetMPosition(Vector2 inputPosition)
    {
        TInputManager.Instance.delta = inputPosition - this.centerPosition;
        TInputManager.Instance.direction = TInputManager.Instance.delta.normalized;
        float distance = Vector2.Distance(inputPosition, this.centerPosition);
        Vector2 result = TInputManager.Instance.direction * Mathf.Min(distance, this.maxOffset);
        this.mt.anchoredPosition = result;
    }

    private void ResetMPosition()
    {
        mt.anchoredPosition = Vector2.zero;
        TInputManager.Instance.delta = Vector2.zero;
        TInputManager.Instance.direction = Vector2.zero;
    }
}
