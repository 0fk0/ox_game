using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public GameObject[] cells; // 9つのマスを格納するための配列
    public GameObject xPrefab; // ×のプレハブ
    public GameObject oPrefab; // ○のプレハブ
    

    private bool isPlayerXTurn = true; // ×プレイヤーのターンかどうか

    private void Update()
    {
        // マウスの左ボタンがクリックされたら、マウス位置に応じて○または×を置く
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject cell = hit.collider.gameObject;
                cell.GetComponent<SpriteRenderer>().color = Color.red;

                if (!cell.GetComponent<CellController>().IsOccupied())
                {
                    PlaceSymbol(cell);
                }
            }
        }
    }

    private void PlaceSymbol(GameObject cell)
    {
        if (isPlayerXTurn)
        {
            Instantiate(xPrefab, cell.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(oPrefab, cell.transform.position, Quaternion.identity);
        }

        isPlayerXTurn = !isPlayerXTurn;
        cell.GetComponent<CellController>().MarkCell(isPlayerXTurn);
    }

}
