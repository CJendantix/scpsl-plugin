using System;

[AttributeUsage(AttributeTargets.Property)]
public sealed class ConfigurableAttribute : Attribute
{
    public string Name { get; private set; }
    public string Description { get; set; }
    public object Default { get; set; }

    public ConfigurableAttribute(string name = null)
    {
        Name = name;
    }
}
