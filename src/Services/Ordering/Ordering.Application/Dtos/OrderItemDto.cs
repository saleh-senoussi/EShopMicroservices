namespace Ordering.Application.Dtos;

public record OrderItemDto(Guid Id, Guid OrderId, Guid ProductId, int Quantity, decimal Price);