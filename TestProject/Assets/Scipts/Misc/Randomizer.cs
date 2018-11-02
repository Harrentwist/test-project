using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Randomizer
{
    static public List<int> randomizeList(int listLength, int maxRange) //возвращает список с рандомно сгенерированным числами
    {
        List<int> randomList = new List<int>
        {
            randomize(maxRange)
        };

        while (randomList.Count != listLength)
        {
            int randomNumber = randomize(maxRange);
            if (!randomList.Contains(randomNumber))
            {
                randomList.Add(randomNumber);
            }
        }
        return randomList;
    }

    static public int randomize(int range) // возвращает рандомное число
    {
        int randomValue = Random.Range(0, range);
        return randomValue;
    }
}
