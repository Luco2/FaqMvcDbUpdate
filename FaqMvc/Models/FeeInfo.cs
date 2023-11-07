using System.ComponentModel.DataAnnotations;

namespace GptWeb.Models
{
    public class FeeInfo
    {
        public int FeeInfoId { get; set; }
        public string ServiceName { get; set; } // Assuming this is added to match prompts with service
        public string? ProvisionInAct { get; set; }
        public decimal Fee { get; set; }
        public decimal? LatePenalty { get; set; } // Nullable for services without penalties
        public string AdditionalDetails { get; set; } // For any additional information
    }
}