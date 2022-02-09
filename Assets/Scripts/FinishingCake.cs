using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    //Проверка торта в соответсвии с заданием
public class FinishingCake : MonoBehaviour
{
    public FinishTask.FinishState CheckCakes(Cake createdCake, Cake taskCake)
    {
        int createdCakeLeght = createdCake.GetIndexLastCake() + 1;
        if (createdCakeLeght <= 0)
            return FinishTask.FinishState.CakeFinishedIncorrect;
        for (int i = 0; i < createdCakeLeght; i++)
        {
            if (createdCake.GetCakeBase(i).prefab == null || !createdCake.GetCakeBase(i).prefab.Equals(taskCake.GetCakeBase(i).prefab))
                return FinishTask.FinishState.CakeFinishedIncorrect;
            if (createdCake.GetCakeMaterial(i) == null || !createdCake.GetCakeMaterial(i).Equals(taskCake.GetCakeMaterial(i)))
                return FinishTask.FinishState.CakeFinishedIncorrect;
            if (createdCake.GetGlazePrefab(i).prefab == null || !createdCake.GetGlazePrefab(i).prefab.Equals(taskCake.GetGlazePrefab(i).prefab))
                return FinishTask.FinishState.CakeFinishedIncorrect;
        }

        if (createdCake.GetDecoration().prefab == null || !createdCake.GetDecoration().prefab.Equals(taskCake.GetDecoration().prefab))
            return FinishTask.FinishState.CakeFinishedIncorrect;
        return FinishTask.FinishState.CakeFinishedCorrect;
    }
}
