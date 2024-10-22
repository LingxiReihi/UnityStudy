using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : State
{
    private Rigidbody _rgb;
    private float _speed;
    private PlayerMovement _playerMovement;

    public void InitState(PlayerMovement player)
    {
        _rgb = player.GetComponent<Rigidbody>();
        _speed = player.Speed;
        _playerMovement = player;
    }

    public void MovePlayer(Vector3 moveDir)
    {
        _rgb.velocity = new Vector3(moveDir.x * _speed, _rgb.velocity.y, moveDir.z * _speed) * Time.deltaTime;
    }
}
