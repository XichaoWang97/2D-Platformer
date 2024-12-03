using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverMenu : MonoBehaviour
{
    public GameObject GameOver;
    [SerializeField] UI_FadeScreen fadeScreen;

    public void Restart()
    {
        GameOver.SetActive(false);
        SceneManager.LoadScene("MainScene");//restart转到主场景
        //SaveManager.instance.SaveGame(); //不知道是否能用？
        //fadeScreen.FadeOut();
    }
    public void MainMenu()
    {
        GameOver.SetActive(false); 
        SceneManager.LoadScene("MainMenu"); //转到开始菜单场景
        //fadeScreen.FadeOut();
    }

    public void setactive(bool active) //封装SetActive用于状态机deadstate，不能直接用SetActive
    {
        GameOver.SetActive(active);
    }
}