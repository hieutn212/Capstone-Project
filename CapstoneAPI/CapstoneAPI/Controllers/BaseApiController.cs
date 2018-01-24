using AutoMapper;
using SkyWeb.DatVM.Mvc;
using System.Web.Http;
using CapstoneAPI.Models;

namespace CapstoneAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        public BaseApiController() { }

        public BreadcrumbViewModel Breadcrumb { get; protected set; }

        public global::AutoMapper.IMapper Mapper { get; }

        public IConfigurationProvider AutoMapperConfig
        {
            get
            {
                return this.Service<IConfigurationProvider>();
            }
        }

    }
}
