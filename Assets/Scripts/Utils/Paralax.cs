using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paralax : MonoBehaviour
{
    public Canvas canvas;
    private float length, startpos;
    public float parallaxEffect;
    float speed = 0.4f;

    void Start()
    {
        startpos = transform.position.y;
        length = GetComponent<RectTransform>().rect.width * canvas.transform.localScale.y;
    }

    void FixedUpdate()
    {
        float dist = (speed * parallaxEffect);
        transform.position = new Vector3(transform.position.x, startpos + dist, transform.position.z);
        startpos = transform.position.y;
        if (startpos > length) startpos -= length;
    }
}