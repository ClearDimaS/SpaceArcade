using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Databases/EnemyDatabase", fileName = "EnemyDatabase")]
public class EnemyDataBase : BaseDB<EnemyData>
{

}

[System.Serializable]
public class EnemyData 
{

    [Tooltip("Main Sprite")]
    [SerializeField] private Sprite mainSprite;
    public Sprite MainSprite
    {
        get { return mainSprite; }
        protected set { }
    }

    [Tooltip("Enemy speed")]
    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
        protected set { }
    }

    [Tooltip("Enemy attack")]
    [SerializeField] private float attack;
    public float Attack
    {
        get { return attack; }
        protected set { }
    }
}
