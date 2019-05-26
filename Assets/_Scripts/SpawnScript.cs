using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SpawnScript : MonoBehaviour
{
    [System.Serializable]
    public struct PrefabsStruct
    {
        public GameObject Prefab;
        public string UnitTag;
        public string TargetTag;
        public string TowerTag;
        public int UnitCost;

        public PrefabsStruct(GameObject _pref, string _unitTag, string _targetTag, string _towerTag, int _unitCost)
        {
            Prefab = _pref;
            UnitTag = _unitTag;
            TargetTag = _targetTag;
            TowerTag =_towerTag;
            UnitCost = _unitCost;
        }

    }
    public PrefabsStruct[] pref;
    private int activePrefab = 0;

    [Header("Units Spawn Points")]
    public Transform[] spawnPoints;
    private int activePoint = 0;

    [Header("Enemies Spawn Interval")]
    public float minT = 0f, maxT = 1f;

    public int Gold = 1500;
    public GameObject ScaleObject;
    public Text GoldText;
    public bool toggleSpawn = false;
    public bool toggleIncome = false;
    public UnityEvent OnSpawn;

    private void Update()
    {
        if (GoldText != null)
        GoldText.text = Gold.ToString();
    }

    public void StartSpawn()
    {
        if (!toggleSpawn)
        {
            StartCoroutine(Spawn(maxT));
            toggleSpawn = true;
        }
    }


    IEnumerator Spawn(float T)
    {
        yield return new WaitForSecondsRealtime(T);
        int i;
        Debug.Log("Circle In!");
        do
        {
            i = Random.Range(0, pref.Length);
            yield return null;
        } while (Gold < pref[i].UnitCost);
        Debug.Log("Circle out!");
         InstantiateUnit(i);
        StartCoroutine(Spawn(Random.Range(minT, maxT)));
    }

    public void StartIncome()
    {
        if (!toggleIncome)
        {
            StartCoroutine(Income(2f));
            toggleIncome = true;
        }
    }


    IEnumerator Income(float T)
    {
        yield return new WaitForSecondsRealtime(T);
        Gold += 5;
        StartCoroutine(Income(2f));
    }

    public void InstantiateUnit(int i)
    {

        activePrefab = i;
        if (Gold >= pref[activePrefab].UnitCost)
        {
            if (activePoint > 2)
            {
                activePoint = 0;
            }
            GameObject newPrefab = Instantiate(pref[activePrefab].Prefab, spawnPoints[activePoint].position, Quaternion.identity, ScaleObject.transform);
            newPrefab.transform.localScale = new Vector3(newPrefab.transform.localScale.x / ScaleObject.transform.localScale.x, newPrefab.transform.localScale.y / ScaleObject.transform.localScale.y, newPrefab.transform.localScale.z / ScaleObject.transform.localScale.z);

            OnSpawn?.Invoke();

            newPrefab.tag = pref[activePrefab].UnitTag;
            newPrefab.GetComponent<Warriors>().targetTag = pref[activePrefab].TargetTag;
            newPrefab.GetComponent<Warriors>().towerTag = pref[activePrefab].TowerTag;

            Gold -= pref[activePrefab].UnitCost;
            activePoint++;
        }
    }
}