using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleStatus : MonoBehaviour
{
    public static ConsoleStatus consoleStatus;

    public enum ControlType {
        Button_A, Button_B, Lever
    }

    public struct Control {
        public bool isOn;
        public ControlType type;
        public string label;

        public Control(ControlType type, bool isOn, string label = "") {
            this.type = type;
            this.isOn = isOn;
            this.label = label;
        }
    }

    public enum PanelId: int {
        First = 0,
        Second = 1,
        Third = 2
    }

    public class Panel {
        internal Control Button_A;
        internal Control Button_B;
        internal Control Lever;

        public Panel(Control buttonA, Control buttonB, Control lever) {
            this.Button_A = buttonA;
            this.Button_B = buttonB;
            this.Lever = lever;
        }
    }

    Panel firstPanel;
    Panel secondPanel;
    Panel thirdPanel;
    

    void Awake()
    {
        if (ConsoleStatus.consoleStatus == null)
        {
            ConsoleStatus.consoleStatus = this;
        }
        else
        {
            // Destroy if it exists already.
            // GameObject.Destroy(this.gameObject);
        }

        firstPanel = new Panel(
            new Control(
                ControlType.Button_A,
                false,
                "Wibbly Button (PLACEHOLDER)"
            ),
            new Control(
                ControlType.Button_B,
                false,
                "WibblyWobbly Button (PLACEHOLDER)"
            ),
            new Control(
                ControlType.Lever,
                false,
                "Wibbly Lever (PLACEHOLDER)"
            )
        );

        secondPanel = new Panel(
            new Control(
                ControlType.Button_A,
                false,
                "Wibbly Button #2 (PLACEHOLDER)"
            ),
            new Control(
                ControlType.Button_B,
                false,
                "WibblyWobbly Button # 2 (PLACEHOLDER)"
            ),
            new Control(
                ControlType.Lever,
                false,
                "Wibbly Lever # 2 (PLACEHOLDER)"
            )
        );

        thirdPanel = new Panel(
            new Control(
                ControlType.Button_A,
                false,
                "Wibbly Button #3 (PLACEHOLDER)"
            ),
            new Control(
                ControlType.Button_B,
                false,
                "WibblyWobbly Button # 3 (PLACEHOLDER)"
            ),
            new Control(
                ControlType.Lever,
                false,
                "Wibbly Lever # 3 (PLACEHOLDER)"
            )
        );
    }

    public void ToggleStatus(PanelId panelId, ControlType controlType) {
        bool currentStatus = GetStatus(panelId, controlType);
        SetStatus(panelId, controlType, !currentStatus);
    }

    public void SetStatus(PanelId panelId, ControlType controlType, bool value)
    {
        switch (panelId)
        {
            case PanelId.First:
                SetStatusOnPanel(firstPanel, controlType, value);
                break;
            case PanelId.Second:
                SetStatusOnPanel(secondPanel, controlType, value);
                break;
            case PanelId.Third:
                SetStatusOnPanel(thirdPanel, controlType, value);
                break;

        }
    }

    private void SetStatusOnPanel(Panel panel, ControlType controlType, bool value)
    {
        switch (controlType)
        {
            case ControlType.Button_A:
                panel.Button_A.isOn = value;
                break;
            case ControlType.Button_B:
                panel.Button_B.isOn = value;
                break;
            case ControlType.Lever:
                panel.Lever.isOn = value;
                break;
        }
    }



    public bool GetStatus(PanelId panelId, ControlType controlType)
    {
        switch (panelId)
        {
            case PanelId.First:
                return GetStatusOnPanel(firstPanel, controlType);
            case PanelId.Second:
                return GetStatusOnPanel(secondPanel, controlType);
            case PanelId.Third:
            default:
                return GetStatusOnPanel(thirdPanel, controlType);
        }
    }
    private bool GetStatusOnPanel(Panel panel, ControlType controlType)
    {
        switch (controlType)
        {
            case ControlType.Button_A:
                return panel.Button_A.isOn;
            case ControlType.Button_B:
                return panel.Button_B.isOn;
            case ControlType.Lever:
            default:
                return panel.Lever.isOn;
        }
    }

    public string GetName(PanelId panelId, ControlType controlType) {
        switch (panelId)
        {
            case PanelId.First:
                return GetNameOnPanel(firstPanel, controlType);
            case PanelId.Second:
                return GetNameOnPanel(secondPanel, controlType);
            case PanelId.Third:
            default:
                return GetNameOnPanel(thirdPanel, controlType);
        }
    }

    private string GetNameOnPanel(Panel panel, ControlType controlType) {
        switch (controlType)
        {
            case ControlType.Button_A:
                return panel.Button_A.label;
            case ControlType.Button_B:
                return panel.Button_B.label;
            case ControlType.Lever:
            default:
                return panel.Lever.label;
        }
    }
}
