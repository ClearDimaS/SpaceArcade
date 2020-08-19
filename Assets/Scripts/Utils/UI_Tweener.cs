using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Tweener : MonoBehaviour
{
    private Vector3 scale;

    bool scaleSet;

    public int UI_ControllerBackPanel;

    public bool ifOneByOne;

    private void Awake()
    {
        if (!scaleSet) 
        {
            scale = new Vector3(gameObject.GetComponent<RectTransform>().localScale.x, gameObject.GetComponent<RectTransform>().localScale.y, gameObject.GetComponent<RectTransform>().localScale.z);

            scaleSet = true;
        }
    }

    private void OnEnable()
    {
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.0f, 0.0f, 0.0f);

        if (!ifOneByOne)
            Invoke("LeanTweenScaleMethod", 0.0f);
        else
            Invoke("LeanTweenScaleMethod", 0.2f * gameObject.transform.GetSiblingIndex());

        LeanTweenScaleMethod();
    }

    void LeanTweenScaleMethod() 
    {
        LeanTween.scale(gameObject, scale, 0.5f);
    }

    public void OnClose() 
    {
        if(!ifOneByOne)
            LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.5f).setOnComplete(disableObject);
        else
            LeanTween.scale(gameObject, scale, 0.5f).setOnComplete(disableObject);
    }

    void disableObject() 
    {
        UI_Controller.Instance.SetActivePanel(UI_ControllerBackPanel);
    }
}
