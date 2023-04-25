using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public List<CharacterBasics> levelClientele;
    public int levelNum;
    public int maxScore;
}
