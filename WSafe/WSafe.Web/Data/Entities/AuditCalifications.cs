using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum AuditCalifications
    {
        [Display(Name = "NO CUMPLE")]
        NoCumple = 1,
        [Display(Name = "CUMPLE PARCIALMENTE")]
        Cumple = 2,
        [Display(Name = "CUMPLE Y DOCUMENTA")]
        CumpleYDocumenta = 3,
    }
}