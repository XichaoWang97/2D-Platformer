using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RebornSite : MonoBehaviour
{
    public bool isActive; //ÊÇ·ñ±»¼¤»î
    private Animator anim;
    private Light2D light;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        anim = GetComponent<Animator>();
        light = GetComponentInChildren<Light2D>();
        light.intensity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            //light.intensity = Random.Range(0.7f, 0.8f);
            light.intensity = 0.6f + Mathf.Sin(Time.time) * 0.1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActive = true;
            anim.SetBool("active", true);
        }
    }
}
