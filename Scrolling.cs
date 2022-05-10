using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float speed = 1.0f;
    public float startPosition;
    public float endPosition;

    void Update()
    {
        // y포지션을 조금씩 이동
        transform.Translate(0, -1 * speed * Time.deltaTime, 0);

        // 목표 지점에 도달했다면
        if (transform.position.y <= endPosition)
        {
            ScrollEnd();
        }
    }

    void ScrollEnd()
    {
        // 원래 위치로 초기화 시킨다.
        transform.Translate(0, -1 * (endPosition - startPosition), 0);
    }
}
