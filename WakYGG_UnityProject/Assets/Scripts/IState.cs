using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IState
{
    void OnEnter(Player player);
    void Update();
    void OnExit();
}

public class PlayerState : IState
{
    protected Player player;
    public virtual void OnEnter(Player player)
    {
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
            player.SetState(new PlayerMoveState());
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
        player.Rigid.velocity = Utils.NewVector3(player.Rigid.velocity.x, player.JumpPower);
        player.SetState(new PlayerIdleState());
    }
}