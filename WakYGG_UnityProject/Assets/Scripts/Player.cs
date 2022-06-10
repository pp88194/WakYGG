using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Vector2 inputDir;
    Vector2 velocity;

    void Move() //FixedUpdate
    {
        transform.position += (Vector3)velocity * Time.fixedDeltaTime;
    }

    private void Update()
    {
        inputDir = Utils.NewVector2(Input.GetAxisRaw("Horizontal"), 0);
        if(inputDir != Vector2.zero)
        {
            velocity = inputDir * moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? 1.7f : 1);
        }
        else
            velocity *= 0.97f;
    }
    private void FixedUpdate()
    {
        Move();
    }
}