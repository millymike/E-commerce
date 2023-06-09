using ECommerce.Models;

namespace ECommerce.Api.Dtos;

public class SuccessResponseDto<TResultDto> : ResponseDto<TResultDto>
{
    public SuccessResponseDto(TResultDto? data, string message = "Success") : base(ResponseCode.Success, message, data!)
    {
    }
}