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
    
    
    public partial interface IMarkerService : SkyWeb.DatVM.Data.IBaseService<Marker>
    {
    }
    
    public partial class MarkerService : SkyWeb.DatVM.Data.BaseService<Marker>, IMarkerService
    {
        public MarkerService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.IMarkerRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}
