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
    
    public partial class UserViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<CapstoneData.Models.Entities.User>
    {
    	
    			public virtual int Id { get; set; }
    			public virtual string Username { get; set; }
    			public virtual string Password { get; set; }
    			public virtual int RoleId { get; set; }
    			public virtual string Fullname { get; set; }
    			public virtual Nullable<System.DateTime> Birthday { get; set; }
    	
    	public UserViewModel() : base() { }
    	public UserViewModel(CapstoneData.Models.Entities.User entity) : base(entity) { }
    
    }
}
