﻿using System.Collections.Generic;
using Elsa.Models;

namespace Elsa.Server.Api.Endpoints.WorkflowDefinitions
{
    public partial class Save
    {
        public sealed record SaveRequest
        {
            public string? WorkflowDefinitionId { get; init; }
            public string? Name { get; init; }
            public string? DisplayName { get; init; }
            public string? Description { get; init; }
            public string? Tag { get; init; }
            public Variables? Variables { get; init; }
            public WorkflowContextOptions? ContextOptions { get; init; }
            public bool IsSingleton { get; init; }
            public WorkflowPersistenceBehavior PersistenceBehavior { get; init; }
            public bool DeleteCompletedInstances { get; init; }
            public bool Publish { get; init; }
            public ICollection<ActivityDefinition> Activities { get; init; } = new List<ActivityDefinition>();
            public ICollection<ConnectionDefinition> Connections { get; init; } = new List<ConnectionDefinition>();
        }
    }
}