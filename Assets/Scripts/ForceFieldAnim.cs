using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldAnim : MonoBehaviour
{
    float scrollSpeed = 0.5f;
    float offset;
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        offset = Time.time * scrollSpeed % 1;
        material.mainTextureOffset = new Vector2(0, offset);
    }
}
