// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Dapr;

namespace Aspire.Hosting;

/// <summary>
/// Extensions to <see cref="IResourceBuilder{T}"/> related to Dapr.
/// </summary>
public static class IDistributedApplicationResourceBuilderExtensions
{
    /// <summary>
    /// Ensures that a Dapr sidecar is started for the resource. The default app is the resource name.
    /// </summary>
    /// <typeparam name="T">The type of the resource.</typeparam>
    /// <param name="builder">The resource builder instance.</param>
    /// <returns>The resource builder instance.</returns>
    public static IResourceBuilder<T> WithDaprSidecar<T>(this IResourceBuilder<T> builder) where T : IResource
    {
        return builder.WithDaprSidecar(builder.Resource.Name);
    }

    /// <summary>
    /// Ensures that a Dapr sidecar is started for the resource.
    /// </summary>
    /// <typeparam name="T">The type of the resource.</typeparam>
    /// <param name="builder">The resource builder instance.</param>
    /// <param name="appId">The ID for the application, used for service discovery.</param>
    /// <returns>The resource builder instance.</returns>
    public static IResourceBuilder<T> WithDaprSidecar<T>(this IResourceBuilder<T> builder, string appId) where T : IResource
    {
        return builder.WithDaprSidecar(new DaprSidecarOptions { AppId = appId });
    }

    /// <summary>
    /// Ensures that a Dapr sidecar is started for the resource.
    /// </summary>
    /// <typeparam name="T">The type of the resource.</typeparam>
    /// <param name="builder">The resource builder instance.</param>
    /// <param name="options">Options for configuring the Dapr sidecar, if any.</param>
    /// <returns>The resource builder instance.</returns>
    public static IResourceBuilder<T> WithDaprSidecar<T>(this IResourceBuilder<T> builder, DaprSidecarOptions? options = null) where T : IResource
    {
        return builder.WithAnnotation(new DaprSidecarAnnotation { Options = options });
    }

    /// <summary>
    /// Associates a Dapr component with the Dapr sidecar started for the resource.
    /// </summary>
    /// <typeparam name="TDestination">The type of the resource.</typeparam>
    /// <param name="builder">The resource builder instance.</param>
    /// <param name="component">The Dapr component to use with the sidecar.</param>
    public static IResourceBuilder<TDestination> WithReference<TDestination>(this IResourceBuilder<TDestination> builder, IResourceBuilder<IDaprComponentResource> component) where TDestination : IResource
    {
        return builder.WithAnnotation(new DaprComponentReferenceAnnotation(component.Resource));
    }
}
