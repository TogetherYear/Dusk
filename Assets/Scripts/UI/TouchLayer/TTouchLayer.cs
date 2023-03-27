using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TTouchLayer : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public UnityAction<Vector2> Drag;

    public bool isPress;

    public float pressTime;

    public void OnDrag(PointerEventData e)
    {
        this.Drag?.Invoke(e.delta);
    }

    public void OnPointerDown(PointerEventData e)
    {
        this.isPress = true;
        this.pressTime += Time.deltaTime;
    }

    public void OnPointerUp(PointerEventData e)
    {
        this.isPress = false;
        this.pressTime = 0.0f;
    }
}
