using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerDefaultStats", menuName = "PlayerDefaultStats")]
public class PlayerDefaultStats : ScriptableObject
{
    public int maxHealth;
    public int maxLives;
}
