using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    public void RotateSocket()
    {
        Rotate();
    }

    private void Rotate()
    {
        StartCoroutine("RotateCoroutine");
    }

    private IEnumerator RotateCoroutine()
    {
        // 물체를 회전시킵니다.
        while (true)
        {
            transform.Rotate(0, 0, 15);
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
}
