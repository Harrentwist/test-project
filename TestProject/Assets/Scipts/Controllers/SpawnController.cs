using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] obeliskPrefab;
    [SerializeField] private GameObject player;

    private List<obeliskTypes> activationOrder;
    int iterator = 0;
    private List<GameObject> prefabs = new List<GameObject>();



    void Awake()
    {
        prefabSpawn();
        activationOrder = createOrderList();
        activationManager.addDelegate(activationOrderCheck);
        foreach(var v in activationOrder)
        {
            Debug.Log(v);
        }
    }


    void prefabSpawn() //спавн трех обелисков + игрок
    {
        if (obeliskPrefab.Length + 1 <= spawnPoints.Length)
        {
            List<int> randomList = Randomizer.randomizeList(obeliskPrefab.Length + 1, spawnPoints.Length);
            for (int i = 0; i < obeliskPrefab.Length; ++i)
            {
                GameObject obj = Instantiate(obeliskPrefab[i], spawnPoints[randomList[i]]) as GameObject;
                prefabs.Add(obj);
            }

            Instantiate(player, spawnPoints[randomList[randomList.Count - 1]]);
        }

        else
            Debug.Log("There is too much prefabs!");
    }

    bool activationOrderCheck(obeliskTypes id) // проверка, в правильном ли порядке активирован обелиск
    {
        if (iterator <= activationOrder.Count)
        {
            if (id == activationOrder[iterator])
            {
                ++iterator;
                print("Activation # " + iterator + "successful");
                if(iterator == 3)
                {
                    StartCoroutine(ReloadScene());
                }
                return true;
            }

            else
            {
                Debug.Log("wrong object");
                iterator = 0;
                Reset();
            }
            return false;
        }

        return false;
    }

    List<obeliskTypes> createOrderList() // создание списка с рандомным порядком активации обелисков
    {
        List<obeliskTypes> obelOrder = new List<obeliskTypes>(obeliskPrefab.Length);
        List<int> randomOrder = Randomizer.randomizeList(obeliskPrefab.Length, obeliskPrefab.Length);
        for(int i = 0; i < randomOrder.Count; ++i)
        {
            obelOrder.Add((obeliskTypes)randomOrder[i]);
        }
        return obelOrder;
    }

    private void Reset() //сброс последовательности, если активирован неправильный обелиск
    {
       for(int i = 0; i < prefabs.Count; ++i)
        {
            obelState state = prefabs[i].GetComponent<obelState>();
            state.isActivated = false;
            iterator = 0;
        }
        Debug.Log("reseted");
    }

    public List<obeliskTypes> getOrderList()
    {
        return activationOrder;
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Scene");
    }
}



