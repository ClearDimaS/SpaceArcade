using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    Image[] LifesImages;

    [SerializeField]
    Text PointsText;

    public void UpdatePoints(int points, int PointsForVictory)
    {
        PointsText.text = points.ToString() + " / " + PointsForVictory.ToString();
    }

    public void UpdateHealth(int LivesCount)
    {
        for (int i = 0; i < LifesImages.Length; i++) 
        {
            int ind = LifesImages.Length - i - 1;
            if (ind >= LivesCount)
                LifesImages[ind].color = Color.grey;
            else
                LifesImages[ind].color = Color.white;
        }
    }
}
