using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour
{

    public float speed;

    // Update is called once per frame
    void Update()
    {
        float y = Time.deltaTime * speed;
        transform.Rotate(0, -y, 0);
    }
}
