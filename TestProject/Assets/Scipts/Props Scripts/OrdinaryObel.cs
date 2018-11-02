﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Данный скрипт отслеживает, сделал ли игрок полный оборот вокруг обелиска обыденности
 * Вокруг обелиска расположена сферическая зона-триггер, радиусом в 5м. 
 * Игрок может зайти в триггер с любого направления, его первая позиция внутри сохраняется в plrPosition.
 * Далее при помощи Vector3.SignedAngle устанавливается угол между первой позицией игрока в зоне и актуальным местоположением внутри триггера.
 * Так как мин/макс значения, возвращаемыеVector3.SignedAngle не выходят за рамки 180/-180 градусов,
 * то при достижении отметки, близкой к 180/-180 градусам (пол оборота вокруг обелиска), 
 * информация в plrPosition заменятся на положение игрока после половины круга,
 * после чего при помощи Vector3.SignedAngle снова высчитывается угол между сохраненной позицией и актуальным местоположением.
 * Игрок может обойти обелиск как по часовой, так и против часовой стрелки. Также, если игрок пройдет всего половину круга, а потом пойдет в обратном направлении,
 * то ему все равно придется совершить полный круг, чтобы активировать обелиск*/

public class OrdinaryObel : MonoBehaviour
{
    public obeliskTypes objType = obeliskTypes.column;
    public GameObject particleSuccess;
    public GameObject particleFail;

    private Vector3 plrPosition;
    private float halfCircle; //данные о том, совершил ли игрок половину оборота
    private bool result;
    private bool isStayActive;
    private GameObject particle;
    public obelState state { get; set; }

    void Start ()
    {
        isStayActive = false;
        state = gameObject.GetComponentInParent<obelState>();
    }

    void Update()
    {
        if (particle != null && !state.isActivated)
        {
            Destroy(particle);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isStayActive)
        {
            // угол между первой позицией игрока при входе в триггер и актуальной позицией при перемешении
            float temp = Vector3.SignedAngle(plrPosition, other.transform.position-transform.position, Vector3.up); 

            if (temp > 165f && halfCircle == 0) // если игрок прошел половину круга по часовой стрелке
            {
                {
                    plrPosition = other.transform.position - transform.position;
                    halfCircle = 165f;
                    temp = 0;
                }
            }
            if (temp < -165f && halfCircle == 0) // если игрок прошел половину круга против часовой стрелке
            {
                {
                    plrPosition = other.transform.position - transform.position;
                    halfCircle = -165f;
                    temp = 0;
                }
            }
            if (halfCircle == 165f && temp > 165f && !state.isActivated) // если игрок совершил полный оборот по часовой стрелке
            {
                result = activationManager.activate(objType);
                activate(result);
            }
            else if (halfCircle == 165f && temp < -165f) // если игрок прошел полкруга по часовой стрелке, но повернул назад и прошел еще половину
            {
                plrPosition = other.transform.position - transform.position;
                halfCircle = -165f;
                temp = 0;
            }
            if(halfCircle == -165f && temp < -165f && !state.isActivated) // если игрок совершил полный оборот против часовой стрелке
            {
                result = activationManager.activate(objType);
                activate(result);
            }

            if (halfCircle == -165f && temp > 165f) // // если игрок прошел полкруга против часовой стрелки, но повернул назад и прошел еще половину
            {
                plrPosition = other.transform.position - transform.position;
                halfCircle = 165f;
                temp = 0;
            }
            //print("half: " + halfCircle + " / " + "temp: " + temp);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        plrPosition = other.transform.position - transform.position;
        isStayActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        halfCircle = 0;
        isStayActive = false;
    }

    private void activate(bool res)
    {
        if (res)
        {
            particle = Instantiate(particleSuccess, gameObject.transform);
            state.isActivated = true;
        }

        else
            Instantiate(particleFail, gameObject.transform);
        isStayActive = false;
    }
    
}