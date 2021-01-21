using SSN.Data.Models;
using SSN.Services.Mapping;

namespace SSN.Web.ViewModels.UserAcc
{
    public class PhotosInUserAccViewModel : IMapFrom<Images>
    {
        public string FilePath { get; set; }
    }
}