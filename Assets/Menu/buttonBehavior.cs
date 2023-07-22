using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class buttonBehavior : MonoBehaviour
{
    public string boolName;
    public string sceneToChange;
   
    public void OnMouseOver()
    {
        gameObject.GetComponent<Animator>().SetBool(boolName, true);
        Debug.Log("a");
    }

    public void OnMouseExit()
    {
        gameObject.GetComponent<Animator>().SetBool(boolName, false);
    }

    public void OnClick()
    {
        SceneManager.LoadScene(sceneToChange);
        
    }

    public void ForExitButton()
    {
        Application.Quit();
    }
}
