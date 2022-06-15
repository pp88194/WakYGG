using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    #region Variable
    bool isGrounded;
    public bool IsGrounded => isGrounded;
    PlayerState currentState;
    [SerializeField] float jumpPower = 15;
    public float JumpPower => jumpPower;
    [SerializeField] float moveSpeed = 5f;
    public float MoveSpeed => moveSpeed;
    [SerializeField] float horizonDrag = 0.9f;
    public float HorizonDrag => horizonDrag;
    Rigidbody2D rigid;
    public Rigidbody2D Rigid => rigid;
    Dictionary<string, PlayerState> playerStateDict = new Dictionary<string, PlayerState>();
    #endregion
    public void SetState<T>(string key) where T : PlayerState, new()
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
    void CheckGrounded()
    {
        if (Physics2D.OverlapBox(transform.position, new Vector2(transform.lossyScale.x, 0.01f), 0, LayerMask.GetMask("Ground")))
            isGrounded = true;
        else
            isGrounded = false;
    }
    private void Awake()
    {
        //Initialize
        rigid = GetComponent<Rigidbody2D>();
        SetState<PlayerIdleState>(nameof(PlayerIdleState));
        InputSystem.Instance.jumpEvent += () => SetState<PlayerJumpState>(nameof(PlayerJumpState));
        InputSystem.Instance.rollEvent += () => SetState<PlayerRollState>(nameof(PlayerRollState));
    }
    private void Update()
    {
        currentState.Update();
    }
    private void FixedUpdate()
    {
        CheckGrounded();
    }
}