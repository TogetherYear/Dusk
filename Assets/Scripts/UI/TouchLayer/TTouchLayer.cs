using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TTouchLayer : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public void OnDrag(PointerEventData e)
    {
        TInputManager.Instance.Drag?.Invoke(e.delta);
    }

    public void OnPointerDown(PointerEventData e)
    {
        TInputManager.Instance.isPress = true;
        TInputManager.Instance.pressTime += Time.deltaTime;
    }

    public void OnPointerUp(PointerEventData e)
    {
        TInputManager.Instance.isPress = false;
        TInputManager.Instance.pressTime = 0.0f;
    }
}
