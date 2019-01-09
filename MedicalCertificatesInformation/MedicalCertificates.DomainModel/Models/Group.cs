using System.Collections.Generic;

namespace MedicalCertificates.DomainModel.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GoogleDriveFolderId { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public virtual List<Student> Students { get; set; }
    }
}