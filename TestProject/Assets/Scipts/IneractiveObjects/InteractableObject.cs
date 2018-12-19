using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class InteractableObject : MonoBehaviour {
    public InteractableObjectType objType { get; set; }

    //префабы частиц 
    public GameObject particleSuccess;
    public GameObject particleFail;

    public bool IsActivated { get; protected set; } //активирован ли обелиск

    protected GameObject particle; //инстанс частиц

    void Start()
    {
        IsActivated = false;
        particle = null;
    }

    public InteractableObject(InteractableObjectType type) //в конструкторе задается соответсвующий тип интерактивного объекта
    {
        objType = type;
    }

    protected bool OrderCorrectnessCheck() //проверка на соответствие активируемого объекта свой позиции в очереди
    {
        bool isCorrectOrder = ActivationManager.Activate(objType);
        return isCorrectOrder;
    }

    protected virtual void ResetParticles() //в случае неверного порядка активации системы частиц уничтожаюся, а статус активированных объектов переходит в неактивный
    {
        if(particle != null)
        {
            Destroy(particle);
        }
        IsActivated = false;
    }

    protected void Activate() //активация интерактивного объекта
    {
        bool isOrderCorrect = OrderCorrectnessCheck();
        if (isOrderCorrect)
        {
            particle = Instantiate(particleSuccess, gameObject.transform);
            IsActivated = true;
        }

        else
            Instantiate(particleFail, gameObject.transform);
    }
}
