using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private PlayerData data;

    public PlayerControls playerInput;

    [SerializeField]
    Vector3 startPos;

    [SerializeField]
    PlayerDataBase playerDataBase;

    // Update is called once per frame
    void FixedUpdate()
    {
        tryMovePlayer();
    }

    private void OnEnable()
    {
        data = playerDataBase.GetRandomElement();

        transform.localPosition = startPos;

        data.CurrentHealth = data.MaxHealth;

        GetComponent<Image>().sprite = data.MainSprite;
    }

    private void tryMovePlayer()
    {
        transform.up = playerInput.GetRotation(transform.up);

        Vector3 desiredMove = transform.position + playerInput.InputPlayerMoveSpeed(transform) * Time.deltaTime * data.Speed;

        if (ScreenViewManager.instance.IsInsideBounds(desiredMove, 0.0f)) 
        {
            transform.position = desiredMove;
        } 
    }

    public event Action<int> onLoseHealth;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (EnemySpawner.Enemies.ContainsKey(obj)) 
        {
            data.CurrentHealth -= (int)EnemySpawner.Enemies[obj].Attack;
            onLoseHealth?.Invoke(data.CurrentHealth);
        }
    }
}
