using ProductAPI.Models;
using AtomicSeller.Models;
using AtomicSeller.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
//using Wavesoft.Models;
using OxatisAPI.Models;
using System.Text.RegularExpressions;
using AtomicCommonAPI.Models;

namespace AtomicSeller
{
    class Oxatis
    {
        public ResponseHeader PutTrackingNumber(Models.OxatisStore _Store)
        {
            ResponseHeader _header = new ResponseHeader();

            return _header;
        }
    }
}