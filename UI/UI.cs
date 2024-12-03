using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.TextCore.Text;

public class UI : MonoBehaviour//, ISaveManager
{

    /*[SerializeField] private GameObject skillUI;
    [SerializeField] private GameObject systemUI;
    [SerializeField] private GameObject settingUI;
    [SerializeField] private GameObject gameoverUI;
    public UI_ItemToolTip itemToolTip;
    public UI_SkillToolTip skillToolTip; //文字说明部分
    public UI_ItemToolTip itemToolTip;
    public UI_statToolTip statToolTip;
    public UI_CraftWindow craftWindow;*/

    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject mainUI;
    public GameObject NowOpenUI; //记录现在正在打开的ui

    [Header("Died Menu")]
    [SerializeField] private UI_FadeScreen fadeScreen;

    public static bool GameIsPaused = false;

    private void Start()
    {
        /*fadeScreen.gameObject.SetActive(true);
        skillUI.gameObject.SetActive(false);
        systemUI.gameObject.SetActive(false);
        settingUI.gameObject.SetActive(false);
        gameoverUI.gameObject.SetActive(false);*/

        /*itemToolTip = GetComponentInChildren<UI_ItemToolTip>();
        skillToolTip.gameObject.SetActive(false);*/

        mainUI.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(true);
        
        NowOpenUI = inGameUI;
    }

    private void Update()
    {
        //按键盘转到相应UI
        if (Input.GetKeyDown(KeyCode.Escape)) //esc到主界面
        {
            if(NowOpenUI == inGameUI)
            {
                NowOpenUI.SetActive(false);
                PauseGame();
                mainUI.SetActive(true);
                NowOpenUI = mainUI;
                return;
            }
            else
            {
                NowOpenUI.SetActive(false);
                NowOpenUI = inGameUI;
                NowOpenUI.SetActive(true);
                PauseGame();
            }
        } //esc调用菜单

        if (NowOpenUI != inGameUI)
        {
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (NowOpenUI != mainUI)
                {
                    NowOpenUI.SetActive(false);
                    mainUI.SetActive(true);
                    NowOpenUI = mainUI;
                }
                else
                {
                    NowOpenUI.SetActive(false);
                    NowOpenUI = inGameUI;
                    NowOpenUI.SetActive(true);
                    PauseGame();
                }
            }
        } //右键退出当前panel
    }

    private static void PauseGame()
    {
        if (GameIsPaused)
        {
            Time.timeScale = 1f; //时间正常
            GameIsPaused = false;
        }
        else
        {
            Time.timeScale = 0f; //冻结时间
            GameIsPaused = true;
        }
    }

    public void SwitchOnEndScreen()
    {
        fadeScreen.FadeOut();
        StartCoroutine(EndScreenCorutione());
    }
    IEnumerator EndScreenCorutione()
    {
        yield return new WaitForSeconds(2f);
        //gameoverUI.SetActive(true);
    }

    /*private void CheckForInGameUI()
    {
        for(int i = 0 ; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).GetComponent<UI_FadeScreen>() == null)
                return;
        }
        SwitchTo(inGameUI);
    }*/

    #region 打开各种ui，供其他脚本调用
    public void SwitchToMain()
    {
    }
    /*public void SwitchToCharacter()
    {
        mainUI.SetActive(false);
        characterUI.SetActive(true);
        NowOpenUI = characterUI;
    }
    public void SwitchToEquipment()
    {
        mainUI.SetActive(false);
        equipmentUI.SetActive(true);
        NowOpenUI = equipmentUI;
    }
    public void SwitchToPackage()
    {
        mainUI.SetActive(false);
        packageUI.SetActive(true);
        NowOpenUI = packageUI;
    }
    public void SwitchToSkill()
    {
        mainUI.SetActive(false);
        skillUI.SetActive(true);
        NowOpenUI = skillUI;
    }
    public void SwitchToSystem() 
    {
        mainUI.SetActive(false);
        systemUI.SetActive(true);
        NowOpenUI = systemUI;
    }
    public void SwitchToSetting()
    {
        mainUI.SetActive(false);
        settingUI.SetActive(true);
        NowOpenUI = settingUI;
    }*/

    /*public void LoadData(GameData _data) //声音读取
    {
        foreach(KeyValuePair<string, float> pair in _data.volumeSettings)
        {
            foreach(UI_Audio item in volumeSettings)
            {
                if(item.parameter == pair.Key)
                    item.LoadSlider(pair.Value);
            }
        }
    }

    public void SaveData(ref GameData _data) //声音保存
    {
        _data.volumeSettings.Clear();
        foreach(UI_Audio item in volumeSettings)
        {
            _data.volumeSettings.Add(item.parameter, item.slider.value);
        }
    }*/
    #endregion
}