using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{

    public static ShadowPool _instance;//单例
    public GameObject shadowprefebs;//预制体
    public int shadowcount;//数量
    private Queue<GameObject> availableObjects = new Queue<GameObject>();
    private void Awake()
    {
        _instance = this;
        //初始化对象池
        GetFormPool();
    }
    public void FillPool()
    {
        //填满池子
        for (int i = 0; i < shadowcount; i++)
        {
            var newshadow = Instantiate(shadowprefebs);
            newshadow.transform.SetParent(transform);
            //取消启用 返回对象池
            returnpool(newshadow);
        }
    }
    public void returnpool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        availableObjects.Enqueue(gameObject);//添加到队列的末端
    }
    public GameObject GetFormPool()
    {
        if (availableObjects.Count == 0)
        {
            //填充池子
            FillPool();
        }
        var outshadow = availableObjects.Dequeue();//从开头获得一个
        outshadow.SetActive(true);//启动enable中的方法
        return outshadow;
    }
}
