using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Image MyImage;

    private EnemyData data;

    bool isDestroyed;
    public void Init(EnemyData _data) 
    {
        isDestroyed = false;
        data = _data;
        MyImage.sprite = data.MainSprite;
    }

    public float Attack
    {
        get { return data.Attack; }
        protected set { }
    }

    public static Action<GameObject> OnEnemyOutOfBounds;

    private void FixedUpdate()
    {
        if(isDestroyed == false)
            transform.Translate(Vector3.down * data.Speed * Time.deltaTime);

        if (ScreenViewManager.instance.IsInsideBounds(transform.position, 0.2f) == false)
            OnEnemyOutOfBounds(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.instance.Play(Constants.SND_ASTEROID_EXPLOSION);
        GetComponent<Collider2D>().enabled = false;
        isDestroyed = true;
        GetComponent<Animator>().Play(Constants.ANIM_ASTEROID_EXPLOSION);
        Invoke("DelayComingBack", 0.5f);
    }

    void DelayComingBack() 
    {
        GetComponent<Animator>().Play(Constants.ANIM_ASTEROID_DEF);
        GetComponent<Collider2D>().enabled = true;
        OnEnemyOutOfBounds(gameObject);
    }
}
