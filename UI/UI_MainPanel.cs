using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MainPanel : MonoBehaviour
{
    [SerializeField] private Button quitGame;
    [SerializeField] private AudioController controller;
    
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private TextMeshProUGUI bgmValue;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI sfxValue;

    private void Awake()
    {
        bgmSlider.value = 0.5f; //音量初始大小
        sfxSlider.value = 0.5f;
    }

    private void Start()
    {
        quitGame.onClick.AddListener(() => //监听退出游戏按键
        {
            if (quitGame != null)
                Application.Quit();
            else
                return;
        });
    }
    
    private void Update()
    {
        controller.BgmAudio.volume = bgmSlider.value; //bgm
        bgmValue.text = Mathf.Round(bgmSlider.value * 100f).ToString();

        controller.SfxAudio.volume = sfxSlider.value; //sfx
        sfxValue.text = Mathf.Round(sfxSlider.value * 100f).ToString();
    }
    /*[SerializeField] private Button CharacterButton;
    [SerializeField] private Button EquipmentButton;
    [SerializeField] private Button PackageButton;
    [SerializeField] private Button SkillButton;
    [SerializeField] private Button SystemButton;
    [SerializeField] private Button SettingButton;
    private UI ui;

    private void Start()
    {
        ui = GetComponentInParent<UI>();

        //点击事件注册在start里，update里会多次执行点击
        CharacterButton.onClick.AddListener(() =>
        {
            if (CharacterButton != null)
            {
                ui.SwitchToCharacter();
            }
            else
                return;
        }); 
        EquipmentButton.onClick.AddListener(() =>
        {
            if (EquipmentButton != null)
            {
                ui.SwitchToEquipment();
            }
            else
                return;
        });
        PackageButton.onClick.AddListener(() =>
        {
            if (PackageButton != null)
            {
                ui.SwitchToPackage();
            }
            else
                return;
        });
        SkillButton.onClick.AddListener(() =>
        {
            if (SkillButton != null)
            {
                ui.SwitchToSkill();
            }
            else
                return;
        });
        SystemButton.onClick.AddListener(() =>
        {
            if (SystemButton != null)
            {
                ui.SwitchToSystem();
            }
            else
                return;
        });
        SettingButton.onClick.AddListener(() =>
        {
            if (SettingButton != null)
            {
                ui.SwitchToSetting();
            }
            else
                return;
        });
    }*/
}