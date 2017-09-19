using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;

namespace Enceladus
{
    class BrandsGroup
    {
        public string GroupName { get; set; }
        public IList<Brand> Brands { get; set; }
    }
}
