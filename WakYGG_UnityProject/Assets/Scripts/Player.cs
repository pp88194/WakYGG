using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public partial class Player
{
    #region Variable
    PlayerState currentState;
    [SerializeField] float jumpPower = 15;
    public float JumpPower => jumpPower;
    [SerializeField] float moveSpeed = 5f;
    public float MoveSpeed => moveSpeed;
    [SerializeField] float horizonDrag = 0.9f;
    public float HorizonDrag => horizonDrag;
    Rigidbody2D rigid;
    public Rigidbody2D Rigid => rigid;
    #endregion
    public void SetState(PlayerState state)
    {
        if (currentState != null)
            currentState.OnExit();
        currentState = state;
        currentState.OnEnter(this);
    }
    private void Awake()
    {
        SetState(new PlayerIdleState());
        rigid = GetComponent<Rigidbody2D>();
        InputSystem.Instance.jumpEvent += () => SetState(new PlayerJumpState());
    }
    private void Update()
    {
        //SetVelocity();
        currentState.Update();
    }
}
//Movement
public partial class Player : MonoBehaviour
{
    public void Jump()
    {
        rigid.velocity = Utils.NewVector3(rigid.velocity.x, jumpPower);
    }
    public void SetVelocity()
    {
        if (InputSystem.Instance.MoveDir.x != 0)
        {
            //velocity = inputDir * moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? 1.7f : 1);
            rigid.velocity = Utils.NewVector3(InputSystem.Instance.MoveDir.x * moveSpeed, rigid.velocity.y, 0);
        }
        else
            rigid.velocity = Utils.NewVector3(rigid.velocity.x * horizonDrag, rigid.velocity.y, 0);
    }
}