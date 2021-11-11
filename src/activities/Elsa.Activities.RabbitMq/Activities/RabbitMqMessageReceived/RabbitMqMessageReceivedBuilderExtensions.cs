using Elsa.Builders;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Elsa.Activities.RabbitMq
{
    public static class RabbitMqMessageReceivedBuilderExtensions
    {
        public static IActivityBuilder MessageReceived(this IBuilder builder, Action<ISetupActivity<RabbitMqMessageReceived>> setup, [CallerLineNumber] int lineNumber = default, [CallerFilePath] string? sourceFile = default) =>
            builder.Then(setup, null, lineNumber, sourceFile);

        public static IActivityBuilder MessageQueueReceived(this IBuilder builder, string connectionString, string routingKey, Dictionary<string, string> headers, [CallerLineNumber] int lineNumber = default, [CallerFilePath] string? sourceFile = default) =>
            builder.MessageReceived(setup => setup.WithConnectionString(connectionString).WithRoutingKey(routingKey).WithHeaders(headers), lineNumber, sourceFile);
    }
}
