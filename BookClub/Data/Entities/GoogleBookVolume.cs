using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Books.v1.Data;

namespace BookClub.Data.Entities
{
    public class GoogleBookVolume : Volume.VolumeInfoData
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Books")]
        public int BookId { get; set; }

        [NotMapped]
        public override IList<string> Authors { get; set; }
        [NotMapped]
        public override IList<string> Categories { get; set; }

        [NotMapped]
        public override DimensionsData Dimensions { get; set; }

        [NotMapped]
        public override ImageLinksData ImageLinks { get; set; }

        [NotMapped]
        public override IList<IndustryIdentifiersData> IndustryIdentifiers { get; set; }
        
        [NotMapped]
        public override PanelizationSummaryData PanelizationSummary { get; set; }

        [NotMapped]
        public override ReadingModesData ReadingModes { get; set; }

        [NotMapped]
        public override Volumeseriesinfo SeriesInfo { get; set; }
    }
}