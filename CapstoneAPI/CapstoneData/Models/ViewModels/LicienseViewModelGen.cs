//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapstoneData.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class LicienseViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<CapstoneData.Models.Entities.Liciense>
    {
    	
    			public virtual int Id { get; set; }
    			public virtual int UserId { get; set; }
    			public virtual int DayOfPurchase { get; set; }
    			public virtual System.DateTime ExpireDate { get; set; }
    			public virtual int PackageId { get; set; }
    			public virtual bool IsUse { get; set; }
    			public virtual bool Active { get; set; }
    			public virtual Nullable<System.DateTime> CreatedDate { get; set; }
    	
    	public LicienseViewModel() : base() { }
    	public LicienseViewModel(CapstoneData.Models.Entities.Liciense entity) : base(entity) { }
    
    }
}
