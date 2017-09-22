using RequestForService.Business.Models;
using RequestForService.Data;
using RequestForService.Models.WorkOrders;
using System;
using System.Data.Entity;
using System.Linq;

namespace RequestForService.Business.Services.WorkOrders
{
    public class WorkOrderCommentsService : Base.DataManagerBase
    {
        public WorkOrderCommentsService(Guid? userId) : base(userId){}
        public WorkOrderCommentsService(DataContext db, Guid? userId) : base(db, userId){}

        public Result<WorkOrder> GetWorkOrderByIdIncludingProperties(Guid id, bool isDeleted = false)
        {
            try
            {
                var w = Db.Set<WorkOrder>()
                    .Where(i => i.IsDeleted == isDeleted && i.Id == id)
                    .Include(i => i.AssignedToUser)
                    .Include(i => i.CreatedByUser)
                    .Include(i => i.HourlyRate)
                    .Include(i => i.WorkOrderType)
                    .Include(i => i.Comments)
                    .FirstOrDefault();
                return w == null
                    ? Results.ErrorResult<WorkOrder>("Work order not found.")
                    : Results.NoneResult(w);
            }
            catch (Exception e)
            {
                LogException(e);
                return Results.ErrorResult<WorkOrder>();
            }
        }

    }
}