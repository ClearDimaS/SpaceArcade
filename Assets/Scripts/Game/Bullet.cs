using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    private BulletData data;

    public void Init(BulletData _data)
    {
        AudioManager.instance.Play(Constants.SND_SHOT);
        data = _data;
        GetComponent<Image>().sprite = data.MainSprite;
    }

    public static Action OnBulletHitEnemy;
    public static Action<GameObject> OnBulletOutOfBounds;

    private void FixedUpdate()
    {
        transform.position += (transform.up * data.Speed * Time.deltaTime);

        if (ScreenViewManager.instance.IsInsideBounds(transform.position, 0.2f) == false)
            OnBulletOutOfBounds(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (EnemySpawner.Enemies.ContainsKey(obj))
        {
            OnBulletHitEnemy();
            OnBulletOutOfBounds(gameObject);
        }
    }
}
