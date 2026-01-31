using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct DialogueStruct
{
    public string UID; 
    public string npcText;
    public List<OptionStruct> options;

}