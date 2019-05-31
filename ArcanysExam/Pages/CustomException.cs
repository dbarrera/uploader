using System;
using System.Net;

namespace ArcanysExam.Pages
{
    public class FileExistException : BaseCustomException
    {
        public FileExistException(string message, string description) : base(message, description, (int)HttpStatusCode.InternalServerError)
        {
        }
    }

    public class FileLimitException : BaseCustomException
    {
        public FileLimitException(string message, string description) : base(message, description, (int)HttpStatusCode.InternalServerError)
        {
        }
    }

    public class BaseCustomException : Exception
    {
        private int _code;
        private string _description;

        public int Code
        {
            get => _code;
        }

        public string Description
        {
            get => _description;
        }

        public BaseCustomException(string message, string description, int code) : base(message)
        {
            _code = code;
            _description = description;
        }
    }
}
