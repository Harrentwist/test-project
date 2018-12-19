using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivationManager : MonoBehaviour
//для отделения реализацию интерактивных объектов от логики порядка проверки
{
    static private Func<InteractableObjectType, bool> mainDelegate;

    static public void AddDelegate(Func<InteractableObjectType, bool> del)
    {
        mainDelegate = del;
    }
    

    static public bool Activate(InteractableObjectType type)
    {
        bool isTypeCorrect = mainDelegate.Invoke(type);
        return isTypeCorrect;
    }

}
