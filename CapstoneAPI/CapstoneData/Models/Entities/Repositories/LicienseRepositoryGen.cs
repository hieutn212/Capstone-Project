//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapstoneData.Models.Entities.Repositories
{
    using System;
    using System.Collections.Generic;
    
    
    public partial interface ILicienseRepository : SkyWeb.DatVM.Data.IBaseRepository<Liciense>
    {
    }
    
    public partial class LicienseRepository : SkyWeb.DatVM.Data.BaseRepository<Liciense>, ILicienseRepository
    {
    	public LicienseRepository(System.Data.Entity.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
