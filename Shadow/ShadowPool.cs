using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{

    public static ShadowPool _instance;//����
    public GameObject shadowprefebs;//Ԥ����
    public int shadowcount;//����
    private Queue<GameObject> availableObjects = new Queue<GameObject>();
    private void Awake()
    {
        _instance = this;
        //��ʼ�������
        GetFormPool();
    }
    public void FillPool()
    {
        //��������
        for (int i = 0; i < shadowcount; i++)
        {
            var newshadow = Instantiate(shadowprefebs);
            newshadow.transform.SetParent(transform);
            //ȡ������ ���ض����
            returnpool(newshadow);
        }
    }
    public void returnpool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        availableObjects.Enqueue(gameObject);//��ӵ����е�ĩ��
    }
    public GameObject GetFormPool()
    {
        if (availableObjects.Count == 0)
        {
            //������
            FillPool();
        }
        var outshadow = availableObjects.Dequeue();//�ӿ�ͷ���һ��
        outshadow.SetActive(true);//����enable�еķ���
        return outshadow;
    }
}
