using UnityEngine;
using System.Collections;

public class Hammer : MonoBehaviour
{
    public float speed = 1;

    void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, new Vector3(0, 0, 90), Mathf.Abs(Mathf.Sin(Time.time * speed))));
    }
}
