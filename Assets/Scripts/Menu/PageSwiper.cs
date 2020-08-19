using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    public float percentThreshold = 0.4f;
    public float easing = 0.5f;
    public GameObject PageExample;
    float pageWidth;
    int pagesNumber;
    int curPageNumber;
    public List<Image> PageMenuImgs;

    void Start()
    {
        curPageNumber = 0;

        UpdateCurPageDisp();

        pagesNumber = transform.childCount - 1;

        panelLocation = transform.localPosition;

        pageWidth = PageExample.GetComponentInChildren<RectTransform>().sizeDelta.x;
    }


    public void OnDrag(PointerEventData eventData)
    {
        float difference = (eventData.pressPosition.x - eventData.position.x) / Screen.width * pageWidth;

        transform.localPosition = panelLocation - new Vector3(difference, 0, 0);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        float percentage = (eventData.pressPosition.x - eventData.position.x) / Screen.width;

        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;

            if (percentage > 0 && transform.localPosition.x > -pageWidth * pagesNumber)
            {
                curPageNumber += 1;
                newLocation += new Vector3(-pageWidth, 0, 0);
            }
            else if (percentage < 0 && transform.localPosition.x < 0)
            {
                curPageNumber -= 1;
                newLocation += new Vector3(pageWidth, 0, 0);
            }
            else 
            {
                StartCoroutine(SmoothMove(transform.localPosition, panelLocation, easing));
                return;
            }

            StartCoroutine(SmoothMove(transform.localPosition, newLocation, easing));

            panelLocation = newLocation;

            UpdateCurPageDisp();
        }
        else 
        {
            StartCoroutine(SmoothMove(transform.localPosition, panelLocation, easing));
        }
    }
    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds) 
    {
        float t = 0.0f;

        while (t <= 1.0f) 
        {
            t += Time.deltaTime / seconds;

            transform.localPosition = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0.0f, 1.0f, t));

            yield return null;
        }
    }

    void UpdateCurPageDisp() 
    {
        for (int i = 0; i < PageMenuImgs.Count; i++) 
        {
            if (i == curPageNumber)
                PageMenuImgs[i].color = Color.Lerp(Color.red, Color.black, 0.5f);
            else
                PageMenuImgs[i].color = Color.grey;
        }
    }
}
