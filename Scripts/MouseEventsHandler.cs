using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventsHandler : MonoBehaviour
{
    
    private string DiceTag = "Dice";
    private string RedTag = "RedPiece";
    private string YellowTag = "YellowPiece";
    
    [SerializeField] private Material RedDiceMaterial;
    [SerializeField] private Material DarkRedDiceMaterial;
    [SerializeField] private Material YellowDiceMaterial;
    [SerializeField] private Material DarkYellowDiceMaterial;
    
    private GameManager _gameManager;
    private Renderer _diceRenderer;
    [SerializeField] private bool _canRollDice = true;

    [SerializeField] private int _currentDiceValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        this._gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        this._diceRenderer = GameObject.Find("Dice").GetComponent<Renderer>();
        this._canRollDice = true;
        this._diceRenderer.material = YellowDiceMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)& Input.GetMouseButtonDown(0))
        {
            var selection = hit.transform;
            if (this._canRollDice )
            {
                if(selection.CompareTag(DiceTag)){
                    var dice = selection.GetComponent<Dice>();
                    this._currentDiceValue = dice.Roll();
                    if (this._currentDiceValue != 6 &
                        (this._gameManager.Role & this._gameManager.UnlockedRedPieces == 0 |
                         !this._gameManager.Role & this._gameManager.UnlockedYellowPieces == 0))
                    {
                        NextPlay();
                    }
                    else
                    {
                        this._canRollDice = false;
                        if (this._gameManager.Role)
                        {
                            this._diceRenderer.material = DarkRedDiceMaterial;
                        }
                        else
                        {
                            this._diceRenderer.material = DarkYellowDiceMaterial;
                        }
                        //this._diceRenderer.material = NeutralDiceMaterial;
                    }
                }
            }
            else
            {
                if (this._gameManager.Role) //Red Plays
                {
                    if ((selection.CompareTag(RedTag)))
                    {
                        var piece = selection.GetComponent<Piece>();
                        if (piece.Locked & this._currentDiceValue == 6)
                        {
                            piece.Initiate(27);
                            this._gameManager.UnlockedRedPieces++;
                            NextPlay();
                        }
                        else if(!piece.Locked)
                        {
                            piece.goalPosition += this._currentDiceValue;
                            NextPlay();
                        }
                            
                    }
                }
                else
                {
                    if ((selection.CompareTag(YellowTag)))
                    {
                        var piece = selection.GetComponent<Piece>();
                        if (piece.Locked & this._currentDiceValue == 6)
                        {
                            piece.Initiate(1);
                            this._gameManager.UnlockedYellowPieces++;
                            NextPlay();
                        }
                        else if(!piece.Locked)
                        {
                            piece.goalPosition += this._currentDiceValue;
                            NextPlay();
                        }
                            
                    }
                }

                
            }
            
        }
    }

    private void NextPlay()
    {
        this._canRollDice = true;
        if (this._currentDiceValue != 6)
        {
            this._gameManager.SwitchRole();
        }
        if (this._gameManager.Role)
        {
            this._diceRenderer.material = RedDiceMaterial;
        }
        else
        {
            this._diceRenderer.material = YellowDiceMaterial;
        }
    }
}
