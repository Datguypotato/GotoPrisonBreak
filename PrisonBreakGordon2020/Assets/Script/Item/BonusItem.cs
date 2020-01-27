using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : Item
{
    public int score;


    public BonusItem(string name, float weight, int score) : base(name, weight)
    {
        this.score = score;
    }
}
