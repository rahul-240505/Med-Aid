using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "Tools/Tool Data")]
public class ToolData : ScriptableObject
{
    public string toolName;
    public GameObject toolPrefab;
}
