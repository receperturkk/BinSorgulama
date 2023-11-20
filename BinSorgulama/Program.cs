using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace BinSorgulama
{
    public class Program
    {
        #region BANK INFO PROPERTIES
        public class bankInfo
        {
            public string Bank_Code { get; set; }
            public string Bank_Name { get; set; }
            public string Bank_Brand { get; set; }
            public string Card_Type { get; set; }
            public string Card_Family { get; set; }
            public string Card_Kind { get; set; }
        }
        #endregion
        static async Task Main(string[] args)
        {
            Console.WriteLine("Sorgulamak istediğiniz bin numarasını giriniz.");
            string bin = Console.ReadLine();
            await NewMethod(bin);
        }

        private static async Task NewMethod(string bin)
        {
            #region HTTPCLİENT
            //Console.Clear();
            //string baseUrl = "https://posservice.esnekpos.com/api/services/EYVBinService";
            //var client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
            //var content = new StringContent("{\r\n\t\"CardNumber\":\""+ bin + "\"\r\n}", null, "application/json");
            //request.Content = content;
            //var response = client.SendAsync(request).Result;
            //response.EnsureSuccessStatusCode();

            //var responseStr = response.Content.ReadAsStringAsync().Result;
            //var binResult = JsonConvert.DeserializeObject<bankInfo>(responseStr);

            //bankInfo bankInfo = new bankInfo()
            //{
            //    Bank_Code = binResult.Bank_Code,
            //    Bank_Name = binResult.Bank_Name,
            //    Bank_Brand = binResult.Bank_Brand,
            //    Card_Type = binResult.Card_Type,
            //    Card_Family = binResult.Card_Family,
            //    Card_Kind = binResult.Card_Kind
            //};
            #endregion

            #region RESTCLİENT
            Console.Clear();
            string baseUrl = "https://posservice.esnekpos.com/api/services/EYVBinService";
            var options = new RestClientOptions()
            {
                MaxTimeout = -1
            };
            var client = new RestClient(options);
            var request = new RestRequest(baseUrl, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = @"{" + "\n" + @"	""CardNumber"":" + bin + "\n" + @"}";
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);

            var binResult = JsonConvert.DeserializeObject<bankInfo>(response.Content);
            bankInfo bankInfo = new bankInfo()
            {
                Bank_Code = binResult.Bank_Code,
                Bank_Name = binResult.Bank_Name,
                Bank_Brand = binResult.Bank_Brand,
                Card_Type = binResult.Card_Type,
                Card_Family = binResult.Card_Family,
                Card_Kind = binResult.Card_Kind
            };
            #endregion

            Console.WriteLine("Bank Code: " + bankInfo.Bank_Code);
            Console.WriteLine("Bank Name: " + bankInfo.Bank_Name);
            Console.WriteLine("Bank Brand: " + bankInfo.Bank_Brand);
            Console.WriteLine("Card Type: " + bankInfo.Card_Type);
            Console.WriteLine("Card Family: " + bankInfo.Card_Family);
            Console.WriteLine("Card Kind: " + bankInfo.Card_Kind);
        }
    }
}