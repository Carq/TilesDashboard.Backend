using System.ComponentModel.DataAnnotations;

namespace MetricsDashboard.WebApi.Dtos
{
    public class SaveValueDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int MetricId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Value { get; set; }
    }
}
