using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScaleX : MonoBehaviour
{
    void LateUpdate()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}