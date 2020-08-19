using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Databases/LevelDatabase", fileName = "LevelDatabase")]
public class LevelDatabase : BaseDB<LevelData>
{
    public override void AddElement() 
    {
        base.AddElement();
        currentElement.LevelStatus = elementsList[elementsList.Count - 2].LevelStatus == LevelStatusEnum.Complete ? LevelStatusEnum.Open : LevelStatusEnum.Locked;
        currentElement.LevelNumber = elementsList.Count;
    }

    public override void ClearDatabase() 
    {
        base.ClearDatabase();
        currentElement.LevelNumber = elementsList.Count;
        currentElement.LevelStatus = LevelStatusEnum.Open;
    }
}

public enum LevelStatusEnum
{
    Locked,
    Open,
    Complete
}

[System.Serializable]
public class LevelData
{
    [ReadOnly]
    [Tooltip("LevelNumber")]
    [SerializeField] private int levelNumber;
    public int LevelNumber
    {
        get { return levelNumber; }
        set { levelNumber = LevelNumber == 0 ? value : LevelNumber; }
    }

    [Tooltip("Is level complete?")]
    [SerializeField] LevelStatusEnum levelStatus;
    public LevelStatusEnum LevelStatus
    {
        get { return levelStatus; }
        set { levelStatus = LevelStatus == LevelStatusEnum.Complete ? levelStatus : value; }
    }

    [Tooltip("Total enemies on this level)")]
    [SerializeField] private int totalEnemies = 0;
    public int TotalEnemies
    {
        get { return totalEnemies; }
        set { totalEnemies = value; }
    }
}