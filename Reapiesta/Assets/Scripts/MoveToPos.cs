using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPos : MonoBehaviour
{

    [SerializeField] bool ui = false;
    [SerializeField] RectTransform rect;
    [SerializeField] Vector3 goal;
    [SerializeField] float speed = 10;
	[SerializeField] Transform toMove;

    void Update()
    {
        if (ui == true)
        {
            rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, goal, Time.deltaTime * speed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, goal, Time.deltaTime * speed);
        }
    }
}
