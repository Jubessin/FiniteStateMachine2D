using UnityEngine;

public class DeadState : BaseState
{
    private PlayerFSM FSM;
    private Rigidbody2D rb;

    private float currentSpeed;
    private const float DECEL = 5.0f;

    public override void OnAssignFSM(PlayerFSM FSM)
    {
        this.FSM = FSM;
        rb = FSM.rb;
        Start();
    }

    public override void Start()
    {
        FSM.InputController.enabled = false;

        currentSpeed = rb.velocity.x;
    }

    public override void Stop()
    {
        FSM.Col2D.enabled = false;
        FSM.rb.simulated = false;
    }

    public override void Update()
    {
        currentSpeed = Decelerate(currentSpeed);
        if (FSM.IsPlayerGrounded)
        {
            Stop();
        }
    }

    private float Decelerate(float speed)
    {
        if (Mathf.Abs(speed) < 0.01f) return 0;
        else speed -= (speed * DECEL * Time.deltaTime);

        return speed;
    }
}
