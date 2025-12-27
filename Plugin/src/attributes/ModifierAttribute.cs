using System;

[AttributeUsage(AttributeTargets.Class)]
public sealed class AutoModifierAttribute : Attribute
{
    public AutoModifierAttribute()
    {
    }
}