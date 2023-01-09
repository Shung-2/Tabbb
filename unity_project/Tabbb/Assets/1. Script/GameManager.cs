using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Camera wpCam;

    private Vector3 target = new Vector3(-2.15f, 1, -1.5f);
    private Vector3 Teleport_pos = new Vector3(12.8f, 1.05f, 2.25f);
    private Vector3 Teleport_rot = new Vector3(-3.0f, 0.2f, -0.01f);
    public bool isArrival = false;

    private void Start()
    {
        SoundManager.Instance.PlaySFX("Yawn");
        Move();
    }

    private void Move()
    {
        if (!isArrival)
        {
            StartCoroutine("MoveCoroutine");
        }
    }

    private IEnumerator MoveCoroutine()
    {
        while (!isArrival)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, 0.01f);
            yield return null;

            if (player.transform.position == target)
            {
                isArrival = true;
            }
        }
    }

    public void charmStart()
    {
        // 캐릭터의 위치를 수정합니다.
        player.transform.position = Teleport_pos;
        player.transform.eulerAngles = Teleport_rot;

        // 코루틴 끔
        StopAllCoroutines();

        // 캔버스 끔
        canvas.gameObject.SetActive(false);
        wpCam.gameObject.SetActive(false);

    }
}
