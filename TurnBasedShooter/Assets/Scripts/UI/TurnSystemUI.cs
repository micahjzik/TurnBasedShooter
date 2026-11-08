using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI turnNumberText;
    [SerializeField] private Button nextTurnButton;

    private void Start()
    {
        TurnSystem.Instance.OnNewTurn += TurnSystem_OnNewTurn;
        nextTurnButton.onClick.AddListener(NextTurnButton_Click);
        TurnSystem_OnNewTurn(null, System.EventArgs.Empty);
    }

    private void NextTurnButton_Click()
    {
        TurnSystem.Instance.NextTurn();
    }

    private void TurnSystem_OnNewTurn(object sender, System.EventArgs e)
    {
        turnNumberText.text = "TURN: " + TurnSystem.Instance.GetTurnNumber().ToString();
    }

}
