using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private Vector2 target = new Vector2(-185.0f, 0);
    [SerializeField] private Image playerImage;

    void Start()
    {
        StartCoroutine("MoveCoroutine");
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            // 물체를 타겟의 위치로 이동시킵니다.
            playerImage.rectTransform.anchoredPosition = Vector2.MoveTowards(playerImage.rectTransform.anchoredPosition, target, 1.0f);
            yield return null;
        }
    }
}
