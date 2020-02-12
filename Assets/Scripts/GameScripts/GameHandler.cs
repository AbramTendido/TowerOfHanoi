using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public GameObject[] treeStumps;
    public Sprite gameDisk;
    public Sprite treeStumpSprite;

    public DiskAmount diskAmountSO;
    public MoveCount moveCountSO;
    public CurrentScene currentScene;

    [SerializeField]
    private SpriteDrawMode spriteDrawMode = SpriteDrawMode.Sliced;
    [SerializeField]
    private float stumpScale = 0.4f;

    private Stack<GameDisk> leftStump = new Stack<GameDisk>();
    private Stack<GameDisk> middleStump = new Stack<GameDisk>();
    private Stack<GameDisk> rightStump = new Stack<GameDisk>();
    
    public Dictionary<Stack<GameDisk>, GameObject> stackDictionary = new Dictionary<Stack<GameDisk>, GameObject>();
   
    // Start is called before the first frame update
    void Start()
    {
        currentScene.scene = Scene.Game;
        AudioManager.instance.PlayBG();

        //Make gameDisks
        DiskSpawn();

        //Make stumps
        StumpSpawn();
        

    }

    private void DiskSpawn()
    {
        for (int i = 1; i < diskAmountSO.diskAmount + 1; i++)
        {
            GameDisk disk = new GameDisk(diskAmountSO.diskAmount - i, gameDisk, spriteDrawMode);

            //Set this gameobject as Parent
            disk.gameObject.transform.SetParent(this.gameObject.transform);

            //Positioning
            disk.gameObject.transform.position = treeStumps[0].transform.position + Vector3.down * (i - 1);

            if (i > 1)
            {
                disk.gameObject.transform.position = GetTopPosition(leftStump.Peek().gameObject);
            }

            AlignBottom(disk.gameObject);
            leftStump.Push(disk);
        }
    }

    private void StumpSpawn()
    {
        for (int i = 0; i < 3; i++)
        {
            treeStumps[i].gameObject.GetComponent<SpriteRenderer>().size += Vector2.up * diskAmountSO.diskAmount * stumpScale;
            AlignBottom(treeStumps[i]);
        }

        stackDictionary.Add(leftStump, treeStumps[0]);
        stackDictionary.Add(middleStump, treeStumps[1]);
        stackDictionary.Add(rightStump, treeStumps[2]);
    }

    private Vector3 GetTopPosition(GameObject g)
    {
        Vector3 v3 = g.transform.position;
        v3.y = g.GetComponent<SpriteRenderer>().bounds.max.y;
        return v3;
    }

    private Vector3 GetBottomPosition(GameObject g)
    {
        Vector3 v3 = g.transform.position;
        v3.y = g.GetComponent<SpriteRenderer>().bounds.min.y;
        return v3;
    }

    private void AlignBottom(GameObject g)
    {
        Vector3 currPos = g.transform.position;
        Vector3 botPos = currPos;
        botPos.y = g.GetComponent<SpriteRenderer>().bounds.min.y;
        g.transform.position += currPos - botPos;
    }

    //GAME 
    private bool Solved()
    {
        return rightStump.Count == diskAmountSO.diskAmount;
    }

    private bool CanMove(Stack<GameDisk> former, Stack<GameDisk> target)
    {
        if(target.Count == 0)
        {
            return true;
        }
        else
        {
            return former.Peek().GetLevel() < target.Peek().GetLevel();
        }
    }

    private void MoveDisk(Stack<GameDisk> former, Stack<GameDisk> target)
    {
        GameDisk gD = former.Pop();
        GameDisk top;
        if (target.Count == 0)
        {
            GameObject g;
            stackDictionary.TryGetValue(target, out g);
            gD.gameObject.transform.position = GetBottomPosition(g);
        }
        else
        {
            top = target.Peek();
            gD.gameObject.transform.position = GetTopPosition(top.gameObject);
        }
        target.Push(gD);
        AlignBottom(gD.gameObject);
    }

    private Stack<GameDisk> GetFromDisk()
    {
        foreach(KeyValuePair<Stack<GameDisk>, GameObject> pair in stackDictionary)
        {
            if(pair.Key.Count > 0)
            {
                Vector3 target = Input.mousePosition;
                Bounds bounds = pair.Key.Peek().gameObject.GetComponent<SpriteRenderer>().bounds;
                target = Camera.main.ScreenToWorldPoint(target);
                target.z = bounds.center.z;
                if (bounds.Contains(target))
                {
                    return pair.Key;
                }
            }
        }
        return null;
    }

    private Stack<GameDisk> GetFromStump()
    {
        foreach (KeyValuePair<Stack<GameDisk>, GameObject> pair in stackDictionary)
        {
            Vector3 target = Input.mousePosition;
            Bounds bounds = pair.Value.GetComponent<SpriteRenderer>().bounds;
            target = Camera.main.ScreenToWorldPoint(target);
            target.z = bounds.center.z;
            if (bounds.Contains(target))
            {
                return pair.Key;
            }
        }
        return null;
    }

    
    Stack<GameDisk> getStack = null;
    Stack<GameDisk> dropStack = null;
    GameDisk selectedDisk = null;
    Vector3 initialVector = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (Solved() == true)
        {
            Debug.Log("Game is Won!");
            SceneManager.LoadScene("Win");
        }
        else
        {
            if (Input.GetMouseButton(0) && getStack == null)
            {
                getStack = GetFromDisk();
                if (getStack != null)
                {
                    selectedDisk = getStack.Peek();
                    initialVector = selectedDisk.gameObject.transform.position;
                }
            }
            if (Input.GetMouseButton(0) && getStack != null)
            {
                Vector3 target = Input.mousePosition;
                target = Camera.main.ScreenToWorldPoint(target);
                target.z = selectedDisk.gameObject.transform.position.z;
                selectedDisk.gameObject.transform.position = target;
            }
            if (!Input.GetMouseButton(0) && getStack != null)
            {
                dropStack = GetFromStump();
                if (dropStack != null && dropStack != getStack && CanMove(getStack, dropStack))
                {
                    if (CanMove(getStack, dropStack))
                    {
                        MoveDisk(getStack, dropStack);
                        moveCountSO.moveCount++;
                        getStack = null; dropStack = null; selectedDisk = null;
                    }
                }
                else
                {
                    selectedDisk.gameObject.transform.position = initialVector;
                    getStack = null; dropStack = null; selectedDisk = null;
                }
            }
        }
    }
}
