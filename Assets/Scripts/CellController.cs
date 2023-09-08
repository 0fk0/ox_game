using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    private bool isOccupied = false;
    private GameObject oObject, xObject;

    private void Start()
    {
        oObject = transform.Find("o").gameObject;
        xObject = transform.Find("x").gameObject; 

        oObject.SetActive(false);
        xObject.SetActive(false);
    }

    // セルが占有されているかどうかを返す
    public bool IsOccupied()
    {
        return isOccupied;
    }

    // セルに印を付ける
    public void MarkCell(bool isPlayerX)
    {
        if (!isOccupied){
            isOccupied = true;

            if (isPlayerX)
            {
                // ×の印を表示する処理
                if (oObject != null)
                {
                    oObject.SetActive(true);
                }
            }
            else
            {
                // ○の印を表示する処理表示にする
                if (xObject != null)
                {
                    xObject.SetActive(true);
                }
            }
        }
    }
}
