using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunderGrow : MonoBehaviour
{
    public GameObject[] seeds;//中に種が入ってます。
    public GameManager GM;

    int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void thunderGrowing()
    {

    }

    void OnTriggerEnter(Collider other)//こっちは機能する
    {
        if (other.CompareTag("Thunder"))
        {

            Debug.Log("雷種成長します!");
        }
    }
}
