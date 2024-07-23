﻿using Azure.Core.Pipeline;

namespace ChatApp.Data;

public class ReturnModel
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
    public int StatusCode { get; set; }
    public int TotalCount { get; set; }

}
