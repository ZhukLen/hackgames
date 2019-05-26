using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [Header("Enemy Colliders tag")]
    public string tag;
    [Header("Tower Health")]
    public int towerHealth = 2000;
    public Text towerHealthUi;
    public Slider towerHealthSlider;
    public ParticleSystem Explousion, Nuke;

    public UnityEvent OnDestroyed, OnHalfHealth;

    private int damage;
    private int maxHealth;
    private bool halfHealth = true;
    private void Start()
    {
        maxHealth = towerHealth;
    }

     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            if(collision.gameObject.GetComponent<Warriors>())
            damage = Mathf.RoundToInt(collision.gameObject.GetComponent<Warriors>().health);
            TakeDamage();
            if (Explousion != null)
            {
                Explousion.gameObject.transform.position = collision.gameObject.transform.position + new Vector3(0, 0.1f, 0);
                Explousion.Play();
            }
            Destroy(collision.gameObject);
            Debug.Log("Tower/Enemy");
        }
    }

    private void TakeDamage()
    {
        towerHealth += -damage;
        if (towerHealthUi != null)
            towerHealthUi.text = towerHealth.ToString();
        if (towerHealthSlider != null)
            towerHealthSlider.value = towerHealth;
        if (towerHealth <= 0)
        {
            Nuke.gameObject.transform.position = gameObject.transform.position;
            Nuke.Play();
            OnDestroyed.Invoke();
        }

        else if (towerHealth <= maxHealth / 2 && halfHealth)
        {
            OnHalfHealth.Invoke();
            halfHealth = false;
        }
    }


}