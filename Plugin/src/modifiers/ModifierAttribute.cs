using System;

[AttributeUsage(AttributeTargets.Class)]
public sealed class AutoModifierAttribute : Attribute
{
    public string InstanceMember { get; }

    public AutoModifierAttribute(string instanceMember)
    {
        InstanceMember = instanceMember;
    }
}
