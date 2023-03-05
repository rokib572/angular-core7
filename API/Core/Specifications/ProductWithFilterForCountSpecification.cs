using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>		
    {
        public ProductWithFilterForCountSpecification(ProductSpecParams productParams)
			: base(x =>
				(string.IsNullOrEmpty(productParams.search)) || x.Name.ToLower().Contains(productParams.search) &&
				(!productParams.brandId.HasValue || x.ProductBrandId == productParams.brandId) &&
				(!productParams.typeId.HasValue || x.ProductTypeId == productParams.typeId)
			)
		{
            
        }
    }
}
