using System;

namespace DiplomaProjectManagement.Model.Abstracts
{
    public interface IAuditable
    {
        DateTime? CreatedDate { get; set; }

        string CreatedBy { get; set; }

        DateTime? UpdatedDate { get; set; }
        string UpdatedBy { get; set; }

        bool Status { get; set; }
    }
}