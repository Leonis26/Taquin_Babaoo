using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaquinTile : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler
{
    [SerializeField] Image m_visual;

    /// <summary>
    /// TILE PARAMETERS
    /// </summary>
    Vector2Int pos;
    public static Vector2Int EmptySpotPos { get; set; } = new Vector2Int();
    Vector2Int neededDir;
    public bool IsMoving { get; set; } = false;
    public int Id { get; private set; }
    public Vector2Int RightPlace { get; private set; }

    public bool IsRightPlace => pos == RightPlace;

    /// <summary>
    /// SETUP
    /// </summary>
    public void SetTile(int _id, in Sprite _image, Vector2Int _pos)
    {
        Id = _id;
        m_visual.sprite = _image;
        pos = _pos;
        if (_id == 0)
        {
            m_visual.enabled = false;
            EmptySpotPos = _pos;
        }
        RightPlace = TaquinManager.Instance.GetTileVirtualPosFromIndex(_id == 0 ? 4 : _id - 1);
        transform.localPosition = TaquinManager.Instance.GetTileLocalPosFromVirtual(pos);
    }


    /// <summary>
    /// MOVES
    /// </summary>
    void TryMoveToPos(Vector2Int _toPos)
    {
        if (_toPos != EmptySpotPos)
            return;
        IsMoving = true;
        EmptySpotPos = pos;
        pos = _toPos;
        TaquinManager.Instance.MoveTileToPos(this, _toPos);
    }

    Vector2 touchedPos;
    public void OnPointerDown(PointerEventData eventData)
    {
        neededDir = EmptySpotPos - pos;
        if (neededDir.magnitude > 1 || IsMoving) // this tile can't move now
            return;
        touchedPos = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var dirPos = eventData.position;
        var dir = (dirPos - touchedPos).normalized;
        if (Vector2.Dot(neededDir, dir) > .5f)
            TryMoveToPos(EmptySpotPos);
    }

    public void OnDrag(PointerEventData eventData) // necessary to use OnBeginDrag
    {
    }


    /// <summary>
    /// LEVEL END
    /// </summary>

    public void Win() => StartCoroutine(WaitForTotalMove());

    IEnumerator WaitForTotalMove()
    {
        yield return new WaitUntil(() => !IsMoving);
        GameManager.Instance.Win();
    }

    public void ShowTile() => m_visual.enabled = true;
}
