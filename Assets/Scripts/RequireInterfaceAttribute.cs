using UnityEngine;

public class RequireInterfaceAttribute : PropertyAttribute
{
    public System.Type RequiredType { get; private set; }

    public RequireInterfaceAttribute(System.Type type)
    {
        this.RequiredType = type;
    }
}
