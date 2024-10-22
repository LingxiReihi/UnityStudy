using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayeMoveController : NetworkBehaviour
{
    public Animator animator;
    [SyncVar] private float inputX;
    [SyncVar] private float inputY;
    [SyncVar] Vector3 position;
    private Vector2 inputDir;
    [SyncVar] private float isRunning;
    [SyncVar] private bool isMoving;

    private void OnEnable()
    {
        inputDir = Vector2.zero;
        inputX = 0;
        inputY = 0;
        isRunning = 0;
        isMoving = false;
        position = transform.position;
    }

    [Client]
    private void Update()
    {
        CheckInput();
        UpdateAnimation();
    }

    [Client]
    private void CheckInput()
    {
        if (isLocalPlayer)
        {
            inputY = Input.GetAxis("Horizontal");
            inputX = Input.GetAxis("Vertical");
            inputDir = new Vector2(inputX, inputY).normalized;
            isMoving = inputDir != Vector2.zero;

            if (isMoving && Input.GetKey(KeyCode.LeftShift))
                isRunning = 1;
            else
                isRunning = 0;
        }
    }
    // [Command]
    // private void CmdUpdateAnimation()
    // {
    //     UpdateAnimation();
    // }

    // [ClientRpc]
    private void UpdateAnimation()
    {
        animator.SetBool("IsMove", isMoving);
        animator.SetFloat("InputX", inputDir.x);
        animator.SetFloat("InputY", inputDir.y);
        animator.SetFloat("IsRunning", isRunning);
    }
}
