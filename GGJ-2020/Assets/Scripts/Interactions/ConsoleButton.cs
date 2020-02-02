using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleButton : MonoBehaviour
{
    public ConsoleStatus.PanelId panelId;
    public ConsoleStatus.ControlType controlType;

    // Start is called before the first frame update
    void Start()
    {
        ConsoleStatus.consoleStatus.GetStatus(
            panelId,
            controlType
        );
    }

    // Update is called once per frame
    void Update()
    {
        // NO-OP
    }

    void OnMouseDown() {
        bool isActive = ConsoleStatus.consoleStatus.GetStatus(
            panelId,
            controlType
        );
        Color newColor = isActive ? Color.green : Color.red;
        gameObject.GetComponentInChildren<Renderer>().material.color = newColor;
        ConsoleStatus.consoleStatus.ToggleStatus(panelId, controlType);
    }
}
