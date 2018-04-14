using System;

namespace DiplomaProjectManagement.Model.Abstracts
{
    public interface IAuditable
    {
        DateTime? CreatedDate { get; set; }

        bool Status { get; set; }
    }
}