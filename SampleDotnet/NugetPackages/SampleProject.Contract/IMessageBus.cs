using MassTransit;

namespace SampleProject.Contract
{
    public interface IMessageBus
    {
        Task<Response<TResponse>> Call<TResponse, TRequest>(TRequest message)
            where TResponse : class, IResponseMessage
            where TRequest : class, IRequestMessage;
    }
}