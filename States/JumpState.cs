using UnityEngine;

public class JumpState : BaseState
{
    private PlayerFSM FSM;
    private Rigidbody2D rb;

    private const float JUMPFORCE = 25f;

    public override void OnAssignFSM(PlayerFSM FSM)
    {
        this.FSM = FSM;
        rb = FSM.rb;
    }

    public override void FixedUpdate()
    {
        if (FSM.IsPlayerGrounded)
        {
            rb.AddForce(new Vector2(0.0f, JUMPFORCE), ForceMode2D.Impulse);
        }
                
        switch (FSM.PreviousState)
        {
            case State.IDLE:
            case State.MOVE:
                FSM.ChangeState(FSM.PreviousState);
                break;

            default:
                FSM.ChangeState(State.MOVE);
                break;
        }
    }
}
