namespace billing_service.Infrastructure
{
    public class StockClient
    {
        private readonly HttpClient _httpClient;

        public StockClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> UpdateStockAsync(string productCode, int quantity)
        {
            
            var response = await _httpClient.PatchAsync($"api/products/code/{productCode}/stock?quantity={-quantity}", null);

            if (!response.IsSuccessStatusCode)
            {

                var errorContent = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Erro ao baixar estoque do produto {productCode}: {response.StatusCode}");
            }

            return true;
        }
}
}
