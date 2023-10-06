using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum AuditCalifications
    {
        [Display(Name = "CUMPLE PARCIALMENTE")]
        Cumple = 1,
        [Display(Name = "NO CUMPLE")]
        NoCumple = 2,
        [Display(Name = "CUMPLE Y DOCUMENTA")]
        CumpleYDocumenta = 3,
    }
}