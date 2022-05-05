using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TaquinManager : MonoBehaviour
{
    public static TaquinManager Instance = null;


    // scene references
    [SerializeField]
    Image m_desiredImageView;
    Image m_desiredImageTaquinBack;
    [SerializeField]
    GridLayoutGroup m_taquinGrid;
    [SerializeField] Canvas m_Canvas;
    
    // private variables
    Transform m_taquinSlicesParent;
    List<TaquinTile> m_taquinTiles = new List<TaquinTile>();
    int taquinWidth;
    float taquinTileSize;
    Vector2Int deactivatedTile = new Vector2Int(1, -1);
    int deactivatedTileIndex = 4;

    // Animation
    [SerializeField] AnimationCurve m_MoveTileCurve; 

    /// <summary>
    /// INITIALIZATION
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    public void LoadGameWithResource(string _resourceFolder)
    {
        m_Canvas.worldCamera = Camera.main;
        InitTaquinSize();
        InitTaquinSlices();
        var allSpritesObj = Resources.LoadAll(_resourceFolder, typeof(Sprite)).Cast<Sprite>().ToArray();
        LoadResultImage(allSpritesObj, out Sprite resultImage);
        var allSpritesList = allSpritesObj.ToList();
        allSpritesList.Remove(resultImage);
        TaquinRandomizer(allSpritesList);
    }
    void InitTaquinSize()
    {
        taquinTileSize = m_taquinGrid.cellSize.x;
        m_desiredImageTaquinBack = m_taquinGrid.GetComponent<Image>();
    }

    void InitTaquinSlices()
    {
        m_taquinSlicesParent = m_desiredImageTaquinBack.transform;
        m_taquinTiles = m_taquinSlicesParent.GetComponentsInChildren<TaquinTile>().ToList();
        taquinWidth = (int)Mathf.Sqrt(m_taquinTiles.Count());
    }

    void LoadResultImage(in Sprite[] _sprites, out Sprite _resultImage)
    {
        _resultImage = _sprites.First(t => t.name == "Result") as Sprite;
        m_desiredImageView.sprite = _resultImage;
        m_desiredImageTaquinBack.sprite = _resultImage;
    }
    
    /// <summary>
    /// GENERATION
    /// </summary>
    void TaquinRandomizer(List<Sprite> _taquinSprites)
    {
        int[,] puzzle = new int[taquinWidth, taquinWidth]; // Value 0 is used for empty space
        do
        {
            var availableIdx = Enumerable.Range(1, puzzle.Length).ToList();
            availableIdx.Remove(deactivatedTileIndex + 1);
            for (int i = 0; i < taquinWidth; i++)
            {
                for (int j = 0; j < taquinWidth; j++)
                {
                    if (new Vector2Int(i, -j) == deactivatedTile)
                        puzzle[i, j] = 0;
                    else
                    {
                        var chosenIdx = GameManager.Instance.Rand.Next(availableIdx.Count);
                        puzzle[i, j] = availableIdx[chosenIdx];
                        availableIdx.RemoveAt(chosenIdx);
                    }
                }
            }
        }while(!PuzzleCheck.IsSolvable(puzzle));

        int sortingIdx = 0;
        m_taquinTiles[deactivatedTileIndex].SetTile(0, _taquinSprites[deactivatedTileIndex], GetTileVirtualPosFromIndex(deactivatedTileIndex));

        foreach (var piece in puzzle)
        {
            var image = _taquinSprites.ElementAtOrDefault(piece - 1);
            if (image)
                m_taquinTiles[sortingIdx].SetTile(piece, image, GetTileVirtualPosFromIndex(sortingIdx));
            sortingIdx++;
        }
    }

    /// <summary>
    /// MOVES
    /// </summary>
    public void MoveTileToPos(in TaquinTile _tile, Vector2Int _toPosV)
    {
        StartCoroutine(MoveToPos(_tile, GetTileLocalPosFromVirtual(_toPosV)));
        if (_tile.IsRightPlace)
        {
            if (CheckWin())
                _tile.Win();
        }
    }

    IEnumerator MoveToPos(TaquinTile _tile, Vector2 _toPos)
    {
        var timer = .5f;
        var time = 0f;
        var tileT = _tile.transform;
        var ogPos = tileT.localPosition;
        while (time < timer)
        {
            time += Time.deltaTime;
            tileT.localPosition = Vector2.Lerp(ogPos, _toPos, m_MoveTileCurve.Evaluate(time / timer));
            yield return new WaitForEndOfFrame();
        }
        tileT.localPosition = _toPos;
        _tile.IsMoving = false;
    }

    public Vector2Int GetTileVirtualPosFromIndex(int _index)
    {
        return new Vector2Int(_index % taquinWidth, - _index / taquinWidth);
    }

    public Vector2 GetTileLocalPosFromVirtual(in Vector2Int _virtualPos)
    {
        return new Vector2(taquinTileSize * (_virtualPos.x - 1),
            taquinTileSize * (1 +    _virtualPos.y));
    }

    /// <summary>
    /// LEVEL Managment
    /// </summary>
    bool CheckWin()
    {
        foreach (var tile in m_taquinTiles)
        {
            if (!tile.IsRightPlace)
                return false;
        }
        return true;
    }
    public void ShowHiddenTile() => m_taquinTiles[deactivatedTileIndex].enabled = true;

}
