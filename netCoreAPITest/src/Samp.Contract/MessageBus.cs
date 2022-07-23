using MassTransit;

namespace Samp.Contract
{
    public sealed class MessageBus : IMessageBus
    {
        private readonly IBusControl busControl;
        private readonly IClientFactory clientFactory;

        public MessageBus(IBusControl busControl)
        {
            this.busControl = busControl;
            this.clientFactory = busControl.CreateClientFactory();
        }

        public async Task<Response<TResponse>> Call<TResponse, TRequest>(TRequest message)
            where TResponse : class, IResponseMessage
            where TRequest : class, IRequestMessage
        {
            var client = clientFactory.CreateRequestClient<TRequest>();
            var response = await client.GetResponse<TResponse>(message);

            return response;
        }
    }
}