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
    
    
    public partial interface IHistoryService : SkyWeb.DatVM.Data.IBaseService<History>
    {
    }
    
    public partial class HistoryService : SkyWeb.DatVM.Data.BaseService<History>, IHistoryService
    {
        public HistoryService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.IHistoryRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}
