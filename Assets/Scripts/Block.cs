using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Vector3 rotationPoint;
    public float fallTime = 1f;
    public static int height = 20;
    public static int width = 10;

    private float prevTime;
    private static Transform[,] grid = new Transform[width,height];

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            transform.position += Vector3.left; //* Time.deltaTime;
            if (!ValidMove()) {
                transform.position -= Vector3.left; //* Time.deltaTime;
            }
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.position += Vector3.right; //* Time.deltaTime;
            if (!ValidMove()) {
                transform.position -= Vector3.right; //* Time.deltaTime;
            }
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            //transform.Rotate(0,0,90);
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1),90);
            if (!ValidMove()) {
                //transform.Rotate(0,0,-90);
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1),-90);
            }
        }

        Debug.DrawLine(transform.TransformPoint(rotationPoint),Vector3.down,Color.white);

        if (Time.time - prevTime > GetFallTime()) {
            transform.position += Vector3.down;
            if (!ValidMove()) {
                transform.position -= Vector3.down;
                AddToGrid();
                this.enabled = false;
                FindObjectOfType<Generator>().NewTetramino();
            }
            prevTime = Time.time;
        }

    }

    float GetFallTime() {
        if (Input.GetKey(KeyCode.DownArrow)) {
            return fallTime /10;
        } 
        return fallTime;
    }

    bool ValidMove() {
        foreach(Transform children in transform) {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            //Debug.Log(roundedX +" "+roundedY);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height ) {
                return false;
            }

            if (grid[roundedX,roundedY] != null) {
                return false;
            }
        }

        return true;
    }

    void AddToGrid() {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX,roundedY] = children;

        }
    }


 
}
