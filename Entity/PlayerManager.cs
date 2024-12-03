using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour//, ISaveManager
{
    public static PlayerManager Instance;
    public Player player;

    //public int currency;
    /*public void LoadData(GameData _data)
    {
        this.currency = _data.currency;
    }

    public void SaveData(ref GameData _data)
    {
        _data.currency = this.currency;
    }*/

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    /*public bool HaveEnoughMoney(int _price)
    {
        if(_price > currency) //没钱解锁技能
        {
            return false;
        }
        currency -= _price;
        return true; 
    }*/
}
