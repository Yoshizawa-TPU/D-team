using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainGrow : MonoBehaviour
{
    //public GameObject[] seeds;//中に種が入ってます。
    public bool R_Ready = false;
    public bool R_Return = false;
    public bool R_Harvest = false;
    private Vector3 seedScale;

    // Start is called before the first frame update
    void Start()
    {
        //seedScale = transform.localScale;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.localScale = seedScale;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rainy"))
        {
            R_Ready = true;
            //Debug.Log("雨種成長します!");
        }

        if (other.CompareTag("Thunder"))
        {
            R_Return = true;
            //Debug.Log("ミス");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rainy"))
        {
            R_Ready = false;
            //Debug.Log("雨種成長しません!");
            //Debug.Log(S_Ready);
        }
        if (other.CompareTag("Thunder"))
        {
            R_Return = false;
        }
    }
}
