using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    /// Available players
    private int numberOfPlayers = 0;

    private int numControlsPerPanel = 3;
    private int currentPlayerIndex = 0;

    void Start()
	{
        this.numberOfPlayers = GameManager.instance.players.Length;
    }

    // DEBUG ONLY
    void Update() {
        if (Input.GetKeyDown("space"))
        {
            string instruction = GenerateNextInstruction();
            print("space key was pressed");
            print($"INSTRUCTION = {instruction}");
        }
    }

    // Generates and Returns the next instruction for the players
    public string GenerateNextInstruction() {

        ConsoleStatus.ControlType? currentControl = this.GetDisabledControl();

        if (currentControl.HasValue) {

            // Get the name of control to interact with
            string controlName = ConsoleStatus.consoleStatus.GetName(
               this.GetPanelForCurrentPlayer(),
               currentControl.Value
            );

            // Move on to the Next Player
            this.SetNextPlayer();

            // done!
            return $"Quickly! Find the {controlName}";
        } 
        else {
            // the current user has 
            this.SetNextPlayer();
            // Try again
            return GenerateNextInstruction();
        }
    }

    private ConsoleStatus.ControlType? GetDisabledControl() {

        ConsoleStatus.PanelId currentPlayer = GetPanelForCurrentPlayer();
        
        bool buttonA = ConsoleStatus.consoleStatus.GetStatus(currentPlayer, ConsoleStatus.ControlType.Button_A);
        bool buttonB = ConsoleStatus.consoleStatus.GetStatus(currentPlayer, ConsoleStatus.ControlType.Button_B);
        bool   lever = ConsoleStatus.consoleStatus.GetStatus(currentPlayer, ConsoleStatus.ControlType.Lever);
        
        if (!buttonA) {
            return ConsoleStatus.ControlType.Button_A;
        } else if (!buttonB) {
            return ConsoleStatus.ControlType.Button_B;
        } else if (!lever) {
            return ConsoleStatus.ControlType.Lever;
        } else {
            return null;
        }
    }

    private ConsoleStatus.PanelId GetPanelForCurrentPlayer() {
        return (ConsoleStatus.PanelId) currentPlayerIndex;
    }

    private void SetNextPlayer() {
        this.currentPlayerIndex = (this.currentPlayerIndex + 1) % numberOfPlayers;
    }

}
