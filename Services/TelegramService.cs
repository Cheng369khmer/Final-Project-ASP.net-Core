using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyPortfolio.Services
{
    public class TelegramService
    {
        private readonly HttpClient _httpClient;
        private readonly string _botToken = "8828776963:AAE-ip6MX0WcFwdPIgA5deRsX2R_t7_yzMI"; 
        private readonly string _chatId = "7929196191";

        public TelegramService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // កំណត់ Timeout បន្តិច ដើម្បីកុំឱ្យវាចាំយូរពេកបើអ៊ីនធឺណិតខ្សោយ
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task SendMessageAsync(string message)
        {
            try
            {
                string url = $"https://api.telegram.org/bot{_botToken}/sendMessage?chat_id={_chatId}&text={Uri.EscapeDataString(message)}&parse_mode=HTML";
                
                // បាញ់ Request និងរង់ចាំលទ្ធផល
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // បើជោគជ័យ វានឹងលោតអក្សរពណ៌សក្នុង Terminal
                    Console.WriteLine("✅ Telegram: Message sent successfully!");
                }
                else
                {
                    var errorDetail = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"❌ Telegram API Error: {errorDetail}");
                }
            }
            catch (Exception ex)
            {
                // បើដាច់អ៊ីនធឺណិត ឬ Firewall Block វានឹងលោតចូលទីនេះ
                Console.WriteLine($"🌐 Network Error: {ex.Message}");
            }
        }
    }
}