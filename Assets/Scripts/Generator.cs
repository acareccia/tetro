using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject[] tetraminos;

    // Start is called before the first frame update
    void Start()
    {
        NewTetramino();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewTetramino() {
        Instantiate(tetraminos[Random.Range(0, tetraminos.Length)], transform.position, Quaternion.identity);
    }

}
