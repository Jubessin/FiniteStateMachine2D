using UnityEngine;

public enum State
{
    IDLE,
    MOVE,
    JUMP,
    DEAD,
}

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BaseFSM : MonoBehaviour
{
    public BoxCollider2D Col2D { get; protected set; }
    public Rigidbody2D rb { get; protected set; }

    public LayerMask PlatformLayer;

    protected virtual bool IsGrounded()
    {
        return false;
    }
}
