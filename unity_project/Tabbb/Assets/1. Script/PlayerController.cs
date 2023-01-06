using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Non-SerializeField Zone
    private Rigidbody myRigid; // 리지드바디
    private CapsuleCollider capsuleCollider; // 캡슐 콜라이더 (땅 착지 여부 확인)

    // Non-SerializeField Zone State Value
    private bool isGround = true; // 바닥에 닿았는지 확인

    // SerializeField Zone
    [SerializeField] private float walkSpeed; // 걷는 속도
    [SerializeField] private float jumpForce; // 점프 힘
    
    void Start()
    {
        myRigid = GetComponent<Rigidbody>(); // myRigid에 리지드바디 컴포넌트를 가져온다.
        capsuleCollider = GetComponent<CapsuleCollider>(); // capsuleCollider에 캡슐 콜라이더 컴포넌트를 가져온다.
    }

    void Update()
    {
        IsGround();
        TryJump();

        Move();
    }

    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f); // 땅에 닿아있는지 확인한다.
    }

    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround) // 바닥에 닿아있는 상태에서 스페이스바를 누르면
        {
            Jump();
        }
    }

    private void Jump()
    {
        myRigid.velocity = transform.up * jumpForce; // 리지드바디의 속도를 위쪽으로 설정한다.
    }

    private void Move()
    {
        // 좌우, 상하
        float _moveDirX = Input.GetAxisRaw("Horizontal"); // 키보드의 좌우 방향키를 누르면 -1, 0, 1의 값을 반환한다.
        float _moveDirZ = Input.GetAxisRaw("Vertical"); // 키보드의 상하 방향키를 누르면 -1, 0, 1의 값을 반환한다.

        Vector3 _MoveHorizontal = transform.right * _moveDirX; // 좌우 이동 (1, 0, 0)
        Vector3 _MoveVertical = transform.forward * _moveDirZ; // 상하 이동 (0, 0, 1)
        Vector3 _velocity = (_MoveHorizontal + _MoveVertical).normalized * walkSpeed; // 이동 방향과 속도를 합친다.

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime); // 리지드바디의 위치를 이동시킨다. (transform.position + _velocity * Time.deltaTime)
    }
}
