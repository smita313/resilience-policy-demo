using System.Net;
using System.Net.Http.Json;
using Polly;

namespace ResiliencePolicyDemo;

public class MockApiClient
{
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("http://localhost:5001")
    };

    public async Task<SurveyData> GetSurveyData(int id)
    {
        var response = await _httpClient.GetAsync($"SurveyData/{id}");
       
        Console.WriteLine($"\nStatus Code: {(int)response.StatusCode}. Timestamp: {DateTime.Now:HH:mm:ss.fff}");

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>();
            
            throw new Exception($"Error retrieving data: {error?.Message}");
        }
        
        var data = await response.Content.ReadFromJsonAsync<SurveyData>();
        
        return data;
    }
    
    
    # region ResiliencePolicy
    
    private readonly AsyncPolicy<HttpResponseMessage> _simpleRetryPolicy =
        Policy
            .HandleResult<HttpResponseMessage>(res => res.StatusCode == HttpStatusCode.InternalServerError)
            .WaitAndRetryAsync(5, retryAttemptNum =>
            {
                Console.WriteLine($"RetryAttemptNum {retryAttemptNum}");
                return TimeSpan.FromMilliseconds(100);
            });
    
    #endregion
}