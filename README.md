# Dapr & Aspire Workshop

A hands-on workshop for building microservices with [Dapr](https://dapr.io/) and [.NET Aspire](https://aspire.dev/). You will build a pizza ordering system composed of five microservices, progressively introducing Dapr building blocks and Aspire orchestration.

> For a detailed technical overview of the codebase, services, and architecture per challenge, see [OVERVIEW.md](./OVERVIEW.md).

![Dapr from dev to hosting](imgs/dapr-slidedeck-overview.png)

---

## Workshop Structure

The workshop is split into two parts:

1. **Part 1 — Dapr Challenges (1-4):** Based on [Diagrid's Dapr Workshop](https://github.com/diagrid-labs/dapr-workshop). Introduces Dapr building blocks one at a time.
2. **Part 2 — Aspire Challenges (5-9):** Adds .NET Aspire orchestration to the Dapr services, culminating in a fully event-driven workflow architecture.

### The Pizza Services

You will build five microservices to simulate the process of ordering a pizza:

- **`pizza-storefront`** — Entry point for customers to order a new pizza.
- **`pizza-kitchen`** — Responsible for cooking the pizza.
- **`pizza-delivery`** — Manages the delivery process, from pickup to the customer's doorstep.
- **`pizza-order`** — Manages the order status in the state store.
- **`pizza-workflow`** — Orchestrates the order steps, from ordering to delivery.

### Goals

On completion of this workshop, you will understand how to use these Dapr Building Block APIs:

- [State Management](https://docs.dapr.io/developing-applications/building-blocks/state-management/)
- [Service Invocation](https://docs.dapr.io/developing-applications/building-blocks/service-invocation/)
- [Publish/Subscribe](https://docs.dapr.io/developing-applications/building-blocks/pubsub/)
- [Workflow](https://docs.dapr.io/developing-applications/building-blocks/workflow/)

### Prerequisites

No existing knowledge of Dapr or microservices is needed, but basic C# programming skills are required.

- [Prerequisites](./docs/prerequisites.md)
- [The Pizza Store](./docs/The%20Pizza%20Store.md)

---

## Part 1: Dapr Challenges

![Dapr sidecar](imgs/dapr_sidecar_pixelart.png)

Microservices architectures are popular for a variety of reasons — they enable polyglot development, are easily scaled, and perform simple, focused tasks. However, as the number of microservices grows, so does the complexity. Managing security, observability, and resiliency becomes increasingly challenging.

Dapr addresses these challenges by providing a set of APIs for building distributed systems with best practices baked in. Leveraging Dapr allows you to reduce development time while building reliable, observable, and secure distributed applications.

### Challenge 1: State Management

Create the `pizza-order` service to manage order state in a Redis database using the [Dapr State Management Building Block](https://docs.dapr.io/developing-applications/building-blocks/state-management/).

<img src="imgs/challenge-1.png" width=50%>

- [Challenge 1 instructions](./docs/dapr/challenge-1/dotnet.md)

### Challenge 2: Service Invocation

Add synchronous communication between services using the [Dapr Service Invocation Building Block](https://docs.dapr.io/developing-applications/building-blocks/service-invocation/). Create the `pizza-storefront`, `pizza-kitchen`, and `pizza-delivery` services.

<img src="imgs/challenge-2.png" width=50%>

- [Challenge 2 instructions](./docs/dapr/challenge-2/dotnet.md)

### Challenge 3: Pub/Sub

Add a pub/sub component using the [Dapr Publish & Subscribe Building Block](https://docs.dapr.io/developing-applications/building-blocks/pubsub/) to publish events representing each stage of the pizza order, cooking, and delivery process. The `pizza-order` service subscribes to these events and updates the order status.

<img src="imgs/challenge-3.png" width=60%>

This challenge has two variants:

- **Declarative** — Subscription defined in an external YAML file, no code changes needed. Allows existing applications to subscribe to topics without modifying code.
- **Programmatic** — Subscription defined in code using the `[Topic]` attribute.

Instructions:
- [Challenge 3a — Declarative](./docs/dapr/challenge-3a/dotnet.md)
- [Challenge 3b — Programmatic](./docs/dapr/challenge-3b/dotnet.md)

### Challenge 4: Workflows

Orchestrate the full order lifecycle using [Dapr's Workflow Building Block](https://docs.dapr.io/developing-applications/building-blocks/workflow/). This guarantees that every step happens in order and adds a validation stage before delivery.

<img src="imgs/workflow.png" width=75%>

- [Challenge 4 instructions](./docs/dapr/challenge-4/dotnet.md)

---

## Part 2: Aspire Challenges

[Aspire](https://aspire.dev/) is a framework for building **cloud-native applications** in .NET with a strong focus on **observability, orchestration, and developer productivity**. It complements Dapr by providing tooling and patterns to manage distributed applications more effectively.

By integrating Aspire into this workshop, you gain:

- **Unified orchestration** — Define and run multiple microservices together, handling dependencies and startup order automatically.
- **Enhanced observability** — Built-in dashboards for logs, metrics, and traces to monitor the health of your services at a glance.
- **Simplified configuration** — Centrally managed connection strings, secrets, and environment variables.
- **Developer productivity** — Streamlined local development experience with minimal setup.

The **Aspire AppHost** orchestrates all five pizza microservices together with their Dapr sidecars. Aspire's **dashboard** lets you visualize service dependencies and monitor the state of your pizza orders in real time.

### Challenge 5: Aspire Basics

- [Challenge 5 instructions](./docs/aspire/challenge-1/aspire.md)

### Challenge 6: Aspire with Service Invocation

- [Challenge 6 instructions](./docs/aspire/challenge-2/aspire.md)

### Challenge 7: Aspire with Pub/Sub

- [Challenge 7 instructions](./docs/aspire/challenge-3/aspire.md)

### Challenge 8: Aspire with Dapr Workflow

- [Challenge 8 instructions](./docs/aspire/challenge-4/aspire.md)

### Challenge 9: Event-Driven Workflow (Fully Decoupled)

- [Challenge 9 instructions](./docs/aspire/challenge-5/aspire.md)

---

## Tools & References

### Dapr Dashboard

The Dapr Dashboard is a lightweight, web-based UI providing real-time visibility into Dapr applications. It connects to the Dapr control plane to present information about running sidecars.

**Key features:**
- **Application Overview** — Lists running Dapr apps with App IDs, ports, and health status.
- **Component Inspection** — View configured components (state stores, pub/sub, bindings, secret stores) and their status.
- **Sidecar Logs** — Access logs from Dapr sidecar processes for troubleshooting.
- **Service Invocation Graph** — Visualize how services call each other through Dapr.
- **Actor Viewing** — Display registered actor types and hosted actors.

**Usage:** Launch via the Dapr CLI with `dapr dashboard` (opens on port 8080). In Aspire challenges, the dashboard is added automatically as a resource.

### Scalar API Reference

This workshop uses [Scalar](https://scalar.com/) as the interactive API documentation UI. Each service registers Scalar in `Program.cs`:

```csharp
using Scalar.AspNetCore;

builder.Services.AddOpenApi();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
```

When running in Development mode, Scalar is available at `http://localhost:<port>/scalar/v1`. For example, if `PizzaOrder` runs on port 8001:

- **Scalar UI:** http://localhost:8001/scalar/v1
- **OpenAPI spec:** http://localhost:8001/openapi/v1.json

Features: browse endpoints, view request/response schemas, send test requests, and switch between API client formats (cURL, Python, C#, etc.).

### Updating Aspire Packages

A PowerShell script is provided to update all Aspire-related NuGet packages and SDK versions across the solution.

**Requires PowerShell 7+** (`pwsh`). Install with:

```powershell
winget install Microsoft.PowerShell
```

**Usage:**

```powershell
# Preview changes (no files modified)
pwsh ./after/Update-AspirePackages.ps1 -DryRun

# Apply updates to latest stable versions
pwsh ./after/Update-AspirePackages.ps1

# Pin to a specific Aspire SDK version
pwsh ./after/Update-AspirePackages.ps1 -TargetVersion "9.2.0"
```
