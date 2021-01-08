using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Definitions
{
    public const int Average = 0;
    public const int Max = 1;
    public const int Min = 2;
    public static List<Mode> modes = new List<Mode>() { 
        new Mode() {name = "Average", index = Average },
        new Mode() {name = "Max", index = Max },
        new Mode() {name = "Min", index = Min}
    };

    public static int getModeIndex(string input)
    {
        input = input.ToLower();
        for (int i = 0; i < modes.Count; i++)
        {
            if (input.ToLower().Equals(modes[i].name.ToLower())){
                return modes[i].index;
            }
        }
        return -1;
    }
}

public class Mode
{
    public string name = "";
    public int index = 0;
}


