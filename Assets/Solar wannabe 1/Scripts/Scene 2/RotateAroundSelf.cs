using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundSelf : MonoBehaviour
{
    // Start is called before the first frame update
    void FixedUpdate()
        {
            transform.Rotate(0, 1, 0);
        }
}
