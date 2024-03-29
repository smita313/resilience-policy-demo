namespace ResiliencePolicyDemo;

public static class Scenarios
{
    private static readonly ApplicationLogic AppLogic = new();
    
    public static async Task Successful()
    {
        await AppLogic.ProcessData(123);
    }
    
    

    public static async Task SingleFailure()
    {    
        // ID 1000 will always return status code 500 from Mock API
        await AppLogic.ProcessData(1000);
    }
    
    
    
    public static async Task IntermittentFailures()
    {    
        // ID 3000 will return status code 500 from Mock API 2/3 of the time

        for (var i = 0; i < 10; i++)
        {
            await AppLogic.ProcessData(3000);
        }
    }
    
    

    public static async Task CircuitBreaker()
    {
        // ID 1000 will always return status code 500 from Mock API
        
        // Triggers circuit breaker to open
        for (var i = 0; i < 10; i++)
        {
            await AppLogic.ProcessData(1000);
        }

        // Waiting for circuit breaker to enter half open state
        Console.WriteLine("\n(1) Pausing for 6s");
        await Task.Delay(6000);
        
        // Requests fails, triggering circuit breaker to open again
        for (var i = 0; i < 7; i++)
        {
            await AppLogic.ProcessData(1000);
        }

        // Successful requests fail automatically because circuit breaker is triggered
        for (var i = 0; i < 3; i++)
        {
            await AppLogic.ProcessData(123);
        }

        // Waiting again for circuit breaker to enter half open state
        Console.WriteLine("\n(2) Pausing for 6s");
        await Task.Delay(6000);

        // Circuit breaker closes, requests are successful
        for (var i = 0; i < 7; i++)
        {
            await AppLogic.ProcessData(123);
        }
        
        // Circuit breaker still closed, requests still processed
        for (var i = 0; i < 3; i++)
        {
            await AppLogic.ProcessData(1000);
        }
    }
    
    

    public static async Task Timeout()
    {
        // ID 2000 will always take 10s to process
        await AppLogic.ProcessData(2000);
    }
    
}