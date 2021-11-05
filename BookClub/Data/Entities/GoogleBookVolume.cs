using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Books.v1.Data;

namespace BookClub.Data.Entities
{
    public class GoogleBookVolume : Volume.VolumeInfoData
    {
        public int BookId { get; set; }
    }
}