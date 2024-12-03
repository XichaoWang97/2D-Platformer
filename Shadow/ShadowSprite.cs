using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer thissprite; //��ǰ��sprite
    private SpriteRenderer playersprite; //��ҵ�sprite
    private Color color;

    [Header("ʱ����Ʋ���")]
    public float activetime; //������ڵ�ʱ��
    public float activestart; //��ʼ��ʱ��

    [Header("��͸���Ȳ���")]
    private float alpha;
    public float alphamul; //�仯���ٶ�
    public float alphaset; //��ʼֵ

    private void OnEnable()
    {
        player = GameObject.Find("player").transform;
        thissprite = GetComponent<SpriteRenderer>();
        thissprite.sortingOrder = 100;
        playersprite = player.GetComponent<SpriteRenderer>();
        alpha = alphaset; //���ó�ʼֵ
        thissprite.sprite = playersprite.sprite; //����ͼ��ֵ
        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation; //��λ�� ��ת ��С���Ŷ�ͬ��
        activestart = Time.time; //ʵʱ��¼��ʼ��ʱ��
    }

    private void Update()
    {
        alpha = alpha * alphamul; //��Ӱ�Ĳ�͸����Խ��Խ��
        color = new Color(1, 1, 1, alpha);
        thissprite.color = color; //��ɫ

        if (Time.time > activestart + activetime)
        {
            ShadowPool._instance.returnpool(this.gameObject);  //���ض����
        }
    }
}
