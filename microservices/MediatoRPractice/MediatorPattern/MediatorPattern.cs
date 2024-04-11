

namespace MediatoRPractice.MediatorPattern
{
    public class MediatorPattern
    {
        public interface IMediatorCustom
        {
            Task<TResponse> Send<TResponse>(IRequestCustom<TResponse> request);


        }
        public interface IRequestCustom<TResponse> { }
        public interface IRequestHandlerCustom<TRequest, TResponse>
where TRequest : IRequestCustom<TResponse>
        {
            Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
        }

        public class SimpleMediator : IMediatorCustom
        {
            private readonly IServiceProvider _serviceProvider;

            // Constructor nhận vào một IServiceProvider để có thể lấy các handler.
            public SimpleMediator(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }
            public async Task<TResponse> Send<TResponse>(IRequestCustom<TResponse> request)
            {
                var handlerType = typeof(IRequestHandlerCustom<,>)
            .MakeGenericType(request.GetType(), typeof(TResponse));

                dynamic handler = _serviceProvider.GetService(handlerType);
                if (handler == null)
                    throw new InvalidOperationException("Handler not found.");
                return await handler.Handle((dynamic)request, new CancellationToken());
            }
        }
    }
}
