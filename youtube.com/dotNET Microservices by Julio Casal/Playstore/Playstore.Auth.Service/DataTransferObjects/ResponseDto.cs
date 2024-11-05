namespace Playstore.Auth.Service.DataTransferObjects;

public class ResponseDto<TData>
{
    public bool Success { get; set; }
    public TData? Data { get; set; }
    public string Error { get; set; }
}