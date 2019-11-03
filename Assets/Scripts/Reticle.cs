using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    [SerializeField] Transform reticle;
    [SerializeField] float negativeOffset = 500f;
    private Transform point;
    // Start is called before the first frame update
    void Start()
    {
        point = transform.parent.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.right, out hit, 20))
        {
            if (hit.collider.transform != null)
            {
                // Debug.DrawLine(transform.position, hit.transform.position, Color.white);
                // reticle.position = new Vector3(transform.position.x, transform.position.y, hit.distance + negativeOffset);
                // reticle.rotation = Quaternion.Euler(point.parent.transform.rotation.x, point.parent.transform.rotation.y, point.parent.transform.rotation.z / reticle.position.z);
            }
        }
    }
}
