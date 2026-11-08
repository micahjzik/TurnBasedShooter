using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;
    [SerializeField] private Image image;

    private BaseAction baseAction;

    private void Awake()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    public void SetBaseAction(BaseAction baseAction)
    {
        this.baseAction = baseAction;
        textMeshPro.text = baseAction.GetActionName().ToUpper();
    }

    private void OnButtonClick()
    {
        if (baseAction != null && UnitActionSystem.Instance != null)
        {
            UnitActionSystem.Instance.SetSelectedAction(baseAction);
        }
    }

    public void UpdateSelectedVisual()
    {
        image.enabled = false;
        if (baseAction == UnitActionSystem.Instance.GetSelectedAction())
        {
            image.enabled = true;
        }
    }
}
