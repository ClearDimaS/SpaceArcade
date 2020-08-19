using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseDB<T> : ScriptableObject where T : class, new()
{
#if UNITY_EDITOR
    [ReadOnly]
#endif
    [SerializeField] private int currentIndex = 0;

    [SerializeField, HideInInspector] protected List<T> elementsList;

    [SerializeField] protected T currentElement;

    public virtual List<T> GetElementList()
    {
        return elementsList;
    }

    public virtual void AddElement()
    {
        if (elementsList == null)
            elementsList = new List<T>();

        currentElement = new T();
        elementsList.Add(currentElement);
        currentIndex = elementsList.Count - 1;
    }

    public virtual T GetNext()
    {
        if (currentIndex < elementsList.Count - 1)
            currentIndex++;
        currentElement = this[currentIndex];
        return currentElement;
    }

    public virtual T GetPrev()
    {
        if (currentIndex > 0)
            currentIndex--;
        currentElement = this[currentIndex];
        return currentElement;
    }

    public virtual void ClearDatabase()
    {
        elementsList.Clear();
        elementsList.Add(new T());
        currentElement = elementsList[0];
        currentIndex = 0;
    }

    public virtual T GetRandomElement()
    {
        int random = Random.Range(0, elementsList.Count);
        return elementsList[random];
    }

    public virtual void RemoveCurrentElement()
    {
        if (currentIndex > 0)
        {
            currentElement = elementsList[--currentIndex];
            elementsList.RemoveAt(++currentIndex);
        }
        else
        {
            elementsList.Clear();
            currentElement = null;
        }
    }

    public virtual T this[int index]
    {
        get
        {
            if (elementsList != null && index >= 0 && index < elementsList.Count)
                return elementsList[index];
            return null;
        }

        set
        {
            if (elementsList == null)
                elementsList = new List<T>();

            if (index >= 0 && index < elementsList.Count && value != null)
                elementsList[index] = value;
            else Debug.LogError("Выход за границы!");
        }
    }
}