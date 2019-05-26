using UnityEngine;

public class Fire_ARCamera : MonoBehaviour
{
    [Header("Missle Prefab")]
    public GameObject misslePrefab;

    public int coast = 1;
    [Header("Missle Spawn Point")]
    public Transform spawnPoint;
    [Header("Physics Setting")]
    public float gravityCoefficient = 9.81f;

    public string GroundTag = "Ground";

    public GameObject OurSpawnScript;
    
    RaycastHit Hit;
    Ray ray;
    Vector3 target;
    GameObject clone;

    void FixedUpdate()
    {
#if UNITY_ANDROID || UNITY_IOS      
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touches.Length; i++)
            {
                if (Input.touches[i].phase == TouchPhase.Began)
                {
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                    if (Physics.Raycast(ray, out Hit))
                    {
                        if (Hit.collider.gameObject.CompareTag(GroundTag))
                        {
                            target = Hit.point;
                            Fire(target);
                        }
                    }
                }
            }
        }
#endif
        //For Debuging
#if UNITY_EDITOR || UNITY_WINDOWS
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out Hit))
            {
                if (Hit.collider.gameObject.CompareTag(GroundTag))
                {
                    target = Hit.point;
                    Fire(target);
                }
            }
        }

#endif
    }

    void Fire(Vector3 target)
    {
        if (OurSpawnScript.GetComponent<SpawnScript>().Gold >= coast)
        {
            OurSpawnScript.GetComponent<SpawnScript>().Gold -= coast;
            Physics.gravity = new Vector3(0f, -gravityCoefficient, 0f);
            clone = Instantiate(misslePrefab, spawnPoint.transform.position, Quaternion.identity);
            float X = Vector3.Distance(Vector3.zero, target);
            float impulse = Mathf.Sqrt((gravityCoefficient * X * X) / (spawnPoint.transform.position.y * 2)) * clone.GetComponent<Rigidbody>().mass;
            clone.GetComponent<Rigidbody>().AddForce(new Vector3(target.x, 0, target.z).normalized * impulse, ForceMode.Impulse);
        }
    }
}