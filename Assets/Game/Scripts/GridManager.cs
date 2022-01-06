using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject grid;          
    [SerializeField] private float tileSize;           
    [SerializeField] private InputField inputText;     
    public Grid[,] Tiles;                            
    public int GridCount { get; private set; }  

    #endregion
    
    #region Mono Methods
    
    private void Start()
    {
        GridCount = int.Parse(inputText.text);        
        Tiles = new Grid[GridCount, GridCount];  
        CreateGrid();
        SetCamera(Screen.height,Screen.width);
    }
    #endregion
    
    #region Private Methods
    private void CreateGrid()
    {
        for (int i = 0; i < GridCount; i++)
        {
            for (int j = 0; j < GridCount; j++)
            {
                GameObject tile = Instantiate(grid, transform);    
                Tiles[i, j] = tile.GetComponent<Grid>();        
                tile.name = i+".sprite , " + j +".sprite " ;      

                Tiles[i,j].xIndex= i;                               
                Tiles[i,j].yIndex= j;

                float posX = i * tileSize;                          
                float posY = j * tileSize;
                tile.transform.position = new Vector2(posX,posY);              
            }
        }
        float gridW = GridCount * -tileSize;
        transform.position = new Vector2(gridW/2+tileSize/2, gridW / 2 + tileSize / 2); 
    }
    private void SetCamera(float height,float width)
    {
        if (Camera.main is null) return;
        if (Screen.width >= Screen.height) Camera.main.orthographicSize = GridCount * 2;
        else Camera.main.orthographicSize = GridCount * (height / width) * 2;
    }
    #endregion
}
