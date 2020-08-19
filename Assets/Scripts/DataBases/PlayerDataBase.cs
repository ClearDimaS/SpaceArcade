using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Databases/PlayerDatabase", fileName = "PlayerDatabase")]
public class PlayerDataBase : BaseDB<PlayerData>
{

}


[System.Serializable]
public class PlayerData
{
    [Tooltip("Main Sprite")]
    [SerializeField] private Sprite mainSprite;
    public Sprite MainSprite
    {
        get { return mainSprite; }
        protected set { }
    }

    [Tooltip("Bullet speed")]
    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
        protected set { }
    }

    [Tooltip("Maximum health")]
    [SerializeField] private int maxHealth;
    public int MaxHealth
    {
        get { return maxHealth; }
        protected set { }
    }

    private int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
}
