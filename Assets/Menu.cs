using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject menuObject;
    void Awake(){
        menuObject.SetActive(false);

        if(Application.platform == RuntimePlatform.WebGLPlayer ){
            GameObject.Find("Quit").SetActive(false); 
        }
    }

    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Escape)){
            menuObject.SetActive( !menuObject.activeSelf );
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("main");
    }

    public void Quit(){
        Application.Quit();
    }
}
