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
    
    public partial class BuildingViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<CapstoneData.Models.Entities.Building>
    {
    	
    			public virtual int Id { get; set; }
    			public virtual string Name { get; set; }
    			public virtual int District { get; set; }
    			public virtual string Address { get; set; }
    			public virtual bool Active { get; set; }
    			public virtual Nullable<double> Width { get; set; }
    			public virtual Nullable<double> Length { get; set; }
    	
    	public BuildingViewModel() : base() { }
    	public BuildingViewModel(CapstoneData.Models.Entities.Building entity) : base(entity) { }
    
    }
}
