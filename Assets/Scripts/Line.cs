using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private SpriteRenderer lineSprite;
    void Awake()
    {

        lineSprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public float GetLineTopPoint(float startY)
    {
        float topPoint = lineSprite.bounds.size.y + startY;
        return topPoint;
    }

}
