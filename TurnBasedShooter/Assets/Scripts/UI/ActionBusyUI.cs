using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{

    [SerializeField] private GameObject actionBusyUI;
    // Start is called before the first frame update
    void Start()
    {
        actionBusyUI.SetActive(false);
        UnitActionSystem.Instance.OnBusyChanged += UnitActionSystem_OnBusyChanged;
    }

    private void UnitActionSystem_OnBusyChanged(object sender, bool isBusy)
    {
        actionBusyUI.SetActive(isBusy);
    }
}
