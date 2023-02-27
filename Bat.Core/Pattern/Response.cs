﻿namespace Bat.Core;

public class Response : IResponse
{
    public Response()
    { }

    public Response(string errorMessage)
    {
        IsSuccess = false;
        Message = errorMessage;
    }

    public Response(string errorMessage, int resultCode)
    {
        IsSuccess = false;
        Message = errorMessage;
        ResultCode = resultCode;
    }


    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public int ResultCode { get; set; } = 200;
    public DateTime ExecutionTime => DateTime.Now;
}