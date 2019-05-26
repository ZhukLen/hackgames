using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Missle : MonoBehaviour
{
    [Header("Missle Setting")]
    public int damage = 100;
    public float splashRadius = 1f, splashDistanceCoefficient = 1f;
    public SphereCollider splashCollider;
    [Header("Ground Colliders Tag")]
    public string tag;
    [Header("Enemy Colliders Tag")]
    public string enemyTag;
    public UnityEvent OnCollision; 

    private float distance = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            splashCollider.radius = splashRadius;
            StartCoroutine(Splashing());
            OnCollision.Invoke();
        }
    }

    IEnumerator Splashing()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(enemyTag))
        {
            if (other.gameObject.GetComponent<Warriors>())
            {
                distance = Vector3.Distance(other.gameObject.transform.position, gameObject.transform.position);
                other.gameObject.GetComponent<Warriors>().TakeDamage(Mathf.RoundToInt(splashDistanceCoefficient/distance));
            }
        }
    }
}