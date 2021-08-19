using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBoardController : MonoBehaviour
{
    public GameObject WinBoard;
    public void btn_ActiveWinBoard() {
        WinBoard.SetActive(true);
    }
    public void btn_DeactiveWinBoard() {
        WinBoard.SetActive(false);
    }
}
