namespace Domain.Models;

public class BaseResponse
{
    public string Id { get; set; } = string.Empty;
    public bool Error { get; set; } = false;
    public string ErrorMessage { get; set; } = string.Empty;
}