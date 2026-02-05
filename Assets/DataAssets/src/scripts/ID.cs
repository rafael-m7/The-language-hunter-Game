
using System;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "ID", menuName = "IDS Game")]
public class ID : ScriptableObject
{
    [SerializeField] public string ID_;

    public string GetID()
    {
        if (ID_.Length == 0) return null;
        
        return ID_.ToString();;
    }
}
