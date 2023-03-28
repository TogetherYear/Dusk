using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCameraController : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float moveSpeed = 5.0f;

    [Range(0.0f, 10.0f)]
    public float rotateSpeed = 5.0f;
    private void Start()
    {
        TInputManager.Instance.Drag += this.UpdateRotation;
    }
    private void Update()
    {
        this.UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (TInputManager.Instance.direction != Vector2.zero)
        {
            this.transform.Translate(new Vector3(TInputManager.Instance.direction.x, 0.0f, TInputManager.Instance.direction.y) * this.moveSpeed * Time.deltaTime, Space.Self);
        }
    }

    private void UpdateRotation(Vector2 delta)
    {
        this.transform.Rotate(new Vector3(0.0f, delta.x, 0.0f) * this.rotateSpeed * Time.deltaTime, Space.World);
        this.transform.Rotate(new Vector3(-delta.y, 0.0f, 0.0f) * this.rotateSpeed * Time.deltaTime, Space.Self);
    }
}
