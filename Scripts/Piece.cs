using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Color
{
    YELLOW,
    RED
}
public class Piece : MonoBehaviour
{
    [SerializeField] public Color color;
    [SerializeField] private int finishPosition;
    [SerializeField] private GameObject[] _enemis = new GameObject[4];
    private Vector3 _initialPosition;
    public int position = -1;
    public int goalPosition = -1;
    public bool isInWin = false;
    public int winPosition = -1;
    public int winGoalPosition = -1;
    public bool Locked = true;
    private const float yPosition = 0.75f;
    public bool Decaled = false;
    
    private int _frame = 0;
    // Start is called before the first frame update
    void Start()
    {
        this._initialPosition = gameObject.transform.localPosition;
        if (color == Color.RED)
        {
            this._enemis = GameObject.FindGameObjectsWithTag("YellowPiece");
        }
        else
        {
            this._enemis = GameObject.FindGameObjectsWithTag("RedPiece");
        }
    }

    // Update is called once per frame
    void Update()
    {
        _frame++;
        if (_frame % 10 == 0)
        {
            if (this.isInWin)
            {
                
            }
            else if (goalPosition != -1 & goalPosition%52!=position)
            {
                position++;
                if (color == Color.RED & position==25 | color==Color.YELLOW & position==51)
                {
                    this.isInWin = true;
                }
                if (position > 51)
                    position = position % 52;
                positionToXZ(position);
            }   
        }

        if (_frame == 1000)
        {
            _frame = 0;
        }
        
        
    }

    public void Initiate(int position)
    {
        this.position = position;
        this.goalPosition = position;
        this.positionToXZ(position);
        this.Locked = false;
    }

    public void ResetPosition()
    {
        gameObject.transform.localPosition = this._initialPosition;
        this.Locked = true;
    }

    public void IncrementPosition(int inc)
    {
        this.goalPosition += inc;
    }
    private void positionToXZ(int pos)
    {
        
        if (pos >= 0 & pos < 6)
        {
            gameObject.transform.localPosition = new Vector3(-1,yPosition,pos-7);
            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (pos >= 6 & pos < 12)
        {
            gameObject.transform.localPosition = new Vector3(4-pos,yPosition,-1);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (pos == 12)
        {
            gameObject.transform.localPosition = new Vector3(-7,yPosition,0);
            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (pos >= 13 & pos < 19)
        {
            gameObject.transform.localPosition = new Vector3(pos-20,yPosition,1);
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (pos >= 19 & pos < 25)
        {
            gameObject.transform.localPosition = new Vector3(-1,yPosition,pos-17);
            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (pos ==25)
        {
            gameObject.transform.localPosition = new Vector3(0,yPosition,7);
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (pos >= 26 & pos < 32)
        {
            gameObject.transform.localPosition = new Vector3(1,yPosition,33-pos);
            gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (pos >= 32 & pos < 38)
        {
            gameObject.transform.localPosition = new Vector3(pos-30,yPosition,1);
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (pos ==38)
        {
            gameObject.transform.localPosition = new Vector3(7,yPosition,0);
            gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else if (pos >= 39 & pos < 45)
        {
            gameObject.transform.localPosition = new Vector3(46-pos,yPosition,-1);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (pos >= 45 & pos < 51)
        {
            gameObject.transform.localPosition = new Vector3(1,yPosition,43-pos);
            gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else if (pos ==51)
        {
            gameObject.transform.localPosition = new Vector3(0,yPosition,-7);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(pos==goalPosition%52)
            foreach (var enemy in this._enemis)
            {
                var enemyObject = enemy.GetComponent<Piece>();
                if (enemyObject.position == pos)
                {   if(pos!=1 & pos!=9 & pos!=14 & pos!=22 & pos != 27 & pos!=35 & pos!= 40 & pos!=48)
                        enemyObject.ResetPosition();
                    else
                    {
                        if (color == Color.RED)
                        {
                            gameObject.transform.localPosition += new Vector3(0.35f, 0f, 0.35f);
                            if (!enemyObject.Decaled)
                            {
                                enemyObject.Decaled = true;
                                enemy.transform.localPosition += new Vector3(-0.35f, 0f, -0.35f);    
                            }
                            
                        }
                        else
                        {
                            gameObject.transform.localPosition += new Vector3(-0.35f, 0f, -0.35f);
                            if (!enemyObject.Decaled)
                            {
                                enemyObject.Decaled = true;
                                enemy.transform.localPosition += new Vector3(0.35f, 0f, 0.35f);    
                            }
                            
                        }
                    }
                }
            }
        
    }
}
