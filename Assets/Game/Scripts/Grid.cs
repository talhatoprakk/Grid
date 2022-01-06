using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SpriteType
{
    UnClicked,
    Clicked
}
public class Grid : MonoBehaviour
{

    #region Variables
    
    [SerializeField] private Sprite clickSprite;                    
    [SerializeField] private Sprite unClickSprite; 
    
    public SpriteType spriteType;
    public List<GameObject> clickedList; 
    
    public int xIndex, yIndex;
    private int count;
    
    private GridManager gridManager;
    private SpriteRenderer myTileSprite;
    
    
    private readonly WaitForSeconds _duration = new WaitForSeconds(.05f);

    #endregion

    #region Mono Methods

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        myTileSprite = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        EventManager.OnButtonClick.AddListener(CheckGrid);
    }

    private void OnDisable()
    {
        EventManager.OnButtonClick.RemoveListener(CheckGrid);
    } 
    
    private void OnMouseDown()
    {
        ChangeSprite();
        EventManager.OnButtonClick.Invoke();
    }
    #endregion

    #region Private Methods
    private void SetSprite(SpriteType type, Sprite sprite)
    {
        spriteType = type;
        myTileSprite.sprite = sprite;
    }
    private void ChangeSprite()
    {
        if (spriteType != SpriteType.UnClicked)  return;
        
        SetSprite(SpriteType.Clicked,clickSprite);
    }
    private void CheckGrid()           
    {
        if (spriteType != SpriteType.Clicked) return;
        
        if (xIndex > 0 && gridManager.Tiles[xIndex - 1, yIndex].spriteType == SpriteType.Clicked)
            clickedList.Add(gridManager.Tiles[xIndex - 1, yIndex].gameObject);                                      

        if (yIndex > 0 && gridManager.Tiles[xIndex, yIndex - 1].spriteType == SpriteType.Clicked)
            clickedList.Add(gridManager.Tiles[xIndex , yIndex - 1].gameObject);                                     

        if (yIndex < gridManager.GridCount - 1 && gridManager.Tiles[xIndex, yIndex + 1].spriteType == SpriteType.Clicked)
            clickedList.Add(gridManager.Tiles[xIndex, yIndex + 1].gameObject);                                     

        if (xIndex < (gridManager.GridCount-1) && gridManager.Tiles[xIndex+1, yIndex].spriteType == SpriteType.Clicked)
            clickedList.Add(gridManager.Tiles[xIndex+1, yIndex ].gameObject);                                      
        
        if (clickedList.Count >= 2) StartCoroutine(ResetSprite());                                                                          
        else clickedList.Clear();                                                                                   
    }
    private IEnumerator ResetSprite()                                                       
    {
        yield return _duration;    
        SetSprite(SpriteType.UnClicked,unClickSprite);

        foreach (var t in clickedList)
        {
            t.GetComponent<SpriteRenderer>().sprite = unClickSprite;
            t.GetComponent<Grid>().spriteType = SpriteType.UnClicked;
        }
        clickedList.Clear();    
    }
 
    #endregion
}
