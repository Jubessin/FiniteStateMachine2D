using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public delegate void PlayerInput();
    public static event PlayerInput InputLeft;
    public static event PlayerInput InputRight;
    public static event PlayerInput StopInputLeft;
    public static event PlayerInput StopInputRight;
    public static event PlayerInput InputUp;

    private PlayerControls playerControls;

    public bool IsPlayerMovingRight { get; private set; }
    public bool IsPlayerMovingLeft { get; private set; }

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();

        playerControls.Gameplay.Left.performed += ctx => InvokeInputLeft(ctx);
        playerControls.Gameplay.Right.performed += ctx => InvokeInputRight(ctx);
        playerControls.Gameplay.Up.performed += ctx => InvokeInputUp(ctx);

        playerControls.Gameplay.Left.canceled += ctx => InvokeStopInputLeft(ctx);
        playerControls.Gameplay.Right.canceled += ctx => InvokeStopInputRight(ctx);
    }

    private void InvokeInputLeft(InputAction.CallbackContext context)
    {
        InputLeft?.Invoke();
        IsPlayerMovingLeft = true;
    }

    private void InvokeInputRight(InputAction.CallbackContext context)
    {
        InputRight?.Invoke();
        IsPlayerMovingRight = true;
    }

    private void InvokeStopInputLeft(InputAction.CallbackContext context)
    {
        StopInputLeft?.Invoke();
        IsPlayerMovingLeft = false;
    }

    private void InvokeStopInputRight(InputAction.CallbackContext context)
    {
        StopInputRight?.Invoke();
        IsPlayerMovingRight = false;
    }

    private void InvokeInputUp(InputAction.CallbackContext context)
    {
        InputUp?.Invoke();
    }

    private void OnDisable()
    {
        playerControls.Disable();

        playerControls.Gameplay.Left.performed -= ctx => InvokeInputLeft(ctx);
        playerControls.Gameplay.Right.performed -= ctx => InvokeInputRight(ctx);
        playerControls.Gameplay.Up.performed -= ctx => InvokeInputUp(ctx);

        playerControls.Gameplay.Left.canceled -= ctx => InvokeStopInputLeft(ctx);
        playerControls.Gameplay.Right.canceled -= ctx => InvokeStopInputRight(ctx);
    }
}
