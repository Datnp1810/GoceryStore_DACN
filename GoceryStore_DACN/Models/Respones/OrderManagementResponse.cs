public class OrderManagementResponse
{
  public int OrderId { get; set; }
  public string UserId { get; set; }
  public string CustomerName { get; set; }
  public string PhoneNumber { get; set; }
  public string DeliveryAddress { get; set; }
  public decimal TotalAmount { get; set; }
  public DateTime OrderDate { get; set; }
  public int StatusId { get; set; }
  public string StatusName { get; set; }
  public int PaymentMethodId { get; set; }
  public string PaymentMethodName { get; set; }
  public List<OrderItemResponse> Items { get; set; }
}

public class OrderItemResponse
{
  public int ProductId { get; set; }
  public string ProductName { get; set; }
  public int Quantity { get; set; }
  public decimal Price { get; set; }
  public decimal Total { get; set; }
}