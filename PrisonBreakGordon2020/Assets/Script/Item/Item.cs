using System.Collections;
using System.Collections.Generic;

public abstract class Item
{
    // properties
    public string name;
    public float weight;

    // constructors

    /// <summary>
    /// this is for testing purposes
    /// </summary>
    public Item()
    {
        name = "item";
        weight = 0;
    }
    public Item(string name, float weight)
    {
        this.name = name;
        this.weight = weight;
    }

    // method(s)
   
}
