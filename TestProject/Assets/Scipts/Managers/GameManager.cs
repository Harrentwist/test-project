using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UnityEvent OnWrongOrder; //ивент, сигнализирующий о нарушении порядка активации объектов
    public UnityEvent OnWeaponHit; //ивент, сигнализирующий об атаке оружием

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] InteractablesPrefabs;
    [SerializeField] private GameObject player;

    private List<InteractableObjectType> activationOrder;
    int iterator = 0;
    private List<GameObject> prefabs = new List<GameObject>(); //список инстанцированных префабов



    void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }

        else if(instance = this)
        {
            Destroy(gameObject);
        }

        Initialization();
        #endregion
    }


    void PrefabSpawn() //спавн трех обелисков + игрока
    {
        if (InteractablesPrefabs.Length + 1 <= spawnPoints.Length)
        {
            List<int> randomList = Randomizer.RandomizeList(InteractablesPrefabs.Length + 1, spawnPoints.Length);
            for (int i = 0; i < InteractablesPrefabs.Length; ++i)
            {
                GameObject obj = Instantiate(InteractablesPrefabs[i], spawnPoints[randomList[i]]) as GameObject;
                prefabs.Add(obj);
            }

            Instantiate(player, spawnPoints[randomList[randomList.Count - 1]]);
        }

        else
            Debug.Log("There is too much prefabs!");
    }

    bool ActivationOrderCheck(InteractableObjectType id) // проверка, в правильном ли порядке активирован обелиск
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

    List<InteractableObjectType> CreateOrderList() // создание списка с рандомным порядком активации обелисков
    {
        List<InteractableObjectType> activationOrder = new List<InteractableObjectType>(InteractablesPrefabs.Length);
        List<int> randomOrder = Randomizer.RandomizeList(InteractablesPrefabs.Length, InteractablesPrefabs.Length);
        for(int i = 0; i < randomOrder.Count; ++i)
        {
            activationOrder.Add((InteractableObjectType)randomOrder[i]);
        }
        return activationOrder;
    }

    private void Reset() //сброс последовательности, если активирован неправильный обелиск
    {
        iterator = 0;
        OnWrongOrder.Invoke();
        Debug.Log("reseted");
    }

    public List<InteractableObjectType> GetOrderList() //метод, возвращающий список с очередностью активации объектов
    {
        return activationOrder;
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Scene");
    }

    private void Initialization()
    {
        PrefabSpawn();
        activationOrder = CreateOrderList();
        ActivationManager.AddDelegate(ActivationOrderCheck);
        Cursor.visible = false;

        if(OnWrongOrder == null)
        {
            OnWrongOrder = new UnityEvent();
        }

        if (OnWeaponHit == null)
        {
            OnWeaponHit = new UnityEvent();
        }
    }
}



