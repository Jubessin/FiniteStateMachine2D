public class IdleState : BaseState
{
    private PlayerFSM FSM;
    private InputController inputController;

    public override void OnAssignFSM(PlayerFSM FSM)
    {
        this.FSM = FSM;
        inputController = FSM.InputController;
        Start();
    }

    public override void Start()
    {
        InputController.InputLeft += _MoveLeft;
        InputController.InputRight += _MoveRight;
        InputController.InputUp += _Jump;
    }

    public override void Stop()
    {
        InputController.InputLeft -= _MoveLeft;
        InputController.InputRight -= _MoveRight;
        InputController.InputUp -= _Jump;
    }

    private void _MoveLeft()
    {
        if (FSM.CurrentState != State.IDLE || inputController.IsPlayerMovingRight) return;

        Stop();
        FSM.ChangeState(State.MOVE);
    }

    private void _MoveRight()
    {
        if (FSM.CurrentState != State.IDLE || inputController.IsPlayerMovingLeft) return;

        Stop();
        FSM.ChangeState(State.MOVE);
    }

    private void _Jump()
    {
        if (FSM.CurrentState != State.IDLE) return;

        Stop();
        FSM.ChangeState(State.JUMP);
    }
}
