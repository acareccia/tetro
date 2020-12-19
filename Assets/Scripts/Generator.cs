using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject[] tetraminos;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Generator ");
        NewTetramino();
    }

    public void NewTetramino() {
        // if (preview == null) {
        //     preview = GetRandomTetramino();
        // } else {
        //     current = preview;
        //     Destroy(preview);
        //     preview = GetRandomTetramino();
        // }

        Instantiate(GetRandomTetramino(), transform.position, Quaternion.identity);
        //preview = Instantiate(preview, new Vector3(-3,17), Quaternion.identity);
    }


    private GameObject GetRandomTetramino() {
        int rand = Random.Range(0, tetraminos.Length);
        return tetraminos[rand];
    }

}
