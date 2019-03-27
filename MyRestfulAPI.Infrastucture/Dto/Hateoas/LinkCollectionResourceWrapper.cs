using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Infrastucture.Dto.Hateoas
{
    public class LinkCollectionResourceWrapper<T> : LinkResourceBase
    {
        public LinkCollectionResourceWrapper(IEnumerable<T> value)
        {
            Value = value;
        }
        public IEnumerable<T> Value { get; set; }
    }
}
