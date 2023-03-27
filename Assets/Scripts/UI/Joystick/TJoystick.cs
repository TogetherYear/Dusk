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

    private Vector2 delta = new Vector2(0.0f, 0.0f);

    private Vector2 direction = new Vector2(0.0f, 0.0f);

    public Vector2 Direction { get { return this.Direction; } }

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
        this.delta = inputPosition - this.centerPosition;
        this.direction = this.delta.normalized;
        float distance = Vector2.Distance(inputPosition, this.centerPosition);
        Vector2 result = this.direction * Mathf.Min(distance, this.maxOffset);
        this.mt.anchoredPosition = result;
    }

    private void ResetMPosition()
    {
        mt.anchoredPosition = Vector2.zero;
        this.delta = Vector2.zero;
        this.direction = Vector2.zero;
    }
}