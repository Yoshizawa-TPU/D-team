using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainGrow : MonoBehaviour
{
    public GameObject[] seeds;//中に種が入ってます。
    public GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rainGrowing()
    {

    }

    void OnTriggerEnter(Collider other)//こっちは機能する
    {
        if (other.CompareTag("Rainy"))
        {

            //Debug.Log("雨種成長します!");
        }
    }
}
