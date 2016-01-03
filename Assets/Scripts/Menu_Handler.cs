using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu_Handler : MonoBehaviour {
    Renderer rend;
    [SerializeField]bool isQuit,isShort,isMedium,isLong,isStartButton,isBack;
    [SerializeField]GameObject time_selector;
    private Renderer time_menu_renderer;
    void Start()
    {
        rend = GetComponent<Renderer>();
        if (isStartButton || isBack)
        {
            time_menu_renderer = time_selector.GetComponent<Renderer>();
            showOrHideTimeSelect(false);
        }
    }

    void OnMouseUp()
    {
        if (isQuit)
        {
            Application.Quit();
        }
        else
        {
            if (isStartButton)
            {
                showOrHideTimeSelect(true);
            }
            else if (isBack)
            {
                showOrHideTimeSelect(false);
            }
            else if (isShort)
            { 
                PlayerPrefs.SetInt("GameLength", 30);
                SceneManager.LoadScene(1);
            }
            else if (isMedium)
            {
                PlayerPrefs.SetInt("GameLength", 60);
                SceneManager.LoadScene(1);
            }
            else if (isLong)
            {
                PlayerPrefs.SetInt("GameLength", 90);
                SceneManager.LoadScene(1);
            }
        }
    }

    void OnMouseEnter()
    {
        rend.material.color = Color.green;
    }

    void OnMouseExit()
    {
        rend.material.color = Color.red;
    }

    void showOrHideTimeSelect(bool show)
    {
        Renderer[] lChildRenderers = time_selector.GetComponentsInChildren<Renderer>();
        foreach (Renderer lRenderer in lChildRenderers)
        {
            lRenderer.enabled = show;
        }
    }
}
