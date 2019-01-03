using System.Collections.Generic;

namespace MedicalSertificates.DomainModel.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GoogleDriveFolderId { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public List<Student> Students { get; set; }

        public List<ApplicationUser> Users { get; set; }
    }
}