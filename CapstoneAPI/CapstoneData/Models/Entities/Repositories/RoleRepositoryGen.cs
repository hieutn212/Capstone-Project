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
    
    
    public partial interface IRoleRepository : SkyWeb.DatVM.Data.IBaseRepository<Role>
    {
    }
    
    public partial class RoleRepository : SkyWeb.DatVM.Data.BaseRepository<Role>, IRoleRepository
    {
    	public RoleRepository(System.Data.Entity.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
