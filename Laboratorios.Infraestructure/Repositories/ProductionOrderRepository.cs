using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.ViewModels;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Newtonsoft.Json;
using OrderTrack.Infraestructure.Data;


namespace Laboratorios.Infraestructure.Repositories
{
    public class ProductionOrderRepository : IProductionOrderRepository
    {
        private readonly LaboratoriosContext _context;
        static ServiceLayerCN serviceLayer = new ServiceLayerCN();
        public ProductionOrderRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProductionOrder>> GetProductionOrders(bool? status, bool? consumibles)
        {
            try
            {
                var ProductionOrders = new List<ProductionOrder>();
                if (status != null)
                {
                    if (status == true)
                    {
                        ProductionOrders = await _context.ProductionOrder.AsNoTracking().Where(x => x.status == status)
                            .ToListAsync();
                    }
                    else
                    {
                        ProductionOrders = await _context.ProductionOrder.AsNoTracking().Where(x => x.status == status || x.status == null)
                            .ToListAsync();
                    }
                }
                else
                {
                    ProductionOrders = await _context.ProductionOrder.AsNoTracking()
                        .ToListAsync();
                }
                if(consumibles == true)
                {
                    ProductionOrders = ProductionOrders.Where(x=>x.documentType == 3).ToList();
                }
                else
                {
                    ProductionOrders = ProductionOrders.Where(x => x.documentType != 3).ToList();
                }
                return ProductionOrders;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<ProductionOrder> GetProductionOrderById(int productionOrderId)
        {
            try
            {
                return await _context.ProductionOrder.Where(x => x.productionOrderId == productionOrderId)
                    .Include(x => x.ProductionOrderDetail)
                    .Include(x => x.recipe).ThenInclude(x => x.RecipeDetail).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        public async Task<IEnumerable<_ReportProductionOrderDetails>> GetReportProductionsOrders(DateTime date, DateTime endDate, int status)
        {
            try
            {
                var query = $"EXECUTE dbo.GetProductionOrdersDetail {date.Date.ToString("yyyyMMdd")}, {endDate.Date.ToString("yyyyMMdd")}, {status}";
                return await _context._ReportProductionOrderDetails.FromSqlRaw(query).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<ProductionOrder> InsertProductionOrder(ProductionOrder ProductionOrder)
        {
            try
            {
                ProductionOrder.dueDate = ProductionOrder._dueDate;
                if (ProductionOrder.productionOrderId > 0)
                {
                    _context.Entry(ProductionOrder).State = EntityState.Modified;
                    foreach (var item in ProductionOrder.ProductionOrderDetail)
                    {
                        if (item.productionOrderDetailId > 0)
                        {
                            if (item.productionOrderId == 0)
                            {
                                item.productionOrderId = ProductionOrder.productionOrderId;
                                _context.ProductionOrderDetail.Remove(item);
                            }
                            else
                            {
                                _context.Entry(item).State = EntityState.Modified;
                            }
                        }
                        else
                        {
                            _context.ProductionOrderDetail.Add(item);
                        }
                    }
                }
                else
                {
                    _context.ProductionOrder.Add(ProductionOrder);
                }
                await _context.SaveChangesAsync();

                return ProductionOrder;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public async Task<ProductionOrder> InsertProductionOrderReplicate(ProductionOrderViewModel data)
        {
            try
            {
                if (data.mbResults?.Count() > 0)
                {
                    if (data.reanalisys)
                    {
                        var send = await _context.ProductionOrder.Where(x => data.mbResults.Select(x => x._productionOrderId).Contains(x.productionOrderId) && x.status == true).FirstOrDefaultAsync();
                        if (send is not null)
                        {
                            throw new InvalidOperationException("La Orden de Producción " + send.refeLab + " no puede ser modificada.");
                        }
                        foreach (var item in data.mbResults)
                        {
                            var temp = data.productionOrder;
                            temp.resultId = item.microResultId;
                            //temp.productionOrderId = item.productionOrderId ?? 0;
                            temp.productionOrderId = item._productionOrderId ?? 0;
                            temp.isreanalisys = true;
                            //temp.status = item.status;
                            ProductionOrder OP = new ProductionOrder(temp);
                            var dato = await insertTemp(OP);
                        }
                    }
                    else
                    {
                        var send = await _context.ProductionOrder.Where(x => data.mbResults.Select(x => x.productionOrderId).Contains(x.productionOrderId) && x.status == true).FirstOrDefaultAsync(); ;
                        if (send is not null)
                        {
                            throw new InvalidOperationException("La Orden de Producción " + send.refeLab + " no puede ser modificada.");
                        }
                        foreach (var item in data.mbResults)
                        {
                            var temp = data.productionOrder;
                            temp.resultId = item.microResultId;
                            temp.productionOrderId = item.productionOrderId ?? 0;
                            //temp.status = item.status;
                            ProductionOrder OP = new ProductionOrder(temp);
                            var dato = await insertTemp(OP);
                        }
                    }
                }
                if (data.fqResults?.Count() > 0)
                {
                    if (data.reanalisys)
                    {
                        var send = await _context.ProductionOrder.Where(x => data.fqResults.Select(x => x._productionOrderId).Contains(x.productionOrderId) && x.status == true).FirstOrDefaultAsync(); ;
                        if (send is not null)
                        {
                            throw new InvalidOperationException("La Orden de Producción " + send.refeLab + " no puede ser modificada.");
                        }
                        foreach (var item in data.fqResults)
                        {
                            var temp = data.productionOrder;
                            temp.resultId = item.phisycalChemistryId;
                            //temp.productionOrderId = item.productionOrderId ?? 0;
                            temp.productionOrderId = item._productionOrderId ?? 0;
                            temp.isreanalisys = true;
                            //temp.status = item.status;
                            ProductionOrder OP = new ProductionOrder(temp);
                            var dato = await insertTemp(OP);
                        }
                    }
                    else
                    {
                        var send = await _context.ProductionOrder.Where(x => data.fqResults.Select(x => x.productionOrderId).Contains(x.productionOrderId) && x.status == true).FirstOrDefaultAsync(); ;
                        if (send is not null)
                        {
                            throw new InvalidOperationException("La Orden de Producción " + send.refeLab + " no puede ser modificada.");
                        }
                        foreach (var item in data.fqResults)
                        {
                            var temp = data.productionOrder;
                            temp.resultId = item.phisycalChemistryId;
                            temp.productionOrderId = item.productionOrderId ?? 0;
                            //temp.status = item.status;
                            ProductionOrder OP = new ProductionOrder(temp);
                            var dato = await insertTemp(OP);
                        }
                    }
                }
                return data.productionOrder;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<bool> insertTemp(ProductionOrder ProductionOrder/*, LaboratoriosContext _context, ICollection<ProductionOrder> productionOrders*/)
        {
            try
            {
                if (ProductionOrder.productionOrderId > 0)
                {
                    var exists = await _context.ProductionOrder.AsNoTracking().FirstOrDefaultAsync(x=>x.productionOrderId == ProductionOrder.productionOrderId);
                    var detalles = await _context.ProductionOrderDetail.AsNoTracking().Where(x => x.productionOrderId == ProductionOrder.productionOrderId).ToListAsync();
                    foreach (var _item in ProductionOrder.ProductionOrderDetail)
                    {
                        ProductionOrderDetail item = new ProductionOrderDetail()
                        {
                            baseQuantity = _item.baseQuantity,
                            include = _item.include,
                            itemName = _item.itemName,
                            itemNo = _item.itemNo,
                            nOfTubesUsed = _item.nOfTubesUsed,
                            plannedQuantity = _item.plannedQuantity,
                            productionOrderDetailId = _item.productionOrderDetailId,
                            productionOrderId = _item.productionOrderId,

                        };
                        if (detalles.Any(x => x.itemNo == item.itemNo))
                        {
                            item.productionOrderDetailId = detalles.Where(x => x.itemNo == item.itemNo).First().productionOrderDetailId;
                            item.productionOrderId = detalles.Where(x => x.itemNo == item.itemNo).First().productionOrderId;
                            _context.Entry(item).State = EntityState.Modified;
                            detalles.RemoveAt(detalles.FindIndex(x => x.itemNo == item.itemNo));
                        }
                        else
                        {
                            item.productionOrderId = ProductionOrder.productionOrderId;
                            _context.ProductionOrderDetail.Add(item);
                        }
                    }
                    _context.ProductionOrderDetail.RemoveRange(detalles);
                    ProductionOrder.refeLab = exists?.refeLab;
                    ProductionOrder.customerName = exists?.customerName;
                    ProductionOrder.dueDate = exists?.dueDate;
                    _context.Entry(ProductionOrder).State = EntityState.Modified;
                }
                else
                {
                    ProductionOrder.ProductionOrderDetail = ProductionOrder.ProductionOrderDetail.Select(_item => new ProductionOrderDetail
                    {
                        baseQuantity = _item.baseQuantity,
                        include = _item.include,
                        itemName = _item.itemName,
                        itemNo = _item.itemNo,
                        nOfTubesUsed = _item.nOfTubesUsed,
                        plannedQuantity = _item.plannedQuantity,
                        productionOrderDetailId = _item.productionOrderDetailId,
                        productionOrderId = _item.productionOrderId,
                    }).ToList();
                    _context.ProductionOrder.Add(ProductionOrder);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<object> sendProductionOrders(ProductionOrderOject productionOrders, int userId, int? count = 0)
        {
            try
            {
                //List<ProductionOrder> ProductionOrder = productionOrders.productionOrders;
                var ProductionOrder = await _context.ProductionOrder
                    .Where(x => productionOrders.productionOrders
                    .Select(x => x.productionOrderId).Contains(x.productionOrderId))
                    .Include(c=>c.ProductionOrderDetail)
                    .ToListAsync();
                var credentials = _context.User.Where(x => x.UserId == userId).FirstOrDefault();
                if (credentials != null)
                {
                    if (credentials.userSAP == null || credentials.passwordSAP == null)
                    {
                        throw new InvalidOperationException("No hay credenciales de SAP asignadas para este usuario");
                    }
                }
                serviceLayer.Login(credentials!.userSAP!, credentials.passwordSAP!);

                var BodyPost = new ProductionOrderSAP()
                {
                    ItemNo = productionOrders.packageCode == null ? ProductionOrder.First().itemNo : productionOrders.packageCode,
                    PlannedQuantity = productionOrders.packageCode == null ? ProductionOrder.Count : 1,
                    DueDate = DateTime.Now,
                    ProductionOrderLines = new List<ProductionOrderDetailSAP>()
                };

                foreach (var item in ProductionOrder)
                {
                    if (item.isreanalisys == true)
                    {
                        var _existspending = await _context.ProductionOrder
                            .FirstOrDefaultAsync(
                            x => x.resultId == item.resultId &&
                            x.areaId == item.areaId &&
                            x.status == true && x.isreanalisys == null);
                        if(_existspending is not null)
                        {
                            throw new InvalidOperationException("No se puede enviar la receta del reanalisis para " + item.refeLab + " porque la receta principal ya fue enviada.");

                        }
                    }
                    if(item.isreanalisys is null) {
                        var _existspending = await _context.ProductionOrder
                           .AnyAsync(
                           x => x.resultId == item.resultId &&
                           x.areaId == item.areaId &&
                           x.status == false && x.isreanalisys == true);

                        if (_existspending && ProductionOrder.Any(
                            x => x.resultId == item.resultId &&
                            x.areaId == item.areaId &&
                            x.isreanalisys == true
                        ) == false){
                            throw new InvalidOperationException("No se puede enviar la receta del analisis para " + item.refeLab + " porque la receta del reanalisis no ha sido enviada.");
                        }
                    }
                    foreach (var detalle in item.ProductionOrderDetail)
                    {
                        if (detalle.plannedQuantity == 0)
                        {
                            throw new InvalidOperationException("El Codigo " + detalle.itemName + " no puede ir con Cantidad Cero");
                        }
                        var exists = BodyPost.ProductionOrderLines.Where(x => x.ItemNo == detalle.itemNo);
                        if (exists.Any())
                        {
                            BodyPost.ProductionOrderLines.First(x => x.ItemNo == detalle.itemNo).PlannedQuantity = BodyPost.ProductionOrderLines.First(x => x.ItemNo == detalle.itemNo).PlannedQuantity + detalle.plannedQuantity;
                        }
                        else
                        {
                            var productLine = new ProductionOrderDetailSAP()
                            {
                                ItemNo = detalle.itemNo,
                                PlannedQuantity = detalle.plannedQuantity
                            };
                            if (productLine.ItemNo.Contains("SV-"))
                            {
                                productLine.ItemType = "pit_Resource";
                            }
                            BodyPost.ProductionOrderLines.Add(productLine);
                        }
                    }
                }
                var existswithZero = BodyPost.ProductionOrderLines.Where(x => x.PlannedQuantity == 0).FirstOrDefault();
                if (existswithZero != null)
                {
                    throw new InvalidOperationException("El Codigo " + existswithZero.ItemNo + " no puede ir con Cantidad Cero");
                }

                var path = @"ProductionOrders";
                var myContent = JsonConvert.SerializeObject(BodyPost);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                serviceLayer.client.DefaultRequestHeaders.Add("Cookie", "B1SESSION=" + serviceLayer.loginResponse.SessionId + ";HttpOnly");
                HttpResponseMessage response = await serviceLayer.client.PostAsync(path, byteContent);

                if (response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    //var responseJson = JsonConvert.DeserializeObject<ResponseSAP>(jsonString);

                    if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "401")
                    {
                        var responseJson = JsonConvert.DeserializeObject<ResponseSAP>(jsonString);
                        serviceLayer = new ServiceLayerCN();
                        if (count < 3)
                        {
                            return await sendProductionOrders(productionOrders, userId, count++);
                        }
                        throw new InvalidOperationException("Error de acceso");
                    }
                    if (response.StatusCode.ToString() == "BadRequest" || response.StatusCode.ToString() == "400")
                    {
                        var responseJson = JsonConvert.DeserializeObject<dynamic>(jsonString);
                        var errorMessage = responseJson.error.message.value;
                        throw new InvalidOperationException((string)errorMessage);
                    }
                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = JsonConvert.DeserializeObject<ResponseSAP>(jsonString);

                        foreach (var item in ProductionOrder)
                        {
                            item.documentNumber = responseJson.DocumentNumber;
                            item.status = true;

                            _context.Entry(item).State = EntityState.Modified;
                        }

                        await _context.SaveChangesAsync();
                        return responseJson!;
                    }
                }
                return BodyPost;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        public async Task<object> sendGoodIssues(ProductionOrderOject productionOrders, int userId, int? count = 0)
        {
            try
            {
                var ProductionOrder = await _context.ProductionOrder
                .Where(x => productionOrders.productionOrders
                .Select(x => x.productionOrderId).Contains(x.productionOrderId))
                .Include(c => c.ProductionOrderDetail)
                .ToListAsync();
                var credentials = _context.User.Where(x => x.UserId == userId).FirstOrDefault();
                if (credentials != null)
                {
                    if (credentials.userSAP == null || credentials.passwordSAP == null)
                    {
                        throw new InvalidOperationException("No hay credenciales de SAP asignadas para este usuario");
                    }
                }
                serviceLayer.Login(credentials!.userSAP!, credentials.passwordSAP!);

                var BodyPost = new Drafts()
                {
                    DocObjectCode = "60",
                    Comments = productionOrders.packageCode,
                    DocumentLines = new List<DocumentLines>(),
                };
                foreach (var item in ProductionOrder)
                {
                    foreach (var detalle in item.ProductionOrderDetail)
                    {
                        if (detalle.plannedQuantity == 0)
                        {
                            throw new InvalidOperationException("El Codigo " + detalle.itemName + " no puede ir con Cantidad Cero");
                        }
                        var exists = BodyPost.DocumentLines.Where(x => x.ItemCode == detalle.itemNo);
                        if (exists.Any())
                        {
                            BodyPost.DocumentLines.First(x => x.ItemCode == detalle.itemNo).Quantity = BodyPost.DocumentLines.First(x => x.ItemCode == detalle.itemNo).Quantity + detalle.plannedQuantity;
                        }
                        else
                        {
                            var productLine = new DocumentLines()
                            {
                                ItemCode = detalle.itemNo,
                                Quantity = detalle.plannedQuantity,
                                UseBaseUnits = "Y"
                            };
                            //if (productLine.ItemCode.Contains("SV-"))
                            //{
                                //productLine.ItemType = "pit_Resource";
                            //}
                            if(!productLine.ItemCode.Contains("SV-"))
                                BodyPost.DocumentLines.Add(productLine);
                        }
                    }
                }

                var path = @"Drafts";
                var myContent = JsonConvert.SerializeObject(BodyPost);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                serviceLayer.client.DefaultRequestHeaders.Add("Cookie", "B1SESSION=" + serviceLayer.loginResponse.SessionId + ";HttpOnly");
                HttpResponseMessage response = await serviceLayer.client.PostAsync(path, byteContent);

                if (response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    //var responseJson = JsonConvert.DeserializeObject<ResponseSAP>(jsonString);

                    if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "401")
                    {
                        var responseJson = JsonConvert.DeserializeObject<ResponseSAP>(jsonString);
                        serviceLayer = new ServiceLayerCN();
                        if (count < 3)
                        {
                            return await sendGoodIssues(productionOrders, userId, count++);
                        }
                        throw new InvalidOperationException("Error de acceso");
                    }
                    if (response.StatusCode.ToString() == "BadRequest" || response.StatusCode.ToString() == "400")
                    {
                        var responseJson = JsonConvert.DeserializeObject<dynamic>(jsonString);
                        var errorMessage = responseJson.error.message.value;
                        throw new InvalidOperationException((string)errorMessage);
                    }
                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = JsonConvert.DeserializeObject<ResponseSAPDraft>(jsonString);

                        foreach (var item in ProductionOrder)
                        {
                            item.docEntry = Convert.ToInt32(responseJson.DocEntry);
                            //item.documentNumber = responseJson.DocNum;
                            //item.sync = false;
                            item.status = true;
                            _context.Entry(item).State = EntityState.Modified;
                        }

                        await _context.SaveChangesAsync();
                        return responseJson!;
                    }
                }
                return BodyPost;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
