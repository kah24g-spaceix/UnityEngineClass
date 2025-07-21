using System;

public class InspectorButtonAttribute : Attribute
{
    public String Name { get; }

    public InspectorButtonAttribute(String pName)
    {
        Name = pName;
    }
}
