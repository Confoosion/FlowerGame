using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFlower", menuName = "Scriptable Objects/Flower")]
public class FlowerSO : ScriptableObject
{
    [System.Serializable]
    public class FlowerStage
    {
        public float timeToGrow;
        public Sprite stageSprite;
    }

    public List<FlowerStage> stages = new List<FlowerStage>();
}