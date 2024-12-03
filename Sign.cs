using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject dialogBox;
    public TextMeshProUGUI dialogBoxText;
    public string signText;
    private bool isPlayerSign;
    [SerializeField] private GameObject prompt;

    void Start()
    {
        //dialogBoxText.text = signText;
        prompt.SetActive(false);
    }

    void Update()
    {
        dialogBoxText.text = signText;
        if (Input.GetKeyDown(KeyCode.E) && isPlayerSign)  //按e弹出对话框
        { 
            dialogBox.SetActive(true);
            prompt.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1) && isPlayerSign)  //鼠标点击退出对话框
        {
            dialogBox.SetActive(false);
            //prompt.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerSign = true;
            prompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerSign = false;
            if (dialogBox)
            {
                dialogBox.SetActive(false);
            }
            prompt.SetActive(false);
        }
    }
}
