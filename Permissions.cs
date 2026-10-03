namespace Summary.Rubika
{
    using Core.Security.Permissions;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    
    public class Permissions : IPermissionProvider
    {
        internal static Permission ManageRubikaSettings =
            new Permission(nameof(ManageRubikaSettings), "Manage Rubika Settings");

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { ManageRubikaSettings }.AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new []{ ManageRubikaSettings }
                }
            };
        }
    }
}