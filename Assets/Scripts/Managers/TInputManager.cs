using UnityEngine;
using UnityEngine.Events;

public class TInputManager : TSingleton<TInputManager>
{
    private TInputActions inputActions;

    [HideInInspector]
    public Vector2 direction;

    [HideInInspector]
    public Vector2 rotate;

    [HideInInspector]
    public Vector2 delta;

    [HideInInspector]
    public bool isPress;

    [HideInInspector]
    public float pressTime;

    public UnityAction<Vector2> Drag;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

}
