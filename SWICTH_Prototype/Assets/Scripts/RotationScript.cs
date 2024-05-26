using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotationScript : MonoBehaviour
{
    float Rotation_angle;//�p�x(�����ɑ��x�����Z�����)
    float Save_angle;//���v�̊p�x(����ŉ�]���I��点�邩�ǂ������肷��)
    float Center_angle;
    [SerializeField] Transform Center;//��]�̒��S�ƂȂ�I�u�W�F�N�g
    [SerializeField] Transform[] Whether;//��]����I�u�W�F�N�g
    [SerializeField] float Rotation_range;//���x��]���邩
    [SerializeField] float Speed = 10f;//��]�̑��x
    bool Right_rotating = false;//�E��]
    bool Left_rotating = false;//����]

    // Start is called before the first frame update
    private void Start()
    {
        Exit_rotation();//�����ŉ�]�ɕK�v�Ȑ��l�̏�����
        Center_angle = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(!InputScript.on_rotation)//InputScript��ɂ���ϐ���false�̎�
        {
            Get_key();
        }

        if(InputScript.on_rotation)//InputScript��ɂ���ϐ���true�̎�
        {
            if(Right_rotating)
            {
                Whether_rotation_Right();
            }
            if(Left_rotating) 
            {
                Whether_rotation_Left();
            }
        } 
    }

    void Whether_rotation_Right()//�E��]
    {
        Rotation_angle += Time.deltaTime * Speed;
        Save_angle += Rotation_angle;
        Vector3 d = new Vector3(0, Rotation_angle, 0);
        transform.Rotate(d);
        Whether_rotation(d);

        if(Save_angle >= Rotation_range)
        {
            Vector3 c = Center.localEulerAngles;
            Center_angle += Rotation_range;
            c.y = Center_angle;
            Center.localEulerAngles = c;
            Whether_set(c);
            Exit_rotation();
        }
    }

    void Whether_rotation_Left()//����]
    {
        Rotation_angle -= Time.deltaTime * Speed;
        Save_angle += -Rotation_angle;
        Vector3 d = new Vector3(0, Rotation_angle, 0);
        transform.Rotate(d);
        Whether_rotation(d);

        if (Save_angle >= Rotation_range)
        {
            Vector3 c = Center.localEulerAngles;
            Center_angle -= Rotation_range;
            c.y = Center_angle;
            Center.localEulerAngles = c;
            Whether_set(c);
            Exit_rotation();
        }
    }

    void Exit_rotation()//���l�����Z�b�g����֐�
    {
        Rotation_angle = 0;
        Save_angle = 0;
        InputScript.on_rotation = false;
        Right_rotating = false;
        Left_rotating = false;
    }

    void Whether_rotation(Vector3 d)
    {
        for(int i = 0; i < Whether.Length; i++)
        {
            Whether[i].transform.Rotate(-d);
        }
    }

    void Whether_set(Vector3 d)
    {
        for(int i = 0; i < Whether.Length;i++)
        {
            Whether[i].transform.localEulerAngles = -d;
        }
    }

    void Set_rotation()
    {
        InputScript.on_rotation = true;
    }

    //����������͂̔���
    void Get_key()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            Set_rotation();
            Right_rotating = true;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            Set_rotation();
            Left_rotating = true;
        }

        //�������Ȃ������̂ŉ��Ƃ����Ă݂��܂�
        if (Input.GetButtonDown("Right"))
        {
            Set_rotation();
            Right_rotating = true;
        }

        if (Input.GetButtonDown("Left"))
        {
            Set_rotation();
            Left_rotating = true;
        }
    }

}