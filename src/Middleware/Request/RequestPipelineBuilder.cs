using Volte.Mediator.Middleware.Request.Contracts;

namespace Volte.Mediator.Middleware.Request;

/// <inheritdoc />
public class RequestPipelineBuilder : IRequestPipelineBuilder
{
    private const string ServicesKey = "mediator.Services";
    private readonly List<Func<RequestMiddlewareDelegate, RequestMiddlewareDelegate>> _components = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestPipelineBuilder"/> class.
    /// </summary>
    public RequestPipelineBuilder(IServiceProvider serviceProvider)
    {
        ApplicationServices = serviceProvider;
    }

    /// <inheritdoc />
    public IDictionary<string, object?> Properties { get; } = new Dictionary<string, object?>();

    /// <inheritdoc />
    public IServiceProvider ApplicationServices
    {
        get => GetProperty<IServiceProvider>(ServicesKey)!;
        set => SetProperty(ServicesKey, value);
    }

    /// <inheritdoc />
    public IRequestPipelineBuilder Use(Func<RequestMiddlewareDelegate, RequestMiddlewareDelegate> middleware)
    {
        _components.Add(middleware);
        return this;
    }

    /// <inheritdoc />
    public RequestMiddlewareDelegate Build()
    {
        RequestMiddlewareDelegate pipeline = _ => new ValueTask();

        for (int i = _components.Count - 1; i >= 0; i--)
        {
            pipeline = _components[i](pipeline);
        }

        return pipeline;
    }

    private T? GetProperty<T>(string key) => Properties.TryGetValue(key, out var value) ? (T?)value : default(T);
    private void SetProperty<T>(string key, T value) => Properties[key] = value;
}
