using System;

namespace DiplomaProjectManagement.Model.Abstracts
{
    public abstract class Auditable : IAuditable
    {
        public DateTime? CreatedDate { get; set; }

        public bool Status { get; set; }
    }
}