using System;

namespace Aranda.Services.Responses
{
    public class ResponseData<T>
    {
        public ResponseData()
        {
            Message = string.Empty;
            Success = false;
        }

        public string Message { get; set; }

        public bool Success { get; set; }

        public T Data { get; set; }

        public void Error(string errorMessage)
        {
            Message = errorMessage;
            Success = false;
        }

        public void Ok(T value, string message = "")
        {
            if (value != null)
                Data = value;

            Message = message;
            Success = true;
        }

        public void Ok(string message = "")
        {
            Message = message;
            Success = true;
        }

        public void Error(Exception ex, string errorMessage = "")
        {
            Message = $"{ex.Message}";
            if (!string.IsNullOrWhiteSpace(errorMessage))
                Message += $"\n error: {errorMessage}";
            Success = false;
        }

        public bool HasError
        { get { return !string.IsNullOrWhiteSpace(Message); } }
    }
}