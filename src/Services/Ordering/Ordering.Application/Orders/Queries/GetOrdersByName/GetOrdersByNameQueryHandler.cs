namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(x => x.OrderItems)
            .AsNoTracking()
            .Where(x => x.OrderName.Value.Contains(query.Name))
            .OrderBy(x => x.OrderName)
            .ToListAsync(cancellationToken);
        
        return new GetOrdersByNameResult(orders.ToOrderDtoList());
    }
}