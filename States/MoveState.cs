using UnityEngine;

public class MoveState : BaseState
{
    private PlayerFSM FSM;
    private Rigidbody2D rb;
    private InputController inputController;

    private float currentSpeed;

    private const float MAXSPEED = 12.5f;
    private const float ACCEL = 3.0f;
    private const float DECEL = 5.0f;
    
    private bool isPlayerMoving = false;

    public override void OnAssignFSM(PlayerFSM FSM)
    {
        this.FSM = FSM;
        rb = FSM.rb;
        inputController = FSM.InputController;
        Start();
    }

    public override void Start()
    {
        InputController.InputUp += _InputUp;

        isPlayerMoving = false;
        currentSpeed = rb.velocity.x;
    }

    public override void Stop()
    {
        InputController.InputUp -= _InputUp;
    }

    public override void Update()
    {
        isPlayerMoving = inputController.IsPlayerMovingLeft != inputController.IsPlayerMovingRight;

        if (FSM.IsPlayerGrounded && !isPlayerMoving)
        {
            if (Mathf.Abs(currentSpeed) < 0.5f)
            {
                currentSpeed = 0f;
                Stop();
                FSM.ChangeState(State.IDLE);
            }
        }
    }

    public override void FixedUpdate()
    {
        if (inputController.IsPlayerMovingLeft == inputController.IsPlayerMovingRight)
        {
            currentSpeed = Decelerate(currentSpeed);
        }
        else
        {
            currentSpeed = Accelerate(currentSpeed);
        }

        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
    }

    private float Accelerate(float speed)
    {
        if (Mathf.Abs(speed) > MAXSPEED)
        {
            if (speed > MAXSPEED)
            {
                if (inputController.IsPlayerMovingRight) speed = MAXSPEED;
                else speed -= MAXSPEED * MAXSPEED * ACCEL * Time.deltaTime;
            }
            else
            {
                if (inputController.IsPlayerMovingLeft) speed = -MAXSPEED;
                else speed += MAXSPEED * MAXSPEED * ACCEL * Time.deltaTime;
            }
            return speed;
        }
        else
        {
            if (inputController.IsPlayerMovingRight)
            {
                if (speed < MAXSPEED) speed += MAXSPEED * ACCEL * Time.deltaTime;
                else speed = MAXSPEED;
            }
            else
            {
                if (speed > -MAXSPEED) speed += -MAXSPEED * ACCEL * Time.deltaTime;
                else speed = -MAXSPEED;
            }
            return speed;
        }
    }

    private float Decelerate(float speed)
    {
        if (Mathf.Abs(speed) < 0.01f) return 0;
        else speed -= (speed * DECEL * Time.deltaTime);

        return speed;
    }

    private void _InputUp()
    {
        if (FSM.CurrentState != State.MOVE) return;

        Stop();
        FSM.ChangeState(State.JUMP);
    }
}
