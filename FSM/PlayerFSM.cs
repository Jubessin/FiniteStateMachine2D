using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(StateController))]
[RequireComponent(typeof(InputController))]
public class PlayerFSM : BaseFSM
{
    private const float raycastErrorValue = 1f;

    private StateController stateController;

    public InputController InputController { get; private set; }
    public State PreviousState { get; private set; }
    public State CurrentState { get; private set; }

    public bool IsPlayerGrounded { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Col2D = GetComponent<BoxCollider2D>();
        stateController = GetComponent<StateController>();
        InputController = GetComponent<InputController>();
    }

    private void Start()
    {
        ChangeState(State.IDLE);
    }

    private void Update()
    {
        IsPlayerGrounded = IsGrounded();
    }

    public void ChangeState(State state)
    {
        PreviousState = CurrentState;
        CurrentState = state;
        stateController.AssignFSMToState();
    }

    protected override bool IsGrounded()
    {
        RaycastHit2D raycast = Physics2D.Raycast(Col2D.bounds.center, Vector2.down, Col2D.bounds.extents.y + raycastErrorValue, PlatformLayer.value);

        return raycast.collider != null || rb.velocity.normalized.y == 0;
    }
}
