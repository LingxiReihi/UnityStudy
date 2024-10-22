using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbState : State
{
    private Rigidbody _rgb;
    private float _speed;
    public void InitState(PlayerMovement player)
    {
        _rgb = player.GetComponent<Rigidbody>();
        _speed = player.Speed;
    }

    public void MovePlayer(Vector3 moveDir)
    {
        _rgb.velocity = new Vector3(moveDir.x * _speed, moveDir.z * _speed, 0) * Time.deltaTime;
    }
}
