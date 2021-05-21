using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public bool isPlayerPiece;
    public bool isSelected;

    private void Start()
    {
        isSelected = false;
    }
}
