using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalCertificates.Common;
using MedicalCertificates.DomainModel.Models;
using MedicalCertificates.Repositories.Interfaces;
using MedicalCertificates.Service.CommonServices;
using MedicalCertificates.Service.ErrorsFetch;
using MedicalCertificates.Service.Interfaces.Auth;
using MedicalCertificates.Service.Interfaces.Common;
using MedicalCertificates.Service.Interfaces.Models;

namespace MedicalCertificates.Service.ModelsServices
{
    class GroupService : CRUDService<Group>, IGroupService, IGetAllStudents<Group>
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IUserManager<ApplicationUser> _userManager;

        public GroupService(IMedicalCertificatesUnitOfWork unitOfWork, IUserManager<ApplicationUser> userManager) : base(unitOfWork)
        {
            _courseRepository = _unitOfWork.GetRepository<Course>();
            _userManager = userManager;
        }

        public async Task<OperationResult<BusinessLogicResultError>> AddGroupAsync(Group newGroup, int courseId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.CourseNotFound });

            var existingGroup = await GetSingleOrDefaultAsync(p => p.Name == newGroup.Name);
            if(existingGroup != null)
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.DuplicateGroupName });

            newGroup.CourseId = course.Id;
            var result = await CreateAsync(newGroup);
            return OperationResult<BusinessLogicResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<BusinessLogicResultError>> EditGroupAsync(Group editGroup)
        {
            var existingGroup = await GetByIdAsync(editGroup.Id);
            if (existingGroup == null)
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.GroupNotFound });

            var sameNameGroup = await GetSingleOrDefaultAsync(p => p.Name == editGroup.Name);
            if (sameNameGroup != null)
                return OperationResult<BusinessLogicResultError>.CreateUnsuccessfulResult(new List<BusinessLogicResultError>() { BusinessLogicResultError.DuplicateGroupName });

            existingGroup.Name = editGroup.Name;
            //gd folder
            await EditGroupAsync(existingGroup);
            return OperationResult<BusinessLogicResultError>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<string>> SwitchGroupAsync(string userId, int groupId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return OperationResult<string>.CreateUnsuccessfulResult(new List<string>() {"Такой пользователь не найден. Обновите страницу."});

            var group = await GetByIdAsync(groupId);
            if(group == null)
                return OperationResult<string>.CreateUnsuccessfulResult(new List<string>() { "Одна или несколько групп не найдено. Обновите страницу." });

            if (!user.Groups.Contains(group))
            {
                user.Groups.Add(group);
            }
            else
            {
                user.Groups.Remove(group);
            }
            await _unitOfWork.SaveAsync();
            return OperationResult<string>.CreateSuccessfulResult();
        }

        public async Task<OperationResult<string>> EditUserGroupsAsync(string userId, IReadOnlyList<int> activeGroupsId, IReadOnlyList<int> inactiveGroupsId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return OperationResult<string>.CreateUnsuccessfulResult(new List<string>() { "UserNotFound" });

            for (int i = 0; i < activeGroupsId.Count; i++)
            {
                if (user.Groups.Where(p=>p.Id == activeGroupsId[i]).SingleOrDefault() == null)
                {
                    var group = await GetByIdAsync(activeGroupsId[i]);
                    if (group == null)
                        return OperationResult<string>.CreateUnsuccessfulResult(new List<string>() { "GroupNotFound" });
                    user.Groups.Add(group);
                }
            }
            for (int i = inactiveGroupsId.Count - 1; i >= 0; i--)
            {
                var group = user.Groups.Where(p => p.Id == inactiveGroupsId[i]).SingleOrDefault();
                if (group != null)
                {
                   
                    user.Groups.Remove(group);
                }
            }
            //foreach (var groupId in activeGroupsId)
            //{
            //    var group = await GetByIdAsync(groupId);
            //    if (group == null)
            //        return OperationResult<string>.CreateUnsuccessfulResult(new List<string>() { "GroupNotFound" });

            //    if (!user.Groups.Contains(group))
            //    {
            //        user.Groups.Add(group);
            //    }
            //    else
            //    {
            //        user.Groups.Remove(group);
            //    }
            //}
            await _unitOfWork.SaveAsync();
            return OperationResult<string>.CreateSuccessfulResult();
        }

        public IReadOnlyList<Student> GetAllStudents(Group group)
        {
            List<Student> studentList = new List<Student>();
            if (group == null)
                return studentList;
            
                if (group != null)
                    studentList.AddRange(group.Students.ToList());
            return studentList;
        }
    }
}
