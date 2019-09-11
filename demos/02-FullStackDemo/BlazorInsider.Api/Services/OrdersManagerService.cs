using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorInsider.Api.Services
{
    public class OrdersManagerService : OrdersManager.OrdersManagerBase
    {
        public override Task<AddOrderResponse> AddOrder(AddOrderRequest request, ServerCallContext context)
        {
            // create model
            var order = new BlazorInsider.Shared.Model.Order
            {
                OrderId = request.OrderID,
                Description = request.Description,
                Quantity = request.Quantity,
                Total = request.Total,
                Status = "Pending"
            };

            // now save order 
            var dbService = new BlazorInsider.App.Services.DatabaseService();
            dbService.AddOrder(order);

            var result = new AddOrderResponse() { NewOrderID = order.OrderId };

            return Task.FromResult(result);
        }

        public override Task<OrderReply> GetNewOrder(OrderRequest request, ServerCallContext context)
        {
            var service = new BlazorInsider.App.Services.DatabaseService();
            var order = service.GetPendingOrder();

            int orderId = order == null ? 0 : order.OrderId;

            var reply = new OrderReply
            {
                OrderId = orderId
            };

            return Task.FromResult(reply);
        }

        public override Task<GetOrdersResponse> GetOrders(GetOrdersRequest request, ServerCallContext context)
        {
            var service = new BlazorInsider.App.Services.DatabaseService();
            var orders = service.GetOrders();

            var result = new GetOrdersResponse();

            foreach (var item in orders)
            {
                var order = new GetOrderModel()
                {
                    OrderID = item.OrderId,
                    Description = item.Description,
                    Quantity = item.Quantity,
                    Status = item.Status,
                    Total = item.Total
                };
                result.GetOrderModels.Add(order);
            }

            return Task.FromResult(result);
        }

        public override Task<UpdateOrderResponse> UpdateOrder(UpdateOrderRequest request, ServerCallContext context)
        {
            // init UpdateOrder 
            var result = new UpdateOrderResponse()
            {
                Success = false,
                ErrorMessage = string.Empty
            };

            // now update order 
            try
            {
                var dbService = new BlazorInsider.App.Services.DatabaseService();
                dbService.UpdateOrder(request.OrderID);
                result.Success = true;
            }
            catch (Exception ex)
            {
                var msg = (ex.InnerException != null)
                    ? $"{ex.Message} {ex.InnerException.Message} {ex.StackTrace}"
                    : $"{ex.Message} {ex.StackTrace}";

                result.Success = false;
                result.ErrorMessage = msg;
            }

            return Task.FromResult(result);
        }


    }
}
