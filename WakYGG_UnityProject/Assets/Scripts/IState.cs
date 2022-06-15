using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IState<T> where T : MonoBehaviour
{
    void OnEnter(T instance);
    void Update();
    void OnExit();
}

public class IPlayerState : IState<Player>
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

public class PlayerIdleState : IPlayerState
{
    public override void Update()
    {
        if (InputSystem.Instance.MoveDir.x != 0)
            player.SetState<PlayerMoveState>(nameof(PlayerMoveState));
        player.Rigid.velocity = Utils.NewVector3(player.Rigid.velocity.x * player.HorizonDrag, player.Rigid.velocity.y, 0);
    }
}

public class PlayerMoveState : IPlayerState
{
    public override void Update()
    {
        //velocity = inputDir * moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? 1.7f : 1);
        player.Rigid.velocity = Utils.NewVector3(InputSystem.Instance.MoveDir.x * player.MoveSpeed, player.Rigid.velocity.y, 0);
    }
}

public class PlayerJumpState : IPlayerState
{
    public override void OnEnter(Player player)
    {
        base.OnEnter(player);
        player.Rigid.velocity = Utils.NewVector3(player.Rigid.velocity.x, player.JumpPower);
        player.SetState<PlayerIdleState>(nameof(PlayerIdleState));
    }
}