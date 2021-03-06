// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: thermostat.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Thermostat {
  public static partial class ThermostatService
  {
    static readonly string __ServiceName = "thermostat.ThermostatService";

    static readonly grpc::Marshaller<global::Thermostat.SetTempRequest> __Marshaller_thermostat_SetTempRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Thermostat.SetTempRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Thermostat.SetTempResponse> __Marshaller_thermostat_SetTempResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Thermostat.SetTempResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Thermostat.ChangeTempRequest> __Marshaller_thermostat_ChangeTempRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Thermostat.ChangeTempRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Thermostat.ChangeTempResponse> __Marshaller_thermostat_ChangeTempResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Thermostat.ChangeTempResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::Thermostat.SetTempRequest, global::Thermostat.SetTempResponse> __Method_SetTemp = new grpc::Method<global::Thermostat.SetTempRequest, global::Thermostat.SetTempResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SetTemp",
        __Marshaller_thermostat_SetTempRequest,
        __Marshaller_thermostat_SetTempResponse);

    static readonly grpc::Method<global::Thermostat.ChangeTempRequest, global::Thermostat.ChangeTempResponse> __Method_ChangeTemp = new grpc::Method<global::Thermostat.ChangeTempRequest, global::Thermostat.ChangeTempResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ChangeTemp",
        __Marshaller_thermostat_ChangeTempRequest,
        __Marshaller_thermostat_ChangeTempResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Thermostat.ThermostatReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of ThermostatService</summary>
    [grpc::BindServiceMethod(typeof(ThermostatService), "BindService")]
    public abstract partial class ThermostatServiceBase
    {
      public virtual global::System.Threading.Tasks.Task<global::Thermostat.SetTempResponse> SetTemp(global::Thermostat.SetTempRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Thermostat.ChangeTempResponse> ChangeTemp(global::Thermostat.ChangeTempRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for ThermostatService</summary>
    public partial class ThermostatServiceClient : grpc::ClientBase<ThermostatServiceClient>
    {
      /// <summary>Creates a new client for ThermostatService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public ThermostatServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for ThermostatService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public ThermostatServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected ThermostatServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected ThermostatServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::Thermostat.SetTempResponse SetTemp(global::Thermostat.SetTempRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SetTemp(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Thermostat.SetTempResponse SetTemp(global::Thermostat.SetTempRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_SetTemp, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Thermostat.SetTempResponse> SetTempAsync(global::Thermostat.SetTempRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SetTempAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Thermostat.SetTempResponse> SetTempAsync(global::Thermostat.SetTempRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_SetTemp, null, options, request);
      }
      public virtual global::Thermostat.ChangeTempResponse ChangeTemp(global::Thermostat.ChangeTempRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ChangeTemp(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Thermostat.ChangeTempResponse ChangeTemp(global::Thermostat.ChangeTempRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_ChangeTemp, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Thermostat.ChangeTempResponse> ChangeTempAsync(global::Thermostat.ChangeTempRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ChangeTempAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Thermostat.ChangeTempResponse> ChangeTempAsync(global::Thermostat.ChangeTempRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_ChangeTemp, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override ThermostatServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new ThermostatServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(ThermostatServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_SetTemp, serviceImpl.SetTemp)
          .AddMethod(__Method_ChangeTemp, serviceImpl.ChangeTemp).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, ThermostatServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_SetTemp, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Thermostat.SetTempRequest, global::Thermostat.SetTempResponse>(serviceImpl.SetTemp));
      serviceBinder.AddMethod(__Method_ChangeTemp, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Thermostat.ChangeTempRequest, global::Thermostat.ChangeTempResponse>(serviceImpl.ChangeTemp));
    }

  }
}
#endregion
