using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.Rotate(new UnityEngine.Vector3(0, speed * Time.deltaTime, 0));
    }


}
