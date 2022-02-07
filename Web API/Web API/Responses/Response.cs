using System.Collections.Generic;
using Web_API.Models;

namespace Web_API.Responses
{
    public class Response<T>
    {


        public Response(List<StudentDTO> data)
        {
            Data = data;
            Message = string.Empty;
            Succeeded = true;
            
        }
        public List<StudentDTO> Data { get; set; }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
