using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class MyCharacterActions : PlayerActionSet {
    public PlayerAction pressStart;
    public PlayerAction leftAbility, rightAbility;
    public PlayerAction leftCycle, rightCycle;
    public PlayerAction quit;

    public MyCharacterActions()
    {
        pressStart = CreatePlayerAction("Press Start");
        leftAbility = CreatePlayerAction("Use Left Ability");
        rightAbility = CreatePlayerAction("Use Right Ability");
        leftCycle = CreatePlayerAction("Cycle Left Slot");
        rightCycle = CreatePlayerAction("Cycle Right Slot");
        quit = CreatePlayerAction("Quit Game");
        pressStart.AddDefaultBinding(InputControlType.Command);
        pressStart.AddDefaultBinding(Key.Return);
        leftAbility.AddDefaultBinding(InputControlType.LeftTrigger);
        leftAbility.AddDefaultBinding(Mouse.LeftButton);
        rightAbility.AddDefaultBinding(InputControlType.RightTrigger);
        leftCycle.AddDefaultBinding(InputControlType.LeftBumper);
        leftCycle.AddDefaultBinding(Mouse.RightButton);
        rightCycle.AddDefaultBinding(InputControlType.RightBumper);
        quit.AddDefaultBinding(InputControlType.DPadDown);
        quit.AddDefaultBinding(Key.Q);
    }
}
