using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class Warriors : MonoBehaviour
{
    public int  health = 100;
    public int damage = 25;
    //public int coast = 50;
    [Header("Enemy")]
    public string targetTag;
    public string towerTag;
    public float attackSpeed = 1f;
    public UnityEvent OnDeath;

    [Header("Missle Colliders tag")]
    public string tag;

    Animator _animator;
    bool run = true, attack = false, death = false;
    RaycastHit hit;
    Collider go;
    SoundDethSpawn sound;
    int maxhealth;

    void Start()
    {
        if (GetComponent<Animator>()) _animator = GetComponent<Animator>();
        sound = FindObjectOfType<SoundDethSpawn>();
        _animator.SetBool("Run", true);
        maxhealth = health;
    }

    void FixedUpdate()
    {
        if(run)
            transform.LookAt(GameObject.FindGameObjectWithTag(towerTag).transform);
        if (health <= 0f && !death)
        {
            death = true;
            sound.PlayDethSound();
            OnDeath?.Invoke();
            StopAllCoroutines();
            GameObject.FindGameObjectWithTag(towerTag).GetComponent<SpawnScript>().Gold += maxhealth;
            _animator.SetBool("Death", true);
            Destroy(GetComponent<BoxCollider>());
            Destroy(GetComponent<SphereCollider>());
            Destroy(gameObject, 2f);
        }
        if (go == null)
        {
            run = true;
            _animator.SetBool("Run", true);
            attack = false;
            _animator.SetBool("Attack", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == targetTag && other.GetType() == typeof(BoxCollider))
        {
            run = false;
            _animator.SetBool("Run", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == targetTag && !attack && other.GetType() == typeof(BoxCollider))
        {
            run = false;
            _animator.SetBool("Run", false);
            _animator.SetBool("Attack", true);
            transform.LookAt(other.transform);
            Physics.Linecast(transform.position, other.transform.position, out hit);
            if (hit.collider != null && hit.collider.gameObject.CompareTag(targetTag))
            StartCoroutine(Attack(hit));
        }

    }

    private void OnTriggerExit(Collider other)
    {
        run = true;
        _animator.SetBool("Run", true);
    }

    IEnumerator Attack(RaycastHit hit)
    {
        if (hit.collider.gameObject.CompareTag(targetTag))
        {
            go = hit.collider;
            attack = true;
            while (go.GetComponent<Warriors>().health > 0)
            {
                yield return new WaitForSecondsRealtime(attackSpeed);
                if (go == null)
                {
                    break;
                }
                go.GetComponent<Warriors>().health -= damage; 
            }
            attack = false;
            _animator.SetBool("Attack", false);
            StopAllCoroutines();
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tag) )
        {
            if (collision.gameObject.GetComponent<Missle>())
               health -= collision.gameObject.GetComponent<Missle>().damage;
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }

}
