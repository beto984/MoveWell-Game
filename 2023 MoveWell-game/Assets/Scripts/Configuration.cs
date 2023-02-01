using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuration : GameConfiguration
{
    [PropertyRange(1, 30)]
    public int numberOfSteps { get; set; }

    /*[PropertyRange(1, 30)]
    public int numberOfOptions { get; set; }*/

    public override string ToString()
    {
        return "Steps: " + numberOfSteps /*+ ", options: " + numberOfOptions*/;
    }

}
