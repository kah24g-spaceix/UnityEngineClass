using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PositionScroller : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    float scrollRange = 9.9f;
    [SerializeField]
    private float speed;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -1 * speed * Time.deltaTime, 0);
        if (transform.position.y <= -scrollRange)
        {
            transform.position = target.position + Vector3.up * 9.9f;
        }
    }
}