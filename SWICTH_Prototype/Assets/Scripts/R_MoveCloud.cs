using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_MoveCloud : MonoBehaviour
{
    [SerializeField] float speed = 1200f;
    [SerializeField] float moveDistance;

    private float startPosX;
    public static bool R_moveStart = false;

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置を記録
        startPosX = transform.position.x;
        R_moveStart = false;
}

    // Update is called once per frame
    void Update()
    {
        if(R_moveStart)
        {
            moveObject();
        }
        
    }

    public void moveObject()
    {
        transform.Translate(Vector3.right * speed *Time.deltaTime);
    }
}
