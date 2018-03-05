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
    
    public partial class RoomViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<CapstoneData.Models.Entities.Room>
    {
    	
    			public virtual int Id { get; set; }
    			public virtual string Name { get; set; }
    			public virtual int Floor { get; set; }
    			public virtual double Length { get; set; }
    			public virtual double Width { get; set; }
    			public virtual int MapId { get; set; }
    			public virtual Nullable<double> Longitude { get; set; }
    			public virtual Nullable<double> Latitude { get; set; }
    			public virtual Nullable<int> PosAX { get; set; }
    			public virtual Nullable<int> PosAY { get; set; }
    			public virtual Nullable<int> PosBX { get; set; }
    			public virtual Nullable<int> PosBY { get; set; }
    	
    	public RoomViewModel() : base() { }
    	public RoomViewModel(CapstoneData.Models.Entities.Room entity) : base(entity) { }
    
    }
}