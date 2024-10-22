using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State
{
    public void InitState(PlayerMovement player);
    public void MovePlayer(Vector3 moveDir);
}
