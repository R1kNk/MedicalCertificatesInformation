using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCertificates.Service.ErrorsFetch
{
    public enum BusinessLogicResultError
    {
        InvalidDate,
        OverlappingDate,
        NoCertificates,
        CertificateNotFound,
        StudentNotFound,
        GroupNotFound,
        CourseNotFound,
        DepartmentNotFound,
        ExpiredCertificate,
        AlreadyInThisGroup,
        DuplicateGroupName,
        DuplicateCourseNumber,
        DuplicateDepartmentName
    }
}
