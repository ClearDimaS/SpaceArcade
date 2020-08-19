using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Databases/BulletDatabase", fileName = "BulletDatabase")]
public class BulletDataBase : BaseDB<BulletData>
{

}


[System.Serializable]
public class BulletData
{
    [Tooltip("Main Sprite")]
    [SerializeField] private Sprite mainSprite;
    public Sprite MainSprite
    {
        get { return mainSprite; }
        protected set { mainSprite = value; }
    }

    [Tooltip("Bullet speed")]
    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
        protected set { speed = value; }
    }
}
