using Polly;
using Polly.Timeout;

namespace ResiliencePolicyDemo;

public class ApplicationLogic
{
    private readonly MockApiClient _mockApiClient = new();
    
    public async Task ProcessData(int id)
    {
        try
        {
            var data = await  _mockApiClient.GetSurveyData(id);
            
            // Do something with data
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Data for Survey ID {data.SurveyId} was processed successfully. Response Count: {data.ResponseCount}");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Exception thrown: {ex.Message}");
        }
        
        Console.ResetColor();
    }
    
    
    
    // ***************************************************************************
    
    
    private readonly AsyncPolicy _simpleRetryPolicy =
        Policy
            .Handle<Exception>()
            .RetryAsync(5);
    
    
    
    
    private readonly AsyncPolicy _exponentialBackoffRetryPolicy =
        Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(5,
                retryAttemptNum => TimeSpan.FromSeconds(Math.Pow(2, retryAttemptNum - 1)));

    
    
    
    private readonly AsyncPolicy _circuitBreakerPolicy =
        Policy
            .Handle<Exception>()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(5));

    
    
    
    private readonly AsyncPolicy _timeoutPolicy =
        Policy.TimeoutAsync(2, TimeoutStrategy.Pessimistic);
    
}