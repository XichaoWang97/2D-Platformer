using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Bounds bounds;
    [SerializeField] private Vector3 stopCameraPosit_R; //相机停的位置
    [SerializeField] private Vector3 stopCameraPosit_L;
    [SerializeField] private Vector3 stopCameraPosit_Up;
    [SerializeField] private Vector3 stopCameraPosit_Down;
    private Vector3 initPosition; //初始位置///////////////////////////
    //[SerializeField] private float boundX => bounds.center.x;
    //private Camera _camera;
    private void Start()
    {
        UpdateStopCameraPosit();
    }

    private void Update()
    {
        if (MoveR.isMoveR)
        {
            TimeStopCameraRightMove();
        }
        if (MoveL.isMoveL)
        {
            TimeStopCameraLeftMove();
        }
        if (MoveUp.isMoveUp)
        {
            TimeStopCameraUpMove();
        }
        if (MoveDown.isMoveDown)
        {
            TimeStopCameraDownMove();
        }

        if (!MoveR.isMoveR && !MoveL.isMoveL && !MoveDown.isMoveDown && !MoveUp.isMoveUp)
        {
            UpdateStopCameraPosit();
        }
    }
    /*IEnumerator TimeStopCameraRightMove() //bug
    {
        Debug.Log("1");
        initPosition = transform.position;
        Time.timeScale = 0;
        while (true)
        {
            transform.position += new Vector3(0.4f, 0, 0);
            if (Player.transform.position.x <= initPosition.x && Player.transform.position.y <= initPosition.y) //人物在相机内才会移动
            {
                Player.transform.position += new Vector3(0.02f, 0, 0); //人物移速，防撞
            }
            if(Vector2.Distance(transform.position, stopCameraPosit_R) < 0.2f)
            {
                break;
            }
        }
        Time.timeScale = 1;
        MoveR.isMoveR = false;
        yield return null;
    }*/
    public void TimeStopCameraRightMove()
    {
        Time.timeScale = 0;
        transform.position += new Vector3(0.6f, 0, 0); //相机移速
        Player.transform.position += new Vector3(0.03f, 0, 0); //人物移速，防撞

        if (Vector2.Distance(transform.position, stopCameraPosit_R) < 0.2f)
        {
            Time.timeScale = 1;
            MoveR.isMoveR = false;
        }
    }
    public void TimeStopCameraLeftMove()
    {
        Time.timeScale = 0;
        transform.position -= new Vector3(0.6f, 0, 0);
        Player.transform.position += new Vector3(-0.04f, 0, 0);

        if (Vector2.Distance(transform.position, stopCameraPosit_L) < 0.2f)
        {
            Time.timeScale = 1;
            MoveL.isMoveL = false;
        }
    }
    public void TimeStopCameraUpMove()
    {
        Time.timeScale = 0;
        transform.position += new Vector3(0, 0.32f, 0);
        Player.transform.position += new Vector3(0, 0.06f, 0);

        if (Vector2.Distance(transform.position, stopCameraPosit_Up) < 0.2f)
        {
            Time.timeScale = 1;
            MoveUp.isMoveUp = false;
        }
    }
    public void TimeStopCameraDownMove()
    {
        Time.timeScale = 0;
        transform.position -= new Vector3(0, 0.32f, 0);
        Player.transform.position -= new Vector3(0, 0.06f, 0);

        if (Vector2.Distance(transform.position, stopCameraPosit_Down) < 0.2f)
        {
            Time.timeScale = 1;
            MoveDown.isMoveDown = false;
        }
    }

    public void UpdateStopCameraPosit()
    {
        stopCameraPosit_R = new Vector3(transform.position.x + 30f, transform.position.y, transform.position.z);
        stopCameraPosit_L = new Vector3(transform.position.x - 30f, transform.position.y, transform.position.z);
        stopCameraPosit_Up = new Vector3(transform.position.x, transform.position.y + 18f, transform.position.z);
        stopCameraPosit_Down = new Vector3(transform.position.x, transform.position.y - 18f, transform.position.z);
    }
}
