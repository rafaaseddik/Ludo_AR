using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool Role = false; // false:Yellow, true: Red

    [SerializeField] private GameObject dice;

    
    [SerializeField] private GameObject[] YellowPiece;
    [SerializeField] private GameObject[] RedPiece;
    
    
    
    private int[] board = new Int32[52];

    public int UnlockedYellowPieces = 0;
    public int UnlockedRedPieces = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchRole()
    {
        this.Role = !this.Role;
        dice.GetComponent<Dice>().Show();
    }

    
}
