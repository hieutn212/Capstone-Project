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
    
    
    public partial interface IRoomService : SkyWeb.DatVM.Data.IBaseService<Room>
    {
    }
    
    public partial class RoomService : SkyWeb.DatVM.Data.BaseService<Room>, IRoomService
    {
        public RoomService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.IRoomRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}
