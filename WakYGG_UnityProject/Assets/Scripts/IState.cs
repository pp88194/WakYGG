using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IState<T> where T : MonoBehaviour
{
    void OnEnter(T instance);
    void Update();
    void OnExit();
}

public class PlayerState : IState<Player>
{
    protected Player player;
    public virtual void OnEnter(Player player)
    {
        if(this.player == null)
            this.player = player;
    }
    public virtual void Update()
    {

    }
    public virtual void OnExit()
    {

    }
}

public class PlayerIdleState : PlayerState
{
    public override void Update()
    {
        if (InputSystem.Instance.MoveDir.x != 0)
            player.SetState<PlayerMoveState>(nameof(PlayerMoveState));
        player.Rigid.velocity = Utils.NewVector3(player.Rigid.velocity.x * player.HorizonDrag, player.Rigid.velocity.y, 0);
    }
}

public class PlayerMoveState : PlayerState
{
    public override void Update()
    {
        //velocity = inputDir * moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? 1.7f : 1);
        player.Rigid.velocity = Utils.NewVector3(InputSystem.Instance.MoveDir.x * player.MoveSpeed, player.Rigid.velocity.y, 0);
    }
}

public class PlayerJumpState : PlayerState
{
    public override void OnEnter(Player player)
    {
        base.OnEnter(player);
        if(player.IsGrounded)
        {
            player.Rigid.velocity = Utils.NewVector3(player.Rigid.velocity.x, player.JumpPower);
        }
        player.SetState<PlayerIdleState>(nameof(PlayerIdleState));
    }
}

public class PlayerRollState : PlayerState
{
    bool isRoll = true;
    IEnumerator C_Roll()
    {
        isRoll = true;
        Vector2 beginPos = player.transform.position;
        Vector2 target = new Vector2(player.transform.position.x + InputSystem.Instance.MoveDir.x * 3, player.transform.position.y);
        for (float t = 0; t < 0.2f; t += Time.deltaTime)
        {
            player.transform.position = Vector2.Lerp(beginPos, target, t * 5);
            yield return null;
        }
        player.transform.position = target;
        isRoll = false;
    }
    public override void OnEnter(Player player)
    {
        if(!player.IsGrounded)
        {
            player.SetState<PlayerIdleState>(nameof(PlayerIdleState));
            return;
        }
        base.OnEnter(player);
        player.StartCoroutine(C_Roll());
    }
    public override void Update()
    {
        if (!isRoll)
            player.SetState<PlayerIdleState>(nameof(PlayerIdleState));
    }
}