using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainScene";
    [SerializeField] private GameObject continueButton;
    [SerializeField] UI_FadeScreen fadeScreen;
    private void Start()
    {
        /*if (SaveManager.instance.HasSavedData() == false)
            continueButton.SetActive(false); //没存档就禁用*/
    }
    public void ContinueGame()
    {
        StartCoroutine(LoadSceneWithFadeEffect(2f));
        //SceneManager.LoadScene(sceneName);
    }

    public void NewGame()
    {
        //SaveManager.instance.DeleteSavedData();
        StartCoroutine(LoadSceneWithFadeEffect(2f));
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
    }

    IEnumerator LoadSceneWithFadeEffect(float _delay)  //淡入+加载游戏scene
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }
}
