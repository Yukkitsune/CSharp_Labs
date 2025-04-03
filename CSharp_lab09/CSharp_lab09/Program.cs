using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text.Json;


public class StockData
{
    public string s { get; set; }
    public List<double> c { get; set; }
    public List<double> h { get; set; }
    public List<double> l { get; set; }
    public List<double> o { get; set; }
    public List<int> t { get; set; }
    public List<int> v { get; set; }

};



class Market
{
    static readonly Mutex mutex = new Mutex();
    static async Task ReadFileAsync(List<string> massive, string filePath)
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            while ((line = await sr.ReadLineAsync()) != null)
            {
                massive.Add(line);
            }
        }
    }
    static void WriteToFile(string filePath, string text)
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }
        mutex.WaitOne();
        try
        {
            File.AppendAllText(filePath, text + Environment.NewLine);
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }
    static double calculateAvgPrice(List<double> highPrice, List<double>lowPrice)
    {
        double totalAvgPrice = 0;
        for (int i = 0; i < highPrice.Count; i++)
        {
            totalAvgPrice += (highPrice[i] + lowPrice[i]) / 2;
        }
        return totalAvgPrice / highPrice.Count;
    }

    static async Task GetData(HttpClient client,string quote, string startDate, string endDate,string output )
    {

        string apiKey = "";
        string URL = $"https://api.marketdata.app/v1/stocks/candles/D/{quote}/?from={startDate}&to={endDate}&token={apiKey}";
        HttpClient cl = new HttpClient();

        HttpResponseMessage response = cl.GetAsync(URL).Result;
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error! Status code: {response.StatusCode}");

        }
        else
        {
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<StockData>(json);
            if (data != null && data.h != null && data.l != null)
            {
                double avgPrice = calculateAvgPrice(data.h,data.l);
                string result = $"{quote}:{avgPrice}";
                WriteToFile(output, result);
                Console.WriteLine($"Average price for {quote}: {avgPrice}");
                

            }
            else Console.WriteLine($"Not enough data for {quote}");
        }
    }
    static async Task Main(string[] args)
    {
        string apiKey = "bWROMnE5ZmVGVFRjclI0c01qNzNFaUVzYWNGbmc4enIwcmJ6Z0ZXZkVHbz0";
        string tickerPath = "D:\\C#_Labs\\CSharp_lab09\\CSharp_lab09\\Files\\ticker.txt";
        string outputPath = "D:\\C#_Labs\\CSharp_lab09\\CSharp_lab09\\Files\\output.txt";

        List<string> ticker = [];
        await ReadFileAsync(ticker, tickerPath);


        DateTime endDateTime = DateTime.Now;
        string endDate = endDateTime.AddMonths(-1).ToString("yyyy-MM-dd");
        string startDate = endDateTime.AddYears(-1).AddMonths(1).ToString("yyyy-MM-dd");
        Console.WriteLine($"Start Date: {startDate}");
        Console.WriteLine($"End Date: {endDate}");
        

        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        List<Task> tasks = new List<Task>();
        
        foreach (var quote in ticker)
        {
            tasks.Add(GetData(client, quote,startDate,endDate,outputPath));
        }
        await Task.WhenAll(tasks);
    }
    
    



}



