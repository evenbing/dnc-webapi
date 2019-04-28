using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Infrastucture.elastcisearch
{
    public interface IEsClientProvider
    {
        ElasticClient GetClient();
    }
}
