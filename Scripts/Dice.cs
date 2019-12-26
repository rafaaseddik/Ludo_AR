using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private int RandomnessFactor = 10;
    
    private Queue<Int32> _toRoll = new Queue<Int32>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this._toRoll.Count > 0)
        {
            RollGameObject(this._toRoll.Dequeue());
        }
    }

    public int Roll()
    {
        System.Random rnd = new System.Random();
        int result = 0;
        this._toRoll.Clear();
        for (int i = 0; i < RandomnessFactor; i++)
        {
            result  = rnd.Next(1, 7);
            this._toRoll.Enqueue(result);
            //
        }
        Debug.Log(result);
        return result;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void RollGameObject(int value)
    {
        
        switch (value)
        {
            case 1:
            {
                gameObject.transform.rotation = Quaternion.Euler(0f,0f,0f);
                break;
            }
            case 2:
            {
                gameObject.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
                break;
            }
            case 3:
            {
                gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                break;
            }
            case 4:
            {
                gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                break;
            }
            case 5:
            {
                gameObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                break;
            }
            case 6:
            {
                gameObject.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
                break;
            }
            
        }
    }
}
