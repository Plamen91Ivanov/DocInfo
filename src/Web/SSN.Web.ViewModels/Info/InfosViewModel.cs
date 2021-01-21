using SSN.Data.Models;
using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSN.Web.ViewModels.Info
{
   public class InfosViewModel : IMapFrom<Information>
    {
        public IEnumerable<InfoViewModel> Infos { get; set; }
    }
}
