using System.Linq;
using System.Text.Json;

namespace ReadingFilesData;

public class FileCode
{
    public Customer readCustomer(string dataPath, string fileName)
    {
        string customerData = File.ReadAllText(Path.Join(dataPath, fileName));
        var customer = JsonSerializer.Deserialize<Customer>(customerData);
        customer.processingCenter = "LODS";

        return customer;
    }

    public List<Product> readProducts(string dataPath, string fileName) =>
        File
            .ReadAllLines(Path.Join(dataPath, fileName))
            .Select(x => x.Substring(2))
            .Select(x => JsonSerializer.Deserialize<Product>(x))
            .ToList();

    public List<Order> generateOrdersReport(string dataPath, string searchPattern)
    {
        var results = new List<Order>();
        foreach (var filePath in Directory.GetFiles(dataPath, searchPattern))
        {
            var orders = File.ReadAllLines(filePath);
            results.AddRange(processMonthlyOrders(orders));
        }
        return results;
    }

    public List<Order> processMonthlyOrders(string[] orderData)
    {
        List<Order> orders = new List<Order>();
        foreach (var orderLine in orderData)
        {
            var orderArray = orderLine.Split(",");
            var order = new Order
            {
                orderNumber = int.Parse(orderArray[0]),
                orderDate = DateTime.Parse(orderArray[1]),
                status = orderArray[2],
                customerNumber = int.Parse(orderArray[3]),
                orderTotal = decimal.Parse(orderArray[4])
            };
            orders.Add(order);
        }

        return orders.Where(o => o.status == "Shipped").OrderByDescending(o => o.orderTotal).Take(5).ToList();
    }
}

public class Customer
{
    public int customerNumber { get; set; }
    public string customerName { get; set; }
    public string phone { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string processingCenter { get; set; }
}

public class Product
{
    public string productCode { get; set; }
    public string productName { get; set; }
    public decimal MSRP { get; set; }
}

public class Order
{
    //10100,2003-01-06,Shipped,363,10223.83
    public int orderNumber { get; set; }
    public DateTime orderDate { get; set; }
    public string status { get; set; }
    public int customerNumber { get; set; }
    public decimal orderTotal { get; set; }
}
