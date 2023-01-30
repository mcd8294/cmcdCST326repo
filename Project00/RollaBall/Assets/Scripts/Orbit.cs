using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float orbitSpeed;

    // Update is called once per frame
    void Update()
    {
        Transform t = GetComponent<Transform>();
        t.Rotate(new Vector3(0, orbitSpeed * Time.deltaTime, 0));
    }
}
