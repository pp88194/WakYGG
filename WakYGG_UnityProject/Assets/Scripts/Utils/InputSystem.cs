using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputSystem : MonoSingleton<InputSystem>
{
    Vector2 moveDir;
    public Vector2 MoveDir => moveDir;
    public Action jumpEvent;
    public Action rollEvent;
    public Action guardEvent;
    private void Update()
    {
        moveDir = Utils.NewVector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(DataManager.keyData.jumpKey))
            jumpEvent?.Invoke();
        if (Input.GetKeyDown(DataManager.keyData.rollKey))
            rollEvent?.Invoke();
        if (Input.GetKeyDown(DataManager.keyData.guardKey))
            guardEvent?.Invoke();
    }
}