using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartCount : MonoBehaviour
{
    public Text startCount;
    public UnityEvent OnVictory, OnDefeat, OnStopCount;

    public GameObject ButtonHolder;

    bool toggle = false;
    Tower player, enemy;

    private string[] count = { "3", "2", "1", "Fight", "VS"};

    private void Start()
    {
        player = GameObject.Find("PlayerTower").GetComponent<Tower>();
        enemy = GameObject.Find("EnemyTower").GetComponent<Tower>();
    }


    private void Update()
    {
        if (GameObject.Find("EnemyTower") != null && GameObject.Find("PlayerTower") != null)
        {
            if (player.towerHealth <= 0)
            {
                OnDefeat?.Invoke();
            }
            else if (enemy.towerHealth <= 0)
            {
                OnVictory?.Invoke();
            }
        }


    }

    public void Wait()
    {
        if (!toggle)
        {
            ButtonHolder.SetActive(false);
            StartCoroutine(StartGame());
            toggle = true;
        }

    }


    public IEnumerator StartGame()
    {
        startCount.enabled = true;
        foreach (string i in count)
        {
            if (i == "VS")
            {
                startCount.enabled = false;
                OnStopCount.Invoke();
                break;
            }
            startCount.text = i;
            yield return new WaitForSeconds(0.7f);
        }
        ButtonHolder.SetActive(true);
        StopCoroutine(StartGame());
    }
}