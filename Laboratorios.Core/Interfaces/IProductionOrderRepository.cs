using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;

namespace Laboratorios.Core.Interfaces
{
    public interface IProductionOrderRepository
    {
        Task<IEnumerable<ProductionOrder>> GetProductionOrders(bool? status, bool? consumibles);
        Task<ProductionOrder> GetProductionOrderById(int productionOrderId);
        Task<IEnumerable<_ReportProductionOrderDetails>> GetReportProductionsOrders(DateTime date, DateTime endDate, int status);
        Task<ProductionOrder> InsertProductionOrder(ProductionOrder ProductionOrder);
        Task<ProductionOrder> InsertProductionOrderReplicate(ProductionOrderViewModel data);
        Task<object> sendProductionOrders(ProductionOrderOject ProductionOrder, int userId, int? count);
        Task<object> sendGoodIssues(ProductionOrderOject ProductionOrder, int userId, int? count);
    }
}
