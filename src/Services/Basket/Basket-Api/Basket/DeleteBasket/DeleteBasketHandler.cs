using BuildingBlocks.CQRS;

namespace Basket_Api.Basket.DeleteBasket
{

    public record DeleteBasketResult(bool IsDelete) { }

    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>
    {
    }
    public class DeleteBasketHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            return new DeleteBasketResult(true);
        }
    }
}
