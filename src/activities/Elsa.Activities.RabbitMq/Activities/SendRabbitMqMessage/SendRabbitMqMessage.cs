using Elsa.Activities.RabbitMq.Configuration;
using Elsa.Activities.RabbitMq.Services;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Design;
using Elsa.Expressions;
using Elsa.Services;
using Elsa.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elsa.Activities.RabbitMq
{
    [Trigger(
        Category = "RabbitMQ",
        DisplayName = "Send RabbitMQ message",
        Description = "Send Message to RabbitMQ",
        Outcomes = new[] { OutcomeNames.Done }
    )]
    public class SendRabbitMqMessage : Activity, IRabbitMqActivity
    {
        private readonly IMessageSenderClientFactory _messageSenderClientFactory;

        public SendRabbitMqMessage(IMessageSenderClientFactory messageSenderClientFactory)
        {
            _messageSenderClientFactory = messageSenderClientFactory;
        }

        [ActivityInput(
            Hint = "Topic",
            Order = 1,
            SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public string RoutingKey { get; set; } = default!;

        [ActivityInput(
            Hint = "List of headers that should be present in the message",
            Order = 2,
            UIHint = ActivityInputUIHints.Dictionary,
            DefaultSyntax = SyntaxNames.Json,
            SupportedSyntaxes = new[] { SyntaxNames.Json })]
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        [ActivityInput(
            Hint = "Message body",
            Order = 3,
            UIHint = ActivityInputUIHints.MultiLine,
            SupportedSyntaxes = new[] { SyntaxNames.Json })]
        public string Message { get; set; } = default!;

        [ActivityInput(
            Hint = "RabbitMQ connection string",
            SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid },
            Order = 1,
            Category = PropertyCategories.Configuration)]
        public string ConnectionString { get; set; } = default!;

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            var config = new RabbitMqBusConfiguration(ConnectionString, RoutingKey, Headers);

            var client = await _messageSenderClientFactory.GetSenderAsync(config);

            await client.PublishMessage(Message);

            return Done();
        }
    }
}
