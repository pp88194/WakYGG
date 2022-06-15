using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Move,
    Jump
}
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    #region Variable
    IPlayerState currentState;
    [SerializeField] float jumpPower = 15;
    public float JumpPower => jumpPower;
    [SerializeField] float moveSpeed = 5f;
    public float MoveSpeed => moveSpeed;
    [SerializeField] float horizonDrag = 0.9f;
    public float HorizonDrag => horizonDrag;
    Rigidbody2D rigid;
    public Rigidbody2D Rigid => rigid;
    Dictionary<string, IPlayerState> playerStateDict = new Dictionary<string, IPlayerState>();
    #endregion
    public void SetState<T>(string key) where T : IPlayerState, new()
    {
        if (!playerStateDict.ContainsKey(key))
        {
            Debug.Log($"Added {key}");
            playerStateDict.Add(key, new T());
        }
        if (currentState != null)
            currentState.OnExit();
        currentState = playerStateDict[key];
        currentState.OnEnter(this);
    }
    private void Awake()
    {
        //Initialize
        rigid = GetComponent<Rigidbody2D>();
        SetState<PlayerIdleState>(nameof(PlayerIdleState));
        InputSystem.Instance.jumpEvent += () => SetState<PlayerJumpState>(nameof(PlayerJumpState));
    }
    private void Update()
    {
        currentState.Update();
    }
}
////Movement
//public partial class Player : MonoBehaviour
//{
//    public void Jump()
//    {
//        rigid.velocity = Utils.NewVector3(rigid.velocity.x, jumpPower);
//    }
//    public void SetVelocity()
//    {
//        if (InputSystem.Instance.MoveDir.x != 0)
//        {
//            //velocity = inputDir * moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? 1.7f : 1);
//            rigid.velocity = Utils.NewVector3(InputSystem.Instance.MoveDir.x * moveSpeed, rigid.velocity.y, 0);
//        }
//        else
//            rigid.velocity = Utils.NewVector3(rigid.velocity.x * horizonDrag, rigid.velocity.y, 0);
//    }
//}