using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerFSM FSM;

    private readonly IdleState IdleState = new IdleState();
    private readonly MoveState MoveState = new MoveState();
    private readonly JumpState JumpState = new JumpState();
    private readonly DeadState DeadState = new DeadState();

    private void Awake()
    {
        FSM = GetComponent<PlayerFSM>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(FSM.CurrentState)
        {
            case State.IDLE:
                IdleState.Update();
                break;

            case State.MOVE:
                MoveState.Update();
                break;

            case State.JUMP:
                JumpState.Update();
                break;

            case State.DEAD:
                DeadState.Update();
                break;

            default:
                throw new System.NotImplementedException($"{FSM.CurrentState} state not implemented.");
        }
    }

    private void FixedUpdate()
    {
        switch(FSM.CurrentState)
        {
            case State.IDLE:
                IdleState.FixedUpdate();
                break;

            case State.MOVE:
                MoveState.FixedUpdate();
                break;

            case State.JUMP:
                JumpState.FixedUpdate();
                break;

            case State.DEAD:
                DeadState.FixedUpdate();
                break;

            default:
                throw new System.NotImplementedException($"{FSM.CurrentState} state not implemented.");
        }
    }

    public void AssignFSMToState()
    {
        switch (FSM.CurrentState)
        {
            case State.IDLE:
                IdleState.OnAssignFSM(FSM);
                break;

            case State.MOVE:
                MoveState.OnAssignFSM(FSM);
                break;

            case State.JUMP:
                JumpState.OnAssignFSM(FSM);
                break;

            case State.DEAD:
                DeadState.OnAssignFSM(FSM);
                break;

            default:
                throw new System.NotImplementedException($"{FSM.CurrentState} state not implemented.");
        }
    }
}
