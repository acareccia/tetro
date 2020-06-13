using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private float prevTime;
    public float fallTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            transform.position += Vector3.left; //* Time.deltaTime;
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.position += Vector3.right; //* Time.deltaTime;
        }

        if (Time.time - prevTime > GetFallTime()) {
            transform.position += Vector3.down;
            prevTime = Time.time;
        }
    }

    private float GetFallTime() {
        if (Input.GetKey(KeyCode.DownArrow)) {
            return fallTime /10;
        } 
        return fallTime;
    }



 
}
