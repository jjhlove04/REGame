using UnityEngine;

[CreateAssetMenu(fileName = "Mine Data", menuName = "MineObj/MineScriptable", order = int.MaxValue)]
public class MineScriptable : ScriptableObject
{
    public string mineName;
    public Sprite img;
    public GameObject obj;
}
