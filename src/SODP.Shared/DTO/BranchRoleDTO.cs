namespace SODP.Shared.DTO
{
    public class BranchRoleDTO : BaseDTO
    {
        //public int PartBranchId { get; set; }

        public string Role { get; set; }

        public virtual LicenseDTO License { get; set; }
    }
}
