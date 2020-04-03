using Newtonsoft.Json;

namespace MFR.Core.DTO.Response
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
