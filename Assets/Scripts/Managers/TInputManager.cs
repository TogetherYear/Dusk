using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class TInputManager : TSingleton<TInputManager>
{

    public enum TInputKeyType
    {
        Down,
        Up
    }

    private TInputActions inputActions;

    [HideInInspector]
    public bool isJump;

    public Action<TInputKeyType> Jump;

    [HideInInspector]
    public bool isCrouch;

    public Action<TInputKeyType> Crouch;

    [HideInInspector]
    public bool isSprint;

    public Action<TInputKeyType> Sprint;

    [HideInInspector]
    public bool isAround;

    public Action<TInputKeyType> Around;

    [HideInInspector]
    public bool isSlow;

    public Action<TInputKeyType> Slow;

    [HideInInspector]
    public Vector2 movement;

    [HideInInspector]
    public Vector2 rotate;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        InitActions();
        BindActions();
    }

    private void Update()
    {
        ReadValues();
    }

    private void InitActions()
    {
        inputActions = new TInputActions();
        inputActions.General.Enable();
    }

    private void BindActions()
    {
        inputActions.General.Jump.performed += OnJump;
        inputActions.General.Jump.canceled += OnJump;
        inputActions.General.Crouch.performed += OnCrouch;
        inputActions.General.Crouch.canceled += OnCrouch;
        inputActions.General.Sprint.performed += OnSprint;
        inputActions.General.Sprint.canceled += OnSprint;
        inputActions.General.Around.performed += OnAround;
        inputActions.General.Around.canceled += OnAround;
        inputActions.General.Slow.performed += OnSlow;
        inputActions.General.Slow.canceled += OnSlow;
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        isJump = ctx.performed;
        Jump?.Invoke(ctx.performed ? TInputKeyType.Down : TInputKeyType.Up);
    }

    private void OnCrouch(InputAction.CallbackContext ctx)
    {
        isCrouch = ctx.performed;
        Crouch?.Invoke(ctx.performed ? TInputKeyType.Down : TInputKeyType.Up);
    }

    private void OnSprint(InputAction.CallbackContext ctx)
    {
        isSprint = ctx.performed;
        Sprint?.Invoke(ctx.performed ? TInputKeyType.Down : TInputKeyType.Up);
    }

    private void OnAround(InputAction.CallbackContext ctx)
    {
        isAround = ctx.performed;
        Around?.Invoke(ctx.performed ? TInputKeyType.Down : TInputKeyType.Up);
    }

    private void OnSlow(InputAction.CallbackContext ctx)
    {
        isSlow = ctx.performed;
        Slow?.Invoke(ctx.performed ? TInputKeyType.Down : TInputKeyType.Up);
    }

    private void ReadValues()
    {
        movement = inputActions.General.Movement.ReadValue<Vector2>();
        rotate = inputActions.General.Rotate.ReadValue<Vector2>();
    }
}
