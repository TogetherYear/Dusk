using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TTouchLayer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{

    private Vector2 delta = new Vector2(0.0f, 0.0f);

    private Vector2 direction = new Vector2(0.0f, 0.0f);

    public Vector2 Direction { get { return this.Direction; } }

    public void OnBeginDrag(PointerEventData e)
    {

    }

    public void OnDrag(PointerEventData e)
    {

    }

    public void OnEndDrag(PointerEventData e)
    {
        this.ResetValues();
    }

    public void OnPointerDown(PointerEventData e)
    {

    }

    public void OnPointerUp(PointerEventData e)
    {
        this.ResetValues();
    }

    private void ResetValues()
    {
        this.delta = Vector2.zero;
        this.direction = Vector2.zero;
    }
}
