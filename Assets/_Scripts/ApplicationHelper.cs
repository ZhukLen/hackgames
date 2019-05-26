using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.Events;

public class ApplicationHelper : MonoBehaviour
{
    public UnityEvent OnResetScene;
    [Header("Dont Destroy On Load")]
    public GameObject[] objects;

    public void OnExitBtnClick()
    {
        Application.Quit();
    }

    public void ResetSceneButt()
    {
        OnResetScene.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Awake()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            DontDestroyOnLoad(objects[i]);
        }         
    }
    
}
