using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollapsePlatform : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D bx2d;
    private SpriteRenderer sp;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private bool isOn;
    //[SerializeField] private bool isStartCoroutine;
    void Start()
    {
        anim = GetComponent<Animator>();
        bx2d = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();
        isOn = false;
        //isStartCoroutine = false;
    }

    private void Update()
    {
        if (isOn) //��ƽ̨��͸���Ƚ���
        {
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a - fadeSpeed * Time.deltaTime);
            if (sp.color.a <= 0.1)
            {
                bx2d.enabled = false;
            }
        }
        if(!isOn && sp.color.a < 1) //ƽ̨��ԭ
        {
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a + fadeSpeed * Time.deltaTime);
            if (sp.color.a >= 0.9)
            {
                bx2d.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //����
    {
        if(collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            //anim.SetTrigger("Collapse");
            isOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //�뿪
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            isOn = false;
        }
    }

    /*IEnumerator Recover()
    {
        //isStartCoroutine = true;
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a + fadeSpeed * Time.deltaTime);
            if (sp.color.a >= 0.9)
            {
                bx2d.enabled = true;
            }
            /*if(sp.color.a >= 1.0f)
            {
                Debug.Log("end");
                //isStartCoroutine = false;
                yield return Break;
            }
        }
    }*/

    /*void DisableBoxCollider() //��ײ��ʧЧ
    {
        bx2d.enabled = false;
    }

    void DestroyPlatform() //��������
    {
        Destroy(gameObject);
    }*/
}
