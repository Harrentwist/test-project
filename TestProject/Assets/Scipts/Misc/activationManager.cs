using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activationManager : MonoBehaviour
{

    static private Func<obeliskTypes, bool> controllerDelegate;

    static public void addDelegate(Func<obeliskTypes, bool> del)
    {
        controllerDelegate = del;
    }

    static public bool activate(obeliskTypes obel)
    {
        bool isCorrect = controllerDelegate.Invoke(obel);
        return isCorrect;
    }

}
