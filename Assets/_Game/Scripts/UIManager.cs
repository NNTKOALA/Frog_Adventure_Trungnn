using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum UIType { Gameplay =  0, Victory = 1 };

public class UIManager : MonoBehaviour
{

    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    public static int level = 0;

    [SerializeField] GameObject[] panels; 
    [SerializeField] Text strawBerryText;
    [SerializeField] Text victoryStrawBerryText;

    private void Start()
    {
        OpenUI(UIType.Gameplay);
    }

    public void OpenUI(UIType ui)
    {
        CloseAll();
        panels[(int)ui].SetActive(true);
    }

    public void CloseAll() 
    {
        for (int  i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(level);
    }

    public void NextButton()
    {
        level++;
        SceneManager.LoadScene(level);
        /*        SceneManager.LoadScene("Level_1");*/
    }

    public void SetStrawberry(int strawBerry)
    {
        strawBerryText.text = strawBerry.ToString();
        victoryStrawBerryText.text = strawBerry.ToString();
    }
}
