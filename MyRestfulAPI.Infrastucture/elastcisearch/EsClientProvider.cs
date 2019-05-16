//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.Extensions.Configuration;
//using Nest;

//namespace MyRestfulAPI.Infrastucture.elastcisearch
//{
//    public class EsClientProvider : IEsClientProvider
//    {
//        private readonly IConfiguration _configuration;
//        private ElasticClient _client;

//        private void InitClient()
//        {
//            var node = new Uri(_configuration["EsUrl"]);
//            _client = new ElasticClient(new ConnectionSettings(node).
//                DefaultIndex("countryDemo"));
//        }

//        public ElasticClient GetClient()
//        {
//            if (_client != null)
//                return _client;
//            InitClient();
//            return _client;
//        }

//    }
//}
