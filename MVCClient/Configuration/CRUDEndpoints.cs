using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Configuration
{
    public class CRUDEndpoints
    {
        public string Base_url { get; set; }
        public string GetAll { get; set; }
        public string Update { get; set; }
        public string Delete { get; set; }
        public string Create { get; set; }
        public object GetById { get; set; }
    }
}
