using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Weapon : MonoBehaviour {
    public bool IsHit { get; private set; }

    void Start()
    {
        IsHit = false;
    }

    private void OnCollisionEnter(Collision collision) //если игрок ударят по вазе, то запускается соответствующий ивент, на который подписан объект вазы
    {
        if (IsHit)
        {
            if (collision.gameObject.tag == "Jar")
            {
                GameManager.instance.OnWeaponHit.Invoke();
                IsHit = false;
            }
        }
    }

    public void WeaponHit()
    {
        IsHit = true;
    }

    public void EndWeaponHit()
    {
        IsHit = false;
    }
}
