namespace ResiliencePolicyDemo;

public class ApiResponse 
{
}

public class Error : ApiResponse
{
    public string Message { get; set; } = string.Empty;
}

public class Data : ApiResponse
{
    public int SurveyId { get; set; }
    public int ResponseCount { get; set; }
}