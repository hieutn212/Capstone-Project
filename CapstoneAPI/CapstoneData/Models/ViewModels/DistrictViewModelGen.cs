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
    
    public partial class DistrictViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<CapstoneData.Models.Entities.District>
    {
    	
    			public virtual int Id { get; set; }
    			public virtual string Name { get; set; }
    	
    	public DistrictViewModel() : base() { }
    	public DistrictViewModel(CapstoneData.Models.Entities.District entity) : base(entity) { }
    
    }
}
