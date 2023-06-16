// File: C:\Users\Koby\source\repos\VALCOBulkSMSAlertSystem\VALCOBulkSMSAlertSystem\Services\HubtelSmsService.cs
using System.Collections.Generic;

namespace VALCOBulkSMSAlertSystem.Services
{
    public class HubtelSmsService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _fromName;

        public HubtelSmsService(string clientId, string clientSecret, string fromName)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _fromName = fromName;
        }

        // Implement Service to send SMS to be used by MessagesController
        public async Task<string> HubtelSmsServiceApi(string toNumber, string message)
        {
            // TODO: Implement Hubtel SMS API
            // Hubtel API URL: https://smsc.hubtel.com/v1/messages/send?clientsecret=fhanifdw&clientid=hiljcvrg&from=Test_sms&to=0548257283&content=This+Is+A+Test+Message
            
            using (var client = new HttpClient())
            {
                var url = "https://smsc.hubtel.com/v1/messages/send";
                var queryParams = $"?clientsecret={_clientSecret}&clientid={_clientId}&from={_fromName}&to={toNumber}&content={message}";

                var request = await client.GetAsync(url + queryParams);
                var response = await request.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(response))
                {
                    Console.WriteLine(response);
                }

                // response = {"rate": 0, "messageId": "string", "status": "string", "networkId": "string"};

                //if responseCode == 201 and data.status == 0 return message status sent
                //else return message status failed
                if (response.Contains("status"))
                {
                    var status = response.Substring(response.IndexOf("status") + 9, 1);

                    if (status == "0")
                    {
                        Console.WriteLine("Message sent successfully");
                        return "Sent";
                    }
                    Console.WriteLine("Message sending failed");
                    return "Failed";
                } 
                return "Failed";
            }
        }        
    }
}
