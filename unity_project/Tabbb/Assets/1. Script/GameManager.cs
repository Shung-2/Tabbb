using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Vector3 target = new Vector3(-2.15f, 1, -1.5f);
    public bool isArrival = false;

    private void Start()
    {
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
}
