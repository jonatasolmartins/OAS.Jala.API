using Microsoft.AspNetCore.Mvc.Filters;

namespace OAS.Jala.API;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class ProducesHeaderAttribute: ResultFilterAttribute
{
    public readonly string _name;
    public readonly string _value;

    public ProducesHeaderAttribute(Type modelType, string name = "X-Response-Type")
    {
        _name = name;
        _value = modelType.FullName;;
    }

    public ProducesHeaderAttribute(string name, string value)
    {
        _name = name;
        _value = value;
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (!context.HttpContext.Response.Headers.ContainsKey(_name)) 
            context.HttpContext.Response.Headers.Add(_name, _value);
        base.OnResultExecuting(context);
    }
}