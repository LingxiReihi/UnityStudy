using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public GameObject head;
    public GameObject foot;
    #region 移动参数
    [SerializeField] private float _speed;
    public float Speed { get => _speed; internal set => _speed = value; }
    [SerializeField] private float _inputHorizontal;
    [SerializeField] private float _inputVertical;
    [SerializeField] private Vector3 _moveDir;
    public Vector3 MoveDir { get => _moveDir; internal set => _moveDir = value; }
    [SerializeField] private Rigidbody _rgb;
    [SerializeField] private bool isOnGround;
    [SerializeField] private bool isOnWall;
    private State _state;
    private readonly BaseState _baseState = new();
    private readonly ClimbState _climbState = new();
    #endregion

    private void Start()
    {
        if (_rgb == null)
            _rgb = GetComponent<Rigidbody>();
        _state = _baseState;
        _state.InitState(this);
        isOnGround = true;
        isOnWall = false;
    }

    private void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        _state.MovePlayer(_moveDir);
        if (isOnGround)
            CheckWall();
        if (isOnWall)
            CheckOutWall();
    }

    private void CheckInput()
    {
        _inputHorizontal = Input.GetAxis("Horizontal");
        _inputVertical = Input.GetAxis("Vertical");
        _moveDir = new Vector3(_inputHorizontal, 0, _inputVertical).normalized;
        if (isOnWall && Input.GetKeyDown(KeyCode.Space))
            _rgb.Move(-transform.forward * 0.1f, Quaternion.identity);
    }

    private void CheckWall()
    {
        // 定义射线的起点和方向
        Vector3 origin = foot.transform.position; // 射线起点在角色前方0.5单位
        Vector3 direction = foot.transform.forward; // 射线方向为角色正前方
        float distance = 0.1f; // 射线检测的距离

        // 使用 Physics.Raycast 检测前方是否有墙壁
        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            // 检查碰撞到的对象是否标记为 "Wall"
            if (hit.collider.CompareTag("Wall"))
            {
                isOnWall = true;
                isOnGround = false;
                Debug.Log("前方有墙壁！");
                _state = _climbState;
                _state.InitState(this);
            }
        }
    }

    private void CheckOutWall()
    {
        // 定义射线的起点和方向
        Vector3 origin = head.transform.position; // 射线起点在角色前方0.5单位
        Vector3 direction = head.transform.forward; // 射线方向为角色正前方
        float distance = 0.1f; // 射线检测的距离

        // 使用 Physics.Raycast 检测前方是否有墙壁
        if (!Physics.Raycast(origin, direction, out RaycastHit hit, distance) || !hit.collider.CompareTag("Wall"))
        {
            // 如果没有命中任何物体
            Debug.Log("前面不是墙壁");
            isOnWall = false;
            isOnGround = true;
            Debug.Log("爬墙完成！");
            _state = _baseState;
            _state.InitState(this);
        }
    }
}
