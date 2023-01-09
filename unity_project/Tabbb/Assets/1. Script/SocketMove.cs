using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocketMove : MonoBehaviour
{
    private Vector2 target = new Vector2(185.0f, 0);
    [SerializeField] private Image socketImage;
    [SerializeField] private GameManager gm;

    private void Start() 
    {
        StartCoroutine("MoveCoroutine");
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            // 물체를 타겟의 위치로 이동시킵니다.
            socketImage.rectTransform.anchoredPosition = Vector2.MoveTowards(socketImage.rectTransform.anchoredPosition, target, 1.0f);
            yield return null;

            if (socketImage.rectTransform.anchoredPosition == target)
            {
                gm.charmStart();
            }
        }
    }
}
