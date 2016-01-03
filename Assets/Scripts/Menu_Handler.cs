using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu_Handler : MonoBehaviour {
    Renderer rend;
    [SerializeField]bool isQuit;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnMouseUp()
    {
        if (isQuit)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(1);
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
}
