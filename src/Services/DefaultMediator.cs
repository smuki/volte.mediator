using Volte.Mediator.Contracts;
using Volte.Mediator.Middleware.Command;
using Volte.Mediator.Middleware.Command.Contracts;
using Volte.Mediator.Middleware.Notification;
using Volte.Mediator.Middleware.Notification.Contracts;
using Volte.Mediator.Middleware.Request;
using Volte.Mediator.Middleware.Request.Contracts;
using Volte.Mediator.Models;
using Volte.Mediator.Options;
using Microsoft.Extensions.Options;

namespace Volte.Mediator.Services;

/// <inheritdoc />
public class DefaultMediator : IMediator
{
    private readonly IRequestPipeline _requestPipeline;
    private readonly ICommandPipeline _commandPipeline;
    private readonly INotificationPipeline _notificationPipeline;
    private readonly IEventPublishingStrategy _defaultPublishingStrategy;
    private readonly ICommandStrategy _defaultCommandStrategy;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultMediator"/> class.
    /// </summary>
    /// <param name="requestPipeline">The request pipeline.</param>
    /// <param name="commandPipeline">The command pipeline.</param>
    /// <param name="notificationPipeline">The notification pipeline.</param>
    /// <param name="options">The mediator options.</param>
    public DefaultMediator(
        IRequestPipeline requestPipeline,
        ICommandPipeline commandPipeline,
        INotificationPipeline notificationPipeline,
        IOptions<MediatorOptions> options)
    {
        _requestPipeline = requestPipeline;
        _commandPipeline = commandPipeline;
        _notificationPipeline = notificationPipeline;
        _defaultPublishingStrategy = options.Value.DefaultPublishingStrategy;
        _defaultCommandStrategy = options.Value.DefaultCommandStrategy;
    }

    /// <inheritdoc />
    public async Task<T?> SendAsync<T>(IRequest<T> request, CancellationToken cancellationToken = default)
    {
        var responseType = typeof(T);
        var context = new RequestContext(request, responseType, cancellationToken);
        await _requestPipeline.ExecuteAsync(context);

        return (T?)context.Response;
    }

    /// <inheritdoc />
    public async Task SendAsync(ICommand command, CancellationToken cancellationToken = default) => await SendAsync(command, _defaultCommandStrategy, cancellationToken);

    /// <inheritdoc />
    public async Task SendAsync(ICommand command, ICommandStrategy? strategy = null, CancellationToken cancellationToken = default)
    {
        var resultType = typeof(Unit);
        strategy ??= _defaultCommandStrategy;
        var context = new CommandContext(command, strategy, resultType, cancellationToken);
        await _commandPipeline.InvokeAsync(context);
    }

    /// <inheritdoc />
    public async Task<T> SendAsync<T>(ICommand<T> command, CancellationToken cancellationToken = default) => await SendAsync(command, _defaultCommandStrategy, cancellationToken);

    /// <inheritdoc />
    public async Task<T> SendAsync<T>(ICommand<T> command, ICommandStrategy? strategy, CancellationToken cancellationToken = default)
    {
        var resultType = typeof(T);
        strategy ??= _defaultCommandStrategy;
        var context = new CommandContext(command, strategy, resultType, cancellationToken);
        await _commandPipeline.InvokeAsync(context);

        return (T)context.Result!;
    }

    /// <inheritdoc />
    public async Task SendAsync(INotification notification, CancellationToken cancellationToken = default) => await SendAsync(notification, _defaultPublishingStrategy, cancellationToken);

    /// <inheritdoc />
    public async Task SendAsync(INotification notification, IEventPublishingStrategy? strategy = null, CancellationToken cancellationToken = default)
    {
        strategy ??= _defaultPublishingStrategy;
        var context = new NotificationContext(notification, strategy, cancellationToken);
        await _notificationPipeline.ExecuteAsync(context);
    }
}
