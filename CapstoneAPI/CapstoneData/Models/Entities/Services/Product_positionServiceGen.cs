//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapstoneData.Models.Entities.Services
{
    using System;
    using System.Collections.Generic;
    
    
    public partial interface IProduct_positionService : SkyWeb.DatVM.Data.IBaseService<Product_position>
    {
    }
    
    public partial class Product_positionService : SkyWeb.DatVM.Data.BaseService<Product_position>, IProduct_positionService
    {
        public Product_positionService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.IProduct_positionRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}