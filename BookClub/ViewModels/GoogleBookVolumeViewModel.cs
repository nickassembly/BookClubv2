using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Google.Apis.Books.v1.Data.Volume;

namespace BookClub.ViewModels
{
    public class GoogleBookVolumeInfoViewModel : VolumeInfoData
    {
        public int BookId{ get; set; }
    }
}
