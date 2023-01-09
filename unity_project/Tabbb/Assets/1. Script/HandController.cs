using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private Hand currentHand; // 현재 장착된 Hand형 타입 무기
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gm;
    [SerializeField] private Socket[] socket;
    [SerializeField] private GameObject canvas;

    // 공격 중
    private bool isAttack = false;
    private bool isSwing = false;
    public bool isFirstTouch = false;
    private int swingCount = 0;

    private RaycastHit hitInfo;
    private Vector3 target = new Vector3(-2.3f, 1.78f, -3.43f);
    private Quaternion targetRotation = Quaternion.Euler(-10f, 3.3f, 89f);

    void Update()
    {
        TryAttack();
    }

    private void FixedUpdate() 
    {
        Debug.DrawRay(transform.position, transform.forward * currentHand.range, Color.green);
    }

    private void TryAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!isAttack)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        currentHand.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentHand.attackDelayA);
        isSwing = true;

        // 공격 활성화 시점
        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(currentHand.attackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentHand.attackDelay - currentHand.attackDelayA - currentHand.attackDelayB);
        isAttack = false;
    }

    IEnumerator HitCoroutine()
    {
        // 누워서 스윙할 때마다 카운트를 증가시킵니다.
        if (isFirstTouch)
        {
            swingCount++;
        }

        while (isSwing)
        {
            if (CheckObject() && gm.isArrival)
            {
                isSwing = false;
                Debug.Log(hitInfo.collider.name);

                if (hitInfo.collider.name == "Prop_SingleBed_Pink" && !isFirstTouch)
                {
                    isFirstTouch = true;
                    player.transform.localPosition = target;
                    player.transform.localRotation = targetRotation;

                    // 누울경우 소켓을 회전시킵니다.
                    for (int i = 0; i < socket.Length; i++)
                    {
                        socket[i].RotateSocket();
                    }
                }
            }

            if (swingCount == 3)
            {
                // 3번 스윙하면 효과음을 재생합니다
                SoundManager.Instance.PlaySFX("COME_ON_MAN");
            }

            if (swingCount == 6)
            {
                SoundManager.Instance.PlaySFX("Fuck");
                canvas.gameObject.SetActive(true);
            }

            yield return null;
        }
    }

    private bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentHand.range))
        {
            return true;
        }

        return false;
    }
}
