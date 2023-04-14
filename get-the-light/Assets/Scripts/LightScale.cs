using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScale : MonoBehaviour
{
    void Update()
    {
        Vector3 scale = Vector3.zero;

        if (Input.GetKey("s"))
        {
            scale.x = 1;
            scale.y = 1;
        }

        transform.localScale += scale / 100;
    }
}
