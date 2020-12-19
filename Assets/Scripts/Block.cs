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
        Debug.Log("Start Block");
    
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
                CheckLine();
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

    void CheckLine() {
        for (int i = height-1; i >= 0; i--)
        {
            if (HasLine(i)) {
                DeleteLine(i);
                RowDown(i);
            }
            
        }
    }

    bool HasLine(int i) {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null) {
                return false;
            }
        }
        return true;
    }

    void DeleteLine(int i) {
        Debug.Log("DeleleLine "+i);
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    } 

    void RowDown(int i) {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null){

                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= Vector3.up;
                }
            }
        }
    }
 
}
