namespace SODP.Shared.DTO
{
    public class ProjectBranchRoleDTO : BaseDTO
    {
        public int ProjectBranchId { get; set; }

        public string Role { get; set; }

        public virtual LicenseDTO License { get; set; }
    }
}
