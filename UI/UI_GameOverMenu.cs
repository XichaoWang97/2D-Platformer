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
        SceneManager.LoadScene("MainScene");//restartת��������
        //SaveManager.instance.SaveGame(); //��֪���Ƿ����ã�
        //fadeScreen.FadeOut();
    }
    public void MainMenu()
    {
        GameOver.SetActive(false); 
        SceneManager.LoadScene("MainMenu"); //ת����ʼ�˵�����
        //fadeScreen.FadeOut();
    }

    public void setactive(bool active) //��װSetActive����״̬��deadstate������ֱ����SetActive
    {
        GameOver.SetActive(active);
    }
}