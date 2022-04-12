using CsvHelper.Configuration;
using FilePoller.Models;

namespace FilePoller.Mappers
{
    public sealed class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Map(x => x.OrderNumber).Name("OrderNumber");
            Map(x => x.CustomerName).Name("CustomerName");
            Map(x => x.Fees).Name("Fees");
            Map(x => x.OrderStatus).Name("OrderStatus");
        }
    }
}
