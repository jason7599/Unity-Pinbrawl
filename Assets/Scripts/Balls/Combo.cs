using UnityEngine;

public class Combo
{

    private Combo() {} // singleton


    private static int _hitCombo = 0;


    public static void Reset() => GameManager.ui.SetCombo(_hitCombo = 0);

    public static void Increment()
    {
        ++_hitCombo;

        // TODO: combo bonus stuff

        GameManager.ui.SetCombo(_hitCombo);
    }

}
